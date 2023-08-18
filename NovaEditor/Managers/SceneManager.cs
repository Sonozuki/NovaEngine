namespace NovaEditor.Managers;

/// <summary>Manages project scenes.</summary>
internal static class SceneManager
{
    /*********
    ** Properties
    *********/
    /// <summary>All the scenes present in the project.</summary>
    public static ObservableCollection<Scene> AllScenes { get; private set; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises the class.</summary>
    static SceneManager()
    {
        GetAllScenes();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Retrieves all the scenes in the project.</summary>
    private static void GetAllScenes()
    {
        var allScenes = new List<Scene>();

        foreach (var sceneFile in Directory.GetFiles(NovaEngine.Constants.SceneDirectory, $"*{NovaEngine.Constants.SceneFileExtension}"))
            try
            {
                allScenes.Add(Content.Load<Scene>(sceneFile));
            }
            catch (ContentException)
            {
                // TODO: notify user scene failed parsing
            }

        AllScenes.Clear();
        foreach (var scene in allScenes)
            AllScenes.Add(scene);
    }
}
