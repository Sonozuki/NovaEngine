namespace NovaEditor.Controls;

/// <summary>Represents a node in a directory tree for an <see cref="AssetsPanel"/>.</summary>
public partial class AssetDirectoryTreeItem : UserControl
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when <see cref="PathInfoProperty"/> is changed.</summary>
    public event EventHandler<PathInfoChangedEventArgs> PathInfoChanged;


    /*********
    ** Fields
    *********/
    /// <summary>The icon to use when the directory item is minimised.</summary>
    private readonly DirectoryMinimisedIcon MinimisedIcon = new();

    /// <summary>The icon to use when the directory item is maximised.</summary>
    private readonly DirectoryMaximisedIcon MaximisedIcon = new();

    /// <summary>The path info the directory item represents.</summary>
    public static readonly DependencyProperty PathInfoProperty = DependencyProperty.Register(nameof(PathInfo), typeof(PathInfo), typeof(AssetDirectoryTreeItem), new((sender, e) => (sender as AssetDirectoryTreeItem).PathInfoChanged?.Invoke(null, new((PathInfo)e.OldValue, (PathInfo)e.NewValue))));


    /*********
    ** Properties
    *********/
    /// <summary>The path info the directory item represents.</summary>
    public PathInfo PathInfo
    {
        get => (PathInfo)GetValue(PathInfoProperty);
        set => SetValue(PathInfoProperty, value);
    }

    /// <summary>The command to toggle the <see cref="PathInfo.IsExpanded"/> value of <see cref="PathInfo"/>.</summary>
    public ICommand ToggleIsExpandedCommand { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public AssetDirectoryTreeItem()
    {
        ToggleIsExpandedCommand = new RelayCommand(ToggleIsExpanded);

        PathInfoChanged += OnPathInfoChanged;

        InitializeComponent();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when <see cref="PathInfoChanged"/> is invoked.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPathInfoChanged(object sender, PathInfoChangedEventArgs e)
    {
        if (e.OldValue != null)
            e.OldValue.IsExpandedChanged -= UpdateIcon;

        if (e.NewValue != null)
        {
            e.NewValue.IsExpandedChanged += UpdateIcon;
            UpdateIcon();
        }
    }

    /// <summary>Toggles <see cref="PathInfo.IsExpanded"/> in <see cref="PathInfo"/>.</summary>
    private void ToggleIsExpanded()
    {
        PathInfo.IsExpanded = !PathInfo.IsExpanded;
        UpdateIcon();
    }

    /// <summary>Updates the minimised/maximised icon.</summary>
    private void UpdateIcon() => IconPresenter.Content = PathInfo.IsExpanded ? MaximisedIcon : MinimisedIcon;
}
