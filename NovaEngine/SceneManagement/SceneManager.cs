namespace NovaEngine.SceneManagement;

/// <summary>Manages the game scenes.</summary>
public static class SceneManager
{
    /*********
    ** Fields
    *********/
    /// <summary>The currently loaded scenes.</summary>
    private static readonly List<Scene> _LoadedScenes = new();


    /*********
    ** Properties
    *********/
    /// <summary>The currently loaded scenes.</summary>
    public static IReadOnlyList<Scene> LoadedScenes => _LoadedScenes.AsReadOnly();

    /// <summary>The scene responsible for storing gizmos.</summary>
    internal static GizmosScene GizmosScene { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises the class.</summary>
    static SceneManager()
    {
        GizmosScene.Start();
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Loads a scene.</summary>
    /// <param name="name">The name of the scene to load.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is <see langword="null"/> or empty.</exception>
    public static void LoadScene(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        var loadedScene = ContentLoader.Load<Scene>(Path.Combine(Constants.RelativeSceneDirectory, name + Constants.SceneFileExtension));
        _LoadedScenes.Add(loadedScene);
        loadedScene.Start();
    }

    /// <summary>Unloads a scene.</summary>
    /// <param name="name">The case-insensitive name of the scene to unload.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is <see langword="null"/> or empty.</exception>
    public static void UnloadScene(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        var sceneToUnload = LoadedScenes.FirstOrDefault(loadedScene => loadedScene.Name.ToLower(G11n.Culture) == name.ToLower(G11n.Culture));
        if (sceneToUnload == null)
            throw new ArgumentException($"No scene with the name '{name}' is loaded.");

        foreach (var rootObject in sceneToUnload.RootGameObjects)
            rootObject.Dispose();
    }


    /*********
    ** Internal Methods
    *********/
    /// <summary>Updates all the loaded scenes.</summary>
    internal static void Update()
    {
        GizmosScene.Update();

        foreach (var scene in LoadedScenes)
            scene.Update();
    }
}
