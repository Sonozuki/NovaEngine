namespace NovaEngine.Components;

/// <summary>How the border of a MTSDF should be rendered.</summary>
public enum MTSDFBorderType
{
    /// <summary>The MTSDF has no border.</summary>
    None,

    /// <summary>The MTSDF has a solid colour border.</summary>
    Colour,

    /// <summary>The MTSDF has a textured border.</summary>
    Texture
}