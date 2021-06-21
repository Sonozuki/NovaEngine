﻿using NovaEngine.Graphics;

namespace NovaEngine.Rendering.Dummy
{
    /// <summary>Represents a platform that is only used when nova is being used without a program instance.</summary>
    public class DummyTexture : RendererTextureBase
    {
        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public DummyTexture(TextureBase baseTexture)
            : base(baseTexture) { }

        /// <inheritdoc/>
        public override void Dispose() { }

        /// <inheritdoc/>
        public override void GenerateMipChain() { }
        
        /// <inheritdoc/>
        public override void Set1DPixels(Colour[] pixels, int xOffset = 0) { }
        
        /// <inheritdoc/>
        public override void Set2DPixels(Colour[,] pixels, int xOffset = 0, int yOffset = 0) { }

        /// <inheritdoc/>
        public override void Set3DPixels(Colour[,,] pixels, int xOffset = 0, int yOffset = 0, int zOffset = 0) { }
    }
}