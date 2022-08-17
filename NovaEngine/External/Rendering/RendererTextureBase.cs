namespace NovaEngine.External.Rendering;

/// <summary>Represents a renderer texture.</summary>
public abstract class RendererTextureBase : IDisposable
{
    /*********
    ** Accessors
    *********/
    /// <summary>The underlying texture.</summary>
    public TextureBase BaseTexture { get; }

    /// <summary>The width of the texture.</summary>
    public uint Width { get; }

    /// <summary>The height of the texture.</summary>
    public uint Height { get; }

    /// <summary>The depth of the texture.</summary>
    public uint Depth { get; }

    /// <summary>The type of pixel the texture stores underlying data as.</summary>
    public TexturePixelType PixelType { get; }

    /// <summary>Whether a mip chain will be generated for the texture and automatically regenerated when the texture is changed.</summary>
    public bool AutomaticallyGenerateMipChain { get; }

    /// <summary>The mip LOD (level of detail) bias of the texture.</summary>
    public float MipLodBias { get; }

    /// <summary>Whether the texture has anisotropic filtering enabled.</summary>
    public bool AnisotropicFilteringEnabled { get; }

    /// <summary>The max anisotropic filtering level of the texture.</summary>
    public float MaxAnisotropicFilteringLevel { get; }

    /// <summary>The number of mip levels the texture has.</summary>
    public uint MipLevels
    {
        get => BaseTexture.MipLevels;
        set => typeof(TextureBase).GetField("_MipLevels", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(BaseTexture, value);
    }

    /// <summary>The number of layers the texture has.</summary>
    public uint LayerCount { get; }

    /// <summary>The usage of the texture.</summary>
    public TextureUsage Usage { get; }

    /// <summary>The type of the texture.</summary>
    public TextureType Type { get; }

    /// <summary>The texture wrap mode of the U axis.</summary>
    public TextureWrapMode WrapModeU { get; }

    /// <summary>The texture wrap mode of the V axis.</summary>
    public TextureWrapMode WrapModeV { get; }

    /// <summary>The texture wrap mode of the W axis.</summary>
    public TextureWrapMode WrapModeW { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="baseTexture">The underlying texture.</param>
    public RendererTextureBase(TextureBase baseTexture)
    {
        BaseTexture = baseTexture;

        // fill in convenience properties. reflection is used instead of changing the accessibility as exposing these would lead to rather confusing Texture types (for example, having a '_Height' and 'Height' members, both of which would be public).
        Width = BaseTexture.Width;
        Height = (uint?)typeof(TextureBase).GetField("_Height", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(BaseTexture) ?? throw new MissingMemberException("Couldn't find '_Height' field.");
        Depth = (uint?)typeof(TextureBase).GetField("_Depth", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(BaseTexture) ?? throw new MissingMemberException("Couldn't find '_Depth' field.");
        PixelType = BaseTexture.PixelType;
        AutomaticallyGenerateMipChain = BaseTexture.AutomaticallyGenerateMipChain;
        MipLodBias = BaseTexture.MipLodBias;
        AnisotropicFilteringEnabled = BaseTexture.AnisotropicFilteringEnabled;
        MaxAnisotropicFilteringLevel = BaseTexture.MaxAnisotropicFilteringLevel;
        LayerCount = (uint?)typeof(TextureBase).GetField("_LayerCount", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(BaseTexture) ?? throw new MissingMemberException("Couldn't find '_LayerCount' field.");
        Usage = (TextureUsage?)typeof(TextureBase).GetProperty("Usage", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(BaseTexture) ?? throw new MissingMemberException("Couldn't find 'Usage' property.");
        Type = (TextureType?)typeof(TextureBase).GetProperty("Type", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(BaseTexture) ?? throw new MissingMemberException("Couldn't find 'Type' property.");
        WrapModeU = BaseTexture.WrapModeU;
        WrapModeV = (TextureWrapMode?)typeof(TextureBase).GetField("_WrapModeV", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(BaseTexture) ?? throw new MissingMemberException("Couldn't find '_WrapModeV' field.");
        WrapModeW = (TextureWrapMode?)typeof(TextureBase).GetField("_WrapModeW", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(BaseTexture) ?? throw new MissingMemberException("Couldn't find '_WrapModeW' field.");
    }

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

    /// <summary>Disposes unmanaged texture resources.</summary>
    public abstract void Dispose();
}
