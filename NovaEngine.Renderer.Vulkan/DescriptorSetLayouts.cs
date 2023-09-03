namespace NovaEngine.Renderer.Vulkan;

/// <summary>A store for all descriptor set layouts.</summary>
internal unsafe static class DescriptorSetLayouts
{
    /*********
    ** Properties
    *********/
    /// <summary>The descriptor set layout for the generate frustums shader.</summary>
    public static VkDescriptorSetLayout GenerateFrustumsDescriptorSetLayout { get; }

    /// <summary>The descriptor set layout for the depth pre-pass shader.</summary>
    public static VkDescriptorSetLayout DepthPrepassDescriptorSetLayout { get; }

    /// <summary>The descriptor set layout for the light culling shader.</summary>
    public static VkDescriptorSetLayout CulLightsDescriptorSetLayout { get; }

    /// <summary>The descriptor set layout for the pbr shaders.</summary>
    public static VkDescriptorSetLayout PBRDescriptorSetLayout { get; }

    /// <summary>The descriptor set layout for the solid colour shaders.</summary>
    public static VkDescriptorSetLayout SolidColourDescriptorSetLayout { get; }

    /// <summary>The descriptor set layout for the MTSDF text shaders.</summary>
    public static VkDescriptorSetLayout MTSDFTextDescriptorSetLayout { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises the class.</summary>
    static DescriptorSetLayouts()
    {
        // generate frustums
        var bindings = new VkDescriptorSetLayoutBinding[]
        {
            new() { Binding = 0, DescriptorType = VkDescriptorType.UniformBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // parameters buffer
            new() { Binding = 1, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute } // frustums buffer
        };
        GenerateFrustumsDescriptorSetLayout = CreateDescriptorSetLayout(bindings);

        // depth pre-pass
        var depthPrepassBindings = new VkDescriptorSetLayoutBinding[]
        {
            new() { Binding = 0, DescriptorType = VkDescriptorType.UniformBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Vertex } // mvp uniform
        };
        DepthPrepassDescriptorSetLayout = CreateDescriptorSetLayout(depthPrepassBindings);

        // cull lights
        var cullLightsBindings = new VkDescriptorSetLayoutBinding[]
        {
            new() { Binding = 0, DescriptorType = VkDescriptorType.UniformBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // parameters buffer
            new() { Binding = 1, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // frustums buffer
            new() { Binding = 2, DescriptorType = VkDescriptorType.CombinedImageSampler, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // depth image
            new() { Binding = 3, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // lights buffer
            new() { Binding = 4, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // opaque light index counter buffer
            new() { Binding = 5, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // transparent light index counter buffer
            new() { Binding = 6, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // opaque light index list buffer
            new() { Binding = 7, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // transparent light index light buffer
            new() { Binding = 8, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute }, // opaque light grid buffer
            new() { Binding = 9, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Compute } // transparent light grid buffer
        };
        CulLightsDescriptorSetLayout = CreateDescriptorSetLayout(cullLightsBindings);

        // pbr
        var pbrBindings = new VkDescriptorSetLayoutBinding[]
        {
            new() { Binding = 0, DescriptorType = VkDescriptorType.UniformBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Vertex | VkShaderStageFlags.Fragment }, // mvp buffer
            new() { Binding = 1, DescriptorType = VkDescriptorType.UniformBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Fragment }, // parameters buffer
            new() { Binding = 2, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Fragment }, // lights buffer
            new() { Binding = 3, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Fragment }, // opaque light index list buffer
            new() { Binding = 4, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Fragment }, // transparent light index list buffer
            new() { Binding = 5, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Fragment }, // opaque light grid buffer
            new() { Binding = 6, DescriptorType = VkDescriptorType.StorageBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Fragment } // transparent light grid buffer
        };
        PBRDescriptorSetLayout = CreateDescriptorSetLayout(pbrBindings);

        // solid colour
        var solidColourBindings = new VkDescriptorSetLayoutBinding[]
        {
            new() { Binding = 0, DescriptorType = VkDescriptorType.UniformBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Vertex } // mvp uniform
        };
        SolidColourDescriptorSetLayout = CreateDescriptorSetLayout(solidColourBindings);

        // mtsdf text
        var mtsdfTextBindings = new VkDescriptorSetLayoutBinding[]
        {
            new() { Binding = 0, DescriptorType = VkDescriptorType.UniformBuffer, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Vertex }, // mvp uniform
            new() { Binding = 1, DescriptorType = VkDescriptorType.CombinedImageSampler, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Fragment }, // font atlas
            new() { Binding = 2, DescriptorType = VkDescriptorType.CombinedImageSampler, DescriptorCount = 1, StageFlags = VkShaderStageFlags.Fragment } // border texture
        };
        MTSDFTextDescriptorSetLayout = CreateDescriptorSetLayout(mtsdfTextBindings);
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Disposes unmanaged descriptor set layout resources.</summary>
    public static void Dispose()
    {
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, GenerateFrustumsDescriptorSetLayout, null);
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, DepthPrepassDescriptorSetLayout, null);
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, CulLightsDescriptorSetLayout, null);
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, PBRDescriptorSetLayout, null);
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, SolidColourDescriptorSetLayout, null);
        VK.DestroyDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, MTSDFTextDescriptorSetLayout, null);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates a descriptor set layout.</summary>
    /// <param name="bindings">The bindings in the descriptor set layout.</param>
    private static VkDescriptorSetLayout CreateDescriptorSetLayout(VkDescriptorSetLayoutBinding[]? bindings)
    {
        bindings ??= Array.Empty<VkDescriptorSetLayoutBinding>();

        fixed (VkDescriptorSetLayoutBinding* bindingsPointer = bindings)
        {
            var descriptorSetLayoutCreateInfo = new VkDescriptorSetLayoutCreateInfo
            {
                SType = VkStructureType.DescriptorSetLayoutCreateInfo,
                BindingCount = (uint)bindings.Length,
                Bindings = bindingsPointer
            };

            if (!VK.CreateDescriptorSetLayout(VulkanRenderer.Instance.Device.NativeDevice, ref descriptorSetLayoutCreateInfo, null, out var descriptorSetLayout, out var result))
                throw new VulkanException($"Failed to create descriptor set layout. \"{result}\"").Log(LogSeverity.Fatal);
            return descriptorSetLayout;
        }
    }
}
