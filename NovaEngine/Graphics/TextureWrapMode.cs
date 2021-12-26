namespace NovaEngine.Graphics;

/// <summary>The wrap modes for a <see cref="TextureBase"/>.</summary>
public enum TextureWrapMode
{
    /// <summary>Tiles the texture, creating a repeating pattern.</summary>
    Repeat,

    /// <summary>Tiles the texture, creating a repeating mirrored pattern.</summary>
    Mirror,

    /// <summary>Clamps the texture to the edge pixel.</summary>
    Clamp
}
