using Vulkan;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>A structure used to store shader stages for the graphics and compute pipelines.</summary>
internal struct ShaderStages
{
    /*********
    ** Fields
    *********/
    /// <summary>The shader stage for the vertex shader.</summary>
    public VkPipelineShaderStageCreateInfo VertexShader;

    /// <summary>The shader stage for the fragment shader.</summary>
    public VkPipelineShaderStageCreateInfo FragmentShader;
}
