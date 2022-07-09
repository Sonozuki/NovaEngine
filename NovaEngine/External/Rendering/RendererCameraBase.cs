﻿namespace NovaEngine.External.Rendering;

/// <summary>Represents a renderer camera.</summary>
public abstract class RendererCameraBase : IDisposable
{
    /*********
    ** Accessors
    *********/
    /// <summary>The underlying camera.</summary>
    public Camera BaseCamera { get; }

    /// <summary>The texture the camera will render to.</summary>
    public abstract Texture2D RenderTarget { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="baseCamera">The underlying camera.</param>
    public RendererCameraBase(Camera baseCamera)
    {
        BaseCamera = baseCamera;
    }

    /// <summary>Invoked when the resolution of the camera is changed.</summary>
    public abstract void OnResolutionChange();

    /// <summary>Invoked when the VSync setting of the camera is changed.</summary>
    public abstract void OnVSyncChange();

    /// <summary>Renders all the specified objects to the camera's render target.</summary>
    /// <param name="gameObjects">The game objects which have <see cref="MeshRenderer"/>s to render.<br/>NOTE: it's the callers responsibility to ensure all game objects have a valid <see cref="MeshRenderer"/>, unexpected behaviour will happen otherwise.</param>
    /// <param name="uiGameObjects">The game objects which have <see cref="MeshRenderer"/>s to render as part of the UI.<br/>NOTE: it's the callers responsibility to ensure all game objects have a valid <see cref="MeshRenderer"/>, unexpected behaviour will happen otherwise.</param>
    /// <param name="presentRenderTarget">Whether the camera's render target should get presented directly to the screen.</param>
    public abstract void Render(IEnumerable<RendererGameObjectBase> gameObjects, IEnumerable<RendererGameObjectBase> uiGameObjects, bool presentRenderTarget);

    /// <inheritdoc/>
    public abstract void Dispose();
}
