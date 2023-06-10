namespace NovaEditor.Controls;

/// <summary>Represents a folder in <see cref="AssetsPanel"/>.</summary>
public partial class AssetFolder : UserControl
{
    /*********
    ** Fields
    *********/
    /// <summary>The name of the folder.</summary>
    public static readonly DependencyProperty FolderNameProperty = DependencyProperty.Register(nameof(Name), typeof(string), typeof(AssetFolder));


    /*********
    ** Properties
    *********/
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
    /// <param name="folderName">The name of the folder.</param>
    public AssetFolder(string folderName)
        : this()
    {
        FolderName = folderName;
    }
}
