namespace NovaEngine.External.Rendering;

/// <summary>Represents a renderer texture.</summary>
public abstract class RendererTextureBase : IDisposable
{
    /*********
    ** Properties
    *********/
    /// <summary>The underlying texture.</summary>
    public TextureBase BaseTexture { get; }

    /// <summary>The width of the texture.</summary>
    public uint Width => BaseTexture.Width;

    /// <summary>The height of the texture.</summary>
    public uint Height => BaseTexture._Height;

    /// <summary>The depth of the texture.</summary>
    public uint Depth => BaseTexture._Depth;

    /// <summary>The type of pixel the texture stores underlying data as.</summary>
    public TexturePixelType PixelType => BaseTexture.PixelType;

    /// <summary>Whether a mip chain will be generated for the texture and automatically regenerated when the texture is changed.</summary>
    public bool AutomaticallyGenerateMipChain => BaseTexture.AutomaticallyGenerateMipChain;

    /// <summary>The mip LOD (level of detail) bias of the texture.</summary>
    public float MipLodBias => BaseTexture.MipLodBias;

    /// <summary>Whether the texture has anisotropic filtering enabled.</summary>
    public bool AnisotropicFilteringEnabled => BaseTexture.AnisotropicFilteringEnabled;

    /// <summary>The max anisotropic filtering level of the texture.</summary>
    public float MaxAnisotropicFilteringLevel => BaseTexture.MaxAnisotropicFilteringLevel;

    /// <summary>The number of mip levels the texture has.</summary>
    public uint MipLevels
    {
        get => BaseTexture.MipLevels;
        set => BaseTexture.MipLevels = value;
    }

    /// <summary>The number of layers the texture has.</summary>
    public uint LayerCount => BaseTexture._LayerCount;

    /// <summary>The usage of the texture.</summary>
    public TextureUsage Usage => BaseTexture.Usage;

    /// <summary>The type of the texture.</summary>
    public TextureType Type => BaseTexture.Type;

    /// <summary>The texture wrap mode of the U axis.</summary>
    public TextureWrapMode WrapModeU => BaseTexture.WrapModeU;

    /// <summary>The texture wrap mode of the V axis.</summary>
    public TextureWrapMode WrapModeV => BaseTexture._WrapModeV;

    /// <summary>The texture wrap mode of the W axis.</summary>
    public TextureWrapMode WrapModeW => BaseTexture._WrapModeW;


    /*********
    ** Constructors
    *********/
    /// <summary>Destructs the instance.</summary>
    ~RendererTextureBase() => Dispose(false);

    /// <summary>Constructs an instance.</summary>
    /// <param name="baseTexture">The underlying texture.</param>
    protected RendererTextureBase(TextureBase baseTexture)
    {
        BaseTexture = baseTexture;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves the pixel data of the texture.</summary>
    /// <returns>The pixel data of the texture.</returns>
    public abstract Colour32[] GetPixels();

    /// <summary>Sets pixel data for a specific one-dimensional location.</summary>
    /// <param name="pixels">The pixel data to set.</param>
    /// <param name="offset">The pixel offset for setting pixel data.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pixels.Length"/> + offset goes out of range of the texture.</exception>
    public abstract void SetPixels(Colour[] pixels, int offset = 0);

    /// <summary>Sets pixel data for a specific one-dimensional location.</summary>
    /// <param name="pixels">The pixel data to set.</param>
    /// <param name="offset">The pixel offset for setting pixel data.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pixels.Length"/> + offset goes out of range of the texture.</exception>
    public abstract void SetPixels(Colour32[] pixels, int offset = 0);

    /// <summary>Sets pixel data for a specific two-dimensional location.</summary>
    /// <param name="pixels">The pixel data to set.</param>
    /// <param name="xOffset">The X offset of the pixel data (from left of the texture).</param>
    /// <param name="yOffset">The Y offset of the pixel data (from top of the texture).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pixels.GetLength(n)"/> + (n)Offset goes out of range of the axis length.</exception>
    public abstract void SetPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0);

    /// <summary>Sets pixel data for a specific two-dimensional location.</summary>
    /// <param name="pixels">The pixel data to set.</param>
    /// <param name="xOffset">The X offset of the pixel data (from left of the texture).</param>
    /// <param name="yOffset">The Y offset of the pixel data (from top of the texture).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pixels.GetLength(n)"/> + (n)Offset goes out of range of the axis length.</exception>
    public abstract void SetPixels(Colour32[,] pixels, int xOffset = 0, int yOffset = 0);

    /// <summary>Sets pixel data for a specific three-dimensional location.</summary>
    /// <param name="pixels">The pixel data to set.</param>
    /// <param name="xOffset">The X offset of the pixel data (from left of the texture).</param>
    /// <param name="yOffset">The Y offset of the pixel data (from top of the texture).</param>
    /// <param name="zOffset">The Y offset of the pixel data (from front of the texture).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pixels.GetLength(n)"/> + (n)Offset goes out of range of the axis length.</exception>
    public abstract void SetPixels(Colour[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0);

    /// <summary>Sets pixel data for a specific three-dimensional location.</summary>
    /// <param name="pixels">The pixel data to set.</param>
    /// <param name="xOffset">The X offset of the pixel data (from left of the texture).</param>
    /// <param name="yOffset">The Y offset of the pixel data (from top of the texture).</param>
    /// <param name="zOffset">The Y offset of the pixel data (from front of the texture).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pixels.GetLength(n)"/> + (n)Offset goes out of range of the axis length.</exception>
    public abstract void SetPixels(Colour32[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0);

    /// <summary>Generates the mip chain for the texture.</summary>
    public abstract void GenerateMipChain();

    /// <summary>Cleans up unmanaged resources in the texture.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the texture.</summary>
    /// <param name="disposing">Whether the texture is being disposed deterministically.</param>
    protected abstract void Dispose(bool disposing);
}
