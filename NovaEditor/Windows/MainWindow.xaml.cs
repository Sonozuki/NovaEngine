namespace NovaEditor.Windows;

/// <summary>Represents the main window of the application.</summary>
public partial class MainWindow : Window
{
    /*********
    ** Properties
    *********/
    /// <summary>The view model of the window.</summary>
    public MainWindowViewModel ViewModel { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    internal MainWindow()
    {
        DataContext = ViewModel;
        InitializeComponent();
    }
}
