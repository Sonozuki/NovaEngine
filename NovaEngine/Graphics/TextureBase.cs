using NovaEngine.Rendering;
using System;

namespace NovaEngine.Graphics
{
    /// <summary>Represents a texture.</summary>
    public abstract class TextureBase : IDisposable
    {
        /*********
        ** Fields
        *********/
        /// <summary>The height of the texture.</summary>
        protected internal uint _Height;

        /// <summary>The depth of the texture.</summary>
        protected internal uint _Depth;

        /// <summary>The number of layers the texture has.</summary>
        protected internal uint _LayerCount;

        /// <summary>The renderer specific texture.</summary>
        protected internal RendererTextureBase RendererTexture;


        /*********
        ** Accessors
        *********/
        /// <summary>The width of the texture.</summary>
        public uint Width { get; }

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

        /// <summary>The texture wrap mode of the V axis.</summary>
        public TextureWrapMode WrapModeV { get; }

        /// <summary>The texture wrap mode of the W axis.</summary>
        public TextureWrapMode WrapModeW { get; }

        /// <summary>The filter mode of the texture.</summary>
        public TextureFilter Filter { get; }

        /// <summary>The usage of the texture.</summary>
        internal abstract TextureUsage Usage { get; }

        /// <summary>The type of the texture.</summary>
        internal abstract TextureType Type { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <param name="generateMipChain">Whether the mip chain should be generated.</param>
        /// <param name="mipLodBias">The mip LOD (level of detail) bias of the texture.</param>
        /// <param name="layerCount">The number of layers the texture has.</param>
        /// <param name="sampleCount">The number of samples per pixel of the texture.</param>
        /// <param name="anisotropicFilteringEnabled">Whether the texture has anisotropic filtering enabled.</param>
        /// <param name="maxAnisotropicFilteringLevel">The max anisotropic filtering level of the texture.</param>
        /// <param name="wrapModeU">The texture wrap mode of the U axis.</param>
        /// <param name="wrapModeV">The texture wrap mode of the V axis.</param>
        /// <param name="wrapModeW">The texture wrap mode of the W axis.</param>
        /// <param name="filter">The filter mode of the texture.</param>
        public TextureBase(uint width, uint height, uint depth, bool generateMipChain, float mipLodBias, uint layerCount, SampleCount sampleCount, bool anisotropicFilteringEnabled, float maxAnisotropicFilteringLevel, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW, TextureFilter filter)
        {
            Width = width;
            _Height = height;
            _Depth = depth;
            MipLodBias = mipLodBias;
            _LayerCount = layerCount;
            SampleCount = sampleCount; // TODO: clamp
            AnisotropicFilteringEnabled = anisotropicFilteringEnabled;
            MaxAnisotropicFilteringLevel = maxAnisotropicFilteringLevel;
            WrapModeU = wrapModeU;
            WrapModeV = wrapModeV;
            WrapModeW = wrapModeW;
            Filter = filter;

            RendererTexture = RendererManager.CurrentRenderer.CreateRendererTexture(this, generateMipChain);
        }

        /// <summary>Sets pixels specific colours.</summary>
        /// <param name="pixels">The rectangle of pixels to set.</param>
        /// <param name="xOffset">The X offset of the pixels (from top left).</param>
        /// <param name="yOffset">The Y offset of the pixels (from top left).</param>
        public void SetPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0) => RendererTexture.SetPixels(pixels, xOffset, yOffset); // TODO: temp

        /// <summary>Disposes unmanaged texture resources.</summary>
        public void Dispose() => RendererTexture.Dispose();
    }
}
