namespace NovaEngine;

/// <summary>Contains application constants.</summary>
public static class Constants
{
    /*********
    ** Properties
    *********/
    /// <summary>The name of the engine.</summary>
    public static string EngineName => "NovaEngine";

    /// <summary>The current version of the engine.</summary>
    public static string EngineVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "[version_unknown]";

    /// <summary>The directory for storing files such as logs and settings.</summary>
    public static string InfoDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", Program.Name);

    /// <summary>The directory for storing settings files.</summary>
    public static string SettingsDirectory => Path.Combine(InfoDirectory, "Settings");

    /// <summary>The path where the log file is located.</summary>
    public static string LogFilePath => Path.Combine(InfoDirectory, "Logs", "LatestLog.txt");

    /// <summary>The rendering settings file.</summary>
    public static string RenderingSettingsFilePath => Path.Combine(SettingsDirectory, "RenderingSettings.json");

    /// <summary>The invalid rendering settings file.</summary>
    /// <remarks>If <see cref="RenderingSettingsFilePath"/> failed to deserialise, it'll get moved here. This is so the existing settings can be recovered if a manual edit was invalid.</remarks>
    public static string InvalidRenderingSettingsFilePath => Path.Combine(SettingsDirectory, "RenderingSettings_Invalid_{time}.json");

    /// <summary>The console settings file.</summary>
    public static string ConsoleSettingsFilePath => Path.Combine(SettingsDirectory, "ConsoleSettings.json");

    /// <summary>The invalid console settings file.</summary>
    /// <remarks>If <see cref="ConsoleSettingsFilePath"/> failed to deserialise, it'll get moved here. This is so the existing settings can be recovered if a manual edit was invalid.</remarks>
    public static string InvalidConsoleSettingsFilePath => Path.Combine(SettingsDirectory, "ConsoleSettings_Invalid_{time}.json");

    /// <summary>The root content directory.</summary>
    public static string ContentDirectory => Path.Combine(Environment.CurrentDirectory, "Data");

    /// <summary>The scene directory.</summary>
    public static string SceneDirectory => Path.Combine(ContentDirectory, RelativeSceneDirectory);

    /// <summary>The scene directory, relative to <see cref="Constants.ContentDirectory"/>.</summary>
    public static string RelativeSceneDirectory => "Scenes";

    /// <summary>The file extension for content files.</summary>
    public static string ContentFileExtension => ".nova";

    /// <summary>The file extension for scene files.</summary>
    public static string SceneFileExtension => ".novascene";
}
