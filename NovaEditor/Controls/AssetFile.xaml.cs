namespace NovaEditor.Controls;

/// <summary>Represents a file in <see cref="AssetsPanel"/>.</summary>
public partial class AssetFile : UserControl
{
    /*********
    ** Fields
    *********/
    /// <summary>The full name of the file.</summary>
    public static readonly DependencyProperty FileFullNameProperty = DependencyProperty.Register(nameof(FileFullName), typeof(string), typeof(AssetFile));

    /// <summary>The name of the file.</summary>
    public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(nameof(FileName), typeof(string), typeof(AssetFile));


    /*********
    ** Properties
    *********/
    /// <summary>The full name of the file.</summary>
    public string FileFullName
    {
        get => (string)GetValue(FileFullNameProperty);
        set => SetValue(FileFullNameProperty, value);
    }

    /// <summary>The name of the file.</summary>
    public string FileName
    {
        get => (string)GetValue(FileNameProperty);
        set => SetValue(FileNameProperty, value);
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public AssetFile()
    {
        InitializeComponent();
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="fileFullName">The full name of the file.</param>
    public AssetFile(string fileFullName)
        : this()
    {
        FileFullName = fileFullName;
        FileName = new FileInfo(fileFullName).Name;
    }
}
