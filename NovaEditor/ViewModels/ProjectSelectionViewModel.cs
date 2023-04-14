namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="ProjectSelectionWindow"/>.</summary>
internal sealed class ProjectSelectionViewModel
{
    /*********
    ** Properties
    *********/
    /// <summary>The recently opened projects.</summary>
    public ObservableCollection<string> RecentProjects { get; } = new();

    /// <summary>The command used to open a file dialog to pick a project.</summary>
    public ICommand OpenProjectCommand { get; set; }

    /// <summary>The command used to create a new project.</summary>
    public ICommand CreateProjectCommand { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public ProjectSelectionViewModel()
    {
        OpenProjectCommand = new RelayCommand(OpenProject);
        CreateProjectCommand = new RelayCommand(CreateProject);

        var recentProjects = ProjectManager.GetRecentProjects();
        foreach (var recentProject in recentProjects)
            RecentProjects.Add(recentProject);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Opens a file dialog to pick a project.</summary>
    private void OpenProject()
    {
        var dialog = new OpenFileDialog
        {
            DefaultExt = ".novaproject",
            Filter = "Nova Project|*.novaproject"
        };

        if (dialog.ShowDialog() == true)
            ProjectManager.CurrentProject = dialog.FileName;
    }

    /// <summary>Changes view to create a project.</summary>
    private void CreateProject()
    {
        // TODO: implement
    }
}
