namespace NovaEditor.Controls;

/// <summary>Represents the panel used for managing assets.</summary>
public partial class AssetsPanel : EditorPanelBase
{
    /*********
    ** Properties
    *********/
    /// <summary>The view model of the panel.</summary>
    public AssetsViewModel ViewModel { get; } = new();


    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    public AssetsPanel()
    {
        DataContext = ViewModel;
        InitializeComponent();
    }
}
