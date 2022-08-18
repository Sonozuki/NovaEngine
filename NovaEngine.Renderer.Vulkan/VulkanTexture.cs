namespace NovaEngine.Renderer.Vulkan;

/// <summary>Represents a Vulkan specific texture.</summary>
public unsafe class VulkanTexture : RendererTextureBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The memory for the <see cref="NativeImage"/>.</summary>
    private readonly VkDeviceMemory NativeMemory;

    /// <summary>The command pool used for mip map generation and image layout transition.</summary>
    private readonly VulkanCommandPool CommandPool;


    /*********
    ** Accessors
    *********/
    /// <summary>The texture image.</summary>
    public VkImage NativeImage { get; }

    /// <summary>The image view for <see cref="NativeImage"/>.</summary>
    public VkImageView NativeImageView { get; }

    /// <summary>The sampler for <see cref="NativeImage"/>.</summary>
    public VkSampler NativeSampler { get; }

    /// <summary>The format of <see cref="NativeImage"/>.</summary>
    public VkFormat Format { get; }

    /// <summary>The subresource aspect flags of <see cref="NativeImage"/>.</summary>
    public VkImageAspectFlags AspectFlags { get; }

    /// <summary>The number of samples per pixel of <see cref="NativeImage"/>.</summary>
    public VkSampleCountFlags SampleCount { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="baseTexture">The underlying texture.</param>
    /// <exception cref="ApplicationException">Thrown if the image, image view, or sampler couldn't be created or the memory couldn't be allocated or bound.</exception>
    public VulkanTexture(TextureBase baseTexture)
        : base(baseTexture)
    {
        // calculate the number of mip levels for the texture
        if (this.AutomaticallyGenerateMipChain)
            this.MipLevels = (uint)Math.Floor(MathF.Log2(Math.Max(this.Width, this.Height))) + 1; // +1 so original image has a mip level

        // convert generic texture types to Vulkan specific ones
        SampleCount = VulkanUtilities.ConvertSampleCount(this.BaseTexture.SampleCount);

        Format = this.Usage switch
        {
            TextureUsage.Colour => VkFormat.B8G8R8A8UNorm,
            TextureUsage.Colour32 => VkFormat.R32G32B32A32SFloat,
            TextureUsage.Depth => VulkanSwapchain.GetDepthFormat(),
            _ => throw new InvalidOperationException($"{nameof(this.Usage)} isn't valid.")
        };

        VkImageUsageFlags usage;
        (usage, AspectFlags) = this.Usage switch
        {
            TextureUsage.Colour => (VkImageUsageFlags.ColorAttachment, VkImageAspectFlags.Color),
            TextureUsage.Colour32 => (VkImageUsageFlags.Sampled, VkImageAspectFlags.Color), // TODO: make this a little nicer than forcing all 32 to sampled
            TextureUsage.Depth => (VkImageUsageFlags.DepthStencilAttachment, VkImageAspectFlags.Depth),
            _ => throw new InvalidOperationException($"{nameof(this.Usage)} isn't valid.")
        };
        usage = usage | VkImageUsageFlags.TransferSource | VkImageUsageFlags.TransferDestination | VkImageUsageFlags.Sampled;

        VkImageType imageType;
        VkImageViewType imageViewType;
        (imageType, imageViewType) = this.Type switch
        {
            TextureType.Texture1D => (VkImageType._1d, VkImageViewType._1d),
            TextureType.Texture1DArray => (VkImageType._1d, VkImageViewType._1dArray),
            TextureType.Texture2D => (VkImageType._2d, VkImageViewType._2d),
            TextureType.Texture2DArray => (VkImageType._2d, VkImageViewType._2dArray),
            TextureType.Texture3D => (VkImageType._3d, VkImageViewType._3d),
            TextureType.CubeMap => (VkImageType._2d, VkImageViewType.Cube),
            TextureType.CubeMapArray => (VkImageType._2d, VkImageViewType.CubeArray),
            _ => throw new InvalidOperationException($"{nameof(this.Type)} isn't valid.")
        };

        // create image
        var imageCreateInfo = new VkImageCreateInfo
        {
            SType = VkStructureType.ImageCreateInfo,
            ImageType = imageType,
            Format = Format,
            Extent = new(this.Width, this.Height, this.Depth),
            MipLevels = this.MipLevels,
            ArrayLayers = this.LayerCount,
            Samples = SampleCount,
            Tiling = VkImageTiling.Optimal,
            Usage = usage,
            SharingMode = VkSharingMode.Exclusive,
            InitialLayout = VkImageLayout.Undefined
        };

        if (VK.CreateImage(VulkanRenderer.Instance.Device.NativeDevice, ref imageCreateInfo, null, out var nativeImage) != VkResult.Success)
            throw new ApplicationException("Failed to create image.").Log(LogSeverity.Fatal);
        NativeImage = nativeImage;

        // allocate memory
        VK.GetImageMemoryRequirements(VulkanRenderer.Instance.Device.NativeDevice, NativeImage, out var memoryRequirements);

        var memoryAllocateInfo = new VkMemoryAllocateInfo
        {
            SType = VkStructureType.MemoryAllocateInfo,
            AllocationSize = memoryRequirements.Size,
            MemoryTypeIndex = VulkanRenderer.Instance.Device.GetMemoryTypeIndex(memoryRequirements.MemoryTypeBits, VkMemoryPropertyFlags.DeviceLocal)
        };

        if (VK.AllocateMemory(VulkanRenderer.Instance.Device.NativeDevice, ref memoryAllocateInfo, null, out NativeMemory) != VkResult.Success)
            throw new ApplicationException("Failed to allocate image memory.").Log(LogSeverity.Fatal);

        if (VK.BindImageMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeImage, NativeMemory, 0) != VkResult.Success)
            throw new ApplicationException("Failed to bind image memory.").Log(LogSeverity.Fatal);

        // create image view
        var imageViewCreateInfo = new VkImageViewCreateInfo
        {
            SType = VkStructureType.ImageViewCreateInfo,
            Image = NativeImage,
            ViewType = imageViewType,
            Format = Format,
            Components = VkComponentMapping.Identity,
            SubresourceRange = new(AspectFlags, 0, this.MipLevels, 0, this.LayerCount)
        };

        if (VK.CreateImageView(VulkanRenderer.Instance.Device.NativeDevice, ref imageViewCreateInfo, null, out var nativeImageView) != VkResult.Success)
            throw new ApplicationException("Failed to create image view.").Log(LogSeverity.Fatal);
        NativeImageView = nativeImageView;

        // create sampler
        var samplerCreateInfo = new VkSamplerCreateInfo
        {
            SType = VkStructureType.SamplerCreateInfo,
            MagFilter = VkFilter.Linear,
            MinFilter = VkFilter.Linear,
            MipmapMode = VkSamplerMipmapMode.Linear,
            AddressModeU = (VkSamplerAddressMode)this.WrapModeU,
            AddressModeV = (VkSamplerAddressMode)this.WrapModeV,
            AddressModeW = (VkSamplerAddressMode)this.WrapModeW,
            MipLodBias = this.MipLodBias,
            AnisotropyEnable = this.AnisotropicFilteringEnabled,
            MaxAnisotropy = this.MaxAnisotropicFilteringLevel,
            CompareEnable = false,
            CompareOp = VkCompareOp.Always,
            MinLod = 0,
            MaxLod = this.MipLevels,
            BorderColor = VkBorderColor.IntOpaqueBlack,
            UnnormalizedCoordinates = false
        };

        if (VK.CreateSampler(VulkanRenderer.Instance.Device.NativeDevice, ref samplerCreateInfo, null, out var nativeSampler) != VkResult.Success)
            throw new ApplicationException("Failed to create sampler.").Log(LogSeverity.Fatal);
        NativeSampler = nativeSampler;

        // create command pool
        CommandPool = new(CommandPoolUsage.Graphics, VkCommandPoolCreateFlags.Transient);
    }

    // TODO: clean up
    /// <inheritdoc/>
    public override Colour32[] GetPixels()
    {
        var bufferSize = this.Width * this.Height * (this.Usage == TextureUsage.Colour32 ? sizeof(Colour32) : sizeof(Colour));

        // transition layout to TransferSource
        TransitionImageLayout(VkImageLayout.Undefined, VkImageLayout.TransferSourceOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);

        // create a staging buffer and copy the texture to it
        using var stagingBuffer = new VulkanBuffer(bufferSize, VkBufferUsageFlags.TransferSource | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
        stagingBuffer.CopyFrom(this);

        // get the pixel data from the staging buffer
        if (this.Usage == TextureUsage.Colour32)
        {
            var pixelBuffer = new Colour32[bufferSize / sizeof(Colour32)];
            var pixelBufferSpan = pixelBuffer.AsSpan();
            stagingBuffer.CopyTo(pixelBufferSpan);

            TransitionImageLayout(VkImageLayout.TransferSourceOptimal, VkImageLayout.ShaderReadOnlyOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);
            return pixelBuffer;
        }
        else
        {
            var pixelBuffer = new Colour[bufferSize / sizeof(Colour)];
            var pixelBufferSpan = pixelBuffer.AsSpan();
            stagingBuffer.CopyTo(pixelBufferSpan);

            var convertedPixelBuffer = new Colour32[pixelBuffer.Length];
            for (int i = 0; i < pixelBuffer.Length; i++)
                convertedPixelBuffer[i] = pixelBuffer[i].ToColour32();

            TransitionImageLayout(VkImageLayout.TransferSourceOptimal, VkImageLayout.ShaderReadOnlyOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);
            return convertedPixelBuffer;
        }
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour[] pixels, int offset = 0)
    {
        var bufferSize = this.Width * this.Height * sizeof(Colour);

        // transition layout to TransferSource
        TransitionImageLayout(VkImageLayout.Undefined, VkImageLayout.TransferSourceOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);

        // create a staging buffer and copy the texture to it
        using var stagingBuffer = new VulkanBuffer(bufferSize, VkBufferUsageFlags.TransferSource | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
        stagingBuffer.CopyFrom(this);

        // get the pixel data from the staging buffer
        var pixelBuffer = new Colour[bufferSize / sizeof(Colour)];
        var pixelBufferSpan = pixelBuffer.AsSpan();
        stagingBuffer.CopyTo(pixelBufferSpan);

        // apply pixel changes
        Array.Copy(pixels, 0, pixelBuffer, offset, pixels.Length);

        stagingBuffer.CopyFrom(pixelBufferSpan);

        // copy the staging buffer to the texture
        TransitionImageLayout(VkImageLayout.TransferSourceOptimal, VkImageLayout.TransferDestinationOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);
        stagingBuffer.CopyTo(this);

        if (this.AutomaticallyGenerateMipChain)
            GenerateMipChain();
        else
            TransitionImageLayout(VkImageLayout.Undefined, VkImageLayout.ShaderReadOnlyOptimal);
    }

    // TODO: clean up
    /// <inheritdoc/>
    public override void SetPixels(Colour32[] pixels, int offset = 0)
    {
        var bufferSize = this.Width * this.Height * (this.Usage == TextureUsage.Colour32 ? sizeof(Colour32) : sizeof(Colour));

        // transition layout to TransferSource
        TransitionImageLayout(VkImageLayout.Undefined, VkImageLayout.TransferSourceOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);

        // create a staging buffer and copy the texture to it
        using var stagingBuffer = new VulkanBuffer(bufferSize, VkBufferUsageFlags.TransferSource | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
        stagingBuffer.CopyFrom(this);

        // get the pixel data from the staging buffer and apply pixel changes
        if (this.Usage == TextureUsage.Colour32)
        {
            var pixelBuffer = new Colour32[this.Width * this.Height];
            var pixelBufferSpan = pixelBuffer.AsSpan();
            stagingBuffer.CopyTo(pixelBufferSpan);

            Array.Copy(pixels, 0, pixelBuffer, offset, pixels.Length);

            stagingBuffer.CopyFrom(pixelBufferSpan);
        }
        else
        {
            var pixelBuffer = new Colour[this.Width * this.Height];
            var pixelBufferSpan = pixelBuffer.AsSpan();
            stagingBuffer.CopyTo(pixelBufferSpan);

            var convertedPixels = new Colour[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
                convertedPixels[i] = pixels[i].ToColour();

            Array.Copy(convertedPixels, 0, pixelBuffer, offset, convertedPixels.Length);

            stagingBuffer.CopyFrom(pixelBufferSpan);
        }

        // copy the staging buffer to the texture
        TransitionImageLayout(VkImageLayout.TransferSourceOptimal, VkImageLayout.TransferDestinationOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);
        stagingBuffer.CopyTo(this);

        if (this.AutomaticallyGenerateMipChain)
            GenerateMipChain();
        else
            TransitionImageLayout(VkImageLayout.Undefined, VkImageLayout.ShaderReadOnlyOptimal);
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0)
    {
        var bufferSize = this.Width * this.Height * sizeof(Colour);

        // transition layout to TransferSource
        TransitionImageLayout(VkImageLayout.Undefined, VkImageLayout.TransferSourceOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);

        // create a staging buffer and copy the texture to it
        using var stagingBuffer = new VulkanBuffer(bufferSize, VkBufferUsageFlags.TransferSource | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent);
        stagingBuffer.CopyFrom(this);

        // get the pixel data from the staging buffer
        var pixelBuffer = new Colour[bufferSize / sizeof(Colour)].AsSpan();
        stagingBuffer.CopyTo(pixelBuffer);

        // apply pixel changes
        for (int y = 0; y < pixels.GetLength(0); y++)
        {
            if (y + yOffset < 0 || y + yOffset >= this.Height)
                continue;

            for (int x = 0; x < pixels.GetLength(1); x++)
            {
                if (x + xOffset < 0 || x + xOffset >= this.Width)
                    continue;

                pixelBuffer[(y + yOffset) * (int)this.Width + (x + xOffset)] = pixels[x, y];
            }
        }

        stagingBuffer.CopyFrom(pixelBuffer);

        // copy the staging buffer to the texture
        TransitionImageLayout(VkImageLayout.TransferSourceOptimal, VkImageLayout.TransferDestinationOptimal, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer);
        stagingBuffer.CopyTo(this);
        GenerateMipChain();
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour32[,] pixels, int xOffset = 0, int yOffset = 0)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour32[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override void GenerateMipChain()
    {
        // ensure there is more than one mip level
        if (this.MipLevels <= 1)
            return;

        // ensure the texture format supports linear blitting
        VK.GetPhysicalDeviceFormatProperties(VulkanRenderer.Instance.Device.NativePhysicalDevice, Format, out var formatProperties);
        if (!formatProperties.OptimalTilingFeatures.HasFlag(VkFormatFeatureFlags.SampledImageFilterLinear))
            throw new InvalidOperationException($"Texture format: {Format} doesn't support linear blitting");

        var commandBuffer = CommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);

        // create the memory barrier that will be used to transition the layout of the levels
        var imageMemoryBarrier = new VkImageMemoryBarrier
        {
            SType = VkStructureType.ImageMemoryBarrier,
            Image = NativeImage,
            SourceQueueFamilyIndex = VK.QueueFamilyIgnored,
            DestinationQueueFamilyIndex = VK.QueueFamilyIgnored,
            OldLayout = VkImageLayout.TransferDestinationOptimal,
            NewLayout = VkImageLayout.TransferSourceOptimal,
            SourceAccessMask = VkAccessFlags.TransferWrite,
            DestinationAccessMask = VkAccessFlags.TransferRead,
            SubresourceRange = new(VkImageAspectFlags.Color, 0, 1, 0, 1)
        };

        var mipWidth = (int)this.Width;
        var mipHeight = (int)this.Height;

        // create a blit command for each mip level
        for (uint i = 1; i < this.MipLevels; i++)
        {
            // transition current level layout to TransferSource
            imageMemoryBarrier.SubresourceRange.BaseMipLevel = i - 1;
            imageMemoryBarrier.OldLayout = VkImageLayout.TransferDestinationOptimal;
            imageMemoryBarrier.NewLayout = VkImageLayout.TransferSourceOptimal;
            imageMemoryBarrier.SourceAccessMask = VkAccessFlags.TransferWrite;
            imageMemoryBarrier.DestinationAccessMask = VkAccessFlags.TransferRead;
            VK.CommandPipelineBarrier(commandBuffer, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.Transfer, 0, 0, null, 0, null, 1, new[] { imageMemoryBarrier });

            // submit the blit operation to create the next mip level
            var imageBlit = new VkImageBlit
            {
                SourceOffsets_0 = VkOffset3D.Zero,
                SourceOffsets_1 = new(mipWidth, mipHeight, 1),
                SourceSubresource = new(VkImageAspectFlags.Color, i - 1, 0, 1),
                DestinationOffsets_0 = VkOffset3D.Zero,
                DestinationOffsets_1 = new(mipWidth > 1 ? mipWidth / 2 : 1, mipHeight > 1 ? mipHeight / 2 : 1, 1),
                DestinationSubresource = new(VkImageAspectFlags.Color, i, 0, 1)
            };

            VK.CommandBlitImage(commandBuffer, NativeImage, VkImageLayout.TransferSourceOptimal, NativeImage, VkImageLayout.TransferDestinationOptimal, 1, new[] { imageBlit }, VkFilter.Linear);

            // transition the layout to shader read
            imageMemoryBarrier.OldLayout = VkImageLayout.TransferSourceOptimal;
            imageMemoryBarrier.NewLayout = VkImageLayout.ShaderReadOnlyOptimal;
            imageMemoryBarrier.SourceAccessMask = VkAccessFlags.TransferRead;
            imageMemoryBarrier.DestinationAccessMask = VkAccessFlags.ShaderRead;
            VK.CommandPipelineBarrier(commandBuffer, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.FragmentShader, 0, 0, null, 0, null, 1, new[] { imageMemoryBarrier });

            // update mip width/height
            if (mipWidth > 1) mipWidth /= 2;
            if (mipHeight > 1) mipHeight /= 2;
        }

        // transition the final mip level to shader readonly as the loop doesn't handle it
        imageMemoryBarrier.SubresourceRange.BaseMipLevel = this.MipLevels - 1;
        imageMemoryBarrier.OldLayout = VkImageLayout.TransferDestinationOptimal;
        imageMemoryBarrier.NewLayout = VkImageLayout.ShaderReadOnlyOptimal;
        imageMemoryBarrier.SourceAccessMask = VkAccessFlags.TransferWrite;
        imageMemoryBarrier.DestinationAccessMask = VkAccessFlags.ShaderRead;
        VK.CommandPipelineBarrier(commandBuffer, VkPipelineStageFlags.Transfer, VkPipelineStageFlags.FragmentShader, 0, 0, null, 0, null, 1, new[] { imageMemoryBarrier });

        CommandPool.SubmitCommandBuffer(true, commandBuffer);
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        CommandPool.Dispose();
        VK.DestroySampler(VulkanRenderer.Instance.Device.NativeDevice, NativeSampler, null);
        VK.DestroyImageView(VulkanRenderer.Instance.Device.NativeDevice, NativeImageView, null);
        VK.DestroyImage(VulkanRenderer.Instance.Device.NativeDevice, NativeImage, null);
        VK.FreeMemory(VulkanRenderer.Instance.Device.NativeDevice, NativeMemory, null);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Converts the texture to another layout.</summary>
    /// <param name="oldLayout">The old layout.</param>
    /// <param name="newLayout">The new layout.</param>
    /// <param name="sourceStage">The source pipeline stage.</param>
    /// <param name="destinationStage">The destination pipeline stage.</param>
    private void TransitionImageLayout(VkImageLayout oldLayout, VkImageLayout newLayout, VkPipelineStageFlags sourceStage = VkPipelineStageFlags.AllCommands, VkPipelineStageFlags destinationStage = VkPipelineStageFlags.AllCommands)
    {
        // create a memory barrier
        var imageMemoryBarrier = new VkImageMemoryBarrier
        {
            SType = VkStructureType.ImageMemoryBarrier,
            OldLayout = oldLayout,
            NewLayout = newLayout,
            SourceQueueFamilyIndex = VK.QueueFamilyIgnored,
            DestinationQueueFamilyIndex = VK.QueueFamilyIgnored,
            Image = NativeImage,
            SubresourceRange = new(AspectFlags, 0, this.MipLevels, 0, 1)
        };

        // set the source access mask
        // this controls actions that have to be finished on the old layout before it will be transitioned to the new layout
        switch (oldLayout)
        {
            case VkImageLayout.Undefined:
                // image layout is undefined (or doesn't matter)
                // this is only valid as an initial layout, this isn't required but here for completeness
                imageMemoryBarrier.SourceAccessMask = VkAccessFlags.NoneKhr;
                break;
            case VkImageLayout.Preinitialized:
                // image is preinitialised
                // this is only valid as an initial layout for linear images, preserves memory content. make sure host writes have been finished
                imageMemoryBarrier.SourceAccessMask = VkAccessFlags.HostWrite;
                break;
            case VkImageLayout.ColorAttachmentOptimal:
                // image is a colour attachment
                // make sure any writes to the colour buffer have been finished
                imageMemoryBarrier.SourceAccessMask = VkAccessFlags.ColorAttachmentWrite;
                break;
            case VkImageLayout.DepthStencilAttachmentOptimal:
                // image is a depth/stencil attachment
                // make sure any writes to the depth/stencil buffer have been finished
                imageMemoryBarrier.SourceAccessMask = VkAccessFlags.DepthStencilAttachmentWrite;
                break;
            case VkImageLayout.TransferSourceOptimal:
                // image is a transfer source
                // make sure any reads from the image have been finished
                imageMemoryBarrier.SourceAccessMask = VkAccessFlags.TransferRead;
                break;
            case VkImageLayout.TransferDestinationOptimal:
                // image is a trasfer destination
                // make sure any writes to the image have been finished
                imageMemoryBarrier.SourceAccessMask = VkAccessFlags.TransferWrite;
                break;
            case VkImageLayout.ShaderReadOnlyOptimal:
                // image is read by a shader
                // make sure any shader reads from the image have been finished
                imageMemoryBarrier.SourceAccessMask = VkAccessFlags.ShaderRead;
                break;
        }

        // set the destination access mask
        // this controls the dependency for the new image layout
        switch (newLayout)
        {
            case VkImageLayout.TransferDestinationOptimal:
                // image will be used as a transfer destination
                // make sure any writes to the iamge have been finished
                imageMemoryBarrier.DestinationAccessMask = VkAccessFlags.TransferWrite;
                break;
            case VkImageLayout.TransferSourceOptimal:
                // image will be used as a transfer source
                // make sure any reads from the image have been finished
                imageMemoryBarrier.DestinationAccessMask = VkAccessFlags.TransferRead;
                break;
            case VkImageLayout.ColorAttachmentOptimal:
                // image will be used as a colour attachment
                // make sure any writes to the colour buffer have been finished
                imageMemoryBarrier.DestinationAccessMask = VkAccessFlags.ColorAttachmentWrite;
                break;
            case VkImageLayout.DepthStencilAttachmentOptimal:
                // image will be used as a depth/stencil attachment
                // make sure any writes to the depth/stencil buffer have been finished
                imageMemoryBarrier.DestinationAccessMask = VkAccessFlags.DepthStencilAttachmentWrite;
                break;
            case VkImageLayout.ShaderReadOnlyOptimal:
                // image will be read in a shader
                // make sure any writes to the image have been finished
                if (imageMemoryBarrier.SourceAccessMask == VkAccessFlags.NoneKhr)
                    imageMemoryBarrier.SourceAccessMask = VkAccessFlags.HostWrite | VkAccessFlags.TransferWrite;
                imageMemoryBarrier.DestinationAccessMask = VkAccessFlags.ShaderRead;
                break;
        }

        // transition layout
        var commandBuffer = CommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);
        VK.CommandPipelineBarrier(commandBuffer, sourceStage, destinationStage, 0, 0, null, 0, null, 1, new[] { imageMemoryBarrier });
        CommandPool.SubmitCommandBuffer(true, commandBuffer);
    }
}
