namespace NovaEngine.Renderer.Vulkan;

/// <summary>The material that'll get passed to a shader.</summary>
internal struct VulkanMaterial
{
    /*********
    ** Fields
    *********/
    /// <summary>The tint of the material.</summary>
    public Vector3<float> Tint;

    /// <summary>The roughness of the material.</summary>
    public float Roughness;

    /// <summary>The metallicness of the material.</summary>
    public float Metallicness;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="tint">The tint of the material.</param>
    /// <param name="roughness">The roughness of the material.</param>
    /// <param name="metallicness">The metallicness of the material.</param>
    public VulkanMaterial(Vector3<float> tint, float roughness, float metallicness)
    {
        Tint = tint;
        Roughness = roughness;
        Metallicness = metallicness;
    }
}
