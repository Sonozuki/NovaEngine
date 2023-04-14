namespace NovaEditor;

/// <summary>Manages nova projects.</summary>
internal static class ProjectManager
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when the current project is changed.</summary>
    public static event EventHandler<CurrentProjectChangedEventArgs> CurrentProjectChanged;


    /*********
    ** Fields
    *********/
    /// <summary>The directory all app data is stored in.</summary>
    private static readonly string AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NovaEditor");

    /// <summary>The json file containing the recent projects.</summary>
    private static readonly string RecentProjectsFile = Path.Combine(AppDataDirectory, "recentProjects.json");

    /// <summary>The current loaded project.</summary>
    private static string _CurrentProject;


    /*********
    ** Properties
    *********/
    /// <summary>The current loaded project.</summary>
    public static string CurrentProject
    {
        get => _CurrentProject;
        set
        {
            var oldProject = _CurrentProject;
            _CurrentProject = value;
            CurrentProjectChanged?.Invoke(null, new(oldProject, value));

            if (value != null)
                AddProjectToRecentProjects(value);
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves the recent projects.</summary>
    /// <returns>The recent projects.</returns>
    public static List<string> GetRecentProjects()
    {
        if (!File.Exists(RecentProjectsFile))
            return new();

        try
        {
            return JsonSerializer.Deserialize<List<string>>(File.ReadAllText(RecentProjectsFile));
        }
        catch
        {
            File.Delete(RecentProjectsFile);
            return new();
        }
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Saves the recent projects.</summary>
    /// <param name="recentProjects">The new contents of the recent projects file.</param>
    private static void SetRecentProjects(IEnumerable<string> recentProjects)
    {
        Directory.CreateDirectory(AppDataDirectory);
        File.WriteAllText(RecentProjectsFile, JsonSerializer.Serialize(recentProjects));
    }

    /// <summary>Adds a project to the recent projects file.</summary>
    /// <param name="project">The project to add to the recent projects.</param>
    private static void AddProjectToRecentProjects(string project)
    {
        var recentProjects = GetRecentProjects();
        recentProjects.Insert(0, project);
        SetRecentProjects(recentProjects.Distinct());
    }
}
