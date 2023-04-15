namespace NovaEditor;

/// <summary>Contains application constants.</summary>
internal static class Constants
{
    /*********
    ** Properties
    *********/
    /// <summary>The directory all app data is stored in.</summary>
    public static string AppDataDirectory { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NovaEditor");

    /// <summary>The json file containing the recent projects.</summary>
    public static string RecentProjectsFile { get; } = Path.Combine(AppDataDirectory, "recentProjects.json");

    /// <summary>The json file containing the serialised workspace.</summary>
    public static string WorkspaceFile { get; } = Path.Combine(AppDataDirectory, "workspace.json");
}
