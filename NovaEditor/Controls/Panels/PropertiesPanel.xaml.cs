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
    }
}
