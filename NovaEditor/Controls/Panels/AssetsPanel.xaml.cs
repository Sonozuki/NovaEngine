namespace NovaEditor.Controls.Panels;

/// <summary>Represents the panel used for managing assets.</summary>
public partial class AssetsPanel : EditorPanelBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The scale of the file and folder icons in the panel.</summary>
    public static readonly DependencyProperty IconScaleProperty = DependencyProperty.Register(nameof(IconScale), typeof(double), typeof(AssetsPanel));

    /// <summary>The height of the file and folder icons in the panel.</summary>
    public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(AssetsPanel));


    /*********
    ** Properties
    *********/
    /// <summary>The view model of the panel.</summary>
    public AssetsViewModel ViewModel { get; }

    /// <summary>The scale of the file and folder icons in the panel.</summary>
    public double IconScale
    {
        get => (double)GetValue(IconScaleProperty);
        set => SetValue(IconScaleProperty, value);
    }

    /// <summary>The height of the file and folder icons in the panel.</summary>
    public double IconHeight
    {
        get => (double)GetValue(IconHeightProperty);
        set => SetValue(IconHeightProperty, value);
    }


    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="settings">The persistent settings of the panel.</param>
    public AssetsPanel(NotificationDictionary<string, string> settings)
        : base(settings)
    {
        ViewModel = new(Settings);
        DataContext = ViewModel;
        InitializeComponent();

        ViewModel.NumberOfColumnsChanged += UpdateIconScale;
        ViewModel.RootAssetsPathChanged += UpdateTreeView;

        UpdateTreeView();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Updates the icon scale based on the current number of columns.</summary>
    private void UpdateIconScale()
    {
        // TODO: these shouldn't be hardcoded
        const int iconWidth = 100;
        const int iconHeight = 70;

        var columnWidth = RootItemsControl.ActualWidth / ViewModel.NumberOfColumns - 4; // -4 is 2x the margin applied to each cell in the uniform grid

        IconScale = columnWidth / iconWidth;
        IconHeight = iconHeight * IconScale;
    }

    /// <summary>Creates a <see cref="TreeViewItem"/> from a path info.</summary>
    /// <param name="pathInfo">The path info to create the <see cref="TreeViewItem"/> from.</param>
    /// <returns>The <see cref="TreeViewItem"/> with the specified name and children.</returns>
    private TreeViewItem CreateTreeViewItem(PathInfo pathInfo)
    {
        var treeViewItem = new TreeViewItem
        {
            Header = pathInfo.Name,
            IsExpanded = pathInfo.IsExpanded
        };
        treeViewItem.Expanded += (_, _) => pathInfo.IsExpanded = true;
        treeViewItem.Collapsed += (_, _) => pathInfo.IsExpanded = false;

        foreach (var childPath in pathInfo.Children)
            treeViewItem.Items.Add(CreateTreeViewItem(childPath));

        return treeViewItem;
    }

    /// <summary>Updates the tree view.</summary>
    private void UpdateTreeView()
    {
        RootTreeView.Items.Clear();
        foreach (var childPath in ViewModel.RootAssetsPath.Children)
            RootTreeView.Items.Add(CreateTreeViewItem(childPath));
    }
}
