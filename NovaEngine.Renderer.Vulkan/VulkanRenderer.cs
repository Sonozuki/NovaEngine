using NovaEngine.Core;
using NovaEngine.Core.Components;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using NovaEngine.Rendering;
using NovaEngine.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>Represents a graphics renderer using Vulkan.</summary>
    public unsafe class VulkanRenderer : IRenderer, IDisposable
    {
        /*********
        ** Constants
        *********/
        /// <summary>The number of frames to calculate concurrently.</summary>
        private static readonly int ConcurrentFrames = 2;

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

        /// <summary>The delegate that will store the debug report for getting a pointer to it.</summary>
        private readonly DebugReportCallbackEXTDelegate DebugReportCallback = DebugReport;

        /// <summary>The Vulkan debug report.</summary>
        private VkDebugReportCallbackEXT NativeDebugReportCallback;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. (When these are actually used, they'll never be null.)

        /// <summary>The graphics pipeline that Vulkan will use.</summary>
        private VulkanPipeline Pipeline;

        /// <summary>The command pool for rendering command buffers.</summary>
        private VulkanCommandPool CommandPool;

        /// <summary>A collection of semaphores, one for each concurrent frame, that will get signalled when an image have been retrieved from the swapchain.</summary>
        private VkSemaphore[] ImageAvailableSemaphores;

        /// <summary>A collection of semaphores, one for each concurrent frame, that will get signalled when rendering to the respective image has finished and presentation can begin.</summary>
        private VkSemaphore[] RenderFinishedSemaphores;

        /// <summary>A collection of fences, one for each concurrent frame, that is used to keep track of when the frame is available.</summary>
        private VkFence[] InFlightFences;

        /// <summary>A collection of fences, one for each swapchain image, used to keep track of when the image is being used.</summary>
        private VkFence[] ImagesInFlight;

        // TODO: temp
        private readonly Vertex[] Vertices = new[] {
            new Vertex(new Vector3(50f, 50f, 0), Vector2.Zero),
            new Vertex(new Vector3(100f, 50f, 0), Vector2.UnitX),
            new Vertex(new Vector3(50f, 100f, 0), Vector2.UnitY),
            new Vertex(new Vector3(100f, 100f, 0), Vector2.One)
        };
        private readonly uint[] Indices = new uint[] {
            0, 1, 2,
            1, 3, 2
        };
        private GameObject CameraObject;
        private GameObject TempObject;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

        /// <summary>The current frame index.</summary>
        /// <remarks>This is used to determine which sync objects to use, it will always be between 0 and --<see cref="ConcurrentFrames"/>.</remarks>
        private int CurrentFrameIndex = 0;


        /*********
        ** Accessors
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

                if (counts.HasFlag(VkSampleCountFlags.Count64)) return SampleCount.Count64;
                else if (counts.HasFlag(VkSampleCountFlags.Count32)) return SampleCount.Count32;
                else if (counts.HasFlag(VkSampleCountFlags.Count16)) return SampleCount.Count16;
                else if (counts.HasFlag(VkSampleCountFlags.Count8)) return SampleCount.Count8;
                else if (counts.HasFlag(VkSampleCountFlags.Count4)) return SampleCount.Count4;
                else if (counts.HasFlag(VkSampleCountFlags.Count2)) return SampleCount.Count2;
                else return SampleCount.Count1;
            }
        }

        /// <summary>The window surface Vulkan will draw to.</summary>
        internal VkSurfaceKHR NativeSurface { get; private set; }

        /// <summary>The render pass.</summary>
        internal VkRenderPass NativeRenderPass { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. (When these are actually used, they'll never be null.)

        /// <summary>The <see cref="VkPhysicalDevice"/> and it's logical <see cref="VkDevice"/> representation.</summary>
        internal VulkanDevice Device { get; private set; }

        /// <summary>The swapchain Vulkan will present to.</summary>
        internal VulkanSwapchain Swapchain { get; private set; }

        /// <summary>The descriptor set layout for <see cref="NativeDescriptorPool"/>.</summary>
        internal VkDescriptorSetLayout NativeDescriptorSetLayout { get; private set; }

        /// <summary>The descriptor pool for game objects.</summary>
        internal VkDescriptorPool NativeDescriptorPool { get; private set; }

        /// <summary>The singleton instance of <see cref="VulkanRenderer"/>.</summary>
        public static VulkanRenderer Instance { get; private set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public RendererTextureBase CreateRendererTexture(TextureBase baseTexture, bool generateMipChain) => new VulkanTexture(baseTexture, generateMipChain);

        /// <inheritdoc/>
        public RendererGameObjectBase CreateRendererGameObject(GameObject baseGameObject) => new VulkanGameObject(baseGameObject);

        /// <inheritdoc/>
        public void OnInitialise(IntPtr windowHandle)
        {
            WindowHandle = windowHandle;
            Instance = this;

            CreateInstance();
            CreateSurface();
            Device = new(PickPhysicalDevice());

            Swapchain = new(false, new VkExtent2D(1280, 720)); // TODO: don't hardcode vsync or size
            CreateRenderPass();
            Swapchain.CreateFramebuffers(NativeRenderPass);

            CreateDescriptorSetLayout();
            Pipeline = new();

            CreateSyncObjects();

            CommandPool = new(CommandPoolUsage.Graphics);
            CreateDescriptorPool();

            // TODO: temp
            CameraObject = new("camera");
            CameraObject.AddComponent(new Camera(1920, 1080, .01f, 1000, true));
            CameraObject.Transform.GlobalPosition = Vector3.UnitZ * -1;

            TempObject = new("tempObject");
            TempObject.AddComponent(new MeshRenderer(new("mesh", Vertices, Indices)));

            (TempObject.RendererGameObject as VulkanGameObject)!.UpdateUBO(CameraObject.GetComponent<Camera>()!);
        }

        /// <inheritdoc/>
        public void OnWindowResize(Size newSize) => RecreateSwapchain(new VkExtent2D((uint)newSize.Width, (uint)newSize.Height));

        /// <inheritdoc/>
        public void OnRenderFrame()
        {
            VK.WaitForFences(Device.NativeDevice, 1, new[] { InFlightFences[CurrentFrameIndex] }, true, ulong.MaxValue);

            // acquire an image from the swapchain
            var imageIndex = Swapchain.AcquireNextImage(ImageAvailableSemaphores[CurrentFrameIndex], VkFence.Null);

            // check if a previous frame is using this images (meaning there is a fence to wait on)
            if (!ImagesInFlight[imageIndex].IsNull)
                VK.WaitForFences(Device.NativeDevice, 1, new[] { ImagesInFlight[imageIndex] }, true, ulong.MaxValue);

            // mark the image as being used by this frame
            ImagesInFlight[imageIndex] = InFlightFences[CurrentFrameIndex];

            // TODO: temp
            // create the command buffer for this frame
            var commandBuffer = CommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);
            var clearValues = new VkClearValue[] { new VkClearColorValue(.05f, .05f, .05f, 0), new VkClearDepthStencilValue(1, 0) };
            fixed (VkClearValue* clearValuePointers = clearValues)
            {
                var renderPassBeginInfo = new VkRenderPassBeginInfo()
                {
                    SType = VkStructureType.RenderPassBeginInfo,
                    RenderPass = NativeRenderPass,
                    Framebuffer = Swapchain.NativeFramebuffers![imageIndex],
                    RenderArea = new VkRect2D(VkOffset2D.Zero, Swapchain.Extent),
                    ClearValueCount = 2,
                    ClearValues = clearValuePointers
                };

                var vulkanGameObject = (TempObject.RendererGameObject as VulkanGameObject)!;
                VK.CommandBeginRenderPass(commandBuffer, ref renderPassBeginInfo, VkSubpassContents.Inline);
                VK.CommandBindPipeline(commandBuffer, VkPipelineBindPoint.Graphics, Pipeline.Pipeline);
                VK.CommandBindDescriptorSets(commandBuffer, VkPipelineBindPoint.Graphics, Pipeline.PipelineLayout, 0, 1, new[] { vulkanGameObject.NativeDescriptorSet }, 0, null);
                var vertexBuffer = vulkanGameObject.VertexBuffer!.NativeBuffer;
                var offsets = (VkDeviceSize)0;
                VK.CommandBindVertexBuffers(commandBuffer, 0, 1, ref vertexBuffer, &offsets);
                VK.CommandBindIndexBuffer(commandBuffer, vulkanGameObject.IndexBuffer!.NativeBuffer, 0, VkIndexType.Uint32);
                VK.CommandDrawIndexed(commandBuffer, (uint)Indices.Length, 1, 0, 0, 0);
                VK.CommandEndRenderPass(commandBuffer);
                VK.EndCommandBuffer(commandBuffer);

                // run graphics queue
                var waitSemaphore = ImageAvailableSemaphores[CurrentFrameIndex];
                var waitDestinationStageMask = VkPipelineStageFlags.ColorAttachmentOutput;
                var signalSemaphore = RenderFinishedSemaphores[CurrentFrameIndex];
                var submitInfo = new VkSubmitInfo()
                {
                    SType = VkStructureType.SubmitInfo,
                    WaitSemaphoreCount = 1,
                    WaitSemaphores = &waitSemaphore,
                    WaitDestinationStageMask = &waitDestinationStageMask,
                    CommandBufferCount = 1,
                    CommandBuffers = &commandBuffer,
                    SignalSemaphoreCount = 1,
                    SignalSemaphores = &signalSemaphore
                };

                VK.ResetFences(Device.NativeDevice, 1, new[] { InFlightFences[CurrentFrameIndex] });
                VK.QueueSubmit(Device.GraphicsQueue, 1, new[] { submitInfo }, InFlightFences[CurrentFrameIndex]);
            }

            // run presentation queue
            Swapchain.QueuePresent(RenderFinishedSemaphores[CurrentFrameIndex], imageIndex);

            // wait for the queue to finish to stop overloading the GPU with work
            VK.QueueWaitIdle(Device.GraphicsQueue);

            // advance the current frame index
            CurrentFrameIndex = (CurrentFrameIndex + 1) % ConcurrentFrames;

            // TODO: temp
            CommandPool.FreeCommandBuffers(new[] { commandBuffer });
        }

        /// <inheritdoc/>
        public void OnCleanUp() => Dispose();

        /// <inheritdoc/>
        public void Dispose()
        {
            CleanUpSwapchain();

            // TODO: temp
            TempObject.RendererGameObject.Dispose();
            CameraObject.RendererGameObject.Dispose();

            VK.DestroyDescriptorPool(Device.NativeDevice, NativeDescriptorPool, null);
            VK.DestroyDescriptorSetLayout(Device.NativeDevice, NativeDescriptorSetLayout, null);

            Texture2D.Undefined.Dispose();

            for (int i = 0; i < ConcurrentFrames; i++)
            {
                VK.DestroySemaphore(Device.NativeDevice, RenderFinishedSemaphores[i], null);
                VK.DestroySemaphore(Device.NativeDevice, ImageAvailableSemaphores[i], null);
                VK.DestroyFence(Device.NativeDevice, InFlightFences[i], null);
            }

            CommandPool.Dispose();

            Device.Dispose();

            if (!NativeDebugReportCallback.IsNull)
                VK.DestroyDebugReportCallbackEXT(NativeInstance, NativeDebugReportCallback, null);

            VK.DestroySurfaceKHR(NativeInstance, NativeSurface, null);
            VK.DestroyInstance(NativeInstance, null);
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Recreates the <see cref="Swapchain"/>.</summary>
        /// <param name="undefinedExtent">The extent to use if the surface extent is undefined.</param>
        private void RecreateSwapchain(VkExtent2D undefinedExtent)
        {
            CleanUpSwapchain();

            Swapchain = new VulkanSwapchain(false, undefinedExtent);
            CreateRenderPass();
            Swapchain.CreateFramebuffers(NativeRenderPass);

            Pipeline = new VulkanPipeline(); // TODO: use dynamic state instead of recreating the entire pipeline
        }

        /// <summary>Cleans up the swapchain resources.</summary>
        private void CleanUpSwapchain()
        {
            VK.DeviceWaitIdle(Device.NativeDevice);

            Pipeline.Dispose();
            VK.DestroyRenderPass(Device.NativeDevice, NativeRenderPass, null);
            Swapchain.Dispose();
        }

        /// <summary>Creates the Vulkan instance.</summary>
        /// <exception cref="ApplicationException">Thrown if the instance or debug report callback (if validation layers are enabled) couldn't be created.</exception>
        private void CreateInstance()
        {
            var applicationInfo = new VkApplicationInfo()
            {
                SType = VkStructureType.ApplicationInfo,
                ApiVersion = new VkVersion(0, 1, 2, 0)
            };

            var instanceCreateInfo = new VkInstanceCreateInfo()
            {
                SType = VkStructureType.InstanceCreateInfo,
                ApplicationInfo = &applicationInfo
            };

            var layerNames = new[] { "VK_LAYER_KHRONOS_validation" };
            var enabledLayerNames = Array.Empty<IntPtr>();

            var extensionNames = new List<string>() { VK.KhrSurfaceExtensionName, VK.KhrWin32SurfaceExtensionName }; // TODO: don't hard code platform specific extension
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
                        .Select(layerName => Marshal.StringToHGlobalAnsi(layerName))
                        .ToArray();

                    if (enabledLayerNames.Length == 0)
                        Console.WriteLine("No validation layers were loaded.");
                    else
                        extensionNames.Add(VK.ExtDebugReportExtensionName);
                }

                // convert extension names to pointers
                enabledExtensionNames = new IntPtr[extensionNames.Count];
                var extensionPropertyCount = 0u;
                VK.EnumerateInstanceExtensionProperties((byte*)null, ref extensionPropertyCount, null);
                var extensionProperties = new VkExtensionProperties[extensionPropertyCount];
                VK.EnumerateInstanceExtensionProperties((byte*)null, ref extensionPropertyCount, extensionProperties);

                var availableExtensionNames = extensionProperties.Select(property => Marshal.PtrToStringAnsi((IntPtr)property.ExtensionName));
                for (int i = 0; i < extensionNames.Count; i++)
                {
                    var extensionName = extensionNames[i];

                    if (!availableExtensionNames.Contains(extensionName))
                        throw new ApplicationException($"Required extension '{extensionName}' is not available.");

                    enabledExtensionNames[i] = Marshal.StringToHGlobalAnsi(extensionNames[i]);
                }

                // create instance
                fixed (void* enabledlayerNamesPointer = enabledLayerNames)
                fixed (void* enabledExtensionNamesPointer = enabledExtensionNames)
                {
                    instanceCreateInfo.EnabledLayerCount = (uint)enabledLayerNames.Length;
                    instanceCreateInfo.EnabledLayerNames = (byte**)enabledlayerNamesPointer;
                    instanceCreateInfo.EnabledExtensionCount = (uint)enabledExtensionNames.Length;
                    instanceCreateInfo.EnabledExtensionNames = (byte**)enabledExtensionNamesPointer;

                    if (VK.CreateInstance(ref instanceCreateInfo, null, out NativeInstance) != VkResult.Success)
                        throw new ApplicationException("Failed to create Vulkan instance.");

                    VK.InitialiseInstanceMethods(NativeInstance);
                }

                // set up debug report
                if (enabledLayerNames.Length > 0)
                {
                    var debugReportCallbackCreateInfo = new VkDebugReportCallbackCreateInfoEXT()
                    {
                        SType = VkStructureType.DebugReportCreateInfoExt,
                        Flags = VkDebugReportFlagsEXT.ErrorExt | VkDebugReportFlagsEXT.WarningExt | VkDebugReportFlagsEXT.PerformanceWarningExt,
                        Callback = Marshal.GetFunctionPointerForDelegate(DebugReportCallback)
                    };

                    if (VK.CreateDebugReportCallbackEXT(NativeInstance, ref debugReportCallbackCreateInfo, null, out NativeDebugReportCallback) != VkResult.Success)
                        throw new ApplicationException("Failed to create debug report callback.");
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
            var win32SurfaceCreateInfo = new VkWin32SurfaceCreateInfoKHR() // TODO: don't hard code platform specific surface
            {
                SType = VkStructureType.Win32SurfaceCreateInfoKhr,
                Hwnd = WindowHandle,
                Hinstance = Program.Handle
            };

            if (VK.CreateWin32SurfaceKHR(NativeInstance, ref win32SurfaceCreateInfo, null, out var nativeSurface) != VkResult.Success)
                throw new ApplicationException("Failed to create surface.");
            NativeSurface = nativeSurface;
        }

        /// <summary>Picks the physical device for Vulkan to use.</summary>
        /// <returns>The physical device that Vulkan should use.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no physical devices have Vulkan support, or if no Vulkan supporting devices were valid.</exception>
        private VkPhysicalDevice PickPhysicalDevice()
        {
            // get the physical devices and ensure atleast one supports Vulkan
            var deviceCount = 0u;
            VK.EnumeratePhysicalDevices(NativeInstance, ref deviceCount, null);
            if (deviceCount == 0)
                throw new InvalidOperationException("No physical devices have support for Vulkan.");
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

            // ensure there is atleast one suitable device
            if (suitablePhysicalDevices.Count == 0)
                throw new InvalidOperationException("No physical devices that have support for Vulkan are suitable.");

            // pick the most suitable physical device
            return suitablePhysicalDevices
                .OrderByDescending(physicalDevice => physicalDevice.Value)
                .First().Key;
        }

        /// <summary>Creates the render pass.</summary>
        /// <exception cref="ApplicationException">Thrown if the render pass couldn't be created.</exception>
        private void CreateRenderPass()
        {
            var colourAttachmentDescription = new VkAttachmentDescription()
            {
                Format = Swapchain.ImageFormat,
                Samples = VulkanUtilities.ConvertSampleCount(RenderingSettings.SampleCount),
                LoadOp = VkAttachmentLoadOp.Clear,
                StoreOp = VkAttachmentStoreOp.Store,
                StencilLoadOp = VkAttachmentLoadOp.DontCare,
                StencilStoreOp = VkAttachmentStoreOp.DontCare,
                InitialLayout = VkImageLayout.Undefined,
                FinalLayout = VkImageLayout.ColorAttachmentOptimal
            };

            var depthAttachmentDescription = new VkAttachmentDescription()
            {
                Format = VulkanSwapchain.GetDepthFormat(),
                Samples = VulkanUtilities.ConvertSampleCount(RenderingSettings.SampleCount),
                LoadOp = VkAttachmentLoadOp.Clear,
                StoreOp = VkAttachmentStoreOp.Store,
                StencilLoadOp = VkAttachmentLoadOp.DontCare,
                StencilStoreOp = VkAttachmentStoreOp.DontCare,
                InitialLayout = VkImageLayout.Undefined,
                FinalLayout = VkImageLayout.DepthStencilAttachmentOptimal
            };

            var colourAttachmentResolveDescription = new VkAttachmentDescription()
            {
                Format = Swapchain.ImageFormat,
                Samples = VkSampleCountFlags.Count1,
                LoadOp = VkAttachmentLoadOp.DontCare,
                StoreOp = VkAttachmentStoreOp.Store,
                StencilLoadOp = VkAttachmentLoadOp.DontCare,
                StencilStoreOp = VkAttachmentStoreOp.DontCare,
                InitialLayout = VkImageLayout.Undefined,
                FinalLayout = VkImageLayout.PresentSourceKhr
            };

            var colourAttachmentReference = new VkAttachmentReference() { Attachment = 0, Layout = VkImageLayout.ColorAttachmentOptimal };
            var depthAttachmentReference = new VkAttachmentReference() { Attachment = 1, Layout = VkImageLayout.DepthStencilAttachmentOptimal };
            var colourAttachmentResolveReference = new VkAttachmentReference() { Attachment = 2, Layout = VkImageLayout.ColorAttachmentOptimal };

            var subpassDescription = new VkSubpassDescription()
            {
                PipelineBindPoint = VkPipelineBindPoint.Graphics,
                ColorAttachmentCount = 1,
                ColorAttachments = &colourAttachmentReference,
                DepthStencilAttachment = &depthAttachmentReference,
                ResolveAttachments = &colourAttachmentResolveReference
            };

            var subpassDependency = new VkSubpassDependency()
            {
                SourceSubpass = VK.SubpassExternal,
                DestinationSubpass = 0,
                SourceStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                SourceAccessMask = 0,
                DestinationStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                DestinationAccessMask = VkAccessFlags.ColorAttachmentWrite,
                DependencyFlags = 0
            };

            var attachments = new[] { colourAttachmentDescription, depthAttachmentDescription, colourAttachmentResolveDescription };

            fixed (VkAttachmentDescription* attachmentsPointer = attachments)
            {
                var renderPassCreateInfo = new VkRenderPassCreateInfo()
                {
                    SType = VkStructureType.RenderPassCreateInfo,
                    AttachmentCount = (uint)attachments.Length,
                    Attachments = attachmentsPointer,
                    SubpassCount = 1,
                    Subpasses = &subpassDescription,
                    DependencyCount = 1,
                    Dependencies = &subpassDependency
                };

                if (VK.CreateRenderPass(Device.NativeDevice, ref renderPassCreateInfo, null, out var nativeRenderPass) != VkResult.Success)
                    throw new ApplicationException("Failed to create render pass.");
                NativeRenderPass = nativeRenderPass;
            }
        }

        /// <summary>Creates the descriptor set layout.</summary>
        /// <exception cref="ApplicationException">Thrown if the descriptor set layout couldn't be created.</exception>
        private void CreateDescriptorSetLayout()
        {
            var uboLayoutBinding = new VkDescriptorSetLayoutBinding()
            {
                Binding = 0,
                DescriptorType = VkDescriptorType.UniformBuffer,
                DescriptorCount = 1,
                StageFlags = VkShaderStageFlags.Vertex
            };

            var samplerLayoutBinding = new VkDescriptorSetLayoutBinding()
            {
                Binding = 1,
                DescriptorType = VkDescriptorType.CombinedImageSampler,
                DescriptorCount = 1,
                StageFlags = VkShaderStageFlags.Fragment
            };

            var bindings = new[] { uboLayoutBinding, samplerLayoutBinding };

            fixed (VkDescriptorSetLayoutBinding* bindingsPointer = bindings)
            {
                var descriptorSetLayoutCreateInfo = new VkDescriptorSetLayoutCreateInfo()
                {
                    SType = VkStructureType.DescriptorSetLayoutCreateInfo,
                    BindingCount = (uint)bindings.Length,
                    Bindings = bindingsPointer
                };

                if (VK.CreateDescriptorSetLayout(Device.NativeDevice, ref descriptorSetLayoutCreateInfo, null, out var descriptorSetLayout) != VkResult.Success)
                    throw new ApplicationException("Failed to create descriptor set layout.");
                NativeDescriptorSetLayout = descriptorSetLayout;
            }
        }

        /// <summary>Create the semaphores and fences.</summary>
        /// <exception cref="ApplicationException">Thrown if the semaphores or fences couldn't be created.</exception>
        private void CreateSyncObjects()
        {
            ImageAvailableSemaphores = new VkSemaphore[ConcurrentFrames];
            RenderFinishedSemaphores = new VkSemaphore[ConcurrentFrames];
            InFlightFences = new VkFence[ConcurrentFrames];
            ImagesInFlight = new VkFence[Swapchain.NativeImages.Length];

            for (int i = 0; i < ConcurrentFrames; i++)
            {
                var semaphoreCreateInfo = new VkSemaphoreCreateInfo()
                {
                    SType = VkStructureType.SemaphoreCreateInfo
                };

                var fenceCreateInfo = new VkFenceCreateInfo()
                {
                    SType = VkStructureType.FenceCreateInfo,
                    Flags = VkFenceCreateFlags.Signaled
                };

                if (VK.CreateSemaphore(Device.NativeDevice, ref semaphoreCreateInfo, null, out ImageAvailableSemaphores[i]) != VkResult.Success ||
                    VK.CreateSemaphore(Device.NativeDevice, ref semaphoreCreateInfo, null, out RenderFinishedSemaphores[i]) != VkResult.Success)
                    throw new ApplicationException("Failed to create semaphores.");

                if (VK.CreateFence(Device.NativeDevice, ref fenceCreateInfo, null, out InFlightFences[i]) != VkResult.Success)
                    throw new ApplicationException("Failed to create fence.");
            }
        }

        /// <summary>Creates the descriptor pool.</summary>
        /// <exception cref="ApplicationException">Thrown if the descriptor pool couldn't be created.</exception>
        private void CreateDescriptorPool()
        {
            var uboDescriptorSize = new VkDescriptorPoolSize() { Type = VkDescriptorType.UniformBuffer, DescriptorCount = 256 };
            var samplerDescriptorSize = new VkDescriptorPoolSize() { Type = VkDescriptorType.CombinedImageSampler, DescriptorCount = 256 };

            var poolSizes = new[] { uboDescriptorSize, samplerDescriptorSize };

            fixed (VkDescriptorPoolSize* poolSizesPointer = poolSizes)
            {
                var descriptorPoolCreateInfo = new VkDescriptorPoolCreateInfo() // TODO: dynamically expand / shrink descriptor pool
                {
                    SType = VkStructureType.DescriptorPoolCreateInfo,
                    MaxSets = 256,
                    PoolSizeCount = (uint)poolSizes.Length,
                    PoolSizes = poolSizesPointer
                };

                if (VK.CreateDescriptorPool(Device.NativeDevice, ref descriptorPoolCreateInfo, null, out var descriptorPool) != VkResult.Success)
                    throw new ApplicationException("Failed to create descriptor pool.");
                NativeDescriptorPool = descriptorPool;
            }
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
            Console.WriteLine(Marshal.PtrToStringAnsi((IntPtr)message));
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

                // check if the device is decrete (have a major performance advantage)
                if (deviceProperties.DeviceType == VkPhysicalDeviceType.DiscreteGpu)
                    score += 1000;

                // maximum image size is typically an indication of a devices performance
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
}
