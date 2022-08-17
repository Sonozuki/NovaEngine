﻿namespace NovaEngine.Graphics;

/// <summary>Represents a one-dimensional texture.</summary>
public class Texture1D : TextureBase
{
    /*********
    ** Accessors
    *********/
    /// <inheritdoc/>
    internal override TextureUsage Usage => this.PixelType == TexturePixelType.Byte ? TextureUsage.Colour : TextureUsage.Colour32;

    /// <inheritdoc/>
    internal override TextureType Type => TextureType.Texture1D;


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="width">The width of the texture.</param>
    /// <param name="pixelType">The type of pixel the texture stores underlying data as.</param>
    /// <param name="sampleCount">The number of samples per pixel of the texture.</param>
    /// <param name="anisotropicFilteringEnabled">Whether the texture has anisotropic filtering enabled.</param>
    /// <param name="maxAnisotropicFilteringLevel">The max anisotropic filtering level of the texture.</param>
    /// <param name="wrapModeU">The texture wrap mode of the U axis.</param>
    /// <param name="filter">The filter mode of the texture.</param>
    public Texture1D(uint width, TexturePixelType pixelType = TexturePixelType.Byte, SampleCount sampleCount = SampleCount.Count1, bool anisotropicFilteringEnabled = true, float maxAnisotropicFilteringLevel = 16, TextureWrapMode wrapModeU = TextureWrapMode.Repeat, TextureFilter filter = TextureFilter.Bilinear)
        : base(width, 1, 1, pixelType, false, 0, 1, sampleCount, anisotropicFilteringEnabled, maxAnisotropicFilteringLevel, wrapModeU, TextureWrapMode.Repeat, TextureWrapMode.Repeat, filter) { }

    /// <summary>Sets pixel data for a specific one-dimensional location.</summary>
    /// <param name="pixels">The pixel data to set.</param>
    /// <param name="offset">The pixel offset for setting pixel data.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pixels.Length"/> + offset goes out of range of the texture.</exception>
    public void SetPixels(Colour[] pixels, int offset = 0) => this.RendererTexture.SetPixels(pixels, offset);

    /// <summary>Sets pixel data for a specific one-dimensional location.</summary>
    /// <param name="pixels">The pixel data to set.</param>
    /// <param name="offset">The pixel offset for setting pixel data.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="pixels.Length"/> + offset goes out of range of the texture.</exception>
    public void SetPixels(Colour32[] pixels, int offset = 0) => this.RendererTexture.SetPixels(pixels, offset);
}