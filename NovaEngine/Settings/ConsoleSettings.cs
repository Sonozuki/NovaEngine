﻿namespace NovaEngine.Settings;

/// <summary>The application settings related to the console.</summary>
public class ConsoleSettings : SettingsBase<ConsoleSettings>
{
    /*********
    ** Fields
    *********/
    /// <summary>The maximum number of lines that are saved in the console.</summary>
    private int _MaxNumberOfLines = 1000;


    /*********
    ** Accessors
    *********/
    /// <summary>The maximum number of lines that are saved in the console.</summary>
    public int MaxNumberOfLines
    {
        get => _MaxNumberOfLines;
        set => _MaxNumberOfLines = Math.Max(1, value);
    }

    /// <inheritdoc/>
    protected override string Path => Constants.ConsoleSettingsFilePath;

    /// <inheritdoc/>
    protected override string InvalidPath => Constants.InvalidConsoleSettintsFilePath;
}