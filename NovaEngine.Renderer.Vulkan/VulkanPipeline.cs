using NovaEngine.Content;
using NovaEngine.Settings;
using System;
using System.Runtime.InteropServices;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>A wrapper a graphics pipeline.</summary>
    internal unsafe class VulkanPipeline : IDisposable
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The pipeline layout for <see cref="Pipeline"/>.</summary>
        public VkPipelineLayout PipelineLayout { get; }

        /// <summary>A graphics pipeline using the shaders.</summary>
        public VkPipeline Pipeline { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <exception cref="ApplicationException">Thrown if the pipeline layout or pipeline couldn't be created.</exception>
        public VulkanPipeline()
        {
            var vertexShaderModule = CreateShaderModule("Shaders/shader_vert");
            var fragmentShaderModule = CreateShaderModule("Shaders/shader_frag");

            // create pipeline layout
            var descriptorSetLayout = VulkanRenderer.Instance.NativeDescriptorSetLayout;
            var pipelineLayoutCreateInfo = new VkPipelineLayoutCreateInfo()
            {
                SType = VkStructureType.PipelineLayoutCreateInfo,
                SetLayoutCount = 1,
                SetLayouts = &descriptorSetLayout
            };

            if (VK.CreatePipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, ref pipelineLayoutCreateInfo, null, out var pipelineLayout) != VkResult.Success)
                throw new ApplicationException("Failed to create pipeline layout.");
            PipelineLayout = pipelineLayout;

            // create pipeline
            fixed (VkVertexInputAttributeDescription* vertexAttributeDescriptionsPointer = VulkanUtilities.VertexAttributeDesciptions)
            fixed (VkVertexInputBindingDescription* vertexBindingDescriptionsPointer = VulkanUtilities.VertexBindingDescription)
            {
                // vertex input
                var vertexInputState = new VkPipelineVertexInputStateCreateInfo()
                {
                    SType = VkStructureType.PipelineVertexInputStateCreateInfo,
                    VertexAttributeDescriptionCount = (uint)VulkanUtilities.VertexAttributeDesciptions.Length,
                    VertexAttributeDescriptions = vertexAttributeDescriptionsPointer,
                    VertexBindingDescriptionCount = (uint)VulkanUtilities.VertexBindingDescription.Length,
                    VertexBindingDescriptions = vertexBindingDescriptionsPointer
                };

                // input assembly
                var inputAssemblyState = new VkPipelineInputAssemblyStateCreateInfo()
                {
                    SType = VkStructureType.PipelineInputAssemblyStateCreateInfo,
                    Topology = VkPrimitiveTopology.TriangleList,
                    PrimitiveRestartEnable = false
                };

                // viewport
                var viewport = new VkViewport()
                {
                    X = 0,
                    Y = 0,
                    Width = VulkanRenderer.Instance.Swapchain.Extent.Width,
                    Height = VulkanRenderer.Instance.Swapchain.Extent.Height,
                    MinDepth = 0,
                    MaxDepth = 1
                };

                var scissorRectangle = new VkRect2D(VkOffset2D.Zero, VulkanRenderer.Instance.Swapchain.Extent);

                var viewportState = new VkPipelineViewportStateCreateInfo()
                {
                    SType = VkStructureType.PipelineViewportStateCreateInfo,
                    ViewportCount = 1,
                    Viewports = &viewport,
                    ScissorCount = 1,
                    Scissors = &scissorRectangle
                };

                // rasterisation
                var rasterisationState = new VkPipelineRasterizationStateCreateInfo()
                {
                    SType = VkStructureType.PipelineRasterizationStateCreateInfo,
                    DepthClampEnable = false,
                    RasterizerDiscardEnable = false,
                    PolygonMode = VkPolygonMode.Fill,
                    LineWidth = 1,
                    CullMode = VkCullModeFlags.Back,
                    FrontFace = VkFrontFace.CounterClockwise,
                    DepthBiasEnable = false,
                    DepthBiasConstantFactor = 0,
                    DepthBiasClamp = 0,
                    DepthBiasSlopeFactor = 0
                };

                // multisample
                var multisampleState = new VkPipelineMultisampleStateCreateInfo()
                {
                    SType = VkStructureType.PipelineMultisampleStateCreateInfo,
                    SampleShadingEnable = true, // TODO: make this a rendering setting
                    RasterizationSamples = VulkanUtilities.ConvertSampleCount(RenderingSettings.Instance.SampleCount),
                    MinSampleShading = 1,
                    SampleMask = null,
                    AlphaToCoverageEnable = false,
                    AlphaToOneEnable = false
                };

                // depth / stencil
                var depthStencilState = new VkPipelineDepthStencilStateCreateInfo()
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
                var colourBlendAttachmentState = new VkPipelineColorBlendAttachmentState()
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

                var colourBlendState = new VkPipelineColorBlendStateCreateInfo()
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

                var shaderEntryPoint = IntPtr.Zero;

                try
                {
                    shaderEntryPoint = Marshal.StringToHGlobalAnsi("main");

                    var shaderStageCreateInfos = new[]
                    {
                        CreateShaderStage(VkShaderStageFlags.Vertex, vertexShaderModule, shaderEntryPoint),
                        CreateShaderStage(VkShaderStageFlags.Fragment, fragmentShaderModule, shaderEntryPoint)
                    };

                    // create graphics pipeline
                    fixed (VkPipelineShaderStageCreateInfo* shaderStageCreateInfosPointer = shaderStageCreateInfos)
                    {
                        var pipelineCreateInfo = new VkGraphicsPipelineCreateInfo()
                        {
                            SType = VkStructureType.GraphicsPipelineCreateInfo,
                            StageCount = (uint)shaderStageCreateInfos.Length,
                            Stages = shaderStageCreateInfosPointer,
                            VertexInputState = &vertexInputState,
                            InputAssemblyState = &inputAssemblyState,
                            ViewportState = &viewportState,
                            RasterizationState = &rasterisationState,
                            MultisampleState = &multisampleState,
                            DepthStencilState = &depthStencilState,
                            ColorBlendState = &colourBlendState,
                            DynamicState = null,
                            Layout = PipelineLayout,
                            RenderPass = VulkanRenderer.Instance.NativeRenderPass,
                            Subpass = 0
                        };

                        if (VK.CreateGraphicsPipelines(VulkanRenderer.Instance.Device.NativeDevice, VkPipelineCache.Null, 1, new[] { pipelineCreateInfo }, null, out var pipeline) != VkResult.Success)
                            throw new ApplicationException("Failed to create pipeline.");
                        Pipeline = pipeline;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(shaderEntryPoint);
                }

                VK.DestroyShaderModule(VulkanRenderer.Instance.Device.NativeDevice, vertexShaderModule, null);
                VK.DestroyShaderModule(VulkanRenderer.Instance.Device.NativeDevice, fragmentShaderModule, null);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, PipelineLayout, null);
            VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, Pipeline, null);
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Creates a shader module using the compiled shader file at a specified path.</summary>
        /// <param name="path">The path to the compiled shader file.</param>
        /// <returns>The shader module loaded from <paramref name="path"/>.</returns>
        /// <exception cref="ApplicationException">Thrown if the shader module couldn't be created.</exception>
        private static VkShaderModule CreateShaderModule(string path)
        {
            var fileBytes = ContentLoader.Load<byte[]>(path);
            var shaderData = new uint[(int)Math.Ceiling(fileBytes.Length / 4f)];
            Buffer.BlockCopy(fileBytes, 0, shaderData, 0, fileBytes.Length);

            fixed (uint* shaderDataPointer = shaderData)
            {
                var shaderModuleCreateInfo = new VkShaderModuleCreateInfo()
                {
                    SType = VkStructureType.ShaderModuleCreateInfo,
                    CodeSize = (uint)shaderData.Length * sizeof(uint),
                    Code = shaderDataPointer
                };

                if (VK.CreateShaderModule(VulkanRenderer.Instance.Device.NativeDevice, ref shaderModuleCreateInfo, null, out var shaderModule) != VkResult.Success)
                    throw new ApplicationException("Failed to create shader module.");

                return shaderModule;
            }
        }

        /// <summary>Creates a <see cref="VkPipelineShaderStageCreateInfo"/>.</summary>
        /// <param name="stage">The pipeline stage of the shader.</param>
        /// <param name="module">The shader module.</param>
        /// <param name="entryPointName">The entry method of the shader.</param>
        /// <returns>The created <see cref="VkPipelineShaderStageCreateInfo"/>.</returns>
        private static VkPipelineShaderStageCreateInfo CreateShaderStage(VkShaderStageFlags stage, VkShaderModule module, IntPtr entryPointName)
        {
            return new()
            {
                SType = VkStructureType.PipelineShaderStageCreateInfo,
                Stage = stage,
                Module = module,
                Name = (byte*)entryPointName
            };
        }
    }
}
