namespace NovaEditor.Controls.Panels;

/// <summary>Represents the panel used for creating a project.</summary>
public partial class ProjectCreationPanel : EditorPanelBase
{
    /*********
    ** Properties
    *********/
    /// <summary>The view model of the panel.</summary>
    public ProjectCreationViewModel ViewModel { get; } = new();


    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    public ProjectCreationPanel()
        : base(null)
    {
        DataContext = ViewModel;
        InitializeComponent();
    }
}
