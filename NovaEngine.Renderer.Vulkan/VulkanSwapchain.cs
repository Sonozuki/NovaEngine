using NovaEngine.Extensions;
using NovaEngine.Graphics;
using NovaEngine.Logging;
using NovaEngine.Settings;
using System;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>Encapsulates a <see cref="VkSwapchainKHR"/>.</summary>
    internal unsafe class VulkanSwapchain : IDisposable
    {
        /*********
        ** Fields
        *********/
        /// <summary>The underlying swapchain Vulkan will use for presentation.</summary>
        private readonly VkSwapchainKHR NativeSwapchain;


        /*********
        ** Accessors
        *********/
        /// <summary>The images in the swapchain.</summary>
        public VkImage[] NativeImages { get; }

        /// <summary>The <see cref="VkImageView"/>s for the <see cref="NativeImages"/>.</summary>
        public VkImageView[] NativeImageViews { get; }

        /// <summary>The framebuffers of the swapchain.</summary>
        public VkFramebuffer[]? NativeFramebuffers { get; private set; }

        /// <summary>The colour attachment of the swapchain.</summary>
        public Texture2D ColourTexture { get; }

        /// <summary>The depth attachment of the swapchain.</summary>
        public DepthTexture DepthTexture { get; }

        /// <summary>The format of the <see cref="NativeImages"/>.</summary>
        public VkFormat ImageFormat { get; }

        /// <summary>The extent (resolution) of the <see cref="NativeImages"/>.</summary>
        public VkExtent2D Extent { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="vsync">Whether vsync should be used when presenting.</param>
        /// <param name="extent">The extent to set the swapchain.</param>
        /// <exception cref="InvalidOperationException">Thrown if the physical device doesn't support the surface.</exception>
        /// <exception cref="ApplicationException">Thrown if the swapchain or swapchain image views couldn't be created.</exception>
        public VulkanSwapchain(bool vsync, VkExtent2D extent)
        {
            // get the swapchain details
            var swapchainSupport = new SwapchainSupportDetails(VulkanRenderer.Instance.Device.NativePhysicalDevice, VulkanRenderer.Instance.NativeSurface);

            var surfaceFormat = ChooseSurfaceFormat(swapchainSupport.Formats);
            var presentationMode = ChoosePresentationMode(swapchainSupport.PresentationModes, vsync);

            ImageFormat = surfaceFormat.Format;
            Extent = new VkExtent2D(
                width: Math.Clamp(extent.Width, swapchainSupport.Capabilities.MinImageExtent.Width, swapchainSupport.Capabilities.MaxImageExtent.Width),
                height: Math.Clamp(extent.Height, swapchainSupport.Capabilities.MinImageExtent.Height, swapchainSupport.Capabilities.MaxImageExtent.Height)
            );

            // determine the number of images in the swapchain
            var imageCount = Math.Clamp(swapchainSupport.Capabilities.MinImageCount + 1, swapchainSupport.Capabilities.MinImageCount, swapchainSupport.Capabilities.MaxImageCount);

            // create the image usage based on what the surface can support
            var imageUsage = VkImageUsageFlags.ColorAttachment;
            if (swapchainSupport.Capabilities.SupportedUsageFlags.HasFlag(VkImageUsageFlags.TransferSource))
                imageUsage |= VkImageUsageFlags.TransferSource;
            if (swapchainSupport.Capabilities.SupportedUsageFlags.HasFlag(VkImageUsageFlags.TransferDestination))
                imageUsage |= VkImageUsageFlags.TransferDestination;

            // find a supported composite alpha format (not all devices support CompositeAlphaFlags.Opaque)
            var compositeAlpha = VkCompositeAlphaFlagsKHR.OpaqueKhr;
            var compositeAlphaFlags = new[] { VkCompositeAlphaFlagsKHR.OpaqueKhr, VkCompositeAlphaFlagsKHR.PreMultipliedKhr, VkCompositeAlphaFlagsKHR.PostMultipliedKhr, VkCompositeAlphaFlagsKHR.InheritKhr };
            foreach (var compositeAlphaFlag in compositeAlphaFlags)
                if ((swapchainSupport.Capabilities.SupportedCompositeAlpha & compositeAlphaFlag) != 0)
                {
                    compositeAlpha = compositeAlphaFlag;
                    break;
                }

            // get the queue families to check how the swapchain should handle image sharing
            VK.GetPhysicalDeviceSurfaceSupportKHR(VulkanRenderer.Instance.Device.NativePhysicalDevice, new QueueFamilyIndices().GraphicsFamily, VulkanRenderer.Instance.NativeSurface, out var isSurfaceSupported);
            if (!isSurfaceSupported)
                throw new InvalidOperationException("Physical device doesn't support surface").Log(LogSeverity.Fatal);

            // create swapchain
            var swapchainCreateInfo = new VkSwapchainCreateInfoKHR()
            {
                SType = VkStructureType.SwapchainCreateInfoKhr,
                Surface = VulkanRenderer.Instance.NativeSurface,
                MinImageCount = imageCount,
                ImageFormat = surfaceFormat.Format,
                ImageColorSpace = surfaceFormat.ColorSpace,
                ImageExtent = Extent,
                ImageArrayLayers = 1,
                ImageUsage = imageUsage,
                ImageSharingMode = VkSharingMode.Exclusive,
                QueueFamilyIndices = null,
                PreTransform = swapchainSupport.Capabilities.CurrentTransform,
                CompositeAlpha = compositeAlpha,
                PresentMode = presentationMode,
                Clipped = true,
            };

            if (VK.CreateSwapchainKHR(VulkanRenderer.Instance.Device.NativeDevice, ref swapchainCreateInfo, null, out var nativeSwapchain) != VkResult.Success)
                throw new ApplicationException("Failed to create swapchain.").Log(LogSeverity.Fatal);
            NativeSwapchain = nativeSwapchain;

            // retrieve images and create image views
            var swapchainImagesCount = 0u;
            VK.GetSwapchainImagesKHR(VulkanRenderer.Instance.Device.NativeDevice, NativeSwapchain, ref swapchainImagesCount, null);
            var swapchainImages = new VkImage[swapchainImagesCount];
            VK.GetSwapchainImagesKHR(VulkanRenderer.Instance.Device.NativeDevice, NativeSwapchain, ref swapchainImagesCount, swapchainImages);
            NativeImages = swapchainImages;

            NativeImageViews = new VkImageView[NativeImages.Length];
            for (int i = 0; i < NativeImages.Length; i++)
            {
                var imageViewCreateInfo = new VkImageViewCreateInfo()
                {
                    SType = VkStructureType.ImageViewCreateInfo,
                    Image = NativeImages[i],
                    ViewType = VkImageViewType._2d,
                    Format = ImageFormat,
                    Components = VkComponentMapping.Identity,
                    SubresourceRange = new VkImageSubresourceRange(VkImageAspectFlags.Color, 0, 1, 0, 1)
                };

                if (VK.CreateImageView(VulkanRenderer.Instance.Device.NativeDevice, ref imageViewCreateInfo, null, out var nativeImageView) != VkResult.Success)
                    throw new ApplicationException("Failed to create swapchain image view.").Log(LogSeverity.Fatal);
                NativeImageViews[i] = nativeImageView;
            }

            // create the colour and depth attachments
            ColourTexture = new Texture2D(Extent.Width, Extent.Height, automaticallyGenerateMipChain: false, sampleCount: RenderingSettings.Instance.SampleCount);
            DepthTexture = new DepthTexture(Extent.Width, Extent.Height, RenderingSettings.Instance.SampleCount);
        }

        /// <summary>Creates the framebuffers.</summary>
        /// <param name="renderPass">The render pass to use when creating the framebuffers.</param>
        /// <exception cref="ApplicationException">Thrown if the framebuffers couldn't be created.</exception>
        public void CreateFramebuffers(VkRenderPass renderPass)
        {
            NativeFramebuffers = new VkFramebuffer[NativeImageViews.Length];
            for (int i = 0; i < NativeImageViews.Length; i++)
            {
                var attachments = new[] { (ColourTexture.RendererTexture as VulkanTexture)!.NativeImageView, (DepthTexture.RendererTexture as VulkanTexture)!.NativeImageView, NativeImageViews[i] };

                fixed (VkImageView* attachmentsPointer = attachments)
                {
                    var framebufferCreateInfo = new VkFramebufferCreateInfo()
                    {
                        SType = VkStructureType.FramebufferCreateInfo,
                        RenderPass = renderPass,
                        AttachmentCount = (uint)attachments.Length,
                        Attachments = attachmentsPointer,
                        Width = Extent.Width,
                        Height = Extent.Height,
                        Layers = 1
                    };

                    if (VK.CreateFramebuffer(VulkanRenderer.Instance.Device.NativeDevice, ref framebufferCreateInfo, null, out var framebuffer) != VkResult.Success)
                        throw new ApplicationException("Failed to create framebuffer.").Log(LogSeverity.Fatal);
                    NativeFramebuffers[i] = framebuffer;
                }
            }
        }

        /// <summary>Retrieves the index of the next available presentable image.</summary>
        /// <param name="semaphore">The semaphore to signal when the image is available.</param>
        /// <param name="fence">The fence to signal when the image is available.</param>
        /// <returns>The index of the next available presentable image.</returns>
        /// <exception cref="ApplicationException">Thrown if the next image couldn't be acquired.</exception>
        public uint AcquireNextImage(VkSemaphore semaphore, VkFence fence)
        {
            if (VK.AcquireNextImageKHR(VulkanRenderer.Instance.Device.NativeDevice, NativeSwapchain, ulong.MaxValue, semaphore, fence, out var imageIndex) != VkResult.Success)
                throw new ApplicationException("Failed to acquire next image.").Log(LogSeverity.Fatal);
            return imageIndex;
        }

        /// <summary>Queues an image for presentation.</summary>
        /// <param name="waitSemaphore">The semaphore to wait on before presenting.</param>
        /// <param name="imageIndex">The index of the swapchain image to  presentation.</param>
        /// <returns>The result from the presentation.</returns>
        /// <exception cref="ApplicationException">Thrown if the image couldn't be presented.</exception>
        public void QueuePresent(VkSemaphore waitSemaphore, uint imageIndex)
        {
            var swapchain = NativeSwapchain;
            var presentInfo = new VkPresentInfoKHR()
            {
                SType = VkStructureType.PresentInfoKhr,
                WaitSemaphoreCount = 1,
                WaitSemaphores = &waitSemaphore,
                ImageIndices = &imageIndex,
                SwapchainCount = 1,
                Swapchains = &swapchain
            };

            if (VK.QueuePresentKHR(VulkanRenderer.Instance.Device.GraphicsQueue, ref presentInfo) != VkResult.Success)
                throw new ApplicationException("Failed to queue presentation.").Log(LogSeverity.Fatal);
        }

        /// <summary>Gets the format for a depth texture.</summary>
        /// <returns>The format for a depth texture.</returns>
        public static VkFormat GetDepthFormat()
        {
            return GetSupportedFormat(
                formats: new[] { VkFormat.D32SFloat, VkFormat.D32SFloatS8UInt, VkFormat.D24UNormS8UInt },
                tiling: VkImageTiling.Optimal,
                features: VkFormatFeatureFlags.DepthStencilAttachment
            );
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            ColourTexture.Dispose();
            DepthTexture.Dispose();

            if (NativeFramebuffers != null)
                foreach (var framebuffer in NativeFramebuffers)
                    VK.DestroyFramebuffer(VulkanRenderer.Instance.Device.NativeDevice, framebuffer, null);

            foreach (var imageView in NativeImageViews)
                VK.DestroyImageView(VulkanRenderer.Instance.Device.NativeDevice, imageView, null);

            VK.DestroySwapchainKHR(VulkanRenderer.Instance.Device.NativeDevice, NativeSwapchain, null);
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Chooses the most desirable surface format from a specified collection.</summary>
        /// <param name="formats">The formats to pick the desired surface format from.</param>
        /// <returns>The most desired surfac format from <paramref name="formats"/>.</returns>
        private static VkSurfaceFormatKHR ChooseSurfaceFormat(VkSurfaceFormatKHR[] formats)
        {
            // if only one format is passed and it's undefined, just use Format.B8G8R8A8UNorm
            if (formats.Length == 1 && formats[0].Format == VkFormat.Undefined)
                return new VkSurfaceFormatKHR() { Format = VkFormat.B8G8R8A8UNorm, ColorSpace = formats[0].ColorSpace };

            // try to find Format.B8G8R8A8UNorm as it's the most desirable
            foreach (var format in formats)
                if (format.Format == VkFormat.B8G8R8A8UNorm)
                    return format;

            // if Format.B8G8R8A8UNorm isn't available, just return the first one
            return formats[0];
        }

        /// <summary>Chooses the most desirable presentation mode from a specified collection.</summary>
        /// <param name="presentationModes">The presentation modes to pick the desired presentation mode from.</param>
        /// <param name="vsync">Whether vsync should be used when presenting.</param>
        /// <returns>The most desired presentation mode from <paramref name="presentationModes"/>.</returns>
        private static VkPresentModeKHR ChoosePresentationMode(VkPresentModeKHR[] presentationModes, bool vsync)
        {
            // if vsync isn't requested, try to find PresentMode.Mailbox as it's the lowest latency non-tearing presentation mode
            if (!vsync)
                foreach (var presentationMode in presentationModes)
                    if (presentationMode == VkPresentModeKHR.MailboxKhr)
                        return presentationMode;

            // otherwise, just return fifo. fifo waits for the vertical blank (vsync) and is always present as per spec
            return VkPresentModeKHR.FifoKhr;
        }

        /// <summary>Gets the first format with the specified features.</summary>
        /// <param name="formats">The available formats.</param>
        /// <param name="tiling">The image tiling.</param>
        /// <param name="features">The format features required.</param>
        /// <returns>The first format from <paramref name="formats"/> that has <paramref name="features"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is no supported format.</exception>
        private static VkFormat GetSupportedFormat(VkFormat[] formats, VkImageTiling tiling, VkFormatFeatureFlags features)
        {
            foreach (var format in formats)
            {
                VK.GetPhysicalDeviceFormatProperties(VulkanRenderer.Instance.Device.NativePhysicalDevice, format, out var properties);

                if (tiling == VkImageTiling.Linear && (properties.LinearTilingFeatures & features) == features)
                    return format;
                else if (tiling == VkImageTiling.Optimal && (properties.OptimalTilingFeatures & features) == features)
                    return format;
            }

            throw new InvalidOperationException("Failed to find a supported format").Log(LogSeverity.Fatal);
        }
    }
}
