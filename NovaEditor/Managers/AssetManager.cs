namespace NovaEditor.Managers;

/// <summary>Manages the assets.</summary>
internal static class AssetManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves the root content directory info.</summary>
    /// <param name="oldAssetsPath">The old assets path info.<br/>This is used for copying over the <see cref="PathInfo.IsExpanded"/> values.</param>
    /// <returns>The root content directory info.</returns>
    public static PathInfo GetAssetPathInfo(PathInfo? oldAssetsPath)
    {
        var expandedDirectories = new List<string>();
        if (oldAssetsPath != null)
        {
            var allPathInfos = PathInfo.Flatten(oldAssetsPath);
            expandedDirectories.AddRange(allPathInfos.Where(pathInfo => pathInfo.IsExpanded).Select(pathInfo => pathInfo.FullName));
        }

        return new(true, NovaEngine.Constants.ContentDirectory, expandedDirectories);
    }

    /// <summary>Retrieves a (recursive) child PathInfo with a specified full name from a root PathInfo</summary>
    /// <param name="rootPathInfo">The PathInfo to recursively search for the PathInfo with a specified name.</param>
    /// <param name="pathFullName">The full name of the PathInfo to retrieve.</param>
    /// <returns>The PathInfo with a full name of <paramref name="pathFullName"/>, if one could be found; otherwise, <see langword="null"/>.</returns>
    public static PathInfo GetPathInfoByPath(PathInfo rootPathInfo, string pathFullName)
    {
        // TODO: this could be cleaned up to traverse the root path info instead of flattening it
        var allPathInfos = PathInfo.Flatten(rootPathInfo);
        var pathInfoFullNames = allPathInfos.ToDictionary(pathInfo => pathInfo.FullName, pathInfo => pathInfo);

        return pathInfoFullNames.GetValueOrDefault(pathFullName);
    }
}
