﻿using NovaEngine.Components;
using NovaEngine.Core;
using NovaEngine.External.Rendering;
using NovaEngine.Graphics;
using System;

namespace NovaEngine.Rendering.Fake
{
    /// <summary>Represents a renderer that is only used when nova is being used without a program instance.</summary>
    internal class FakeRenderer : IRenderer
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool CanUseOnPlatform => true;

        /// <inheritdoc/>
        public MVPSettings MVPSettings { get; } = new(false, false, false, false);

        /// <inheritdoc/>
        public SampleCount MaxSampleCount => SampleCount.Count1;


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public RendererTextureBase CreateRendererTexture(TextureBase baseTexture) => new FakeTexture(baseTexture);

        /// <inheritdoc/>
        public RendererGameObjectBase CreateRendererGameObject(GameObject baseGameObject) => new FakeGameObject(baseGameObject);

        /// <inheritdoc/>
        public RendererCameraBase CreateRendererCamera(Camera baseCamera) => new FakeCamera(baseCamera);

        /// <inheritdoc/>
        public void OnInitialise(IntPtr windowHandle) { }

        /// <inheritdoc/>
        public void Dispose() { }
    }
}
