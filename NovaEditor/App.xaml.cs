namespace NovaEditor;

/// <summary>The application.</summary>
public partial class App : Application
{
    /*********
    ** Fields
    *********/
    /// <summary>The project selection window.</summary>
    private ProjectSelectionWindow ProjectSelectionWindow;

    /// <summary>The main window.</summary>
    private new MainWindow MainWindow;


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when the application has been loaded.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        ProjectManager.CurrentProjectChanged += OnCurrentProjectChanged;
        ProjectManager.CurrentProject = null;
    }

    /// <summary>Invoked when the current loaded project changes.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnCurrentProjectChanged(object sender, CurrentProjectChangedEventArgs e)
    {
        if (e.NewProject == null)
        {
            ProjectSelectionWindow = new();
            ProjectSelectionWindow.Show();

            MainWindow?.Close();
        }
        else
        {
            MainWindow = new();
            MainWindow.Show();

            ProjectSelectionWindow?.Close();
        }
    }
}
