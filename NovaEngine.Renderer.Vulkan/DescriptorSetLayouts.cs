using System;
using Vulkan;

namespace NovaEngine.Renderer.Vulkan;

/// <summary>A store for all descriptor set layouts for the graphics and compute pipelines.</summary>
internal unsafe class DescriptorSetLayouts : IDisposable
{
    /*********
    ** Accessors
    *********/
    /// <summary>The descriptor set layout for the generate frustums compute shader.</summary>
    public VkDescriptorSetLayout GenerateFrustumsDescriptorSetLayout;

    /// <summary>The descriptor set layout for the depth pre-pass shader.</summary>
    public VkDescriptorSetLayout DepthPrepassDescriptorSetLayout;

    /// <summary>The descriptor set layout for the final rendering shaders.</summary>
    public VkDescriptorSetLayout RenderingDescriptorSetLayout;


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public void Dispose()
    {
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, GenerateFrustumsDescriptorSetLayout, null);
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, DepthPrepassDescriptorSetLayout, null);
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, RenderingDescriptorSetLayout, null);
    }
}
