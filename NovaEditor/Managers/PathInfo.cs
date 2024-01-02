namespace NovaEditor.Managers;

/// <summary>Represents a file or directory in a directory tree.</summary>
public class PathInfo : DependencyObject
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when <see cref="IsExpanded"/> is changed.</summary>
    public event Action IsExpandedChanged;


    /*********
    ** Fields
    *********/
    /// <summary>Whether the path is expanded.</summary>
    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(PathInfo), new((sender, _) => (sender as PathInfo).IsExpandedChanged?.Invoke()));


    /*********
    ** Properties
    *********/
    /// <summary>The name of the path.</summary>
    public string Name { get; }

    /// <summary>The full name of the path.</summary>
    public string FullName { get; }

    /// <summary>Whether the path is a directory.</summary>
    public bool IsDirectory { get; }

    /// <summary>Whether the path is expanded.</summary>
    /// <remarks>This is only used if the path is a directory.</remarks>
    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    /// <summary>Whether the path has child paths.</summary>
    public bool HasChildren => Children.Any();

    /// <summary>The child paths of the path.</summary>
    public ReadOnlyObservableCollection<PathInfo> Children { get; private set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="isDirectory">Whether the path is a directory.</param>
    /// <param name="fullName">The full name of the path.</param>
    internal PathInfo(bool isDirectory, string fullName)
    {
        IsDirectory = isDirectory;
        FullName = fullName;
        Name = Path.GetFileName(fullName);

        CalculateChildren();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Calculates the children of the path.</summary>
    private void CalculateChildren()
    {
        var children = new List<PathInfo>();

        if (IsDirectory)
        {
            var directoryInfo = new DirectoryInfo(FullName);

            foreach (var directory in directoryInfo.GetDirectories())
                children.Add(new(isDirectory: true, directory.FullName));

            foreach (var file in directoryInfo.GetFiles())
                children.Add(new(isDirectory: false, file.FullName));
        }

        Children = new(new(children));
    }
}
