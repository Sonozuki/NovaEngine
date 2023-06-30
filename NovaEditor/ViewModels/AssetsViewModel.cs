namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="AssetsPanel"/>.</summary>
public sealed class AssetsViewModel : DependencyObject
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when <see cref="SelectedDirectoryInfo"/> is changed.</summary>
    public event Action SelectedDirectoryInfoChanged;

    /// <summary>Invoked when <see cref="NumberOfColumns"/> is changed.</summary>
    public event Action NumberOfColumnsChanged;


    /*********
    ** Fields
    *********/
    /// <summary>The path info of the currently selected directory.</summary>
    private PathInfo _SelectedDirectoryInfo;

    /// <summary>The number of columns in the assets panel.</summary>
    public static readonly DependencyProperty NumberOfColumnsProperty = DependencyProperty.Register(nameof(NumberOfColumns), typeof(int), typeof(AssetsViewModel), new((sender, _) => (sender as AssetsViewModel).NumberOfColumnsChanged?.Invoke()));
    
    /// <summary>The path of the selected item in the assets panel.</summary>
    public static readonly DependencyProperty SelectedPathProperty = DependencyProperty.Register(nameof(SelectedPath), typeof(string), typeof(AssetsViewModel));


    /*********
    ** Properties
    *********/
    /// <summary>The path info of the currently selected directory.</summary>
    public PathInfo SelectedDirectoryInfo
    {
        get => _SelectedDirectoryInfo;
        set
        {
            _SelectedDirectoryInfo = value;
            SelectedPath = "";
            SelectedDirectoryInfoChanged?.Invoke();
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
        SelectedDirectoryInfo = AssetManager.GetAssetPathInfo();

        SelectPathCommand = new RelayCommand<string>(SelectDirectory);
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
}
