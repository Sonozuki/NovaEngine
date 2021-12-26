namespace NovaEngine
{
    /// <summary>Contains application constants.</summary>
    public static class Constants
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The name of the engine.</summary>
        public static string EngineName => "NovaEngine";

        /// <summary>The current version of the engine.</summary>
        public static string EngineVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "[version_unknown]";

        /// <summary>The directory for storing files such as logs and settings.</summary>
        public static string InfoDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", Program.Name);

        /// <summary>The rendering settings file.</summary>
        public static string RenderingSettingsFilePath => Path.Combine(InfoDirectory, "Settings", "RenderingSettings.json");

        /// <summary>The invalid rendering settings file.</summary>
        /// <remarks>If <see cref="RenderingSettingsFilePath"/> failed to deserialise, it'll get moved here. This is so the existing settings can be recovered if a manual edit was invalid.</remarks>
        public static string InvalidRenderingSettingsFilePath => Path.Combine(InfoDirectory, "Settings", "RenderingSettings_Invalid.json");

        /// <summary>The path where the log file is located.</summary>
        public static string LogFilePath => Path.Combine(InfoDirectory, "Logs", "LatestLog.txt");

        /// <summary>The root content directory.</summary>
        public static string ContentDirectory => Path.Combine(Environment.CurrentDirectory, "Data");

        /// <summary>The scene directory, relative to <see cref="Constants.ContentDirectory"/>.</summary>
        public static string RelativeSceneDirectory => "Scenes";

        /// <summary>The scene directory.</summary>
        public static string SceneDirectory => Path.Combine(ContentDirectory, RelativeSceneDirectory);

        /// <summary>The file extension for content files.</summary>
        public static string ContentFileExtension => ".nova";

        /// <summary>The file extension for scene files.</summary>
        public static string SceneFileExtension => ".novascene";
    }
}
