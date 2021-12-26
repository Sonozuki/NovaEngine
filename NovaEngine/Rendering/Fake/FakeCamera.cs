namespace NovaEngine.Rendering.Fake;

/// <summary>Represents a camera that is only used when nova is being used without a program instance.</summary>
internal class FakeCamera : RendererCameraBase
{
    /*********
    ** Accessors
    *********/
    /// <inheritdoc/>
    public override Texture2D RenderTarget => Texture2D.Undefined;


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public FakeCamera(Camera baseCamera)
        : base(baseCamera) { }

    /// <inheritdoc/>
    public override void OnResolutionChange() { }

    /// <inheritdoc/>
    public override void Render(IEnumerable<RendererGameObjectBase> gameObjects, bool presentRenderTarget) { }

    /// <inheritdoc/>
    public override void Dispose() { }
}
