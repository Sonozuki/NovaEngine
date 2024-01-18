namespace NovaEditor.Controls.Panels;

/// <summary>Represents the panel used for editing properties of game objects.</summary>
public partial class PropertiesPanel : EditorPanelBase
{
    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="settings">The persistent settings of the panel.</param>
    public PropertiesPanel(NotificationDictionary<string, string> settings)
        : base(settings)
    {
        InitializeComponent();

        GUI.GUIUpdated += OnGUIUpdated;
    }

    /// <summary>Invoked when the controls in <see cref="GUI"/> has changed.</summary>
    private void OnGUIUpdated()
    {
        RootStackPanel.Children.Clear();

        foreach (var control in GUI.CreateControls())
            RootStackPanel.Children.Add(control);
    }
}
