﻿namespace NovaEngine.Graphics;

/// <summary>Represents a texture.</summary>
public abstract class TextureBase : IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>Whether the texture has been disposed.</summary>
    private bool IsDisposed;

    /// <summary>The height of the texture.</summary>
    internal uint _Height;

    /// <summary>The depth of the texture.</summary>
    internal uint _Depth;

    /// <summary>The texture wrap mode of the V axis.</summary>
    internal TextureWrapMode _WrapModeV;

    /// <summary>The texture wrap mode of the W axis.</summary>
    internal TextureWrapMode _WrapModeW;

    /// <summary>The number of layers the texture has.</summary>
    internal uint _LayerCount;

    /// <summary>The pixel data of the texture.</summary>
    /// <remarks>This will always be <see langword="null"/> when accessed manually.<br/>When a texture gets serialised it means it wouldn't be loaded through the content pipeline when deserialised, therefore, we need to store the pixel data for the serialiser to save; this will be populated just before being serialised and set to <see langword="null"/> straight after.</remarks>
    private Colour32[]? SerialiserPixelData;


    /*********
    ** Properties
    *********/
    /// <summary>The width of the texture.</summary>
    public uint Width { get; }

    /// <summary>The type of pixel the texture stores underlying data as.</summary>
    public TexturePixelType PixelType { get; }

    /// <summary>The number of mip levels the texture has.</summary>
    public uint MipLevels { get; internal set; } = 1;

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
    ** Constructors
    *********/
    /// <summary>Destructs the instance.</summary>
    ~TextureBase() => Dispose(false);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Constructs an instance.</summary>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="pixelType">The type of pixel the texture stores underlying data as.</param>
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
    protected TextureBase(uint width, uint height, uint depth, TexturePixelType pixelType, bool automaticallyGenerateMipChain, float mipLodBias, uint layerCount, SampleCount sampleCount, bool anisotropicFilteringEnabled, float maxAnisotropicFilteringLevel, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW, TextureFilter filter)
    {
        Width = width;
        _Height = height;
        _Depth = depth;
        PixelType = pixelType;
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


    /*********
    ** Public Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the texture.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Retrieves the texture pixel data ready for the serialiser.</summary>
    [OnSerialising]
    protected void StorePixelData() => SerialiserPixelData = RendererTexture.GetPixels();

    /// <summary>Clears the pixel data.</summary>
    /// <remarks>This is done so it's not accidently used, this is due to <see cref="SerialiserPixelData"/> being able to desync from the texture data until the texture is next serialised.</remarks>
    [OnSerialised]
    protected void ClearPixelData() => SerialiserPixelData = null;

    /// <summary>Creates the renderer texture.</summary>
    [OnDeserialised(SerialiserCallbackPriority.High)]
    protected void CreateRendererTexture()
    {
        RendererTexture = RendererManager.CurrentRenderer.CreateRendererTexture(this);

        if (SerialiserPixelData != null)
        {
            RendererTexture.SetPixels(SerialiserPixelData);
            ClearPixelData();
        }
    }

    /// <summary>Cleans up unmanaged resources in the texture.</summary>
    /// <param name="disposing">Whether the texture is being disposed deterministically.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
            RendererTexture?.Dispose();

        IsDisposed = true;
    }
}
