namespace NovaEngine.Graphics;

/// <summary>The type of pixel the underlying data is stored as for a texture.</summary>
public enum TexturePixelType
{
    /// <summary>The texture stores pixel data in <see cref="Colour"/>s (each pixel channel is a <see langword="byte"/>).</summary>
    Byte,

    /// <summary>The texture stores pixel data in <see cref="Colour32"/>s (each pixel channel is a <see langword="float"/>).</summary>
    Float
}
