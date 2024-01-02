namespace NovaEditor.Controls.Panels;

/// <summary>Represents the panel used for selecting a project to load.</summary>
public partial class ProjectSelectionPanel : EditorPanelBase
{
    /*********
    ** Properties
    *********/
    /// <summary>The view model of the panel.</summary>
    public ProjectSelectionViewModel ViewModel { get; } = new();


    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    public ProjectSelectionPanel()
        : base(null)
    {
        DataContext = ViewModel;
        InitializeComponent();
    }
}
