namespace NovaEngine.Renderer.Vulkan;

/// <summary>A structure used to store shader stages for the graphics and compute pipelines.</summary>
internal unsafe static class ShaderStages
{
    /*********
    ** Fields
    *********/
    /// <summary>A pointer to a <see langword="string"/> containing "main", specifying the name of a shader entry point.</summary>
    private static IntPtr ShaderEntryPointNamePointer;

    /// <summary>The created shader modules, stored for destroying.</summary>
    private static readonly List<VkShaderModule> ShaderModules = new();


    /*********
    ** Accessors
    *********/
    /// <summary>The shader stage for the generate frustums shader.</summary>
    public static VkPipelineShaderStageCreateInfo GenerateFrustumsShader;

    /// <summary>The shader stage for the depth pre-pass shader.</summary>
    public static VkPipelineShaderStageCreateInfo DepthShader;

    /// <summary>The shader stage for the cull lights shader.</summary>
    public static VkPipelineShaderStageCreateInfo CullLightsShader;

    /// <summary>The shader stage for the PBR vertex shader.</summary>
    public static VkPipelineShaderStageCreateInfo PBRVertexShader;

    /// <summary>The shader stage for the PBR fragment shader.</summary>
    public static VkPipelineShaderStageCreateInfo PBRFragmentShader;

    /// <summary>The shader stage for the solid colour vertex shader.</summary>
    public static VkPipelineShaderStageCreateInfo SolidColourVertexShader;

    /// <summary>The shader stage for the solid colour fragment shader.</summary>
    public static VkPipelineShaderStageCreateInfo SolidColourFragmentShader;

    /// <summary>The shader stage for the MTSDF text vertex shader.</summary>
    public static VkPipelineShaderStageCreateInfo MTSDFTextVertexShader;

    /// <summary>The shader stage for the MTSDF text fragment shader.</summary>
    public static VkPipelineShaderStageCreateInfo MTSDFTextFragmentShader;


    /*********
    ** Public Methods
    *********/
    static ShaderStages()
    {
        ShaderEntryPointNamePointer = Marshal.StringToHGlobalAnsi("main");

        GenerateFrustumsShader = LoadShader("Shaders/generate_frustums_comp", VkShaderStageFlags.Compute);
        DepthShader = LoadShader("Shaders/depth_vert", VkShaderStageFlags.Vertex);
        CullLightsShader = LoadShader("Shaders/cull_lights_comp", VkShaderStageFlags.Compute);
        PBRVertexShader = LoadShader("Shaders/pbr_vert", VkShaderStageFlags.Vertex);
        PBRFragmentShader = LoadShader("Shaders/pbr_frag", VkShaderStageFlags.Fragment);
        SolidColourVertexShader = LoadShader("Shaders/solid_colour_vert", VkShaderStageFlags.Vertex);
        SolidColourFragmentShader = LoadShader("Shaders/solid_colour_frag", VkShaderStageFlags.Fragment);
        MTSDFTextVertexShader = LoadShader("Shaders/mtsdf_text_vert", VkShaderStageFlags.Vertex);
        MTSDFTextFragmentShader = LoadShader("Shaders/mtsdf_text_frag", VkShaderStageFlags.Fragment);
    }

    /// <summary>Disposes unmanaged shader stage resources.</summary>
    public static void Dispose()
    {
        if (ShaderEntryPointNamePointer != IntPtr.Zero)
            Marshal.FreeHGlobal(ShaderEntryPointNamePointer);

        foreach (var shaderModule in ShaderModules)
            VK.DestroyShaderModule(VulkanRenderer.Instance.Device.NativeDevice, shaderModule, null);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Loads a shader.</summary>
    /// <param name="path">The path to the compiled shader file.</param>
    /// <param name="stage">The stage the shader will be used at.</param>
    private static VkPipelineShaderStageCreateInfo LoadShader(string path, VkShaderStageFlags stage)
    {
        // create shader module
        var fileData = new Span<byte>(ContentLoader.Load<byte[]>(path));
        var shaderData = MemoryMarshal.Cast<byte, uint>(fileData);

        VkShaderModule shaderModule;
        fixed (uint* shaderDataPointer = &shaderData.GetPinnableReference())
        {
            var shaderModuleCreateInfo = new VkShaderModuleCreateInfo
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
