namespace NovaEngine.External.Rendering;

/// <summary>Represents a graphics renderer.</summary>
public interface IRenderer : IDisposable
{
    /*********
    ** Accessors
    *********/
    /// <summary>Whether the renderer can be used on the current environment platform.</summary>
    public bool CanUseOnPlatform { get; }

    /// <summary>The max supported sample count.</summary>
    public SampleCount MaxSampleCount { get; }

    /// <summary>The name of the device being used.</summary>
    public string DeviceName { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Creates a renderer specific texture.</summary>
    /// <param name="baseTexture">The underlying texture.</param>
    /// <returns>A renderer specific texture.</returns>
    public RendererTextureBase CreateRendererTexture(TextureBase baseTexture);

    /// <summary>Creates a renderer specific game object.</summary>
    /// <param name="baseGameObject">The underlying game object.</param>
    /// <returns>A renderer specific game object.</returns>
    public RendererGameObjectBase CreateRendererGameObject(GameObject baseGameObject);

    /// <summary>Creates a renderer specific camera.</summary>
    /// <param name="baseCamera">The underlying camera.</param>
    /// <returns>A renderer specific camera.</returns>
    public RendererCameraBase CreateRendererCamera(Camera baseCamera);

    /// <summary>Invoked when the renderer should initialise itself.</summary>
    /// <param name="windowHandle">The handle to the application window.</param>
    public void OnInitialise(IntPtr windowHandle);

    /// <summary>Invoked just before disposing begins.</summary>
    public void PrepareDispose();
}
