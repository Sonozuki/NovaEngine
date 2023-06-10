namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="AssetsPanel"/>.</summary>
public sealed class AssetsViewModel
{
    /*********
    ** Properties
    *********/
    /// <summary>The path info of the currently selected directory.</summary>
    public PathInfo SelectedDirectoryInfo { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public AssetsViewModel()
    {
        SelectedDirectoryInfo = AssetManager.GetAssetPathInfo();
    }
}
