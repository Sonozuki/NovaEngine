namespace NovaEngine.SceneManagement;

/// <summary>Represents a scene for user interface controls.</summary>
public class UIScene : Scene
{
    /*********
    ** Accessors
    *********/
    /// <summary>The rendering mode of the scene.</summary>
    public UISceneRenderMode RenderMode { get; set; }

    /// <summary>The camera used for specific render modes.</summary>
    /// <remarks>This is only used with <see cref="UISceneRenderMode.ScreenSpaceCamera"/>.</remarks>
    public Camera? Camera { get; set; }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public UIScene(string name, bool isActive)
        : base(name, isActive) { }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    protected UIScene() { } // required for serialiser
}
