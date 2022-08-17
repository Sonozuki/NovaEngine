namespace NovaEngine.Graphics;

/// <summary>The uses of a <see cref="TextureBase"/>.</summary>
public enum TextureUsage
{
    /// <summary>Texture will be used as a colour texture that is samplable from a shader, with each channel being a <see langword="byte"/>.</summary>
    Colour,

    /// <summary>Texture will be used as a colour texture that is samplable from a shader, with each channel being a <see langword="float"/>.</summary>
    Colour32,

    /// <summary>Texture will be used as a depth texture.</summary>
    Depth
}
