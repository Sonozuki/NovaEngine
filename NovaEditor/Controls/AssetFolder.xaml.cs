namespace NovaEditor.Controls;

/// <summary>Represents a folder in <see cref="AssetsPanel"/>.</summary>
public partial class AssetFolder : UserControl
{
    /*********
    ** Fields
    *********/
    /// <summary>The full name of the folder.</summary>
    public static readonly DependencyProperty FolderFullNameProperty = DependencyProperty.Register(nameof(FolderFullName), typeof(string), typeof(AssetFolder));

    /// <summary>The name of the folder.</summary>
    public static readonly DependencyProperty FolderNameProperty = DependencyProperty.Register(nameof(FolderName), typeof(string), typeof(AssetFolder));


    /*********
    ** Properties
    *********/
    /// <summary>The full name of the folder.</summary>
    public string FolderFullName
    {
        get => (string)GetValue(FolderFullNameProperty);
        set => SetValue(FolderFullNameProperty, value);
    }

    /// <summary>The name of the folder.</summary>
    public string FolderName
    {
        get => (string)GetValue(FolderNameProperty);
        set => SetValue(FolderNameProperty, value);
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public AssetFolder()
    {
        InitializeComponent();
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="folderFullName">The full name of the folder.</param>
    public AssetFolder(string folderFullName)
        : this()
    {
        FolderFullName = folderFullName;
        FolderName = new DirectoryInfo(folderFullName).Name;
    }
}
