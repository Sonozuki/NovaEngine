namespace NovaEngine.Renderer.Vulkan;

/// <summary>A store for all descriptor pools.</summary>
internal static class DescriptorPools
{
    /*********
    ** Accessors
    *********/
    /// <summary>The descriptor pool for the generate frustums shader.</summary>
    public static VulkanDescriptorPool GenerateFrustumsDescriptorPool { get; }

    /// <summary>The descriptor set layout for the depth pre-pass shader.</summary>
    public static VulkanDescriptorPool DepthPrepassDescriptorPool { get; }

    /// <summary>The descriptor set layout for the light culling shader.</summary>
    public static VulkanDescriptorPool CullLightsDescriptorPool { get; }

    /// <summary>The descriptor set layout for the pbr shaders.</summary>
    public static VulkanDescriptorPool PBRDescriptorPool { get; }

    /// <summary>The descriptor set layout for the solid colour shaders.</summary>
    public static VulkanDescriptorPool SolidColourDescriptorPool { get; }

    /// <summary>The descriptor set layout for the user interface shaders.</summary>
    public static VulkanDescriptorPool MTSDFTextDescriptorPool { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Initialises the class.</summary>
    static DescriptorPools()
    {
        GenerateFrustumsDescriptorPool = new(DescriptorSetLayouts.GenerateFrustumsDescriptorSetLayout);
        DepthPrepassDescriptorPool = new(DescriptorSetLayouts.DepthPrepassDescriptorSetLayout);
        CullLightsDescriptorPool = new(DescriptorSetLayouts.CulLightsDescriptorSetLayout);
        PBRDescriptorPool = new(DescriptorSetLayouts.PBRDescriptorSetLayout);
        SolidColourDescriptorPool = new(DescriptorSetLayouts.SolidColourDescriptorSetLayout);
        MTSDFTextDescriptorPool = new(DescriptorSetLayouts.MTSDFTextDescriptorSetLayout);
    }

    /// <summary>Disposes unmanaged descriptor pool resources.</summary>
    public static void Dispose()
    {
        GenerateFrustumsDescriptorPool.Dispose();
        DepthPrepassDescriptorPool.Dispose();
        CullLightsDescriptorPool.Dispose();
        PBRDescriptorPool.Dispose();
        SolidColourDescriptorPool.Dispose();
        MTSDFTextDescriptorPool.Dispose();
    }
}
