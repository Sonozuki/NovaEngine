﻿using NovaEngine.Rendering;

namespace NovaEngine.Graphics
{
    /// <summary>Represents a two-dimensional texture.</summary>
    public class Texture2D : TextureBase
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The height of the texture.</summary>
        public uint Height => _Height;

        /// <inheritdoc/>
        internal override TextureUsage Usage => TextureUsage.Colour;

        /// <inheritdoc/>
        internal override TextureType Type => TextureType.Texture2D;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        public Texture2D(uint width, uint height)
            : this(width, height, true) { } // ", true" is only here so it doesn't try to call itself

        /// <summary>Constructs an instance.</summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="generateMipChain">Whether a mip chain should be generated.</param>
        /// <param name="mipLodBias">The mip LOD (level of detail) bias of the texture.</param>
        /// <param name="sampleCount">The number of samples per pixel of the texture.</param>
        /// <param name="anisotropicFilteringEnabled">Whether the texture has anisotropic filtering enabled.</param>
        /// <param name="maxAnisotropicFilteringLevel">The max anisotropic filtering level of the texture.</param>
        /// <param name="wrapModeU">The texture wrap mode of the U axis.</param>
        /// <param name="wrapModeV">The texture wrap mode of the V axis.</param>
        /// <param name="filter">The filter mode of the texture.</param>
        public Texture2D(uint width, uint height, bool generateMipChain = true, float mipLodBias = 0, SampleCount sampleCount = SampleCount.Count1, bool anisotropicFilteringEnabled = true, float maxAnisotropicFilteringLevel = 16, TextureWrapMode wrapModeU = TextureWrapMode.Repeat, TextureWrapMode wrapModeV = TextureWrapMode.Repeat, TextureFilter filter = TextureFilter.Bilinear)
            : base(width, height, 1, generateMipChain, mipLodBias, 1, sampleCount, anisotropicFilteringEnabled, maxAnisotropicFilteringLevel, wrapModeU, wrapModeV, TextureWrapMode.Repeat, filter) { }
    }
}
