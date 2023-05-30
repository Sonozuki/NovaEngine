namespace NovaEditor;

/// <summary>The application.</summary>
public partial class App : Application
{
    /*********
    ** Properties
    *********/
    /// <summary>The main window.</summary>
    internal new MainWindow MainWindow { get; private set; }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when the application has been loaded.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        MainWindow = new();

        ProjectManager.CurrentProjectChanged += OnCurrentProjectChanged;
        ProjectManager.CurrentProject = null;

        MainWindow.Show();
    }

    /// <summary>Invoked when the current loaded project changes.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnCurrentProjectChanged(object sender, CurrentProjectChangedEventArgs e)
    {
        if (e.NewProject == null)
            WorkspaceManager.LoadProjectSelectionWorkspace();
        else
            WorkspaceManager.LoadWorkspace();
    }
}
