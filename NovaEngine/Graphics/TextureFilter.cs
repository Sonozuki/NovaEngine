namespace NovaEngine.Graphics;

/// <summary>The filters for a <see cref="TextureBase"/>.</summary>
public enum TextureFilter
{
    /// <summary>Point filter (none).</summary>
    Point,

    /// <summary>Bilinear filter (texture samples are averaged).</summary>
    Bilinear

    // TODO: add trilinear
}
