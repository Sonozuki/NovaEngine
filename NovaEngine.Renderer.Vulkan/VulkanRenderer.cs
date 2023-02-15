namespace NovaEngine.Renderer.Vulkan;

/// <summary>Represents a graphics renderer using Vulkan.</summary>
public unsafe class VulkanRenderer : IRenderer
{
    /*********
    ** Constants
    *********/
#if DEBUG
    /// <summary>Whether validation layers should be enabled.</summary>
    private const bool EnableValidationLayers = true;
#else
    /// <summary>Whether validation layers should be enabled.</summary>
    private const bool EnableValidationLayers = false;
#endif


    /*********
    ** Fields
    *********/
    /// <summary>A handle to the application window.</summary>
    private IntPtr WindowHandle;

    /// <summary>The Vulkan instance.</summary>
    private VkInstance NativeInstance;

    /// <summary>The Vulkan debug report.</summary>
    private VkDebugReportCallbackEXT NativeDebugReportCallback;


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public bool CanUseOnPlatform => OperatingSystem.IsWindows();

    /// <inheritdoc/>
    public SampleCount MaxSampleCount
    {
        get
        {
            VK.GetPhysicalDeviceProperties(Device.NativePhysicalDevice, out var physicalDeviceProperties);
            var counts = physicalDeviceProperties.Limits.FramebufferColorSampleCounts & physicalDeviceProperties.Limits.FramebufferDepthSampleCounts;
            return VulkanUtilities.ConvertSampleCount(counts);
        }
    }

    /// <inheritdoc/>
    public string DeviceName
    {
        get
        {
            VK.GetPhysicalDeviceProperties(Device.NativePhysicalDevice, out var physicalDeviceProperties);
            return Encoding.ASCII.GetString(physicalDeviceProperties.DeviceName, 256);
        }
    }

