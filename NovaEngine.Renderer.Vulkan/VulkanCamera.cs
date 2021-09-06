using NovaEngine.Components;
using NovaEngine.Extensions;
using NovaEngine.External.Rendering;
using NovaEngine.Graphics;
using NovaEngine.Logging;
using NovaEngine.SceneManagement;
using NovaEngine.Settings;
using System;
using System.Linq;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>Represents a Vulkan camera.</summary>
    public unsafe class VulkanCamera : RendererCameraBase
    {
        /*********
        ** Constants
        *********/
        /// <summary>The number of frames that can be calculated concurrently.</summary>
        private const int ConcurrentFrames = 2;


        /*********
        ** Fields
        *********/
        /// <summary>The pipelines that Vulkan will use.</summary>
        private VulkanPipelines Pipeline;

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

        /// <summary>The current frame index.</summary>
        /// <remarks>This is used to determine which sync objects to use, it will always be between 0 and --<see cref="ConcurrentFrames"/>.</remarks>
        private int CurrentFrameIndex = 0;


        /*********
        ** Accessors
        *********/
        /// <summary>The swapchain Vulkan will present to.</summary>
        internal VulkanSwapchain Swapchain { get; private set; }

        /// <summary>The render pass.</summary>
        internal VkRenderPass NativeRenderPass { get; private set; }

        /// <inheritdoc/>
        public override Texture2D RenderTarget => Swapchain.ColourTexture;


        /*********
        ** Public Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <inheritdoc/>
        public VulkanCamera(Camera baseCamera)
            : base(baseCamera)
        {
            Swapchain = new(false, new((uint)this.BaseCamera.Resolution.X, (uint)this.BaseCamera.Resolution.Y)); // TODO: retrieve vsync from settings file
            CreateRenderPass();
            Swapchain.CreateFramebuffers(NativeRenderPass);

            Pipeline = new(this);

            CreateSyncObjects();

            CommandPool = new(CommandPoolUsage.Graphics);
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <inheritdoc/>
        public override void OnResolutionChange() => RecreateSwapchain();

        /// <inheritdoc/>
        public override void Render(bool presentRenderTarget)
        {
            // TODO: temp
            foreach (var meshObject in SceneManager.LoadedScenes.SelectMany(scene => scene.RootGameObjects).SelectMany(gameObject => gameObject.Children))
                (meshObject.RendererGameObject as VulkanGameObject)!.UpdateUBO(this.BaseCamera);

            VK.WaitForFences(VulkanRenderer.Instance.Device.NativeDevice, 1, new[] { InFlightFences[CurrentFrameIndex] }, true, ulong.MaxValue);

            // acquire an image from the swapchain
            var imageIndex = Swapchain.AcquireNextImage(ImageAvailableSemaphores[CurrentFrameIndex], VkFence.Null);

            // check if a previous frame is using this images (meaning there is a fence to wait on)
            if (!ImagesInFlight[imageIndex].IsNull)
                VK.WaitForFences(VulkanRenderer.Instance.Device.NativeDevice, 1, new[] { ImagesInFlight[imageIndex] }, true, ulong.MaxValue);

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

                VK.CommandBeginRenderPass(commandBuffer, ref renderPassBeginInfo, VkSubpassContents.Inline);
                VK.CommandBindPipeline(commandBuffer, VkPipelineBindPoint.Graphics, Pipeline.GraphicsPipeline);

                // TODO: get a reference to each game object recursively, not just the first level
                var vulkanGameObjects = SceneManager.LoadedScenes.SelectMany(scene => scene.RootGameObjects).SelectMany(gameObject => gameObject.Children).Select(meshObject => meshObject.RendererGameObject).Cast<VulkanGameObject>();
                foreach (var vulkanGameObject in vulkanGameObjects)
                {
                    VK.CommandBindDescriptorSets(commandBuffer, VkPipelineBindPoint.Graphics, Pipeline.GraphicsPipelineLayout, 0, 1, new[] { vulkanGameObject.DescriptorSet.NativeDescriptorSet }, 0, null);
                    var vertexBuffer = vulkanGameObject.VertexBuffer!.NativeBuffer;
                    var offsets = (VkDeviceSize)0;
                    VK.CommandBindVertexBuffers(commandBuffer, 0, 1, ref vertexBuffer, &offsets);
                    VK.CommandBindIndexBuffer(commandBuffer, vulkanGameObject.IndexBuffer!.NativeBuffer, 0, VkIndexType.Uint32);
                    VK.CommandDrawIndexed(commandBuffer, (uint)vulkanGameObject.IndexCount, 1, 0, 0, 0);
                }

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

                VK.ResetFences(VulkanRenderer.Instance.Device.NativeDevice, 1, new[] { InFlightFences[CurrentFrameIndex] });
                VK.QueueSubmit(VulkanRenderer.Instance.Device.GraphicsQueue, 1, new[] { submitInfo }, InFlightFences[CurrentFrameIndex]);
            }

            // TODO: fix this, skipping this doesn't allow the swapchain the return the acquired image
            if (presentRenderTarget)
            {
                // run presentation queue
                Swapchain.QueuePresent(RenderFinishedSemaphores[CurrentFrameIndex], imageIndex);
            }

            // wait for the queue to finish to stop overloading the GPU with work
            VK.QueueWaitIdle(VulkanRenderer.Instance.Device.GraphicsQueue);

            // advance the current frame index
            CurrentFrameIndex = (CurrentFrameIndex + 1) % ConcurrentFrames;

            // TODO: temp
            CommandPool.FreeCommandBuffers(new[] { commandBuffer });
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            CleanUpSwapchain();

            for (int i = 0; i < ConcurrentFrames; i++)
            {
                VK.DestroySemaphore(VulkanRenderer.Instance.Device.NativeDevice, RenderFinishedSemaphores[i], null);
                VK.DestroySemaphore(VulkanRenderer.Instance.Device.NativeDevice, ImageAvailableSemaphores[i], null);
                VK.DestroyFence(VulkanRenderer.Instance.Device.NativeDevice, InFlightFences[i], null);
            }

            CommandPool.Dispose();
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Recreates the <see cref="Swapchain"/>.</summary>
        private void RecreateSwapchain()
        {
            CleanUpSwapchain();

            Swapchain = new(false, new((uint)this.BaseCamera.Resolution.X, (uint)this.BaseCamera.Resolution.Y)); // TODO: retrieve vsync from settings file
            CreateRenderPass();
            Swapchain.CreateFramebuffers(NativeRenderPass);

            Pipeline = new(this); // TODO: use dynamic state instead of recreating the entire pipeline
        }

        /// <summary>Cleans up the swapchain resources.</summary>
        private void CleanUpSwapchain()
        {
            VK.DeviceWaitIdle(VulkanRenderer.Instance.Device.NativeDevice);

            Pipeline.Dispose();
            VK.DestroyRenderPass(VulkanRenderer.Instance.Device.NativeDevice, NativeRenderPass, null);
            Swapchain.Dispose();
        }

        /// <summary>Creates the render pass.</summary>
        /// <exception cref="ApplicationException">Thrown if the render pass couldn't be created.</exception>
        private void CreateRenderPass()
        {
            var colourAttachmentDescription = new VkAttachmentDescription()
            {
                Format = Swapchain.ImageFormat,
                Samples = VulkanUtilities.ConvertSampleCount(RenderingSettings.Instance.SampleCount),
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
                Samples = VulkanUtilities.ConvertSampleCount(RenderingSettings.Instance.SampleCount),
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

                if (VK.CreateRenderPass(VulkanRenderer.Instance.Device.NativeDevice, ref renderPassCreateInfo, null, out var nativeRenderPass) != VkResult.Success)
                    throw new ApplicationException("Failed to create render pass.").Log(LogSeverity.Fatal);
                NativeRenderPass = nativeRenderPass;
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

                if (VK.CreateSemaphore(VulkanRenderer.Instance.Device.NativeDevice, ref semaphoreCreateInfo, null, out ImageAvailableSemaphores[i]) != VkResult.Success ||
                    VK.CreateSemaphore(VulkanRenderer.Instance.Device.NativeDevice, ref semaphoreCreateInfo, null, out RenderFinishedSemaphores[i]) != VkResult.Success)
                    throw new ApplicationException("Failed to create semaphores.").Log(LogSeverity.Fatal);

                if (VK.CreateFence(VulkanRenderer.Instance.Device.NativeDevice, ref fenceCreateInfo, null, out InFlightFences[i]) != VkResult.Success)
                    throw new ApplicationException("Failed to create fence.").Log(LogSeverity.Fatal);
            }
        }
    }
}
