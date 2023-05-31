namespace NovaEngine;

/// <summary>The command-line arguments of the application.</summary>
public sealed class Arguments
{
    /*********
    ** Properties
    *********/
    /// <summary>Whether the window should be hidden.</summary>
    public bool HideWindow { get; private set; }

    /// <summary>Whether the console commands should be disabled.</summary>
    public bool DisableReadCommands { get; private set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    private Arguments() { }


    /*********
    ** Internal Methods
    *********/
    /// <summary>Parses command-line arguments.</summary>
    /// <param name="args">The command-line arguments to parse.</param>
    /// <returns>The parsed command-line arguments.</returns>
    internal static Arguments Parse(string[] args)
    {
        var arguments = new Arguments();

        foreach (var arg in args)
            switch (arg)
            {
                case "-hide-window":
                    arguments.HideWindow = true;
                    break;

                case "-disable-read-commands":
                    arguments.DisableReadCommands = true;
                    break;

                default:
                    Logger.LogError($"Unrecognised command-line argument '{arg}'.");
                    break;
            }

        return arguments;
    }
}
