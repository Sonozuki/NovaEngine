namespace NovaEngine.Settings;

/// <summary>Represents the base for a settings file.</summary>
/// <typeparam name="TSelf">The type that is deriving <see cref="SettingsBase{TSelf}"/> (for example: <see cref="RenderingSettings"/> : SettingsBase&lt;<see cref="RenderingSettings"/>&gt;).</typeparam>
public abstract class SettingsBase<TSelf>
    where TSelf : SettingsBase<TSelf>, new()
{
    /*********
    ** Fields
    *********/
    /// <summary>The singleton instance for the settings file.</summary>
    private static TSelf? _Instance;


    /*********
    ** Accessors
    *********/
    /// <summary>The path of the settings file.</summary>
    protected abstract string Path { get; }

    /// <summary>The path of the settings file when deserialisation fails.</summary>
    protected abstract string InvalidPath { get; }

    /// <summary>The singleton instance for the settings file.</summary>
    public static TSelf Instance
    {
        get
        {
            if (_Instance != null)
                return _Instance;

            var emptySettings = new TSelf();

            // deserialise file if it already exists
            TSelf? instance = null;
            if (File.Exists(emptySettings.Path))
            {
                try { instance = JsonSerializer.Deserialize<TSelf>(File.ReadAllText(emptySettings.Path)); }
                catch (Exception ex)
                {
                    Logger.LogError($"Failed to deserialise a settings file {emptySettings.Path}, reverting to default settings. Technical details:\n{ex}");
                    Logger.LogError($"The invalid settings file has been moved to: \"{emptySettings.InvalidPath}\".");
                    File.Move(emptySettings.Path, emptySettings.InvalidPath, true);
                }
            }

            _Instance = instance ?? emptySettings;
            _Instance.Save(); // save settings, this adds any missing properties as well as creating the settings file if it doesn't exist

            Logger.LogDebug($"{typeof(TSelf).Name}:\n{JsonSerializer.Serialize(_Instance, new JsonSerializerOptions() { WriteIndented = true })}");

            return _Instance;
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Saves the settings file.</summary>
    public void Save()
    {
        // ensure directory exists before attempting to reserialise settings
        var directoryName = new FileInfo(Path).DirectoryName!;
        Directory.CreateDirectory(directoryName);

        // serialise settings
        try { File.WriteAllText(Path, JsonSerializer.Serialize(Instance, new JsonSerializerOptions() { WriteIndented = true })); }
        catch { Logger.LogError($"Failed to serialise {typeof(TSelf).Name}, settings won't persist between sessions."); }
    }
}
