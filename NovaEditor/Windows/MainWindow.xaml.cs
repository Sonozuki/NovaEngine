namespace NovaEditor.Windows;

/// <summary>Represents the main window of the application.</summary>
public partial class MainWindow : Window
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public MainWindow()
    {
        InitializeComponent();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when the window is loaded.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        WorkspaceManager.LoadWorkspace();
    }
}
