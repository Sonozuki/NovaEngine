using NovaEngine.Core;
using NovaEngine.Graphics;
using NovaEngine.Maths;
using System;

namespace NovaEngine.Rendering
{
    /// <summary>Represents a graphics renderer.</summary>
    public interface IRenderer
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether the renderer can be used on the current environment platform.</summary>
        public bool CanUseOnPlatform { get; }

        /// <summary>The max supported sample count.</summary>
        public SampleCount MaxSampleCount { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Creates a renderer specific texture.</summary>
        /// <param name="baseTexture">The underlying texture.</param>
        /// <param name="generateMipChain">Whether the mip chain should be generated for the texture.</param>
        /// <returns>A renderer specific texture.</returns>
        public RendererTextureBase CreateRendererTexture(TextureBase baseTexture, bool generateMipChain);

        /// <summary>Creates a renderer specific game object.</summary>
        /// <param name="baseGameObject">The underlying game object.</param>
        /// <returns>A renderer specific game object.</returns>
        public RendererGameObjectBase CreateRendererGameObject(GameObject baseGameObject);

        /// <summary>Invoked when the renderer should initialise itself.</summary>
        /// <param name="windowHandle">The handle to the application window.</param>
        public void OnInitialise(IntPtr windowHandle);

        /// <summary>Invoked when the window gets resized.</summary>
        /// <param name="newSize">The new size of the window.</param>
        public void OnWindowResize(Size newSize);

        /// <summary>Invoked when the renderer should render a frame.</summary>
        public void OnRenderFrame();

        /// <summary>Invoked when the renderer should clean itself up.</summary>
        public void OnCleanUp();
    }
}
