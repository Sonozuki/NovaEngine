namespace NovaEngine.Graphics;

/// <summary>The types of a <see cref="TextureBase"/>.</summary>
public enum TextureType
{
    /// <summary>A one-dimensional texture.</summary>
    Texture1D,

    /// <summary>A one-dimensional array texture.</summary>
    Texture1DArray,

    /// <summary>A two-dimensional texture.</summary>
    Texture2D,

    /// <summary>A two-dimensional array texture.</summary>
    Texture2DArray,

    /// <summary>A three-dimensional texture.</summary>
    Texture3D,

    /// <summary>A cube map texture.</summary>
    CubeMap,

    /// <summary>A cube map array texture.</summary>
    CubeMapArray
}
