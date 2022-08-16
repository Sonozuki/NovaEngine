namespace NovaEngine.Graphics;

/// <summary>Represents a texture.</summary>
public abstract class TextureBase : IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>The height of the texture.</summary>
    protected uint _Height;

    /// <summary>The depth of the texture.</summary>
    protected uint _Depth;

    /// <summary>The texture wrap mode of the V axis.</summary>
    protected TextureWrapMode _WrapModeV;

    /// <summary>The texture wrap mode of the W axis.</summary>
    protected TextureWrapMode _WrapModeW;

    /// <summary>The number of mip levels the texture has.</summary>
    protected uint _MipLevels = 1; // MipLevels has a protected backing field so renderer specific textures can set this

    /// <summary>The number of layers the texture has.</summary>
    protected uint _LayerCount;


    /*********
    ** Accessors
    *********/
    /// <summary>The width of the texture.</summary>
    public uint Width { get; }

    /// <summary>The number of mip levels the texture has.</summary>
    public uint MipLevels => _MipLevels;

    /// <summary>The mip LOD (level of detail) bias of the texture.</summary>
    public float MipLodBias { get; }

    /// <summary>The number of samples per pixel of the texture.</summary>
    public SampleCount SampleCount { get; }

    /// <summary>Whether the texture has anisotropic filtering enabled.</summary>
    public bool AnisotropicFilteringEnabled { get; }

    /// <summary>The max anisotropic filtering level of the texture.</summary>
    public float MaxAnisotropicFilteringLevel { get; }

    /// <summary>The texture wrap mode of the U axis.</summary>
    public TextureWrapMode WrapModeU { get; }

    /// <summary>The filter mode of the texture.</summary>
    public TextureFilter Filter { get; }

    /// <summary>Whether a mip chain will be generated for the texture and automatically regenerated when the texture is changed.</summary>
    public bool AutomaticallyGenerateMipChain { get; }

    /// <summary>The renderer specific texture.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [NonSerialisable]
    public RendererTextureBase RendererTexture { get; private set; }

    /// <summary>The usage of the texture.</summary>
    internal abstract TextureUsage Usage { get; }

    /// <summary>The type of the texture.</summary>
    internal abstract TextureType Type { get; }


    /*********
    ** Public Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="automaticallyGenerateMipChain">Whether a mip chain will be generated for the texture and automatically regenerated when the texture is changed.</param>
    /// <param name="mipLodBias">The mip LOD (level of detail) bias of the texture.</param>
    /// <param name="layerCount">The number of layers the texture has.</param>
    /// <param name="sampleCount">The number of samples per pixel of the texture.</param>
    /// <param name="anisotropicFilteringEnabled">Whether the texture has anisotropic filtering enabled.</param>
    /// <param name="maxAnisotropicFilteringLevel">The max anisotropic filtering level of the texture.</param>
    /// <param name="wrapModeU">The texture wrap mode of the U axis.</param>
    /// <param name="wrapModeV">The texture wrap mode of the V axis.</param>
    /// <param name="wrapModeW">The texture wrap mode of the W axis.</param>
    /// <param name="filter">The filter mode of the texture.</param>
    public TextureBase(uint width, uint height, uint depth, bool automaticallyGenerateMipChain, float mipLodBias, uint layerCount, SampleCount sampleCount, bool anisotropicFilteringEnabled, float maxAnisotropicFilteringLevel, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW, TextureFilter filter)
    {
        Width = width;
        _Height = height;
        _Depth = depth;
        MipLodBias = mipLodBias;
        _LayerCount = layerCount;
        SampleCount = (SampleCount)Math.Clamp((int)sampleCount, 0, (int)RenderingSettings.Instance.MaxSampleCount);
        AnisotropicFilteringEnabled = anisotropicFilteringEnabled;
        MaxAnisotropicFilteringLevel = maxAnisotropicFilteringLevel;
        WrapModeU = wrapModeU;
        _WrapModeV = wrapModeV;
        _WrapModeW = wrapModeW;
        Filter = filter;
        AutomaticallyGenerateMipChain = automaticallyGenerateMipChain;

        CreateRendererTexture();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Disposes unmanaged texture resources.</summary>
    public void Dispose() => RendererTexture.Dispose();


    /*********
    ** Protected Methods
    *********/
    /// <summary>Creates the renderer texture.</summary>
    [OnDeserialised(SerialiserCallbackPriority.High)]
    protected void CreateRendererTexture() => RendererTexture = RendererManager.CurrentRenderer.CreateRendererTexture(this);
}
