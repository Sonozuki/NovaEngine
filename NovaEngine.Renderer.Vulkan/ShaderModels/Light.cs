namespace NovaEngine.Renderer.Vulkan.ShaderModels;

/// <summary>Represents a light.</summary>
internal struct Light
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the light in world space (for point and spot lights).</summary>
    public Vector4 PositionWorldSpace;

    /// <summary>The direction of the light in world space (for spot and directional lights).</summary>
    public Vector4 DirectionWorldSpace;

    /// <summary>The position of the light in view space (for point and spot lights).</summary>
    public Vector4 PositionViewSpace;

    /// <summary>The direction of the light in view space (for spot and directional lights).</summary>
    public Vector4 DirectionViewSpace;

    /// <summary>The colour of the light.</summary>
    public Vector4 Colour;

    /// <summary>The half angle of the spotlight cone.</summary>
    public float SpotlightAngle;

    /// <summary>The range of the light.</summary>
    public float Range;

    /// <summary>The intensity of the light.</summary>
    public float Intensity;

    /// <summary>The type of the light.</summary>
    public uint Type;

    /// <summary>Whether the light is enabled.</summary>
    public bool IsEnabled;

    /// <summary>Unused.</summary>
    private Vector3 Padding;
}
