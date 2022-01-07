using Vulkan;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>A structure used to store shader stages for the graphics and compute pipelines.</summary>
internal struct ShaderStages
{
    /*********
    ** Fields
    *********/
    /// <summary>The shader stage for the generate frustums compute shader.</summary>
    public VkPipelineShaderStageCreateInfo GenerateFrustumsShader;

    /// <summary>The shader stage for the PBR vertex shader.</summary>
    public VkPipelineShaderStageCreateInfo PBRVertexShader;

    /// <summary>The shader stage for the PBR fragment shader.</summary>
    public VkPipelineShaderStageCreateInfo PBRFragmentShader;

    /// <summary>The shader stage for the solid colour vertex shader.</summary>
    public VkPipelineShaderStageCreateInfo SolidColourVertexShader;

    /// <summary>The shader stage for the solid colour fragment shader.</summary>
    public VkPipelineShaderStageCreateInfo SolidColourFragmentShader;
}
