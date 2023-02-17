namespace NovaEngine.Settings;

/// <summary>The application settings related to the console.</summary>
public class ConsoleSettings : SettingsBase<ConsoleSettings>
{
    /*********
    ** Fields
    *********/
    /// <summary>The maximum number of lines that are saved in the console.</summary>
    private int _MaxNumberOfLines = 1000;


    /*********
    ** Properties
    *********/
    /// <summary>The maximum number of lines that are saved in the console.</summary>
    public int MaxNumberOfLines
    {
        get => _MaxNumberOfLines;
        set => _MaxNumberOfLines = Math.Max(1, value);
    }

    /// <summary>The size of the font.</summary>
    public int FontSize { get; set; } = 48;

    /// <summary>Whether time stamps should be shown.</summary>
    public bool ShowTimeStamp { get; set; }

    /// <summary>Whether debug logs should be shown.</summary>
    public bool ShowDebugLogs { get; set; }

    /// <inheritdoc/>
    protected override string Path => Constants.ConsoleSettingsFilePath;

    /// <inheritdoc/>
    protected override string InvalidPath => Constants.InvalidConsoleSettingsFilePath;
}
