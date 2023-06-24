namespace NovaEditor.Controls;

/// <summary>Represents the panel used for managing assets.</summary>
public partial class AssetsPanel : EditorPanelBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The scale of the file and folder icons in the panel.</summary>
    public static readonly DependencyProperty IconScaleProperty = DependencyProperty.Register(nameof(IconScale), typeof(double), typeof(AssetsPanel));


    /*********
    ** Properties
    *********/
    /// <summary>The view model of the panel.</summary>
    public AssetsViewModel ViewModel { get; } = new();

    /// <summary>The scale of the file and folder icons in the panel.</summary>
    public double IconScale
    {
        get => (double)GetValue(IconScaleProperty);
        set => SetValue(IconScaleProperty, value);
    }


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
        ViewModel.NumberOfColumnsChanged += UpdateIconScale;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Updates the content of the panel.</summary>
    private void UpdateContent()
    {
        RootItemsControl.Items.Clear();
        foreach (var children in ViewModel.SelectedDirectoryInfo.Children)
            RootItemsControl.Items.Add(children.IsDirectory ? new AssetFolder(children.FullName) : new AssetFile(children.Name));
    }

    /// <summary>Updates the icon scale based on the current number of columns.</summary>
    private void UpdateIconScale()
    {
        var columnWidth = RootItemsControl.ActualWidth / ViewModel.NumberOfColumns - 4; // -4 is 2x the margin applied to each cell in the uniform grid
        var iconWidth = 100; // this is the folder icon width (as it's wider that the file one) // TODO: this shouldn't be hardcoded

        IconScale = columnWidth / iconWidth;
    }
}
