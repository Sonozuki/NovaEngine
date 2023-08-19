namespace NovaEditor.Controls;

/// <summary>Represents the title bar used in <see cref="MainWindow"/>.</summary>
public partial class MainWindowTitleBar : UserControl
{
    /*********
    ** Fields
    *********/
    /// <summary>The window the title bar belongs to.</summary>
    private Window Window;


    /*********
    ** Properties
    *********/
    /// <summary>The view model of the window.</summary>
    public MainWindowTitleBarViewModel ViewModel { get; private set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public MainWindowTitleBar()
    {
        InitializeComponent();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when the title bar is loaded.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    /// <remarks>This can't be put in the constructor as Window.GetWindow(this) returns <see langword="null"/> in the constructor.</remarks>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Window = Window.GetWindow(this);
        ViewModel = new(Window);
        ViewModel.MaximiseStateChanged += OnMaximisedStateChanged;
        DataContext = ViewModel;

        OnMaximisedStateChanged(); // sets the default state
    }

    /// <summary>Invoked when the maximise state of the containing window has changed.</summary>
    private void OnMaximisedStateChanged()
    {
        if (Window.WindowState == WindowState.Maximized)
            MaximiseIcon.ChangeIconToRestore();
        else
            MaximiseIcon.ChangeIconToMaximise();
    }
}
