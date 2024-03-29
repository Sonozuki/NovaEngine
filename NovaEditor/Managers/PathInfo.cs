﻿namespace NovaEditor.Managers;

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
    /// <param name="expandedDirectories">The full names of the directories (including children directories) whose path infos should have <see cref="IsExpanded"/> initialised to <see langword="true"/>.</param>
    internal PathInfo(bool isDirectory, string fullName, List<string> expandedDirectories)
    {
        IsDirectory = isDirectory;
        FullName = fullName;
        IsExpanded = expandedDirectories.Contains(fullName);
        Name = Path.GetFileName(fullName);

        CalculateChildren(expandedDirectories);
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves <paramref name="rootPathInfo"/> as well as all recursive child PathInfos.</summary>
    /// <param name="rootPathInfo">The root PathInfo of the tree to flatten.</param>
    /// <returns><paramref name="rootPathInfo"/> as well as all recursive child PathInfos.</returns>
    public static ImmutableArray<PathInfo> Flatten(PathInfo rootPathInfo)
    {
        ArgumentNullException.ThrowIfNull(rootPathInfo);

        var allPathInfos = new List<PathInfo>();
        Flatten(allPathInfos, rootPathInfo);
        return allPathInfos.ToImmutableArray();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Calculates the children of the path.</summary>
    /// <param name="expandedDirectories">The full names of the directories (including children directories) whose path infos should have <see cref="IsExpanded"/> initialised to <see langword="true"/>.</param>
    private void CalculateChildren(List<string> expandedDirectories)
    {
        var children = new List<PathInfo>();

        if (IsDirectory)
        {
            var directoryInfo = new DirectoryInfo(FullName);

            foreach (var directory in directoryInfo.GetDirectories())
                children.Add(new(isDirectory: true, directory.FullName, expandedDirectories));

            foreach (var file in directoryInfo.GetFiles())
                children.Add(new(isDirectory: false, file.FullName, expandedDirectories));
        }

        Children = new(new(children));
    }

    /// <summary>Adds <paramref name="pathInfo"/> and all recursive children to <paramref name="allPathInfos"/>.</summary>
    /// <param name="allPathInfos">The collection that <paramref name="pathInfo"/> and all recursive children should get added to.</param>
    /// <param name="pathInfo">The node to start flattening from.</param>
    private static void Flatten(List<PathInfo> allPathInfos, PathInfo pathInfo)
    {
        allPathInfos.Add(pathInfo);

        foreach (var child in pathInfo.Children)
            if (!child.IsDirectory)
                allPathInfos.Add(child);
            else
                Flatten(allPathInfos, child);
    }
}
