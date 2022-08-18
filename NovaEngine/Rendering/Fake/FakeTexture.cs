namespace NovaEngine.Rendering.Fake;

/// <summary>Represents a platform that is only used when nova is being used without a program instance.</summary>
internal class FakeTexture : RendererTextureBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The pixel dataof the texture.</summary>
    /// <remarks>This is required for textures to be serialised correctly as pixel data isn't stored on the graphics memory like actual renderers.</remarks>
    private readonly Colour32[] Pixels;


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public FakeTexture(TextureBase baseTexture)
        : base(baseTexture)
    {
        Pixels = new Colour32[this.Width * this.Height * this.Depth];
    }

    /// <inheritdoc/>
    public override void Dispose() { }

    /// <inheritdoc/>
    public override void GenerateMipChain() { }

    /// <inheritdoc/>
    public override Colour32[] GetPixels() => Pixels;

    /// <inheritdoc/>
    public override void SetPixels(Colour[] pixels, int offset = 0)
    {
        if (pixels.Length + offset > Pixels.Length)
            throw new ArgumentOutOfRangeException(nameof(pixels), "Specified pixel data goes out of range of the texture.");

        for (int i = 0; i < pixels.Length; i++)
            Pixels[i + offset] = pixels[i];
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour32[] pixels, int offset = 0)
    {
        if (pixels.Length + offset > Pixels.Length)
            throw new ArgumentOutOfRangeException(nameof(pixels), "Specified pixel data goes out of range of the texture.");

        for (int i = 0; i < pixels.Length; i++)
            Pixels[i + offset] = pixels[i];
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0) // TODO: y,x or x,y?
    {
        if (pixels.GetLength(0) + xOffset > this.Width || pixels.GetLength(1) + yOffset > this.Height)
            throw new ArgumentOutOfRangeException(nameof(pixels), "Specified pixel data goes out of range of the texture.");

        for (int x = 0; x < pixels.GetLength(0); x++)
            for (int y = 0; y < pixels.GetLength(1); y++)
                Pixels[(y + yOffset) * this.Width + x + xOffset] = pixels[x, y];
    }

    /// <inheritdoc/>
    public override void SetPixels(Colour32[,] pixels, int xOffset = 0, int yOffset = 0) => throw new NotImplementedException();

    /// <inheritdoc/>
    public override void SetPixels(Colour[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0) => throw new NotImplementedException();

    /// <inheritdoc/>
    public override void SetPixels(Colour32[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0) => throw new NotImplementedException();
}