    /// <summary>The window surface Vulkan will draw to.</summary>
    internal VkSurfaceKHR NativeSurface { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>The <see cref="VkPhysicalDevice"/> and it's logical <see cref="VkDevice"/> representation.</summary>
    internal VulkanDevice Device { get; private set; }

    /// <summary>The singleton instance of <see cref="VulkanRenderer"/>.</summary>
    public static VulkanRenderer Instance { get; private set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public RendererTextureBase CreateRendererTexture(TextureBase baseTexture) => new VulkanTexture(baseTexture);

    /// <inheritdoc/>
    public RendererGameObjectBase CreateRendererGameObject(GameObject baseGameObject) => new VulkanGameObject(baseGameObject);

    /// <inheritdoc/>
    public RendererCameraBase CreateRendererCamera(Camera baseCamera) => new VulkanCamera(baseCamera);

    /// <inheritdoc/>
    public void OnInitialise(IntPtr windowHandle)
    {
        WindowHandle = windowHandle;
        Instance = this;

        CreateInstance();
        CreateSurface();
        Device = new(PickPhysicalDevice());
    }

    /// <inheritdoc/>
    public void PrepareDispose()
    {
        // waiting for all work submitted is required before disposing because the scenes are cleaned up before the renderer is (where the other DeviceWaitIdle is)
        // this resulted in exceptions being thrown as game objects that were in use where trying to be destroyed
        VK.DeviceWaitIdle(Device.NativeDevice);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ShaderStages.Dispose();
        DescriptorSetLayouts.Dispose();
        DescriptorPools.Dispose();

        Device.Dispose();

        if (!NativeDebugReportCallback.IsNull)
            VK.DestroyDebugReportCallbackEXT(NativeInstance, NativeDebugReportCallback, null);

        VK.DestroySurfaceKHR(NativeInstance, NativeSurface, null);
        VK.DestroyInstance(NativeInstance, null);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates the Vulkan instance.</summary>
    /// <exception cref="ApplicationException">Thrown if the instance or debug report callback (if validation layers are enabled) couldn't be created.</exception>
    private void CreateInstance()
    {
        var applicationInfo = new VkApplicationInfo
        {
            SType = VkStructureType.ApplicationInfo,
            ApiVersion = new VkVersion(0, 1, 3, 0)
        };

        var instanceCreateInfo = new VkInstanceCreateInfo
        {
            SType = VkStructureType.InstanceCreateInfo,
            ApplicationInfo = &applicationInfo
        };

        var layerNames = new[] { "VK_LAYER_KHRONOS_validation" };
        var enabledLayerNames = Array.Empty<IntPtr>();

        var extensionNames = new List<string> { VK.KhrSurfaceExtensionName };
        if (OperatingSystem.IsWindows())
            extensionNames.Add(VK.KhrWin32SurfaceExtensionName);
        // TODO: add mac and linux support

        var enabledExtensionNames = Array.Empty<IntPtr>();

        try
        {
            applicationInfo.EngineName = (byte*)Marshal.StringToHGlobalAnsi("Nova");

            // convert layer names to pointers
            if (EnableValidationLayers)
            {
                var layerPropertyCount = 0u;
                VK.EnumerateInstanceLayerProperties(ref layerPropertyCount, null);
                var layerProperties = new VkLayerProperties[layerPropertyCount];
                VK.EnumerateInstanceLayerProperties(ref layerPropertyCount, layerProperties);

                var availableLayerNames = layerProperties.Select(property => Marshal.PtrToStringAnsi((IntPtr)property.LayerName));
                enabledLayerNames = layerNames
                    .Where(layerName => availableLayerNames.Contains(layerName))
                    .Select(Marshal.StringToHGlobalAnsi)
                    .ToArray();

                if (enabledLayerNames.Length == 0)
                    Logger.LogError("No validation layers were loaded.");
                else
                    extensionNames.Add(VK.ExtDebugReportExtensionName);
            }

            // convert extension names to pointers
            enabledExtensionNames = new IntPtr[extensionNames.Count];
            var extensionPropertyCount = 0u;
            VK.EnumerateInstanceExtensionProperties(null, ref extensionPropertyCount, null);
            var extensionProperties = new VkExtensionProperties[extensionPropertyCount];
            VK.EnumerateInstanceExtensionProperties(null, ref extensionPropertyCount, extensionProperties);

            var availableExtensionNames = extensionProperties.Select(property => Marshal.PtrToStringAnsi((IntPtr)property.ExtensionName));
            for (int i = 0; i < extensionNames.Count; i++)
            {
                var extensionName = extensionNames[i];

                if (!availableExtensionNames.Contains(extensionName))
                    throw new ApplicationException($"Required extension '{extensionName}' is not available.").Log(LogSeverity.Fatal);

                enabledExtensionNames[i] = Marshal.StringToHGlobalAnsi(extensionNames[i]);
            }

            // create instance
            fixed (void* enabledLayerNamesPointer = enabledLayerNames)
            fixed (void* enabledExtensionNamesPointer = enabledExtensionNames)
            {
                instanceCreateInfo.EnabledLayerCount = (uint)enabledLayerNames.Length;
                instanceCreateInfo.EnabledLayerNames = (byte**)enabledLayerNamesPointer;
                instanceCreateInfo.EnabledExtensionCount = (uint)enabledExtensionNames.Length;
                instanceCreateInfo.EnabledExtensionNames = (byte**)enabledExtensionNamesPointer;

                if (VK.CreateInstance(ref instanceCreateInfo, null, out NativeInstance) != VkResult.Success)
                    throw new ApplicationException("Failed to create Vulkan instance.").Log(LogSeverity.Fatal);

                VK.InitialiseInstanceMethods(NativeInstance);
            }

            // set up debug report
            if (enabledLayerNames.Length > 0)
            {
                var debugReportCallbackCreateInfo = new VkDebugReportCallbackCreateInfoEXT
                {
                    SType = VkStructureType.DebugReportCallbackCreateInfoEXT,
                    Flags = VkDebugReportFlagsEXT.ErrorEXT | VkDebugReportFlagsEXT.WarningEXT | VkDebugReportFlagsEXT.PerformanceWarningEXT,
                    Callback = &DebugReport
                };

                if (VK.CreateDebugReportCallbackEXT(NativeInstance, ref debugReportCallbackCreateInfo, null, out NativeDebugReportCallback) != VkResult.Success)
                    throw new ApplicationException("Failed to create debug report callback.").Log(LogSeverity.Fatal);
            }
        }
        finally
        {
            Marshal.FreeHGlobal((IntPtr)applicationInfo.EngineName);

            foreach (var enabledExtensionName in enabledExtensionNames)
                Marshal.FreeHGlobal(enabledExtensionName);
            foreach (var enabledLayerName in enabledLayerNames)
                Marshal.FreeHGlobal(enabledLayerName);
        }
    }

    /// <summary>Creates the surface Vulkan will draw to.</summary>
    private void CreateSurface()
    {
        if (OperatingSystem.IsWindows())
        {
            var win32SurfaceCreateInfo = new VkWin32SurfaceCreateInfoKHR
            {
                SType = VkStructureType.Win32SurfaceCreateInfoKHR,
                Hwnd = WindowHandle,
                Hinstance = Program.Handle
            };

            if (VK.CreateWin32SurfaceKHR(NativeInstance, ref win32SurfaceCreateInfo, null, out var nativeSurface) != VkResult.Success)
                throw new ApplicationException("Failed to create surface.").Log(LogSeverity.Fatal);
            NativeSurface = nativeSurface;
        }
        // TODO: add mac and linux support
    }

    /// <summary>Picks the physical device for Vulkan to use.</summary>
    /// <returns>The physical device that Vulkan should use.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no physical devices have Vulkan support, or if no Vulkan supporting devices were valid.</exception>
    private VkPhysicalDevice PickPhysicalDevice()
    {
        // get the physical devices and ensure at least one supports Vulkan
        var deviceCount = 0u;
        VK.EnumeratePhysicalDevices(NativeInstance, ref deviceCount, null);
        if (deviceCount == 0)
            throw new InvalidOperationException("No physical devices have support for Vulkan.").Log(LogSeverity.Fatal);
        var physicalDevices = new VkPhysicalDevice[deviceCount];
        VK.EnumeratePhysicalDevices(NativeInstance, ref deviceCount, physicalDevices);

        // calculate the suitability of each physical device
        var suitablePhysicalDevices = new Dictionary<VkPhysicalDevice, float>(); // Key: physical device, Value: suitability
        foreach (var physicalDevice in physicalDevices)
        {
            var suitability = CalculatePhysicalDeviceSuitability(physicalDevice);
            if (suitability > 0)
                suitablePhysicalDevices[physicalDevice] = suitability;
        }

        // ensure there is at least one suitable device
        if (suitablePhysicalDevices.Count == 0)
            throw new InvalidOperationException("No physical devices that have support for Vulkan are suitable.").Log(LogSeverity.Fatal);

        // pick the most suitable physical device
        return suitablePhysicalDevices
            .OrderByDescending(physicalDevice => physicalDevice.Value)
            .First().Key;
    }

    /// <summary>Invoked when a validation layer fails.</summary>
    /// <param name="flags">The severity of the message.</param>
    /// <param name="objectType">The type of message.</param>
    /// <param name="object">The object where the issue was detected.</param>
    /// <param name="location">A component (layer, driver, loader) defined value specifying the location of the trigger.</param>
    /// <param name="messageCode">The layer-defined value indicating what test triggered the callback.</param>
    /// <param name="layerPrefix">An abbreviation of the name of the component making the callback.</param>
    /// <param name="message">The callback message.</param>
    /// <param name="userData">Pointer to custom passed data.</param>
    /// <returns><see langword="false"/>, meaning the call won't be aborted.</returns>
    private static VkBool32 DebugReport(VkDebugReportFlagsEXT flags, VkDebugReportObjectTypeEXT objectType, ulong @object, nuint location, int messageCode, byte* layerPrefix, byte* message, void* userData)
    {
        Logger.LogError(Marshal.PtrToStringAnsi((IntPtr)message) ?? "");
        return false;
    }

    /// <summary>Calculates the suitability of a physical device.</summary>
    /// <param name="physicalDevice">The physical device to calculate the suitability of.</param>
    /// <returns>The suitability of the physical device.</returns>
    private float CalculatePhysicalDeviceSuitability(VkPhysicalDevice physicalDevice)
    {
        VK.GetPhysicalDeviceProperties(physicalDevice, out var deviceProperties);
        VK.GetPhysicalDeviceFeatures(physicalDevice, out var deviceFeatures);

        // calculate the suitability score for the specified physical device
        {
            var score = 0f;

            // check if the device is decrete (typically have a major performance advantage)
            if (deviceProperties.DeviceType == VkPhysicalDeviceType.DiscreteGPU)
                score += 2;

            // maximum image size can be an indication of a devices performance
            score += MathF.Log2(deviceProperties.Limits.MaxImageDimension2D);

            // ensure device contains the required queue families and has proper swapchain support
            if (!new QueueFamilyIndices(physicalDevice).IsComplete || !new SwapchainSupportDetails(physicalDevice, NativeSurface).IsComplete)
                return 0;

            // ensure device has required features
            if (!deviceFeatures.SamplerAnisotropy)
                return 0;

            return score;
        }
    }
}
