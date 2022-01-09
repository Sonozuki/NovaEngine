using NovaEngine.Components;
using NovaEngine.Core;
using NovaEngine.Extensions;
using NovaEngine.External.Rendering;
using NovaEngine.Graphics;
using NovaEngine.Logging;
using NovaEngine.Maths;
using NovaEngine.Renderer.Vulkan.ShaderModels;
using NovaEngine.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using Vulkan;

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
    /// <remarks>Note: this must be the same as specified in the shader.</remarks>
    private const int TileSize = 16;


    /*********
    ** Fields
    *********/
    /// <summary>The pipelines that Vulkan will use.</summary>
    private VulkanPipelines Pipelines;

    /// <summary>The command pool for compute command buffers.</summary>
    private VulkanCommandPool ComputeCommandPool;

    /// <summary>The command pool for graphics command buffers.</summary>
    private VulkanCommandPool GraphicsCommandPool;

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

    /// <summary>The buffer containing the parameters for the compute shaders.</summary>
    private VulkanBuffer ParametersBuffer;

    /// <summary>The buffer containing the pre-computed tile frustums.</summary>
    private VulkanBuffer FrustumsBuffer;

    /// <summary>The descriptor set for the generate frustums compute shader.</summary>
    private VulkanDescriptorSet GenerateFrustumsDescriptorSet;

    /// <summary>The command buffer for generating tile frustums.</summary>
    private VkCommandBuffer GenerateFrustumsCommandBuffer;


    /*********
    ** Accessors
    *********/
    /// <summary>The swapchain Vulkan will present to.</summary>
    internal VulkanSwapchain Swapchain { get; private set; }

    /// <summary>The render pass.</summary>
    internal VkRenderPass NativeRenderPass { get; private set; }

    /// <summary>The number of tiles in both axis.</summary>
    private Vector2U NumberOfTiles => new((uint)MathF.Ceiling(BaseCamera.Resolution.X / (float)TileSize), (uint)MathF.Ceiling(BaseCamera.Resolution.Y / (float)TileSize));

    /// <summary>The total number of tiles.</summary>
    private uint TotalNumberOfTiles => NumberOfTiles.X * NumberOfTiles.Y;

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

        Pipelines = new(this);

        CreateSyncObjects();

        ComputeCommandPool = new(CommandPoolUsage.Compute);
        GraphicsCommandPool = new(CommandPoolUsage.Graphics);

        CreateBuffers();
        CreateDescriptorSets();
        CreateComputeCommandBuffers();

        GenerateFrustums();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <inheritdoc/>
    public override void OnResolutionChange() => RecreateSwapchain();

    // TODO: clean up this method
    /// <inheritdoc/>
    public override void Render(IEnumerable<RendererGameObjectBase> gameObjects, bool presentRenderTarget)
    {
        var vulkanGameObjects = gameObjects.Cast<VulkanGameObject>();
        var triangleVulkanGameObjects = vulkanGameObjects.Where(vulkanGameObject => vulkanGameObject.MeshType == MeshType.TriangleList).ToList();
        var lineVulkanGameObjects = vulkanGameObjects.Where(vulkanGameObject => vulkanGameObject.MeshType == MeshType.LineList).ToList();

        foreach (var vulkanGameObject in vulkanGameObjects)
            vulkanGameObject.UpdateUBO(this.BaseCamera);

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
        var commandBuffer = GraphicsCommandPool.AllocateCommandBuffer(true, VkCommandBufferUsageFlags.OneTimeSubmit);
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

            if (triangleVulkanGameObjects.Count > 0)
            {
                VK.CommandBindPipeline(commandBuffer, VkPipelineBindPoint.Graphics, Pipelines.TriangleGraphicsPipeline);
                foreach (var triangleVulkanGameObject in triangleVulkanGameObjects)
                {
                    var gameObjectPosition = triangleVulkanGameObject.BaseGameObject.Transform.GlobalPosition;
                    var gameObjectMaterial = triangleVulkanGameObject.BaseGameObject.Components.MeshRenderer!.Material;
                    var vulkanMaterial = new VulkanMaterial(new(gameObjectMaterial.Tint.R / 255f, gameObjectMaterial.Tint.G / 255f, gameObjectMaterial.Tint.B / 255f), gameObjectMaterial.Roughness, gameObjectMaterial.Metallicness);
                    VK.CommandPushConstants(commandBuffer, Pipelines.TriangleGraphicsPipelineLayout, VkShaderStageFlags.Vertex, 0, (uint)sizeof(Vector3), &gameObjectPosition);
                    VK.CommandPushConstants(commandBuffer, Pipelines.TriangleGraphicsPipelineLayout, VkShaderStageFlags.Fragment, (uint)sizeof(Vector3), (uint)sizeof(VulkanMaterial), &vulkanMaterial);

                    VK.CommandBindDescriptorSets(commandBuffer, VkPipelineBindPoint.Graphics, Pipelines.TriangleGraphicsPipelineLayout, 0, 1, new[] { triangleVulkanGameObject.DescriptorSet.NativeDescriptorSet }, 0, null);
                    var vertexBuffer = triangleVulkanGameObject.VertexBuffer!.NativeBuffer;
                    var offsets = (VkDeviceSize)0;
                    VK.CommandBindVertexBuffers(commandBuffer, 0, 1, ref vertexBuffer, &offsets);
                    VK.CommandBindIndexBuffer(commandBuffer, triangleVulkanGameObject.IndexBuffer!.NativeBuffer, 0, VkIndexType.Uint32);
                    VK.CommandDrawIndexed(commandBuffer, (uint)triangleVulkanGameObject.IndexCount, 1, 0, 0, 0);
                }
            }

            if (lineVulkanGameObjects.Count > 0)
            {
                VK.CommandBindPipeline(commandBuffer, VkPipelineBindPoint.Graphics, Pipelines.LineGraphicsPipeline);
                foreach (var lineVulkanGameObject in lineVulkanGameObjects)
                {
                    var position = Vector3.Zero;
                    var material = lineVulkanGameObject.BaseGameObject.Components.MeshRenderer!.Material;
                    var colour = new Vector3(material.Tint.R / 255f, material.Tint.G / 255f, material.Tint.B / 255f);
                    VK.CommandPushConstants(commandBuffer, Pipelines.LineGraphicsPipelineLayout, VkShaderStageFlags.Fragment, 0, (uint)sizeof(Vector3), &colour);

                    VK.CommandBindDescriptorSets(commandBuffer, VkPipelineBindPoint.Graphics, Pipelines.LineGraphicsPipelineLayout, 0, 1, new[] { lineVulkanGameObject.DescriptorSet.NativeDescriptorSet }, 0, null);
                    var vertexBuffer = lineVulkanGameObject.VertexBuffer!.NativeBuffer;
                    var offsets = (VkDeviceSize)0;
                    VK.CommandBindVertexBuffers(commandBuffer, 0, 1, ref vertexBuffer, &offsets);
                    VK.CommandBindIndexBuffer(commandBuffer, lineVulkanGameObject.IndexBuffer!.NativeBuffer, 0, VkIndexType.Uint32);
                    VK.CommandDrawIndexed(commandBuffer, (uint)lineVulkanGameObject.IndexCount, 1, 0, 0, 0);
                }
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
        GraphicsCommandPool.FreeCommandBuffers(new[] { commandBuffer });
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

        ComputeCommandPool.Dispose();
        GraphicsCommandPool.Dispose();

        ParametersBuffer.Dispose();
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

        Pipelines = new(this); // TODO: use dynamic state instead of recreating the entire pipeline

        FrustumsBuffer = new VulkanBuffer(sizeof(Frustum) * TotalNumberOfTiles, VkBufferUsageFlags.StorageBuffer, VkMemoryPropertyFlags.DeviceLocal);

        CreateDescriptorSets();
        CreateComputeCommandBuffers();
        GenerateFrustums();
    }

    /// <summary>Cleans up the swapchain resources.</summary>
    private void CleanUpSwapchain()
    {
        VK.DeviceWaitIdle(VulkanRenderer.Instance.Device.NativeDevice);

        ComputeCommandPool.FreeCommandBuffers(new[] { GenerateFrustumsCommandBuffer });

        Pipelines.Dispose();
        VK.DestroyRenderPass(VulkanRenderer.Instance.Device.NativeDevice, NativeRenderPass, null);
        Swapchain.Dispose();

        FrustumsBuffer.Dispose();
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

    /// <summary>Creates the buffers required for the tiled rendering.</summary>
    /// <exception cref="ApplicationException">Thrown if a buffer couldn't be created.</exception>
    private void CreateBuffers()
    {
        ParametersBuffer = new VulkanBuffer(sizeof(ParametersUBO), VkBufferUsageFlags.UniformBuffer | VkBufferUsageFlags.TransferDestination, VkMemoryPropertyFlags.DeviceLocal);
        FrustumsBuffer = new VulkanBuffer(sizeof(Frustum) * TotalNumberOfTiles, VkBufferUsageFlags.StorageBuffer, VkMemoryPropertyFlags.DeviceLocal);
    }

    /// <summary>Creates the descriptor sets required for the tiled rendering.</summary>
    private void CreateDescriptorSets()
    {
        // generate frustums
        {
            GenerateFrustumsDescriptorSet = VulkanRenderer.Instance.GenerateFrustumsDescriptorPool.GetDescriptorSet();

            var bufferInfo1 = new VkDescriptorBufferInfo() { Buffer = ParametersBuffer.NativeBuffer, Offset = 0, Range = ParametersBuffer.Size };
            var bufferInfo2 = new VkDescriptorBufferInfo() { Buffer = FrustumsBuffer.NativeBuffer, Offset = 0, Range = FrustumsBuffer.Size };

            GenerateFrustumsDescriptorSet
                .Bind(0, &bufferInfo1, VkDescriptorType.UniformBuffer)
                .Bind(1, &bufferInfo2, VkDescriptorType.StorageBuffer)
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
    }

    /// <summary>Generates the tile frustums.</summary>
    private void GenerateFrustums()
    {
        // update parameters
        var parameters = new ParametersUBO() { InverseProjection = BaseCamera.ProjectionMatrix.Inverse, ScreenResolution = BaseCamera.Resolution };
        ParametersBuffer.CopyFrom(parameters);

        // generate frustums
        var fenceCreateInfo = new VkFenceCreateInfo()
        {
            SType = VkStructureType.FenceCreateInfo,
        };

        // TODO: don't create a fence each time
        if (VK.CreateFence(VulkanRenderer.Instance.Device.NativeDevice, ref fenceCreateInfo, null, out var fence) != VkResult.Success)
            throw new ApplicationException("Failed to create fence.").Log(LogSeverity.Fatal);

        var furstumsCommandBuffer = GenerateFrustumsCommandBuffer;
        var submitInfo = new VkSubmitInfo()
        {
            SType = VkStructureType.SubmitInfo,
            CommandBufferCount = 1,
            CommandBuffers = &furstumsCommandBuffer
        };

        VK.QueueSubmit(VulkanRenderer.Instance.Device.ComputeQueue, 1, new[] { submitInfo }, fence);
        VK.WaitForFences(VulkanRenderer.Instance.Device.NativeDevice, 1, new[] { fence }, true, long.MaxValue);

        VK.DestroyFence(VulkanRenderer.Instance.Device.NativeDevice, fence, null);
    }
}
