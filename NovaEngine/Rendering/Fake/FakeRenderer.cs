namespace NovaEngine.Rendering.Fake;

/// <summary>Represents a renderer that is only used when nova is being used without a program instance.</summary>
internal class FakeRenderer : IRenderer
{
    /*********
    ** Accessors
    *********/
    /// <inheritdoc/>
    public bool CanUseOnPlatform => true;

    /// <inheritdoc/>
    public SampleCount MaxSampleCount => SampleCount._1;

    /// <inheritdoc/>
    public string DeviceName => "";

    /// <inheritdoc/>
    public bool VSync { set { } }


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
    public void PrepareDispose() { }

    /// <inheritdoc/>
    public void Dispose() { }
}
