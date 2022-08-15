namespace NovaEngine.Components;

/// <summary>Represents a camera.</summary>
public sealed class Camera : ComponentBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The resolution of the camera's render target.</summary>
    [Serialisable]
    private Vector2I? _Resolution;

    /// <summary>The aspect ratio of the camera (width / height).</summary>
    [Serialisable]
    private float? _AspectRatio;

    /// <summary>Whether VSync is enabled.</summary>
    [Serialisable]
    private bool _IsVSyncEnabled;


    /*********
    ** Acecssors
    *********/
    /// <summary>The projection to use for the camera.</summary>
    public CameraProjection Projection { get; set; }

    /// <summary>The field of view of the camera in degrees (perspective projection only).</summary>
    public float FieldOfView { get; set; }

    /// <summary>The width of the view frustum, in units (orthograhic projection only).</summary>
    public float Width { get; set; }

    /// <summary>The height of the view frustum, in units (orthograhic projection only).</summary>
    public float Height { get; set; }

    /// <summary>The near clipping plane of the camera.</summary>
    public float NearClippingPlane { get; set; }

    /// <summary>The far clipping plane of the camera.</summary>
    public float FarClippingPlane { get; set; }

    /// <summary>The resolution of the camera's render target.</summary>
    public Vector2I Resolution
    {
        get => _Resolution ?? Program.MainWindow.Size;
        set
        {
            _Resolution = Vector2I.ComponentMax(Vector2I.One, value);
            RendererCamera.OnResolutionChange();
        }
    }

    /// <summary>The aspect ratio of the camera (width / height).</summary>
    public float AspectRatio
    {
        get => _AspectRatio ?? Program.MainWindow.Size.X / (float)Program.MainWindow.Size.Y;
        set => _AspectRatio = value;
    }

    /// <summary>The texture the camera will render to.</summary>
    public Texture2D RenderTarget => RendererCamera.RenderTarget;

    /// <summary>Whether VSync is enabled.</summary>
    public bool IsVSyncEnabled
    {
        get => _IsVSyncEnabled;
        set
        {
            _IsVSyncEnabled = value;
            RendererCamera.OnVSyncChange();
        }
    }

    /// <summary>The projection matrix of the camera.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Matrix4x4 ProjectionMatrix
    {
        get
        {
            if (Projection == CameraProjection.Perspective)
                return Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearClippingPlane, FarClippingPlane);
            else
                return Matrix4x4.CreateOrthographic(Width, Height, NearClippingPlane, FarClippingPlane);
        }
    }

    /// <summary>The renderer specific camera.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [NonSerialisable]
    public RendererCameraBase RendererCamera { get; private set; }

    /// <summary>The camera to use when rendering.</summary>
    public static Camera? Main { get; set; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="resolution">The resolution of the camera's render target; specifying <see langword="null"/> will automatically update the resolution to be the same as the window's.</param>
    /// <param name="setMainCamera">Whether <see cref="Main"/> should be set to this camera.</param>
    public Camera(Vector2I? resolution, bool setMainCamera)
        : this(90, .01f, 1000, resolution, null, setMainCamera) { }

    /// <summary>Constructs a perspective instance.</summary>
    /// <param name="fieldOfView">The field of view of the camera, in degrees.</param>
    /// <param name="nearClippingPlane">The near clipping plane of the camera.</param>
    /// <param name="farClippingPlane">The far clipping plane of the camera.</param>
    /// <param name="resolution">The resolution of the camera's render target; specifying <see langword="null"/> will automatically update the resolution to be the same as the window's.</param>
    /// <param name="aspectRatio">The aspect ratio of the camera (width / height); specifying <see langword="null"/> will automatically update the aspect ratio to be the same as the window's.</param>
    /// <param name="setMainCamera">Whether <see cref="Main"/> should be set to this camnera.</param>
    public Camera(float fieldOfView, float nearClippingPlane, float farClippingPlane, Vector2I? resolution, float? aspectRatio, bool setMainCamera)
        : this(CameraProjection.Perspective, fieldOfView, 0, 0, nearClippingPlane, farClippingPlane, resolution, aspectRatio, setMainCamera) { }

    /// <summary>Constructs an orthographic instance.</summary>
    /// <param name="width">The width of the view frustum, in units.</param>
    /// <param name="height">The height of the view frustum, in units.</param>
    /// <param name="nearClippingPlane">The near clipping plane of the camera.</param>
    /// <param name="farClippingPlane">The far clipping plane of the camera.</param>
    /// <param name="resolution">The resolution of the camera's render target; specifying <see langword="null"/> will automatically update the resolution to be the same as the window's.</param>
    /// <param name="setMainCamera">Whether <see cref="Main"/> should be set to this camnera.</param>
    public Camera(float width, float height, float nearClippingPlane, float farClippingPlane, Vector2I? resolution, bool setMainCamera)
        : this(CameraProjection.Othographic, 0, width, height, nearClippingPlane, farClippingPlane, resolution, null, setMainCamera) { }

    /// <summary>Renders a frame using the camera.</summary>
    public void Render(bool presentRenderTarget) // TODO: create a separate system for retrieving objects to render after frustum culling etc
    {
        var gameObjects = new List<RendererGameObjectBase>();
        var uiGameObjects = new List<RendererGameObjectBase>();

        // add all game objects from game object scenes
        gameObjects.AddRange(SceneManager.LoadedScenes
            .Where(scene => scene is not UIScene)
            .SelectMany(scene => scene.RootGameObjects)
            .SelectMany(gameObject => gameObject.GetAllGameObjects(false))
            .Where(gameObject => gameObject.Components.GetRange<MeshRenderer>().Any())
            .Select(gameObject => gameObject.RendererGameObject));
        gameObjects.AddRange(SceneManager.GizmosScene.RootGameObjects
            .SelectMany(gameObject => gameObject.Children)
            .Select(gameObject => gameObject.RendererGameObject));

        // add all relevant ui control game objects
        foreach (var uiScene in SceneManager.LoadedScenes.Where(scene => scene is UIScene).Cast<UIScene>())
        {
            if (uiScene.RenderMode == UISceneRenderMode.WorldSpace)
                throw new NotImplementedException(); // TODO: implement

            if (uiScene.RenderMode == UISceneRenderMode.ScreenSpaceCamera && uiScene.Camera != this)
                continue;

            uiGameObjects.AddRange(uiScene.RootGameObjects
                .SelectMany(gameObject => gameObject.GetAllGameObjects(false))
                .Where(gameObject => gameObject.Components.GetRange<MeshRenderer>().Any())
                .Select(gameObject => gameObject.RendererGameObject));
        }

        RendererCamera.Render(gameObjects, uiGameObjects, presentRenderTarget);
    }

    /// <inheritdoc/>
    public override void Dispose() => RendererCamera.Dispose();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /*********
    ** Private Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="projection">The projection to use for the camera.</param>
    /// <param name="fieldOfView">The field of view of the camera, in degrees (perspective projection only).</param>
    /// <param name="width">The width of the view frustum, in units (orthograhic projection only).</param>
    /// <param name="height">The height of the view frustum, in units (orthograhic projection only).</param>
    /// <param name="nearClippingPlane">The near clipping place of the camera.</param>
    /// <param name="farClippingPlane">The far clipping place of the camera.</param>
    /// <param name="resolution">The resolution of the camera's render target; specifying <see langword="null"/> will automatically update the resolution to be the same as the window's.</param>
    /// <param name="aspectRatio">The aspect ratio of the camera (width / height).</param>
    /// <param name="setMainCamera">Whether <see cref="Main"/> should be set to this camnera.</param>
    private Camera(CameraProjection projection, float fieldOfView, float width, float height, float nearClippingPlane, float farClippingPlane, Vector2I? resolution, float? aspectRatio, bool setMainCamera)
    {
        Projection = projection;
        FieldOfView = fieldOfView;
        Width = width;
        Height = height;
        NearClippingPlane = nearClippingPlane;
        FarClippingPlane = farClippingPlane;
        _Resolution = resolution;
        _AspectRatio = aspectRatio;

        if (setMainCamera)
            Main = this;

        CreateRendererCamera();
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>Creates the renderer camera.</summary>
    [OnDeserialised]
    private void CreateRendererCamera()
    {
        if (_Resolution == null && Program.HasProgramInstance)
            Program.MainWindow.Resize += e => Resolution = e.NewSize;

        RendererCamera = RendererManager.CurrentRenderer.CreateRendererCamera(this);
    }
}
