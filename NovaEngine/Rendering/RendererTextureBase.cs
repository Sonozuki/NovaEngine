using NovaEngine.Graphics;
using System;

namespace NovaEngine.Rendering
{
    /// <summary>Represents a renderer texture.</summary>
    public abstract class RendererTextureBase : IDisposable
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The underlying texture.</summary>
        public TextureBase BaseTexture { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="baseTexture">The underlying texture.</param>
        public RendererTextureBase(TextureBase baseTexture)
        {
            BaseTexture = baseTexture;
        }

        /// <summary>Sets pixels specific colours.</summary>
        /// <param name="pixels">The rectangle of pixels to set.</param>
        /// <param name="xOffset">The X offset of the pixels (from top left).</param>
        /// <param name="yOffset">The Y offset of the pixels (from top left).</param>
        public abstract void SetPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0);

        /// <summary>Generates the mip chain for the texture.</summary>
        public abstract void GenerateMipChain();

        /// <summary>Disposes unmanaged texture resources.</summary>
        public abstract void Dispose();
    }
}
