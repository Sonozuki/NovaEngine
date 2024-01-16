namespace NovaEditor.Managers;

/// <summary>Manages nova projects.</summary>
internal class ProjectManager : DependencyObject
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when the current project is changed.</summary>
    public static event EventHandler<CurrentProjectChangedEventArgs> CurrentProjectChanged;


    /*********
    ** Fields
    *********/
    /// <summary>The currently selected game object in the project.</summary>
    public static readonly DependencyProperty SelectedGameObjectProperty = DependencyProperty.Register(nameof(SelectedGameObject), typeof(GameObject), typeof(ProjectManager));

    /// <summary>The current loaded project.</summary>
    private static string _CurrentProject;


    /*********
    ** Properties
    *********/
    /// <summary>The singleton instance of <see cref="ProjectManager"/>.</summary>
    public static ProjectManager Instance { get; } = new();

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

    /// <summary>The currently selected game object in the project.</summary>
    public static GameObject SelectedGameObject
    {
        get => Instance.InternalSelectedGameObject;
        set => Instance.InternalSelectedGameObject = value;
    }

    /// <summary>The currently selected game object in the project.</summary>
    private GameObject InternalSelectedGameObject
    {
        get => (GameObject)GetValue(SelectedGameObjectProperty);
        set => SetValue(SelectedGameObjectProperty, value);
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    private ProjectManager() { }


    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves the recent projects.</summary>
    /// <returns>The recent projects.</returns>
    public static List<RecentProject> GetRecentProjects()
    {
        if (!File.Exists(Constants.RecentProjectsFile))
            return new();

        try
        {
            return JsonSerializer.Deserialize<List<RecentProject>>(File.ReadAllText(Constants.RecentProjectsFile));
        }
        catch
        {
            File.Delete(Constants.RecentProjectsFile);
            return new();
        }
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Saves the recent projects.</summary>
    /// <param name="recentProjects">The new contents of the recent projects file.</param>
    private static void SaveRecentProjects(IEnumerable<RecentProject> recentProjects)
    {
        Directory.CreateDirectory(Constants.AppDataDirectory);
        File.WriteAllText(Constants.RecentProjectsFile, JsonSerializer.Serialize(recentProjects));
    }

    /// <summary>Adds a project to the recent projects file.</summary>
    /// <param name="project">The project to add to the recent projects.</param>
    private static void AddProjectToRecentProjects(string project)
    {
        var projectFileInfo = new FileInfo(project);
        var recentProject = new RecentProject(projectFileInfo.Name, projectFileInfo.DirectoryName, DateTime.UtcNow);

        var recentProjects = GetRecentProjects();
        recentProjects.Insert(0, recentProject);
        SaveRecentProjects(recentProjects.Distinct());
    }
}
