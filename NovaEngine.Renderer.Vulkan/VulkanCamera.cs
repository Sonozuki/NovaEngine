#pragma warning disable CA1506 // Too coupled TODO: fix with renderer rewrite (once NSL has been finished)

namespace NovaEngine.Renderer.Vulkan;

/// <summary>Represents a Vulkan camera.</summary>
public unsafe class VulkanCamera : RendererCameraBase
{
    /*********
    ** Constants
    *********/
    /// <summary>The number of frames that can be calculated concurrently.</summary>
    private const int ConcurrentFrames = 2;

    /// <summary>The size of each tile.</summary>
    private const int TileSize = 16;

    /// <summary>The maximum number of lights that can be used to render a frame.</summary>
    private const int MaxNumberOfLights = 4096;

    /// <summary>The average number of lights per tile.</summary>
    /// <remarks>This determines how much memory to allocate for <see cref="OpaqueLightIndexListBuffer"/> &amp; <see cref="TransparentLightIndexListBuffer"/>.</remarks>
    private const int AverageNumberOfLightsPerTile = 128;


    /*********
    ** Fields
    *********/
    /// <summary>Whether the camera has been disposed.</summary>
    private bool IsDisposed;

    /// <summary>The pipelines that Vulkan will use.</summary>
    private VulkanPipelines Pipelines;

    /// <summary>The command pool for compute command buffers.</summary>
    private readonly VulkanCommandPool ComputeCommandPool;

    /// <summary>The command pool for graphics command buffers.</summary>
    private readonly VulkanCommandPool GraphicsCommandPool;

    /// <summary>The semaphores that will get signalled when the depth pre-pass have finished.</summary>
    private VkSemaphore[] DepthPrepassFinishedSemaphores;

    /// <summary>The semaphores that will get signalled when the light culling has been finished.</summary>
    private VkSemaphore[] LightCullingFinishedSemaphores;

    /// <summary>The semaphores that will get signalled when an image have been retrieved from the swapchain.</summary>
    private VkSemaphore[] ImageAvailableSemaphores;

    /// <summary>The semaphores that will get signalled when rendering to the respective image has finished and presentation can begin.</summary>
    private VkSemaphore[] RenderFinishedSemaphores;

    /// <summary>The fences that are used to keep track of when a frame is available.</summary>
    private VkFence[] InFlightFences;

    /// <summary>The fences used to keep track of when an image is being used.</summary>
    private VkFence[] ImagesInFlight;

    /// <summary>The current frame index.</summary>
    /// <remarks>This is used to determine which sync objects to use, it will always be between 0 and --<see cref="ConcurrentFrames"/>.</remarks>
    private int CurrentFrameIndex;

    /// <summary>The depth pre-pass textures.</summary>
    private DepthTexture[] DepthPrepassTextures;

    /// <summary>The depth pre-pass framebuffers.</summary>
    private VkFramebuffer[] DepthPrepassFramebuffers;

    /// <summary>The buffer to store the pre-computed tile frustums.</summary>
    private VulkanBuffer FrustumsBuffer;

    /// <summary>The buffer to store the global counter for the current index into the light index list for the opaque light list.</summary>
    private VulkanBuffer OpaqueLightIndexCounterBuffer;

    /// <summary>The buffer to store the global counter for the current index into the light index list for the transparent light list.</summary>
    private VulkanBuffer TransparentLightIndexCounterBuffer;

    /// <summary>The descriptor set for the generate frustums compute shader.</summary>
    private VulkanDescriptorSet GenerateFrustumsDescriptorSet;

    /// <summary>The descriptor sets for the cull lights compute shader.</summary>
    private VulkanDescriptorSet[] CullLightsDescriptorSets;

    /// <summary>The command buffer for generating tile frustums.</summary>
    private VkCommandBuffer GenerateFrustumsCommandBuffer;

    /// <summary>The command buffers for culling lights.</summary>
    private VkCommandBuffer[] CullLightsCommandBuffers;

    /// <summary>The rendering command buffer currently being executed.</summary>
    /// <remarks>This is used so the previous frame being rendered can be freed once it has finished execution.</remarks>
    private VkCommandBuffer RenderingCommandBufferInFlight;


    /*********
    ** Properties
    *********/
    /// <summary>The swapchain Vulkan will present to.</summary>
    internal VulkanSwapchain Swapchain { get; private set; }

    /// <summary>The depth pre-pass render pass.</summary>
    internal VkRenderPass DepthPrepassRenderPass { get; private set; }

    /// <summary>The final rendering render pass.</summary>
    internal VkRenderPass FinalRenderingRenderPass { get; private set; }

    /// <summary>The buffer containing the parameters for the compute shaders.</summary>
    internal VulkanBuffer ParametersBuffer { get; private set; }

    /// <summary>The buffer containing the lights to use when rendering.</summary>
    internal VulkanBuffer LightsBuffer { get; private set; }

    /// <summary>The buffer to store the light indices for opaque geometry.</summary>
    internal VulkanBuffer OpaqueLightIndexListBuffer { get; private set; }

    /// <summary>The buffer to store the light indices for transparent geometry.</summary>
    internal VulkanBuffer TransparentLightIndexListBuffer { get; private set; }

    /// <summary>The buffer to store the opaque light grid.</summary>
    internal VulkanBuffer OpaqueLightGridBuffer { get; private set; }

    /// <summary>The buffer to store the transparent light grid.</summary>
    internal VulkanBuffer TransparentLightGridBuffer { get; private set; }

