namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="AssetsPanel"/>.</summary>
public sealed class AssetsViewModel : DependencyObject, IDisposable
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when <see cref="NumberOfColumns"/> is changed.</summary>
    public event Action NumberOfColumnsChanged;

    /// <summary>Invoked when <see cref="RootAssetsPath"/> is changed.</summary>
    public event Action RootAssetsPathChanged;


    /*********
    ** Fields
    *********/
    /// <summary>The number of columns in the assets panel.</summary>
    public static readonly DependencyProperty NumberOfColumnsProperty = DependencyProperty.Register(nameof(NumberOfColumns), typeof(int), typeof(AssetsViewModel), new((sender, _) => (sender as AssetsViewModel).NumberOfColumnsChanged?.Invoke()));

    /// <summary>The root path info of the assets directory.</summary>
    public static readonly DependencyProperty RootAssetsPathProperty = DependencyProperty.Register(nameof(RootAssetsPath), typeof(PathInfo), typeof(AssetsViewModel), new((sender, _) => (sender as AssetsViewModel).RootAssetsPathChanged?.Invoke()));

    /// <summary>The path info of the currently selected directory.</summary>
    public static readonly DependencyProperty SelectedDirectoryInfoProperty = DependencyProperty.Register(nameof(SelectedDirectoryInfo), typeof(PathInfo), typeof(AssetsViewModel));

    /// <summary>The path of the selected item in the assets panel.</summary>
    public static readonly DependencyProperty SelectedPathProperty = DependencyProperty.Register(nameof(SelectedPath), typeof(string), typeof(AssetsViewModel));

    /// <summary>Whether the view model has been disposed.</summary>
    private bool IsDisposed;

    /// <summary>The file system watcher for the assets panel.</summary>
    private FileSystemWatcher FileSystemWatcher;


    /*********
    ** Properties
    *********/
    /// <summary>The root path info of the assets directory.</summary>
    public PathInfo RootAssetsPath
    {
        get => (PathInfo)GetValue(RootAssetsPathProperty);
        set => SetValue(RootAssetsPathProperty, value);
    }

    /// <summary>The path info of the currently selected directory.</summary>
    public PathInfo SelectedDirectoryInfo
    {
        get => (PathInfo)GetValue(SelectedDirectoryInfoProperty);
        set
        {
            SetValue(SelectedDirectoryInfoProperty, value);
            SelectedPath = "";
        }
    }

    /// <summary>The number of columns in the assets panel.</summary>
    public int NumberOfColumns
    {
        get => (int)GetValue(NumberOfColumnsProperty);
        set => SetValue(NumberOfColumnsProperty, value);
    }

    /// <summary>The path of the selected item in the assets panel.</summary>
    public string SelectedPath
    {
        get => (string)GetValue(SelectedPathProperty);
        set => SetValue(SelectedPathProperty, value);
    }

    /// <summary>The command to change the selected path.</summary>
    public ICommand SelectPathCommand { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public AssetsViewModel()
    {
        UpdateRootAssetsPath();

        SelectPathCommand = new RelayCommand<string>(SelectDirectory);

        FileSystemWatcher = new()
        {
            Path = RootAssetsPath.FullName,
            IncludeSubdirectories = true,
            EnableRaisingEvents = true
        };
        FileSystemWatcher.Created += (_, _) => UpdateRootAssetsPath();
        FileSystemWatcher.Renamed += (_, _) => UpdateRootAssetsPath();
        FileSystemWatcher.Deleted += (_, _) => UpdateRootAssetsPath();
    }

    /// <summary>Updates the <see cref="RootAssetsPath"/> <see cref="SelectedDirectoryInfo"/>.</summary>
    public void UpdateRootAssetsPath() =>
        Dispatcher.Invoke(() => // TODO: only temp, instead of recreating it should just apply the changes (or at the very least copy over IsExpanded)
        {
            RootAssetsPath = AssetManager.GetAssetPathInfo();
            SelectedDirectoryInfo = RootAssetsPath;
        });


    /*********
    ** Public Methods
    *********/
    /// <summary>Cleans up unmanaged resources in the game object.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Changes the selected path.</summary>
    /// <param name="newSelectedPath">The new selected path.</param>
    private void SelectDirectory(string newSelectedPath)
    {
        if (string.IsNullOrEmpty(newSelectedPath))
            return;

        var newRelativeSelectedPath = Path.GetRelativePath(NovaEngine.Constants.ContentDirectory, newSelectedPath);
        if (SelectedPath != newRelativeSelectedPath)
        {
            // the directory or path has been clicked for the first time, just set the selected path string
            SelectedPath = newRelativeSelectedPath;
            return;
        }

        // the icon has been double clicked
        var directory = new DirectoryInfo(newSelectedPath);
        if (!directory.Exists)
            return;

        SelectedDirectoryInfo = new(isDirectory: true, directory.FullName);
    }

    /// <summary>Cleans up unmanaged resources in the component.</summary>
    /// <param name="disposing">Whether the component is being disposed deterministically.</param>
    private void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;
        IsDisposed = true;

        if (disposing)
        {
            FileSystemWatcher.Dispose();

            NumberOfColumnsChanged = null;
            RootAssetsPathChanged = null;
        }
    }
}
