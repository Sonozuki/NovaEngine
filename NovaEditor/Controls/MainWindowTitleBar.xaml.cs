namespace NovaEditor.Controls;

/// <summary>Represents the title bar used in <see cref="MainWindow"/>.</summary>
public partial class MainWindowTitleBar : UserControl
{
    /*********
    ** Fields
    *********/
    /// <summary>The window the title bar belongs to.</summary>
    private Window Window;

    /// <summary>Whether the window state should be restored from maximised when the window is drag moved.</summary>
    private bool RestoreOnDragMove;


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

    /// <summary>Invoked when the left mouse button is pressed.</summary>
    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
            ViewModel.MaximiseCommand.Execute(null);
        else
        {
            RestoreOnDragMove = Window.WindowState == WindowState.Maximized;
            Window.DragMove();
        }
    }

    /// <summary>Invoked when the left mouse button is released.</summary>
    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) => RestoreOnDragMove = false;

    /// <summary>Invoked when the mouse is moved.</summary>
    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (!RestoreOnDragMove)
            return;
        RestoreOnDragMove = false;

        // TODO: currently moves the window so the mouse is in the middle, change this to be the same position as it's dragged from
        // (more specifically, the percent) e.g. if you drag when the mouse is 90% the way across the bar, the mouse should be 90% along
        // the bar in the restored size
        var point = PointToScreen(e.MouseDevice.GetPosition(Window));

        Window.Left = point.X - (Window.RestoreBounds.Width * 0.5);
        Window.Top = point.Y - 16;

        Window.WindowState = WindowState.Normal;

        Window.DragMove();
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
