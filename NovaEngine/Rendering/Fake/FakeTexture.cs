namespace NovaEngine.Rendering.Fake;

/// <summary>Represents a platform that is only used when nova is being used without a program instance.</summary>
internal sealed class FakeTexture : RendererTextureBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The pixel data of the texture.</summary>
    /// <remarks>This is required for textures to be serialised correctly as pixel data isn't stored on the graphics memory like actual renderers.</remarks>
    private readonly Colour32[] Pixels;


    /*********
    ** Constructors
    *********/
    /// <summary>Destructs the instance.</summary>
    ~FakeTexture() => Dispose(false);

    /// <inheritdoc/>
    public FakeTexture(TextureBase baseTexture)
        : base(baseTexture)
    {
        Pixels = new Colour32[Width * Height * Depth];
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override void GenerateMipChain() { }

    /// <inheritdoc/>
    public override Colour32[] GetPixels() => Pixels;

    /// <inheritdoc/>
    public override void SetPixels(Colour[] pixels, int offset = 0)
    {
        if (pixels.Length + offset > Pixels.Length)
            throw new ArgumentOutOfRangeException(nameof(pixels), "Specified pixel data goes out of range of the texture.");

        for (var i = 0; i < pixels.Length; i++)
            Pixels[i + offset] = pixels[i];
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour32[] pixels, int offset = 0)
    {
        if (pixels.Length + offset > Pixels.Length)
            throw new ArgumentOutOfRangeException(nameof(pixels), "Specified pixel data goes out of range of the texture.");

        for (var i = 0; i < pixels.Length; i++)
            Pixels[i + offset] = pixels[i];
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0) // TODO: y,x or x,y?
    {
        if (pixels.GetLength(0) + xOffset > Width || pixels.GetLength(1) + yOffset > Height)
            throw new ArgumentOutOfRangeException(nameof(pixels), "Specified pixel data goes out of range of the texture.");

        for (var x = 0; x < pixels.GetLength(0); x++)
            for (var y = 0; y < pixels.GetLength(1); y++)
                Pixels[(y + yOffset) * Width + x + xOffset] = pixels[x, y];
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour32[,] pixels, int xOffset = 0, int yOffset = 0) => throw new NotImplementedException();

    /// <inheritdoc/>
    public override void SetPixels(Colour[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0) => throw new NotImplementedException();

    /// <inheritdoc/>
    public override void SetPixels(Colour32[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0) => throw new NotImplementedException();


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the texture.</summary>
    /// <param name="disposing">Whether the texture is being disposed deterministically.</param>
    protected override void Dispose(bool disposing) { }
}
