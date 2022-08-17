namespace NovaEngine.Renderer.Vulkan;

/// <summary>Encapsulates all graphics and compute pipelines.</summary>
internal unsafe class VulkanPipelines : IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>The camera that the pipelines belong to.</summary>
    private readonly VulkanCamera Camera;


    /*********
    ** Accessors
    *********/
    /// <summary>The layout of <see cref="GenerateFrustumsPipeline"/>.</summary>
    public VkPipelineLayout GenerateFrustumsPipelineLayout { get; private set; }

    /// <summary>The layout of <see cref="DepthPrepassPipeline"/>.</summary>
    public VkPipelineLayout DepthPrepassPipelineLayout { get; private set; }

    /// <summary>The layout of <see cref="CullLightsPipeline"/>.</summary>
    public VkPipelineLayout CullLightsPipelineLayout { get; private set; }

    /// <summary>The layout of <see cref="PBRTrianglePipeline"/>.</summary>
    public VkPipelineLayout PBRTrianglePipelineLayout { get; private set; }

    /// <summary>The layout of <see cref="PBRLinePipeline"/>.</summary>
    public VkPipelineLayout PBRLinePipelineLayout { get; private set; }

    /// <summary>The layout of <see cref="SolidColourTrianglePipeline"/>.</summary>
    public VkPipelineLayout SolidColourTrianglePipelineLayout { get; private set; }

    /// <summary>The layout of <see cref="SolidColourLinePipeline"/>.</summary>
    public VkPipelineLayout SolidColourLinePipelineLayout { get; private set; }

    /// <summary>The layout of <see cref="MTSDFTextPipeline"/>.</summary>
    public VkPipelineLayout MTSDFTextPipelineLayout { get; private set; }

    /// <summary>The tile frustum generation pipeline.</summary>
    public VkPipeline GenerateFrustumsPipeline { get; private set; }

    /// <summary>The depth pre-pass pipeline.</summary>
    public VkPipeline DepthPrepassPipeline { get; private set; }

    /// <summary>The light culling pipeline.</summary>
    public VkPipeline CullLightsPipeline { get; private set; }

    /// <summary>The pbr pipeline with a topology of <see cref="VkPrimitiveTopology.TriangleList"/>.</summary>
    public VkPipeline PBRTrianglePipeline { get; private set; }

    /// <summary>The pbr pipeline with a topology of <see cref="VkPrimitiveTopology.LineList"/>.</summary>
    public VkPipeline PBRLinePipeline { get; private set; }

    /// <summary>The solid colour pipeline with a topology of <see cref="VkPrimitiveTopology.TriangleList"/>.</summary>
    public VkPipeline SolidColourTrianglePipeline { get; private set; }

    /// <summary>The solid colour pipeline with a topology of <see cref="VkPrimitiveTopology.LineList"/>.</summary>
    public VkPipeline SolidColourLinePipeline { get; private set; }

    /// <summary>The MTSDF text pipeline.</summary>
    public VkPipeline MTSDFTextPipeline { get; private set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="camera">The camera which the pipelines belong to.</param>
    /// <exception cref="ApplicationException">Thrown if the pipeline layout or a pipeline couldn't be created.</exception>
    public VulkanPipelines(VulkanCamera camera)
    {
        Camera = camera;

        CreateComputePipelines();
        CreateGraphicsPipelines();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, GenerateFrustumsPipelineLayout, null);
        VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, DepthPrepassPipelineLayout, null);
        VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, CullLightsPipelineLayout, null);
        VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, PBRTrianglePipelineLayout, null);
        VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, PBRLinePipelineLayout, null);
        VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, SolidColourTrianglePipelineLayout, null);
        VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, SolidColourLinePipelineLayout, null);
        VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, MTSDFTextPipelineLayout, null);

        VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, GenerateFrustumsPipeline, null);
        VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, DepthPrepassPipeline, null);
        VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, CullLightsPipeline, null);
        VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, PBRTrianglePipeline, null);
        VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, PBRLinePipeline, null);
        VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, SolidColourTrianglePipeline, null);
        VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, SolidColourLinePipeline, null);
        VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, MTSDFTextPipeline, null);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates the compute pipelines.</summary>
    /// <exception cref="ApplicationException">Thrown if a pipeline couldn't be created.</exception>
    private void CreateComputePipelines()
    {
        // generate frustums
        {
            // layout
            GenerateFrustumsPipelineLayout = CreatePipelineLayout(null, DescriptorSetLayouts.GenerateFrustumsDescriptorSetLayout);

            // pipeline
            GenerateFrustumsPipeline = CreateComputePipeline(ShaderStages.GenerateFrustumsShader, GenerateFrustumsPipelineLayout);
        }

        // cull lights
        {
            // layout
            CullLightsPipelineLayout = CreatePipelineLayout(null, DescriptorSetLayouts.CulLightsDescriptorSetLayout);

            // pipeline
            CullLightsPipeline = CreateComputePipeline(ShaderStages.CullLightsShader, CullLightsPipelineLayout);
        }
    }

    /// <summary>Creates the graphics pipelines.</summary>
    /// <exception cref="ApplicationException">Thrown if the graphics pipeline layout or a pipeline couldn't be created.</exception>
    private void CreateGraphicsPipelines()
    {
        // depth pre-pass
        {
            // layout
            DepthPrepassPipelineLayout = CreatePipelineLayout(null, DescriptorSetLayouts.DepthPrepassDescriptorSetLayout);

            // pipeline
            DepthPrepassPipeline = CreateGraphicsPipeline(VulkanUtilities.VertexAttributeDesciptions, VulkanUtilities.VertexBindingDescription, new[] { ShaderStages.DepthShader }, DepthPrepassPipelineLayout, VkPrimitiveTopology.TriangleList, SampleCount.Count1, Camera.DepthPrepassRenderPass);
        }

        // pbr
        {
            // layouts
            var pbrPushConstantRanges = new VkPushConstantRange[]
            {
                new() { StageFlags = VkShaderStageFlags.Fragment, Offset = 0, Size = (uint)sizeof(VulkanMaterial) }
            };

            PBRTrianglePipelineLayout = CreatePipelineLayout(pbrPushConstantRanges, DescriptorSetLayouts.PBRDescriptorSetLayout);
            PBRLinePipelineLayout = CreatePipelineLayout(pbrPushConstantRanges, DescriptorSetLayouts.PBRDescriptorSetLayout);

            // pipelines
            var pbrShaderStages = new[] { ShaderStages.PBRVertexShader, ShaderStages.PBRFragmentShader };

            PBRTrianglePipeline = CreateGraphicsPipeline(VulkanUtilities.VertexAttributeDesciptions, VulkanUtilities.VertexBindingDescription, pbrShaderStages, PBRTrianglePipelineLayout, VkPrimitiveTopology.TriangleList, RenderingSettings.Instance.SampleCount, Camera.FinalRenderingRenderPass);
            PBRLinePipeline = CreateGraphicsPipeline(VulkanUtilities.VertexAttributeDesciptions, VulkanUtilities.VertexBindingDescription, pbrShaderStages, PBRLinePipelineLayout, VkPrimitiveTopology.LineList, RenderingSettings.Instance.SampleCount, Camera.FinalRenderingRenderPass);
        }

        // solid colour
        {
            // layouts
            var solidColourPushConstantRanges = new VkPushConstantRange[]
            {
                new() { StageFlags = VkShaderStageFlags.Fragment, Offset = 0, Size = (uint)sizeof(Vector3) },
            };

            SolidColourTrianglePipelineLayout = CreatePipelineLayout(solidColourPushConstantRanges, DescriptorSetLayouts.SolidColourDescriptorSetLayout);
            SolidColourLinePipelineLayout = CreatePipelineLayout(solidColourPushConstantRanges, DescriptorSetLayouts.SolidColourDescriptorSetLayout);

            // pipelines
            var solidColourShaderStages = new[] { ShaderStages.SolidColourVertexShader, ShaderStages.SolidColourFragmentShader };

            SolidColourTrianglePipeline = CreateGraphicsPipeline(VulkanUtilities.VertexAttributeDesciptions, VulkanUtilities.VertexBindingDescription, solidColourShaderStages, SolidColourTrianglePipelineLayout, VkPrimitiveTopology.TriangleList, RenderingSettings.Instance.SampleCount, Camera.FinalRenderingRenderPass);
            SolidColourLinePipeline = CreateGraphicsPipeline(VulkanUtilities.VertexAttributeDesciptions, VulkanUtilities.VertexBindingDescription, solidColourShaderStages, SolidColourLinePipelineLayout, VkPrimitiveTopology.LineList, RenderingSettings.Instance.SampleCount, Camera.FinalRenderingRenderPass);
        }

        // mtsdf
        {
            // layout
            var mtsdfPushConstantRanges = new VkPushConstantRange[]
            {
                new() { StageFlags = VkShaderStageFlags.Fragment, Offset = 0, Size = (uint)sizeof(MTSDFParameters) }
            };

            MTSDFTextPipelineLayout = CreatePipelineLayout(mtsdfPushConstantRanges, DescriptorSetLayouts.MTSDFTextDescriptorSetLayout);

            // pipeline
            var mtsdfColourShaderStages = new[] { ShaderStages.MTSDFTextVertexShader, ShaderStages.MTSDFTextFragmentShader };

            MTSDFTextPipeline = CreateGraphicsPipeline(VulkanUtilities.VertexAttributeDesciptions, VulkanUtilities.VertexBindingDescription, mtsdfColourShaderStages, MTSDFTextPipelineLayout, VkPrimitiveTopology.TriangleList, RenderingSettings.Instance.SampleCount, Camera.FinalRenderingRenderPass, 2);
        }
    }

    /// <summary>Creates a <see cref="VkPipelineLayout"/>.</summary>
    /// <returns>The created <see cref="VkPipelineLayout"/>.</returns>
    private static VkPipelineLayout CreatePipelineLayout(VkPushConstantRange[]? pushConstantRanges, VkDescriptorSetLayout descriptorSetLayout)
    {
        pushConstantRanges ??= Array.Empty<VkPushConstantRange>();

        fixed (VkPushConstantRange* pushConstantRangesPointer = pushConstantRanges)
        {
            var pipelineLayoutCreateInfo = new VkPipelineLayoutCreateInfo
            {
                SType = VkStructureType.PipelineLayoutCreateInfo,
                SetLayoutCount = 1,
                SetLayouts = &descriptorSetLayout,
                PushConstantRangeCount = (uint)pushConstantRanges.Length,
                PushConstantRanges = pushConstantRangesPointer
            };

            if (VK.CreatePipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, ref pipelineLayoutCreateInfo, null, out var pipelineLayout) != VkResult.Success)
                throw new ApplicationException("Failed to create pipeline layout.").Log(LogSeverity.Fatal);
            return pipelineLayout;
        }
    }

    /// <summary>Creates a compute pipeline.</summary>
    /// <param name="shaderStage">The shader stage to use when creating the pipeline.</param>
    /// <param name="layout">The pipeline layout to use when creating the pipeline.</param>
    /// <returns>The created compute pipeline.</returns>
    /// <exception cref="ApplicationException">Thrown if the compute pipeline couldn't be created.</exception>
    private static VkPipeline CreateComputePipeline(VkPipelineShaderStageCreateInfo shaderStage, VkPipelineLayout layout)
    {
        var pipelineCreateInfo = new VkComputePipelineCreateInfo
        {
            SType = VkStructureType.ComputePipelineCreateInfo,
            Stage = shaderStage,
            Layout = layout
        };

        if (VK.CreateComputePipelines(VulkanRenderer.Instance.Device.NativeDevice, VkPipelineCache.Null, 1, new[] { pipelineCreateInfo }, null, out var computePipeline) != VkResult.Success)
            throw new ApplicationException("Failed to create compute pipeline.").Log(LogSeverity.Fatal);
        return computePipeline;
    }

    /// <summary>Creates a graphics pipeline.</summary>
    /// <param name="vertexAttributeDescriptions">The vertex attribute descriptions to use when creating the pipeline.</param>
    /// <param name="vertexBindingDescriptions">The vertex binding descriptions to use when creating the pipeline.</param>
    /// <param name="shaderStages">The shader stages to use when creating the pipeline.</param>
    /// <param name="layout">The layout to use when creating the pipeline.</param>
    /// <param name="topology">The topology of the meshes rendered through the pipeline.</param>
    /// <param name="sampleCount">The sample count to use when creating the pipeline.</param>
    /// <param name="renderPass">The render pass that the pipeline will use.</param>
    /// <param name="subpass">The subpass the pipeline will be build for.</param>
    /// <returns>The created graphics pipeline.</returns>
    private VkPipeline CreateGraphicsPipeline(VkVertexInputAttributeDescription[] vertexAttributeDescriptions, VkVertexInputBindingDescription[] vertexBindingDescriptions, VkPipelineShaderStageCreateInfo[] shaderStages, VkPipelineLayout layout, VkPrimitiveTopology topology, SampleCount sampleCount, VkRenderPass renderPass, uint subpass = 0)
    {
        fixed (VkVertexInputAttributeDescription* vertexAttributeDescriptionsPointer = vertexAttributeDescriptions)
        fixed (VkVertexInputBindingDescription* vertexBindingDescriptionsPointer = vertexBindingDescriptions)
        fixed (VkPipelineShaderStageCreateInfo* shaderStagesPointer = shaderStages)
        {
            // vertex input
            var vertexInputState = new VkPipelineVertexInputStateCreateInfo
            {
                SType = VkStructureType.PipelineVertexInputStateCreateInfo,
                VertexAttributeDescriptionCount = (uint)vertexAttributeDescriptions.Length,
                VertexAttributeDescriptions = vertexAttributeDescriptionsPointer,
                VertexBindingDescriptionCount = (uint)vertexBindingDescriptions.Length,
                VertexBindingDescriptions = vertexBindingDescriptionsPointer
            };

            // input assembly
            var inputAssemblyState = new VkPipelineInputAssemblyStateCreateInfo
            {
                SType = VkStructureType.PipelineInputAssemblyStateCreateInfo,
                Topology = topology,
                PrimitiveRestartEnable = false
            };

            // viewport
            var viewport = new VkViewport
            {
                X = 0,
                Y = 0,
                Width = Camera.Swapchain.Extent.Width,
                Height = Camera.Swapchain.Extent.Height,
                MinDepth = 0,
                MaxDepth = 1
            };

            var scissorRectangle = new VkRect2D(VkOffset2D.Zero, Camera.Swapchain.Extent);

            var viewportState = new VkPipelineViewportStateCreateInfo
            {
                SType = VkStructureType.PipelineViewportStateCreateInfo,
                ViewportCount = 1,
                Viewports = &viewport,
                ScissorCount = 1,
                Scissors = &scissorRectangle
            };

            // rasterisation
            var rasterisationState = new VkPipelineRasterizationStateCreateInfo
            {
                SType = VkStructureType.PipelineRasterizationStateCreateInfo,
                DepthClampEnable = false,
                RasterizerDiscardEnable = false,
                PolygonMode = VkPolygonMode.Fill,
                LineWidth = 1,
                CullMode = VkCullModeFlags.Back,
                FrontFace = VkFrontFace.Clockwise, // mesh data is still wound CCW, Vulkan uses CW because of how the Z axis direction has been flipped
                DepthBiasEnable = false,
                DepthBiasConstantFactor = 0,
                DepthBiasClamp = 0,
                DepthBiasSlopeFactor = 0
            };

            // multisample
            var multisampleState = new VkPipelineMultisampleStateCreateInfo
            {
                SType = VkStructureType.PipelineMultisampleStateCreateInfo,
                SampleShadingEnable = true, // TODO: make this a rendering setting
                RasterizationSamples = VulkanUtilities.ConvertSampleCount(sampleCount),
                MinSampleShading = 1,
                SampleMask = null,
                AlphaToCoverageEnable = false,
                AlphaToOneEnable = false
            };

            // depth / stencil
            var depthStencilState = new VkPipelineDepthStencilStateCreateInfo
            {
                SType = VkStructureType.PipelineDepthStencilStateCreateInfo,
                DepthTestEnable = true,
                DepthWriteEnable = true,
                DepthCompareOp = VkCompareOp.Less,
                DepthBoundsTestEnable = false,
                MinDepthBounds = 0,
                MaxDepthBounds = 1,
                StencilTestEnable = false
            };

            // colour blend
            var colourBlendAttachmentState = new VkPipelineColorBlendAttachmentState
            {
                ColorWriteMask = VkColorComponentFlags.R | VkColorComponentFlags.G | VkColorComponentFlags.B | VkColorComponentFlags.A,
                BlendEnable = false,
                SourceColorBlendFactor = VkBlendFactor.One,
                DestinationColorBlendFactor = VkBlendFactor.Zero,
                ColorBlendOp = VkBlendOp.Add,
                SourceAlphaBlendFactor = VkBlendFactor.One,
                DestinationAlphaBlendFactor = VkBlendFactor.Zero,
                AlphaBlendOp = VkBlendOp.Add
            };

            var colourBlendState = new VkPipelineColorBlendStateCreateInfo
            {
                SType = VkStructureType.PipelineColorBlendStateCreateInfo,
                LogicOpEnable = false,
                LogicOp = VkLogicOp.Copy,
                AttachmentCount = 1,
                Attachments = &colourBlendAttachmentState
            };
            colourBlendState.BlendConstants[0] = 0;
            colourBlendState.BlendConstants[1] = 0;
            colourBlendState.BlendConstants[2] = 0;
            colourBlendState.BlendConstants[3] = 0;

            // pipeline
            var pipelineCreateInfo = new VkGraphicsPipelineCreateInfo
            {
                SType = VkStructureType.GraphicsPipelineCreateInfo,
                StageCount = (uint)shaderStages.Length,
                Stages = shaderStagesPointer,
                VertexInputState = &vertexInputState,
                InputAssemblyState = &inputAssemblyState,
                ViewportState = &viewportState,
                RasterizationState = &rasterisationState,
                MultisampleState = &multisampleState,
                DepthStencilState = &depthStencilState,
                ColorBlendState = &colourBlendState,
                Layout = layout,
                RenderPass = renderPass,
                Subpass = subpass
            };

            if (VK.CreateGraphicsPipelines(VulkanRenderer.Instance.Device.NativeDevice, VkPipelineCache.Null, 1, new[] { pipelineCreateInfo }, null, out var graphicsPipeline) != VkResult.Success)
                throw new ApplicationException("Failed to create graphics pipeline.").Log(LogSeverity.Fatal);
            return graphicsPipeline;
        }
    }
}
