namespace NovaEditor.Controls.Panels;

/// <summary>Represents the panel that displays the scene view.</summary>
public sealed partial class SceneViewPanel : EditorPanelBase, IDisposable
{
    /*********
    ** Fields
    *********/
    /// <summary>Whether the panel has been disposed.</summary>
    private bool IsDisposed;

    /// <summary>The NovaEngine host.</summary>
    private EngineHost EngineHost;


    /*********
    ** Constructors
    *********/
    /// <summary>Destructs the instance.</summary>
    ~SceneViewPanel() => Dispose(false);

    /// <summary>Constructs an instance.</summary>
    /// <param name="settings">The persistent settings of the panel.</param>
    public SceneViewPanel(NotificationDictionary<string, string> settings)
        : base(settings)
    {
        InitializeComponent();
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the panel.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the panel.</summary>
    /// <param name="disposing">Whether the panel is being disposed deterministically.</param>
    private void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
            EngineHost?.Dispose();

        IsDisposed = true;
    }

    /// <summary>Invoked when the panel is loaded.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        EngineHost ??= new(RootBorder.ActualWidth, RootBorder.ActualHeight);
        RootBorder.Child = EngineHost;
    }
}
