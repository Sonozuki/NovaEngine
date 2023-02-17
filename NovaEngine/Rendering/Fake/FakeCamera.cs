namespace NovaEngine.Rendering.Fake;

/// <summary>Represents a camera that is only used when nova is being used without a program instance.</summary>
internal sealed class FakeCamera : RendererCameraBase
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public override Texture2D RenderTarget => Texture2D.Undefined;


    /*********
    ** Constructors
    *********/
    /// <inheritdoc/>
    public FakeCamera(Camera baseCamera)
        : base(baseCamera) { }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override void OnResolutionChange() { }

    /// <inheritdoc/>
    public override void OnVSyncChange() { }

    /// <inheritdoc/>
    public override void Render(IEnumerable<RendererGameObjectBase> gameObjects, IEnumerable<RendererGameObjectBase> uiGameObjects, bool presentRenderTarget) { }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the camera.</summary>
    /// <param name="disposing">Whether the camera is being disposed deterministically.</param>
    protected override void Dispose(bool disposing) { }
}
