using Microsoft.WindowsAPICodePack.Dialogs;
using NovaEngine.Globalisation;

namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="ProjectCreationPanel"/>.</summary>
internal sealed class ProjectCreationViewModel
{
    /*********
    ** Constants
    *********/
    /// <summary>The format of the default project name.</summary>
    private const string DefaultProjectNameTemplate = "NovaGame{0}";


    /*********
    ** Fields
    *********/
    /// <summary>The source code directory the project directory will be created in.</summary>
    private string _SourceLocation;


    /*********
    ** Properties
    *********/
    /// <summary>The name of the project.</summary>
    public string ProjectName { get; set; }

    /// <summary>The source code directory the project directory will be created in.</summary>
    public string SourceLocation
    {
        get => _SourceLocation;
        set
        {
            _SourceLocation = value;
            CalculateDefaultProjectName();
        }
    }

    /// <summary>The command used to create a new project.</summary>
    public ICommand ChooseSourceLocationCommand { get; set; }

    /// <summary>The command used to go back to the project selection panel.</summary>
    public ICommand BackCommand { get; set; }

    /// <summary>The command used to create the project using <see cref="ProjectName"/> and <see cref="SourceLocation"/>.</summary>
    public ICommand CreateCommand { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public ProjectCreationViewModel()
    {
        ChooseSourceLocationCommand = new RelayCommand(ChooseSourceLocation);
        BackCommand = new RelayCommand(Back);
        CreateCommand = new RelayCommand(Create);

        // TODO: save last used source location and use that instead, perhaps have a dropdown like VS that stores a history
        SourceLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "source", "repos");
    }

    /*********
    ** Private Methods
    *********/
    /// <summary>Opens of folder selection dialog to pick the source location.</summary>
    private void ChooseSourceLocation()
    {
        using var dialog = new CommonOpenFileDialog
        {
            IsFolderPicker = true
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            SourceLocation = dialog.FileName;
    }

    /// <summary>Closes the project creation panel and opens the project selection panel.</summary>
    private void Back() => WorkspaceManager.LoadProjectSelectionWorkspace();

    /// <summary>Creates the project using <see cref="ProjectName"/> and <see cref="SourceLocation"/>.</summary>
    private void Create()
    {
        var projectPath = Path.Combine(SourceLocation, ProjectName);
        Directory.CreateDirectory(projectPath);

        var projectFile = Path.Combine(projectPath, $"{ProjectName}.novaproject");
        File.Create(projectFile).Close();
        ProjectManager.CurrentProject = projectFile;
    }

    /// <summary>Calculates the default project name for the selected source location.</summary>
    private void CalculateDefaultProjectName()
    {
        if (!Directory.Exists(SourceLocation))
        {
            ProjectName = string.Format(G11n.Culture, DefaultProjectNameTemplate, 1);
            return;
        }

        var defaultProjectNumber = 1;
        while (true)
        {
            var projectName = string.Format(G11n.Culture, DefaultProjectNameTemplate, defaultProjectNumber++);
            var projectPath = Path.Combine(SourceLocation, projectName);
            if (!Directory.GetDirectories(SourceLocation).Any(path => path == projectPath))
            {
                ProjectName = projectName;
                break;
            }
        }
    }
}
