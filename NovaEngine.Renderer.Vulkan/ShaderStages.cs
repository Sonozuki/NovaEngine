namespace NovaEngine.Renderer.Vulkan;

/// <summary>A structure used to store shader stages for the graphics and compute pipelines.</summary>
internal unsafe static class ShaderStages
{
    /*********
    ** Fields
    *********/
    /// <summary>A pointer to a <see langword="string"/> containing "main", specifying the name of a shader entry point.</summary>
    private static readonly IntPtr ShaderEntryPointNamePointer = Marshal.StringToHGlobalAnsi("main");

    /// <summary>The created shader modules, stored for destroying.</summary>
    private static readonly List<VkShaderModule> ShaderModules = new();


    /*********
    ** Properties
    *********/
    /// <summary>The shader stage for the generate frustums shader.</summary>
    public static VkPipelineShaderStageCreateInfo GenerateFrustumsShader = LoadShader("Shaders/generate_frustums_comp", VkShaderStageFlags.Compute);

    /// <summary>The shader stage for the depth pre-pass shader.</summary>
    public static VkPipelineShaderStageCreateInfo DepthShader = LoadShader("Shaders/depth_vert", VkShaderStageFlags.Vertex);

    /// <summary>The shader stage for the cull lights shader.</summary>
    public static VkPipelineShaderStageCreateInfo CullLightsShader = LoadShader("Shaders/cull_lights_comp", VkShaderStageFlags.Compute);

    /// <summary>The shader stage for the PBR vertex shader.</summary>
    public static VkPipelineShaderStageCreateInfo PBRVertexShader = LoadShader("Shaders/pbr_vert", VkShaderStageFlags.Vertex);

    /// <summary>The shader stage for the PBR fragment shader.</summary>
    public static VkPipelineShaderStageCreateInfo PBRFragmentShader = LoadShader("Shaders/pbr_frag", VkShaderStageFlags.Fragment);

    /// <summary>The shader stage for the solid colour vertex shader.</summary>
    public static VkPipelineShaderStageCreateInfo SolidColourVertexShader = LoadShader("Shaders/solid_colour_vert", VkShaderStageFlags.Vertex);

    /// <summary>The shader stage for the solid colour fragment shader.</summary>
    public static VkPipelineShaderStageCreateInfo SolidColourFragmentShader = LoadShader("Shaders/solid_colour_frag", VkShaderStageFlags.Fragment);

    /// <summary>The shader stage for the MTSDF text vertex shader.</summary>
    public static VkPipelineShaderStageCreateInfo MTSDFTextVertexShader = LoadShader("Shaders/mtsdf_text_vert", VkShaderStageFlags.Vertex);

    /// <summary>The shader stage for the MTSDF text fragment shader.</summary>
    public static VkPipelineShaderStageCreateInfo MTSDFTextFragmentShader = LoadShader("Shaders/mtsdf_text_frag", VkShaderStageFlags.Fragment);


    /*********
    ** Public Methods
    *********/
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
        var fileData = new Span<byte>(Content.Load<byte[]>(path));
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
                throw new VulkanException($"Failed to create shader module: {path}.").Log(LogSeverity.Fatal);
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
