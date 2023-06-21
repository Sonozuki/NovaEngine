namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="AssetsPanel"/>.</summary>
public sealed class AssetsViewModel : DependencyObject
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when <see cref="SelectedDirectoryInfo"/> is changed.</summary>
    public event Action SelectedDirectoryInfoChanged;


    /*********
    ** Fields
    *********/
    /// <summary>The path info of the currently selected directory.</summary>
    private PathInfo _SelectedDirectoryInfo;

    /// <summary>The scale of the icons.</summary>
    public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(AssetsViewModel));


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
            SelectedDirectoryInfoChanged?.Invoke();
        }
    }

    /// <summary>The scale of the icons.</summary>
    public double Scale
    {
        get => (double)GetValue(ScaleProperty);
        set => SetValue(ScaleProperty, value);
    }

    /// <summary>The command to change the selected directory.</summary>
    public ICommand ChangeDirectoryCommand { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public AssetsViewModel()
    {
        SelectedDirectoryInfo = AssetManager.GetAssetPathInfo();
        Scale = .75;

        ChangeDirectoryCommand = new RelayCommand<string>(ChangeDirectory);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Changes the selected directory.</summary>
    /// <param name="newSelectedDirectory">The new selected directory.</param>
    private void ChangeDirectory(string newSelectedDirectory)
    {
        var directory = new DirectoryInfo(newSelectedDirectory);
        if (!directory.Exists)
        {
            // TODO: notify the directory wasn't found
            return;
        }

        SelectedDirectoryInfo = new(isDirectory: true, directory.FullName);
    }
}
