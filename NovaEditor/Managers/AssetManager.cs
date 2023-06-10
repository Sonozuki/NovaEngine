namespace NovaEditor.Managers;

/// <summary>Manages the assets.</summary>
internal static class AssetManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves the root content directory info.</summary>
    /// <returns>The root content directory info.</returns>
    public static PathInfo GetAssetPathInfo() => new(true, NovaEngine.Constants.ContentDirectory);
}
