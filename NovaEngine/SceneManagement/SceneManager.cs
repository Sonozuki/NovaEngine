namespace NovaEngine.SceneManagement;

/// <summary>Manages the game scenes.</summary>
public static class SceneManager
{
    /*********
    ** Fields
    *********/
    /// <summary>The currently loaded scenes keyed on scene name.</summary>
    private static readonly Dictionary<string, Scene> _LoadedScenes = new();


    /*********
    ** Properties
    *********/
    /// <summary>The currently loaded scenes.</summary>
    public static ImmutableArray<Scene> LoadedScenes => _LoadedScenes.Values.ToImmutableArray();

    /// <summary>The scene responsible for storing gizmos.</summary>
    internal static GizmosScene GizmosScene { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises the class.</summary>
    static SceneManager()
    {
        GizmosScene.Start();

        if (Program.Arguments.ForceLoadScenes)
            foreach (var sceneFile in Directory.GetFiles(Constants.SceneDirectory, $"*{Constants.SceneFileExtension}"))
                LoadScene(Path.GetFileNameWithoutExtension(sceneFile));
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

        if (_LoadedScenes.ContainsKey(name))
            return;

        var loadedScene = Content.Load<Scene>(Path.Combine(Constants.RelativeSceneDirectory, name + Constants.SceneFileExtension));
        _LoadedScenes[name] = loadedScene;
        loadedScene.Start();
    }

    /// <summary>Unloads a scene.</summary>
    /// <param name="name">The case-insensitive name of the scene to unload.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is <see langword="null"/> or empty.</exception>
    public static void UnloadScene(string name)
    {
        if (Program.Arguments.ForceLoadScenes)
            return;

        ArgumentException.ThrowIfNullOrEmpty(name);

        var sceneToUnload = LoadedScenes.FirstOrDefault(loadedScene => loadedScene.Name.ToLower(G11n.Culture) == name.ToLower(G11n.Culture))
            ?? throw new ArgumentException($"No scene with the name '{name}' is loaded.");

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
