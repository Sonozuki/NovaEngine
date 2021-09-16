using NovaEngine.Content;
using NovaEngine.Extensions;
using NovaEngine.Logging;
using NovaEngine.Settings;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan
{
    /// <summary>Encapsulates all graphics and compute pipelines.</summary>
    internal unsafe class VulkanPipelines : IDisposable
    {
        /*********
        ** Fields
        *********/
        /// <summary>The camera whichthe pipelines belong to.</summary>
        private VulkanCamera Camera;

        /// <summary>A pointer to a <see langword="string"/> containing "main", specifying the name of a shader entry point.</summary>
        private IntPtr ShaderEntryPointNamePointer;

        /// <summary>The created shader modules, stored for destroying.</summary>
        private readonly List<VkShaderModule> ShaderModules = new();

        /// <summary>The created shader stages.</summary>
        private ShaderStages ShaderStages;


        /*********
        ** Accessors
        *********/
        /// <summary>The layout of <see cref="GraphicsPipeline"/>.</summary>
        public VkPipelineLayout GraphicsPipelineLayout { get; private set; }

        /// <summary>The main graphics pipeline.</summary>
        public VkPipeline GraphicsPipeline { get; private set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="camera">The camera which the pipelines belong to.</param>
        /// <exception cref="ApplicationException">Thrown if the pipeline layout or a pipeline couldn't be created.</exception>
        public VulkanPipelines(VulkanCamera camera)
        {
            try
            {
                Camera = camera;

                CreateShaderStages();

                CreateGraphicsPipelines();
                CreateComputePipelines();
            }
            finally
            {
                CleanupShaderModules();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            VK.DestroyPipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, GraphicsPipelineLayout, null);
            VK.DestroyPipeline(VulkanRenderer.Instance.Device.NativeDevice, GraphicsPipeline, null);
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Creates the shader stages.</summary>
        private void CreateShaderStages()
        {
            ShaderEntryPointNamePointer = Marshal.StringToHGlobalAnsi("main");
            ShaderStages.VertexShader = LoadShader("Shaders/shader_vert", VkShaderStageFlags.Vertex);
            ShaderStages.FragmentShader = LoadShader("Shaders/shader_frag", VkShaderStageFlags.Fragment);
        }

        /// <summary>Creates the graphics pipelines.</summary>
        /// <exception cref="ApplicationException">Thrown if the graphics pipeline layout or a pipeline couldn't be created.</exception>
        private void CreateGraphicsPipelines()
        {
            // create pipelines
            fixed (VkVertexInputAttributeDescription* vertexAttributeDescriptionsPointer = VulkanUtilities.VertexAttributeDesciptions)
            fixed (VkVertexInputBindingDescription* vertexBindingDescriptionsPointer = VulkanUtilities.VertexBindingDescription)
            {
                // vertex and fragment shader stages
                var shaderStages = new[] { ShaderStages.VertexShader, ShaderStages.FragmentShader };

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
                    Width = Camera.Swapchain.Extent.Width,
                    Height = Camera.Swapchain.Extent.Height,
                    MinDepth = 0,
                    MaxDepth = 1
                };

                var scissorRectangle = new VkRect2D(VkOffset2D.Zero, Camera.Swapchain.Extent);

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
                
                // create pipeline layout
                var descriptorSetLayout = VulkanRenderer.Instance.NativeDescriptorSetLayout;
                var pipelineLayoutCreateInfo = new VkPipelineLayoutCreateInfo()
                {
                    SType = VkStructureType.PipelineLayoutCreateInfo,
                    SetLayoutCount = 1,
                    SetLayouts = &descriptorSetLayout
                };

                if (VK.CreatePipelineLayout(VulkanRenderer.Instance.Device.NativeDevice, ref pipelineLayoutCreateInfo, null, out var graphicsPipelineLayout) != VkResult.Success)
                    throw new ApplicationException("Failed to create graphics pipeline layout.").Log(LogSeverity.Fatal);
                GraphicsPipelineLayout = graphicsPipelineLayout;

                // create base create info
                var pipelineCreateInfo = new VkGraphicsPipelineCreateInfo()
                {
                    SType = VkStructureType.GraphicsPipelineCreateInfo,
                    VertexInputState = &vertexInputState,
                    InputAssemblyState = &inputAssemblyState,
                    ViewportState = &viewportState,
                    RasterizationState = &rasterisationState,
                    MultisampleState = &multisampleState,
                    DepthStencilState = &depthStencilState,
                    ColorBlendState = &colourBlendState,
                    Layout = GraphicsPipelineLayout,
                    RenderPass = Camera.NativeRenderPass,
                    Subpass = 0
                };

                // create main graphics pipeline
                fixed (VkPipelineShaderStageCreateInfo* shaderStagesPointer = shaderStages)
                {
                    pipelineCreateInfo.StageCount = 2;
                    pipelineCreateInfo.Stages = shaderStagesPointer;

                    if (VK.CreateGraphicsPipelines(VulkanRenderer.Instance.Device.NativeDevice, VkPipelineCache.Null, 1, new[] { pipelineCreateInfo }, null, out var graphicsPipeline) != VkResult.Success)
                        throw new ApplicationException("Failed to create graphics pipeline.").Log(LogSeverity.Fatal);
                    GraphicsPipeline = graphicsPipeline;
                }
            }
        }

        /// <summary>Creates the compute pipelines.</summary>
        /// <exception cref="ApplicationException">Thrown if a pipeline couldn't be created.</exception>
        private void CreateComputePipelines()
        {
            // TODO
        }

        /// <summary>Cleans up the shader modules.</summary>
        private void CleanupShaderModules()
        {
            if (ShaderEntryPointNamePointer != IntPtr.Zero)
                Marshal.FreeHGlobal(ShaderEntryPointNamePointer);

            foreach (var shaderModule in ShaderModules)
                VK.DestroyShaderModule(VulkanRenderer.Instance.Device.NativeDevice, shaderModule, null);
        }

        /// <summary>Loads a shader.</summary>
        /// <param name="path">The path to the compiled shader file.</param>
        /// <param name="stage">The stage the shader will be used at.</param>
        private VkPipelineShaderStageCreateInfo LoadShader(string path, VkShaderStageFlags stage)
        {
            // create shader module
            var fileData = new Span<byte>(ContentLoader.Load<byte[]>(path));
            var shaderData = MemoryMarshal.Cast<byte, uint>(fileData);

            VkShaderModule shaderModule;
            fixed (uint* shaderDataPointer = &shaderData.GetPinnableReference())
            {
                var shaderModuleCreateInfo = new VkShaderModuleCreateInfo()
                {
                    SType = VkStructureType.ShaderModuleCreateInfo,
                    CodeSize = (uint)shaderData.Length * sizeof(uint),
                    Code = shaderDataPointer
                };

                if (VK.CreateShaderModule(VulkanRenderer.Instance.Device.NativeDevice, ref shaderModuleCreateInfo, null, out shaderModule) != VkResult.Success)
                    throw new ApplicationException($"Failed to create shader module: {path}.").Log(LogSeverity.Fatal);
                ShaderModules.Add(shaderModule);
            }

            // create shader stage
            return new()
            {
                SType = VkStructureType.PipelineShaderStageCreateInfo,
                Stage = stage,
                Module = shaderModule,
                Name = (byte*)ShaderEntryPointNamePointer
            };
        }
    }
}
