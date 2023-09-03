namespace NovaEngine.Renderer.Vulkan;

/// <summary>Encapsulates a <see cref="VkPhysicalDevice"/> and it's logical <see cref="VkDevice"/> representation.</summary>
internal unsafe sealed class VulkanDevice : IDisposable
{
    /*********
    ** Properties
    *********/
    /// <summary>The physical device Vulkan will use.</summary>
    public VkPhysicalDevice NativePhysicalDevice { get; }

    /// <summary>The Vulkan logical device.</summary>
    public VkDevice NativeDevice { get; }

    /// <summary>The queue for graphics operations.</summary>
    public VkQueue GraphicsQueue { get; }

    /// <summary>The queue for transfer operations.</summary>
    public VkQueue TransferQueue { get; }

    /// <summary>The queue for compute operations.</summary>
    public VkQueue ComputeQueue { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="physicalDevice">The physical device Vulkan will use.</param>
    /// <exception cref="VulkanException">Thrown if the device couldn't be created.</exception>
    public VulkanDevice(VkPhysicalDevice physicalDevice)
    {
        NativePhysicalDevice = physicalDevice;

        var indices = new QueueFamilyIndices(NativePhysicalDevice);
        var queueFamilyIndices = new[] { indices.GraphicsFamily, indices.TransferFamily, indices.ComputeFamily }.Distinct().ToArray();

        // create device queues
        var deviceQueueCreateInfos = new VkDeviceQueueCreateInfo[queueFamilyIndices.Length];
        for (var i = 0; i < queueFamilyIndices.Length; i++)
        {
            var queuePriorities = 0f;
            deviceQueueCreateInfos[i] = new VkDeviceQueueCreateInfo
            {
                SType = VkStructureType.DeviceQueueCreateInfo,
                QueueFamilyIndex = queueFamilyIndices[i],
                QueueCount = 1,
                QueuePriorities = &queuePriorities
            };
        }

        var enabledFeatures = new VkPhysicalDeviceFeatures
        {
            SamplerAnisotropy = true,
            SampleRateShading = true
        };

        var extensionNames = new List<string>() { VK.KhrSwapchainExtensionName };
        var enabledExtensionNames = Array.Empty<IntPtr>();

        try
        {
            // convert extension names to pointers
            enabledExtensionNames = new IntPtr[extensionNames.Count];
            var extensionPropertyCount = 0u;
            VK.EnumerateDeviceExtensionProperties(NativePhysicalDevice, null, ref extensionPropertyCount, null);
            var extensionProperties = new VkExtensionProperties[extensionPropertyCount];
            VK.EnumerateDeviceExtensionProperties(NativePhysicalDevice, null, ref extensionPropertyCount, extensionProperties);

            var availableExtensionNames = extensionProperties.Select(property => Marshal.PtrToStringAnsi((IntPtr)property.ExtensionName)).ToList();
            for (var i = 0; i < extensionNames.Count; i++)
            {
                var extensionName = extensionNames[i];

                if (!availableExtensionNames.Contains(extensionName))
                    throw new VulkanException($"Required extension '{extensionName}' is not available.").Log(LogSeverity.Fatal);

                enabledExtensionNames[i] = Marshal.StringToHGlobalAnsi(extensionNames[i]);
            }

            // create logical device
            fixed (VkDeviceQueueCreateInfo* deviceQueueCreateInfosPointer = deviceQueueCreateInfos)
            fixed (void* enabledExtensionNamesPointer = enabledExtensionNames)
            {
                var deviceCreateInfo = new VkDeviceCreateInfo
                {
                    SType = VkStructureType.DeviceCreateInfo,
                    QueueCreateInfoCount = (uint)deviceQueueCreateInfos.Length,
                    QueueCreateInfos = deviceQueueCreateInfosPointer,
                    EnabledFeatures = &enabledFeatures,
                    EnabledExtensionCount = (uint)enabledExtensionNames.Length,
                    EnabledExtensionNames = (byte**)enabledExtensionNamesPointer
                };

                if (!VK.CreateDevice(NativePhysicalDevice, ref deviceCreateInfo, null, out var nativeDevice, out var result))
                    throw new VulkanException($"Failed to create device. \"{result}\"").Log(LogSeverity.Fatal);
                NativeDevice = nativeDevice;
            }
        }
        finally
        {
            foreach (var enabledExtensionName in enabledExtensionNames)
                Marshal.FreeHGlobal(enabledExtensionName);
        }

        // retrieve the queues from the logical device
        var queues = new Dictionary<uint, VkQueue>();
        foreach (var queueFamilyIndex in queueFamilyIndices)
        {
            VK.GetDeviceQueue(NativeDevice, queueFamilyIndex, 0, out var queue);
            queues[queueFamilyIndex] = queue;
        };

        GraphicsQueue = queues[indices.GraphicsFamily];
        TransferQueue = queues[indices.TransferFamily];
        ComputeQueue = queues[indices.ComputeFamily];
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Gets the index of a memory type that has all the requested properties.</summary>
    /// <param name="typeFilter">The bit mask of memory types that are suitable.</param>
    /// <param name="properties">The bit maks of properties the memory should have.</param>
    /// <returns>The index of the memory type that has the properties.</returns>
    /// <exception cref="InvalidOperationException">Thrown if there is no valid memory type.</exception>
    public uint GetMemoryTypeIndex(uint typeFilter, VkMemoryPropertyFlags properties)
    {
        VK.GetPhysicalDeviceMemoryProperties(NativePhysicalDevice, out var memoryProperties);

        for (var i = 0; i < memoryProperties.MemoryTypeCount; i++)
        {
            var memoryType = *(&memoryProperties.MemoryTypes_0 + i);
            if ((typeFilter & (1 << i)) != 0 && (memoryType.PropertyFlags & properties) == properties)
                return (uint)i;
        }

        throw new InvalidOperationException("Failed to find a suitable memory type").Log(LogSeverity.Fatal);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        VK.DestroyDevice(NativeDevice, null);
    }
}
