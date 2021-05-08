using NovaEngine.Core;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using System;

namespace NovaEngine.Rendering.Dummy
{
    /// <summary>Represents a renderer that is only used when nova is being used without a program instance.</summary>
    public class DummyRenderer : IRenderer
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool CanUseOnPlatform => true;

        /// <inheritdoc/>
        public SampleCount MaxSampleCount => SampleCount.Count1;


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public RendererGameObjectBase CreateRendererGameObject(GameObject baseGameObject) => new DummyGameObject(baseGameObject);

        /// <inheritdoc/>
        public RendererTextureBase CreateRendererTexture(TextureBase baseTexture, bool generateMipChain) => new DummyTexture(baseTexture);

        /// <inheritdoc/>
        public void OnCleanUp() { }

        /// <inheritdoc/>
        public void OnInitialise(IntPtr windowHandle) { }

        /// <inheritdoc/>
        public void OnRenderFrame() { }

        /// <inheritdoc/>
        public void OnWindowResize(Size newSize) { }
    }
}
