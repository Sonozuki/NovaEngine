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

        UpdateContent();
        ViewModel.SelectedDirectoryInfoChanged += UpdateContent;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Updates the content of the panel.</summary>
    private void UpdateContent()
    {
        RootUniformGrid.Children.Clear();

        foreach (var children in ViewModel.SelectedDirectoryInfo.Children)
            RootUniformGrid.Children.Add(children.IsDirectory ? new AssetFolder(children.FullName) : new AssetFile(children.Name));
    }
}