    /// <summary>The number of tiles in both axis.</summary>
    private Vector2U NumberOfTiles => new((uint)MathF.Ceiling(BaseCamera.Resolution.X / (float)TileSize), (uint)MathF.Ceiling(BaseCamera.Resolution.Y / (float)TileSize));

    /// <summary>The total number of tiles.</summary>
    private uint TotalNumberOfTiles => NumberOfTiles.X * NumberOfTiles.Y;

    /// <inheritdoc/>
    public override Texture2D RenderTarget => Swapchain.ColourTexture;


    /*********
    ** Constructors
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <inheritdoc/>
    public VulkanCamera(Camera baseCamera)
        : base(baseCamera)
    {
        Swapchain = new(BaseCamera.IsVSyncEnabled, new((uint)BaseCamera.Resolution.X, (uint)BaseCamera.Resolution.Y));
        CreateRenderPasses();
        Swapchain.CreateFramebuffers(FinalRenderingRenderPass);
        CreateDepthPrepassResources();

        Pipelines = new(this);

        CreateSyncObjects();

        ComputeCommandPool = new(CommandPoolUsage.Compute);
        GraphicsCommandPool = new(CommandPoolUsage.Graphics);

        CreateFixedBuffers();
        CreateDynamicBuffers();
        CreateDescriptorSets();
        CreateComputeCommandBuffers();

        GenerateFrustums();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override void OnResolutionChange() => RecreateSwapchain();

    /// <inheritdoc/>
    public override void OnVSyncChange() => RecreateSwapchain();

    /// <inheritdoc/>
    public override void Render(IEnumerable<RendererGameObjectBase> gameObjects, IEnumerable<RendererGameObjectBase> uiGameObjects, bool presentRenderTarget)
    {
        // retrieve image to render to
        var imageIndex = Swapchain.AcquireNextImage(ImageAvailableSemaphores[CurrentFrameIndex]);
        VK.WaitForFences(VulkanRenderer.Instance.Device.NativeDevice, 1, new[] { InFlightFences[CurrentFrameIndex] }, true, ulong.MaxValue);

        // check if a previous frame is using this image or has a command buffer
        if (!ImagesInFlight[imageIndex].IsNull)
            VK.WaitForFences(VulkanRenderer.Instance.Device.NativeDevice, 1, new[] { ImagesInFlight[imageIndex] }, true, ulong.MaxValue);
        if (!RenderingCommandBufferInFlight.IsNull)
        {
            // TODO: this is because the first two frames don't have their fences waited on resulting in freeing prematurely, this shouldn't wait on the queue like this eventually
            VK.QueueWaitIdle(VulkanRenderer.Instance.Device.GraphicsQueue);
            GraphicsCommandPool.FreeCommandBuffers(new[] { RenderingCommandBufferInFlight });
        }

        // mark the image as being used by this frame
        ImagesInFlight[imageIndex] = InFlightFences[CurrentFrameIndex];

        // TODO: temp
        var position = -Camera.Main!.Transform.GlobalPosition;
        var rotation = Camera.Main.Transform.GlobalRotation;
        var viewMatrix = Matrix4x4<float>.CreateTranslation(position)
                       * Matrix4x4<float>.CreateFromQuaternion(new(-rotation.X, -rotation.Y, rotation.Z, rotation.W));
        viewMatrix.Transpose();

        var worldSpace = new Vector4<float>(0, 0, 4.5f, 1);
        var light = new Light()
        {
            Type = 0, // point
            Intensity = 5,
            Range = 1f,
            Colour = new Vector4<float>(1, 0, 0, 1),
            PositionWorldSpace = worldSpace,
            PositionViewSpace = viewMatrix * worldSpace,
            IsEnabled = true
        };
        LightsBuffer.CopyFrom(light);

        var vulkanGameObjects = gameObjects.Cast<VulkanGameObject>().ToList();
        var triangleVulkanGameObjects = vulkanGameObjects.Where(vulkanGameObject => vulkanGameObject.MeshType == MeshType.TriangleList).ToList();
        var lineVulkanGameObjects = vulkanGameObjects.Where(vulkanGameObject => vulkanGameObject.MeshType == MeshType.LineList).ToList();
        foreach (var vulkanGameObject in vulkanGameObjects)
            vulkanGameObject.PrepareForCamera(BaseCamera);

        var vulkanUiGameObjects = uiGameObjects.Cast<VulkanGameObject>();
        foreach (var vulkanUiGameObject in vulkanUiGameObjects)
            vulkanUiGameObject.PrepareForCamera(BaseCamera);

        // depth pre-pass
        {
            var commandBuffer = GraphicsCommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);

            VkClearValue clearValue = new VkClearDepthStencilValue(1, 0);
            var renderPassBeginInfo = new VkRenderPassBeginInfo
            {
                SType = VkStructureType.RenderPassBeginInfo,
                RenderPass = DepthPrepassRenderPass,
                Framebuffer = DepthPrepassFramebuffers[CurrentFrameIndex],
                RenderArea = new(VkOffset2D.Zero, Swapchain.Extent),
                ClearValueCount = 1,
                ClearValues = &clearValue
            };

            VK.CommandBeginRenderPass(commandBuffer, ref renderPassBeginInfo, VkSubpassContents.Inline);

            DrawObjects(commandBuffer, Pipelines.DepthPrepassPipeline, Pipelines.DepthPrepassPipelineLayout, triangleVulkanGameObjects, vulkanGameObject => vulkanGameObject.DepthPrepassDescriptorSet.NativeDescriptorSet, false, false);

            VK.CommandEndRenderPass(commandBuffer);
            GraphicsCommandPool.SubmitCommandBuffer(true, commandBuffer, signalSemaphores: new[] { DepthPrepassFinishedSemaphores[CurrentFrameIndex] });
        }

        // cull lights
        {
            // TODO: populate lights buffer
            OpaqueLightIndexCounterBuffer.CopyFrom(0);
            TransparentLightIndexCounterBuffer.CopyFrom(0);

            ComputeCommandPool.SubmitCommandBuffer(false, CullLightsCommandBuffers[CurrentFrameIndex], freeCommandBuffer: false, waitSemaphores: new[] { DepthPrepassFinishedSemaphores[CurrentFrameIndex] }, waitDestinationStageMask: new[] { VkPipelineStageFlags.ComputeShader }, signalSemaphores: new[] { LightCullingFinishedSemaphores[CurrentFrameIndex] });
        }

        // final rendering
        {
            var commandBuffer = GraphicsCommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);

            var clearValues = new VkClearValue[] { new VkClearDepthStencilValue(1, 0), new VkClearColorValue(.05f, .05f, .05f, 0) };
            fixed (VkClearValue* clearValuePointers = clearValues)
            {
                var renderPassBeginInfo = new VkRenderPassBeginInfo
                {
                    SType = VkStructureType.RenderPassBeginInfo,
                    RenderPass = FinalRenderingRenderPass,
                    Framebuffer = Swapchain.NativeFramebuffers![imageIndex],
                    RenderArea = new VkRect2D(VkOffset2D.Zero, Swapchain.Extent),
                    ClearValueCount = (uint)clearValues.Length,
                    ClearValues = clearValuePointers
                };

                VK.CommandBeginRenderPass(commandBuffer, ref renderPassBeginInfo, VkSubpassContents.Inline);

                // opaque
                DrawObjects(commandBuffer, Pipelines.PBRTrianglePipeline, Pipelines.PBRTrianglePipelineLayout, triangleVulkanGameObjects, vulkanGameObject => vulkanGameObject.PBRDescriptorSet.NativeDescriptorSet, true, false);
                DrawObjects(commandBuffer, Pipelines.PBRLinePipeline, Pipelines.PBRLinePipelineLayout, lineVulkanGameObjects, vulkanGameObject => vulkanGameObject.PBRDescriptorSet.NativeDescriptorSet, true, false);
                // TODO: solid colour

                // transparent
                VK.CommandNextSubpass(commandBuffer, VkSubpassContents.Inline);
                // TODO: pbr and solid

                // mtsdf text
                VK.CommandNextSubpass(commandBuffer, VkSubpassContents.Inline);
                DrawObjects(commandBuffer, Pipelines.MTSDFTextPipeline, Pipelines.MTSDFTextPipelineLayout, vulkanUiGameObjects, vulkanUiGameObject => vulkanUiGameObject.MTSDFTextDescriptorSet.NativeDescriptorSet, false, true);

                VK.CommandEndRenderPass(commandBuffer);
                VK.EndCommandBuffer(commandBuffer);

                // run graphics queue
                var waitSemaphores = new VkSemaphore[] { ImageAvailableSemaphores[CurrentFrameIndex], LightCullingFinishedSemaphores[CurrentFrameIndex] };
                var waitDestinationStageMasks = new VkPipelineStageFlags[] { VkPipelineStageFlags.ColorAttachmentOutput, VkPipelineStageFlags.ColorAttachmentOutput };
                fixed (VkSemaphore* waitSemaphoresPointer = waitSemaphores)
                fixed (VkPipelineStageFlags* waitDestinationStageMasksPointer = waitDestinationStageMasks)
                {
                    var signalSemaphore = RenderFinishedSemaphores[CurrentFrameIndex];
                    var submitInfo = new VkSubmitInfo
                    {
                        SType = VkStructureType.SubmitInfo,
                        WaitSemaphoreCount = (uint)waitSemaphores.Length,
                        WaitSemaphores = waitSemaphoresPointer,
                        WaitDestinationStageMask = waitDestinationStageMasksPointer,
                        CommandBufferCount = 1,
                        CommandBuffers = &commandBuffer,
                        SignalSemaphoreCount = 1,
                        SignalSemaphores = &signalSemaphore
                    };

                    VK.ResetFences(VulkanRenderer.Instance.Device.NativeDevice, 1, new[] { InFlightFences[CurrentFrameIndex] });
                    VK.QueueSubmit(VulkanRenderer.Instance.Device.GraphicsQueue, 1, new[] { submitInfo }, InFlightFences[CurrentFrameIndex]);
                }

                RenderingCommandBufferInFlight = commandBuffer;
            }
        }

        // run presentation queue
        Swapchain.QueuePresent(RenderFinishedSemaphores[CurrentFrameIndex], imageIndex);

        CurrentFrameIndex = (CurrentFrameIndex + 1) % ConcurrentFrames;

        // Helper function that records draw commands for a collection of game objects
        static void DrawObjects(VkCommandBuffer commandBuffer, VkPipeline pipeline, VkPipelineLayout pipelineLayout, IEnumerable<VulkanGameObject> gameObjects, Func<VulkanGameObject, VkDescriptorSet> retrieveDescriptorSet, bool addPBRPushConstants, bool addMTSDFPushConstants)
        {
            VK.CommandBindPipeline(commandBuffer, VkPipelineBindPoint.Graphics, pipeline);

            foreach (var gameObject in gameObjects)
            {
                // TODO: temp, this should not be hard coded at all
                if (addPBRPushConstants)
                    SetPBRPushConstants(commandBuffer, pipelineLayout, gameObject);
                if (addMTSDFPushConstants)
                    SetMTSDFPushConstants(commandBuffer, pipelineLayout, gameObject);

                var vertexBuffer = gameObject.VertexBuffer!.NativeBuffer;
                var offsets = (VkDeviceSize)0;

                VK.CommandBindDescriptorSets(commandBuffer, VkPipelineBindPoint.Graphics, pipelineLayout, 0, 1, new[] { retrieveDescriptorSet(gameObject) }, 0, null);
                VK.CommandBindVertexBuffers(commandBuffer, 0, 1, ref vertexBuffer, ref offsets);
                VK.CommandBindIndexBuffer(commandBuffer, gameObject.IndexBuffer!.NativeBuffer, 0, VkIndexType.UInt32);
                VK.CommandDrawIndexed(commandBuffer, (uint)gameObject.IndexCount, 1, 0, 0, 0);
            }
        }

        // Helper function that records the commands for PBR push constants
        static void SetPBRPushConstants(VkCommandBuffer commandBuffer, VkPipelineLayout pipelineLayout, VulkanGameObject gameObject)
        {
            var material = gameObject.BaseGameObject.Components.Get<MeshRenderer>()!.Material;
            var vulkanMaterial = new VulkanMaterial(new(material.Tint.R / 255f, material.Tint.G / 255f, material.Tint.B / 255f), material.Roughness, material.Metallicness);

            VK.CommandPushConstants(commandBuffer, pipelineLayout, VkShaderStageFlags.Fragment, 0, (uint)sizeof(VulkanMaterial), &vulkanMaterial);
        }

        // Helper function that records the command for MTSDF push constants
        static void SetMTSDFPushConstants(VkCommandBuffer commandBuffer, VkPipelineLayout pipelineLayout, VulkanGameObject gameObject)
        {
            var textRenderer = gameObject.BaseGameObject.Components.Get<TextRenderer>()!;
            var mtsdfParams = new MTSDFParameters(textRenderer.FontSize / textRenderer.Font.MaxGlyphHeight * textRenderer.Font.PixelRange, textRenderer.FillType, textRenderer.BorderType, textRenderer.BorderWidth, textRenderer.BloomPower, textRenderer.BloomBrightness, textRenderer.FillColour, textRenderer.BorderColour);
        
            VK.CommandPushConstants(commandBuffer, pipelineLayout, VkShaderStageFlags.Fragment, 0, (uint)sizeof(MTSDFParameters), &mtsdfParams);
        }
    }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the camera.</summary>
    /// <param name="disposing">Whether the camera is being disposed deterministically.</param>
    protected override void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        CleanUpSwapchain();
        DisposeFixedBuffers();

        if (disposing)
        {
            ComputeCommandPool?.Dispose();
            GraphicsCommandPool?.Dispose();
        }

        for (var i = 0; i < ConcurrentFrames; i++)
        {
            VK.DestroySemaphore(VulkanRenderer.Instance.Device.NativeDevice, DepthPrepassFinishedSemaphores[i], null);
            VK.DestroySemaphore(VulkanRenderer.Instance.Device.NativeDevice, LightCullingFinishedSemaphores[i], null);
            VK.DestroySemaphore(VulkanRenderer.Instance.Device.NativeDevice, RenderFinishedSemaphores[i], null);
            VK.DestroySemaphore(VulkanRenderer.Instance.Device.NativeDevice, ImageAvailableSemaphores[i], null);
            VK.DestroyFence(VulkanRenderer.Instance.Device.NativeDevice, InFlightFences[i], null);
        }

        IsDisposed = true;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Recreates the <see cref="Swapchain"/>.</summary>
    private void RecreateSwapchain()
    {
        CleanUpSwapchain();

        Swapchain = new(BaseCamera.IsVSyncEnabled, new((uint)BaseCamera.Resolution.X, (uint)BaseCamera.Resolution.Y));
        CreateRenderPasses();
        Swapchain.CreateFramebuffers(FinalRenderingRenderPass);
        CreateDepthPrepassResources();

        Pipelines = new(this); // TODO: use dynamic state instead of recreating the entire pipeline

        CreateDynamicBuffers();
        CreateDescriptorSets();
        CreateComputeCommandBuffers();
        GenerateFrustums();
    }

    /// <summary>Cleans up the swapchain resources.</summary>
    private void CleanUpSwapchain()
    {
        VK.DeviceWaitIdle(VulkanRenderer.Instance.Device.NativeDevice);

        ComputeCommandPool?.FreeCommandBuffers(new[] { GenerateFrustumsCommandBuffer });
        ComputeCommandPool?.FreeCommandBuffers(CullLightsCommandBuffers);
        GraphicsCommandPool?.FreeCommandBuffers(new[] { RenderingCommandBufferInFlight });
        RenderingCommandBufferInFlight = new(); // reset so the rendering loop doesn't try to clean it up

        Pipelines?.Dispose();
        VK.DestroyRenderPass(VulkanRenderer.Instance.Device.NativeDevice, DepthPrepassRenderPass, null);
        VK.DestroyRenderPass(VulkanRenderer.Instance.Device.NativeDevice, FinalRenderingRenderPass, null);
        Swapchain?.Dispose();

        DisposeDepthPrepassResources();

        DisposeDynamicBuffers();
    }

    /// <summary>Creates the render passes.</summary>
    /// <exception cref="VulkanException">Thrown if a render pass couldn't be created.</exception>
    private void CreateRenderPasses()
    {
        // two render pass are required, this is because Vulkan doesn't support compute subpasses nor does it support dispatching compute buffers inside a render pass,
        // the first render pass is used for the depth pre-pass, then the light culling can be dispatched, then the second render pass can be started to draw geometry

        // depth pre-pass render pass
        {
            // attachment
            var attachment = new VkAttachmentDescription()
            {
                Format = VulkanSwapchain.GetDepthFormat(),
                Samples = VkSampleCountFlags._1,
                LoadOp = VkAttachmentLoadOp.Clear,
                StoreOp = VkAttachmentStoreOp.Store,
                StencilLoadOp = VkAttachmentLoadOp.DontCare,
                StencilStoreOp = VkAttachmentStoreOp.DontCare,
                InitialLayout = VkImageLayout.Undefined,
                FinalLayout = VkImageLayout.ShaderReadOnlyOptimal
            };

            // attachment reference
            var depthAttachmentReference = new VkAttachmentReference() { Attachment = 0, Layout = VkImageLayout.DepthStencilAttachmentOptimal };

            // subpass
            var subpass = new VkSubpassDescription
            {
                // depth pre-pass
                PipelineBindPoint = VkPipelineBindPoint.Graphics,
                DepthStencilAttachment = &depthAttachmentReference
            };

            // dependency
            var dependency = new VkSubpassDependency()
            {
                SourceSubpass = VK.SubpassExternal,
                DestinationSubpass = 0,
                SourceStageMask = VkPipelineStageFlags.BottomOfPipe,
                SourceAccessMask = VkAccessFlags.MemoryRead,
                DestinationStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                DestinationAccessMask = VkAccessFlags.ColorAttachmentRead | VkAccessFlags.ColorAttachmentWrite,
                DependencyFlags = VkDependencyFlags.ByRegion
            };

            var renderPassCreateInfo = new VkRenderPassCreateInfo()
            {
                SType = VkStructureType.RenderPassCreateInfo,
                AttachmentCount = 1,
                Attachments = &attachment,
                SubpassCount = 1,
                Subpasses = &subpass,
                DependencyCount = 1,
                Dependencies = &dependency
            };

            if (VK.CreateRenderPass(VulkanRenderer.Instance.Device.NativeDevice, ref renderPassCreateInfo, null, out var nativeRenderPass) != VkResult.Success)
                throw new VulkanException("Failed to create depth pre-pass render pass.").Log(LogSeverity.Fatal);
            DepthPrepassRenderPass = nativeRenderPass;
        }

        // final rendering render pass
        {
            // attachments
            var attachments = new VkAttachmentDescription[]
            {
                new() // depth
                {
                    Format = VulkanSwapchain.GetDepthFormat(),
                    Samples = VulkanUtilities.ConvertSampleCount(RenderingSettings.Instance.SampleCount),
                    LoadOp = VkAttachmentLoadOp.Clear,
                    StoreOp = VkAttachmentStoreOp.DontCare,
                    StencilLoadOp = VkAttachmentLoadOp.DontCare,
                    StencilStoreOp = VkAttachmentStoreOp.DontCare,
                    InitialLayout = VkImageLayout.Undefined,
                    FinalLayout = VkImageLayout.DepthStencilAttachmentOptimal
                },
                new() // multisampled colour
                {
                    Format = Swapchain.ImageFormat,
                    Samples = VulkanUtilities.ConvertSampleCount(RenderingSettings.Instance.SampleCount),
                    LoadOp = VkAttachmentLoadOp.Clear,
                    StoreOp = VkAttachmentStoreOp.DontCare,
                    StencilLoadOp = VkAttachmentLoadOp.DontCare,
                    StencilStoreOp = VkAttachmentStoreOp.DontCare,
                    InitialLayout = VkImageLayout.Undefined,
                    FinalLayout = VkImageLayout.ColorAttachmentOptimal
                },
                new() // resolved colour
                {
                    Format = Swapchain.ImageFormat,
                    Samples = VkSampleCountFlags._1,
                    LoadOp = VkAttachmentLoadOp.DontCare,
                    StoreOp = VkAttachmentStoreOp.Store,
                    StencilLoadOp = VkAttachmentLoadOp.DontCare,
                    StencilStoreOp = VkAttachmentStoreOp.DontCare,
                    InitialLayout = VkImageLayout.Undefined,
                    FinalLayout = VkImageLayout.PresentSourceKHR
                }
            };

            // attachment references
            var depthWriteAttachmentReference = new VkAttachmentReference() { Attachment = 0, Layout = VkImageLayout.DepthStencilAttachmentOptimal };
            var colourAttachmentReference = new VkAttachmentReference() { Attachment = 1, Layout = VkImageLayout.ColorAttachmentOptimal };
            var resolveAttachmentReference = new VkAttachmentReference() { Attachment = 2, Layout = VkImageLayout.ColorAttachmentOptimal };

            // subpasses
            var subpasses = new VkSubpassDescription[]
            {
                new() // opaque
                {
                    PipelineBindPoint = VkPipelineBindPoint.Graphics,
                    ColorAttachmentCount = 1,
                    ColorAttachments = &colourAttachmentReference,
                    DepthStencilAttachment = &depthWriteAttachmentReference
                },
                new() // transparent
                {
                    PipelineBindPoint = VkPipelineBindPoint.Graphics,
                    ColorAttachmentCount = 1,
                    ColorAttachments = &colourAttachmentReference,
                    DepthStencilAttachment = &depthWriteAttachmentReference
                },
                new() // ui
                {
                    PipelineBindPoint = VkPipelineBindPoint.Graphics,
                    ColorAttachmentCount = 1,
                    ColorAttachments = &colourAttachmentReference,
                    ResolveAttachments = &resolveAttachmentReference
                }
            };

            // dependencies
            var dependencies = new VkSubpassDependency[]
            {
                new()
                {
                    SourceSubpass = VK.SubpassExternal,
                    DestinationSubpass = 0,
                    SourceStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                    SourceAccessMask = 0,
                    DestinationStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                    DestinationAccessMask = VkAccessFlags.ColorAttachmentWrite,
                    DependencyFlags = 0
                },
                new()
                {
                    // opaque -> transparent
                    SourceSubpass = 0,
                    DestinationSubpass = 1,
                    SourceStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                    DestinationStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                    SourceAccessMask = VkAccessFlags.ColorAttachmentWrite,
                    DestinationAccessMask = VkAccessFlags.ColorAttachmentWrite,
                    DependencyFlags = VkDependencyFlags.ByRegion
                },
                new()
                {
                    // transparent -> ui
                    SourceSubpass = 1,
                    DestinationSubpass = 2,
                    SourceStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                    DestinationStageMask = VkPipelineStageFlags.ColorAttachmentOutput,
                    SourceAccessMask = VkAccessFlags.ColorAttachmentWrite,
                    DestinationAccessMask = VkAccessFlags.ColorAttachmentWrite,
                    DependencyFlags = VkDependencyFlags.ByRegion
                }
            };

            fixed (VkAttachmentDescription* attachmentsPointer = attachments)
            fixed (VkSubpassDescription* subpassesPointer = subpasses)
            fixed (VkSubpassDependency* dependenciesPointer = dependencies)
            {
                var renderPassCreateInfo = new VkRenderPassCreateInfo()
                {
                    SType = VkStructureType.RenderPassCreateInfo,
                    AttachmentCount = (uint)attachments.Length,
                    Attachments = attachmentsPointer,
                    SubpassCount = (uint)subpasses.Length,
                    Subpasses = subpassesPointer,
                    DependencyCount = (uint)dependencies.Length,
                    Dependencies = dependenciesPointer
                };

                if (VK.CreateRenderPass(VulkanRenderer.Instance.Device.NativeDevice, ref renderPassCreateInfo, null, out var nativeRenderPass) != VkResult.Success)
                    throw new VulkanException("Failed to create final rendering render pass.").Log(LogSeverity.Fatal);
                FinalRenderingRenderPass = nativeRenderPass;
            }
        }
    }

    /// <summary>Creates the depth pre-pass textures and framebuffers.</summary>
    private void CreateDepthPrepassResources()
    {
        // textures
        DepthPrepassTextures = new DepthTexture[ConcurrentFrames];
        for (var i = 0; i < ConcurrentFrames; i++)
            DepthPrepassTextures[i] = new DepthTexture(Swapchain.Extent.Width, Swapchain.Extent.Height, SampleCount._1);

        // framebuffers
        DepthPrepassFramebuffers = new VkFramebuffer[ConcurrentFrames];
        for (var i = 0; i < ConcurrentFrames; i++)
        {
            var attachment = (DepthPrepassTextures[i].RendererTexture as VulkanTexture)!.NativeImageView;

            var framebufferCreateInfo = new VkFramebufferCreateInfo
            {
                SType = VkStructureType.FramebufferCreateInfo,
                RenderPass = DepthPrepassRenderPass,
                AttachmentCount = 1,
                Attachments = &attachment,
                Width = Swapchain.Extent.Width,
                Height = Swapchain.Extent.Height,
                Layers = 1
            };

            if (VK.CreateFramebuffer(VulkanRenderer.Instance.Device.NativeDevice, ref framebufferCreateInfo, null, out DepthPrepassFramebuffers[i]) != VkResult.Success)
                throw new VulkanException("Failed to create depth pre-pass framebuffer").Log(LogSeverity.Fatal);
        }
    }

    /// <summary>Disposes the depth pre-pass textures and framebuffers.</summary>
    private void DisposeDepthPrepassResources()
    {
        foreach (var texture in DepthPrepassTextures)
            texture.Dispose();

        foreach (var framebuffer in DepthPrepassFramebuffers)
            VK.DestroyFramebuffer(VulkanRenderer.Instance.Device.NativeDevice, framebuffer, null);
    }

    /// <summary>Create the semaphores and fences.</summary>
    /// <exception cref="VulkanException">Thrown if the semaphores or fences couldn't be created.</exception>
    private void CreateSyncObjects()
    {
        DepthPrepassFinishedSemaphores = new VkSemaphore[ConcurrentFrames];
        LightCullingFinishedSemaphores = new VkSemaphore[ConcurrentFrames];
        ImageAvailableSemaphores = new VkSemaphore[ConcurrentFrames];
        RenderFinishedSemaphores = new VkSemaphore[ConcurrentFrames];
        InFlightFences = new VkFence[ConcurrentFrames];
        ImagesInFlight = new VkFence[Swapchain.NativeImages.Length];

        for (var i = 0; i < ConcurrentFrames; i++)
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

            if (VK.CreateSemaphore(VulkanRenderer.Instance.Device.NativeDevice, ref semaphoreCreateInfo, null, out DepthPrepassFinishedSemaphores[i]) != VkResult.Success ||
                VK.CreateSemaphore(VulkanRenderer.Instance.Device.NativeDevice, ref semaphoreCreateInfo, null, out LightCullingFinishedSemaphores[i]) != VkResult.Success ||
                VK.CreateSemaphore(VulkanRenderer.Instance.Device.NativeDevice, ref semaphoreCreateInfo, null, out ImageAvailableSemaphores[i]) != VkResult.Success ||
                VK.CreateSemaphore(VulkanRenderer.Instance.Device.NativeDevice, ref semaphoreCreateInfo, null, out RenderFinishedSemaphores[i]) != VkResult.Success)
                throw new VulkanException("Failed to create semaphores.").Log(LogSeverity.Fatal);

            if (VK.CreateFence(VulkanRenderer.Instance.Device.NativeDevice, ref fenceCreateInfo, null, out InFlightFences[i]) != VkResult.Success)
                throw new VulkanException("Failed to create fence.").Log(LogSeverity.Fatal);
        }
    }

    /// <summary>Creates the buffers that don't get recreated when the swapchain gets recreated.</summary>
    /// <exception cref="VulkanException">Thrown if a buffer couldn't be created.</exception>
    private void CreateFixedBuffers()
    {
        ParametersBuffer = new VulkanBuffer(sizeof(ParametersUBO), VkBufferUsageFlags.UniformBuffer | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.DeviceLocal);
        LightsBuffer = new VulkanBuffer(sizeof(Light) * MaxNumberOfLights, VkBufferUsageFlags.StorageBuffer | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.DeviceLocal);
        OpaqueLightIndexCounterBuffer = new VulkanBuffer(sizeof(uint), VkBufferUsageFlags.StorageBuffer | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.DeviceLocal);
        TransparentLightIndexCounterBuffer = new VulkanBuffer(sizeof(uint), VkBufferUsageFlags.StorageBuffer | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.DeviceLocal);
    }

    /// <summary>Creates the buffers that get recreated when the swapchain gets recreated.</summary>
    private void CreateDynamicBuffers()
    {
        FrustumsBuffer = new VulkanBuffer(sizeof(Frustum) * TotalNumberOfTiles, VkBufferUsageFlags.StorageBuffer, VkMemoryPropertyFlags.DeviceLocal);
        OpaqueLightIndexListBuffer = new VulkanBuffer(sizeof(uint) * TotalNumberOfTiles * AverageNumberOfLightsPerTile, VkBufferUsageFlags.StorageBuffer, VkMemoryPropertyFlags.DeviceLocal);
        TransparentLightIndexListBuffer = new VulkanBuffer(sizeof(uint) * TotalNumberOfTiles * AverageNumberOfLightsPerTile, VkBufferUsageFlags.StorageBuffer, VkMemoryPropertyFlags.DeviceLocal);
        OpaqueLightGridBuffer = new VulkanBuffer(sizeof(Vector2U) * TotalNumberOfTiles, VkBufferUsageFlags.StorageBuffer, VkMemoryPropertyFlags.DeviceLocal);
        TransparentLightGridBuffer = new VulkanBuffer(sizeof(Vector2U) * TotalNumberOfTiles, VkBufferUsageFlags.StorageBuffer, VkMemoryPropertyFlags.DeviceLocal);
    }

    /// <summary>Disposes the buffers that don't get recreated when the swapchain gets recreated.</summary>
    private void DisposeFixedBuffers()
    {
        ParametersBuffer?.Dispose();
        LightsBuffer?.Dispose();
        OpaqueLightIndexCounterBuffer?.Dispose();
        TransparentLightIndexCounterBuffer?.Dispose();
    }

    /// <summary>Disposes the buffers that get recreated when the swapchain gets recreated.</summary>
    private void DisposeDynamicBuffers()
    {
        FrustumsBuffer?.Dispose();
        OpaqueLightIndexListBuffer?.Dispose();
        TransparentLightIndexListBuffer?.Dispose();
        OpaqueLightGridBuffer?.Dispose();
        TransparentLightGridBuffer?.Dispose();
    }

    /// <summary>Creates the descriptor sets required for the tiled rendering.</summary>
    private void CreateDescriptorSets()
    {
        // generate frustums
        {
            GenerateFrustumsDescriptorSet = DescriptorPools.GenerateFrustumsDescriptorPool.GetDescriptorSet();

            var bufferInfo1 = new VkDescriptorBufferInfo() { Buffer = ParametersBuffer.NativeBuffer, Offset = 0, Range = ParametersBuffer.Size };
            var bufferInfo2 = new VkDescriptorBufferInfo() { Buffer = FrustumsBuffer.NativeBuffer, Offset = 0, Range = FrustumsBuffer.Size };

            GenerateFrustumsDescriptorSet
                .Bind(0, &bufferInfo1, VkDescriptorType.UniformBuffer)
                .Bind(1, &bufferInfo2, VkDescriptorType.StorageBuffer)
                .UpdateBindings();
        }

        // cull lights
        CullLightsDescriptorSets = new VulkanDescriptorSet[ConcurrentFrames];
        for (var i = 0; i < ConcurrentFrames; i++)
        {
            CullLightsDescriptorSets[i] = DescriptorPools.CullLightsDescriptorPool.GetDescriptorSet();

            var depthPrepassTexture = (DepthPrepassTextures[i].RendererTexture as VulkanTexture)!;

            var bufferInfo1 = new VkDescriptorBufferInfo() { Buffer = ParametersBuffer.NativeBuffer, Offset = 0, Range = ParametersBuffer.Size };
            var bufferInfo2 = new VkDescriptorBufferInfo() { Buffer = FrustumsBuffer.NativeBuffer, Offset = 0, Range = FrustumsBuffer.Size };
            var imageInfo = new VkDescriptorImageInfo() { ImageLayout = VkImageLayout.ShaderReadOnlyOptimal, ImageView = depthPrepassTexture.NativeImageView, Sampler = depthPrepassTexture.NativeSampler };
            var bufferInfo3 = new VkDescriptorBufferInfo() { Buffer = LightsBuffer.NativeBuffer, Offset = 0, Range = LightsBuffer.Size };
            var bufferInfo4 = new VkDescriptorBufferInfo() { Buffer = OpaqueLightIndexCounterBuffer.NativeBuffer, Offset = 0, Range = OpaqueLightIndexCounterBuffer.Size };
            var bufferInfo5 = new VkDescriptorBufferInfo() { Buffer = TransparentLightIndexCounterBuffer.NativeBuffer, Offset = 0, Range = TransparentLightIndexCounterBuffer.Size };
            var bufferInfo6 = new VkDescriptorBufferInfo() { Buffer = OpaqueLightIndexListBuffer.NativeBuffer, Offset = 0, Range = OpaqueLightIndexListBuffer.Size };
            var bufferInfo7 = new VkDescriptorBufferInfo() { Buffer = TransparentLightIndexListBuffer.NativeBuffer, Offset = 0, Range = TransparentLightIndexListBuffer.Size };
            var bufferInfo8 = new VkDescriptorBufferInfo() { Buffer = OpaqueLightGridBuffer.NativeBuffer, Offset = 0, Range = OpaqueLightGridBuffer.Size };
            var bufferInfo9 = new VkDescriptorBufferInfo() { Buffer = TransparentLightGridBuffer.NativeBuffer, Offset = 0, Range = TransparentLightGridBuffer.Size };

            CullLightsDescriptorSets[i]
                .Bind(0, &bufferInfo1, VkDescriptorType.UniformBuffer)
                .Bind(1, &bufferInfo2, VkDescriptorType.StorageBuffer)
                .Bind(2, &imageInfo)
                .Bind(3, &bufferInfo3, VkDescriptorType.StorageBuffer)
                .Bind(4, &bufferInfo4, VkDescriptorType.StorageBuffer)
                .Bind(5, &bufferInfo5, VkDescriptorType.StorageBuffer)
                .Bind(6, &bufferInfo6, VkDescriptorType.StorageBuffer)
                .Bind(7, &bufferInfo7, VkDescriptorType.StorageBuffer)
                .Bind(8, &bufferInfo8, VkDescriptorType.StorageBuffer)
                .Bind(9, &bufferInfo9, VkDescriptorType.StorageBuffer)
                .UpdateBindings();
        }
    }

    /// <summary>Creates the command buffers required for the tiled rendering.</summary>
    private void CreateComputeCommandBuffers()
    {
        // generate frustums
        {
            GenerateFrustumsCommandBuffer = ComputeCommandPool.AllocateCommandBuffer(true);

            VK.CommandBindPipeline(GenerateFrustumsCommandBuffer, VkPipelineBindPoint.Compute, Pipelines.GenerateFrustumsPipeline);
            VK.CommandBindDescriptorSets(GenerateFrustumsCommandBuffer, VkPipelineBindPoint.Compute, Pipelines.GenerateFrustumsPipelineLayout, 0, 1, new[] { GenerateFrustumsDescriptorSet.NativeDescriptorSet }, 0, null);
            VK.CommandDispatch(GenerateFrustumsCommandBuffer, NumberOfTiles.X, NumberOfTiles.Y, 1);

            VK.EndCommandBuffer(GenerateFrustumsCommandBuffer);
        }

        // cull lights
        {
            CullLightsCommandBuffers = new VkCommandBuffer[ConcurrentFrames];
            for (var i = 0; i < ConcurrentFrames; i++)
            {
                var commandBuffer = ComputeCommandPool.AllocateCommandBuffer(true);

                VK.CommandBindPipeline(commandBuffer, VkPipelineBindPoint.Compute, Pipelines.CullLightsPipeline);
                VK.CommandBindDescriptorSets(commandBuffer, VkPipelineBindPoint.Compute, Pipelines.CullLightsPipelineLayout, 0, 1, new[] { CullLightsDescriptorSets[i].NativeDescriptorSet }, 0, null);
                VK.CommandDispatch(commandBuffer, NumberOfTiles.X, NumberOfTiles.Y, 1);

                VK.EndCommandBuffer(commandBuffer);

                CullLightsCommandBuffers[i] = commandBuffer;
            }
        }
    }

    /// <summary>Generates the tile frustums.</summary>
    private void GenerateFrustums()
    {
        // update parameters
        var parameters = new ParametersUBO() { InverseProjection = BaseCamera.ProjectionMatrix.Inverse, ScreenResolution = BaseCamera.Resolution, NumberOfTilesWide = NumberOfTiles.X };
        ParametersBuffer.CopyFrom(parameters);

        // generate frustums
        var fenceCreateInfo = new VkFenceCreateInfo()
        {
            SType = VkStructureType.FenceCreateInfo,
        };

        // TODO: don't create a fence each time
        if (VK.CreateFence(VulkanRenderer.Instance.Device.NativeDevice, ref fenceCreateInfo, null, out var fence) != VkResult.Success)
            throw new VulkanException("Failed to create fence.").Log(LogSeverity.Fatal);

        var frustumsCommandBuffer = GenerateFrustumsCommandBuffer;
        var submitInfo = new VkSubmitInfo
        {
            SType = VkStructureType.SubmitInfo,
            CommandBufferCount = 1,
            CommandBuffers = &frustumsCommandBuffer
        };

        VK.QueueSubmit(VulkanRenderer.Instance.Device.ComputeQueue, 1, new[] { submitInfo }, fence);
        VK.WaitForFences(VulkanRenderer.Instance.Device.NativeDevice, 1, new[] { fence }, true, long.MaxValue);

        VK.DestroyFence(VulkanRenderer.Instance.Device.NativeDevice, fence, null);
    }
}
