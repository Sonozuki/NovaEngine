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

    /// <summary>Whether the block on the engine thread should be removed.</summary>
    public bool RemoveEngineThreadBlock { get; private set; }


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

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            switch (arg)
            {
                case "-hide-window":
                    arguments.HideWindow = true;
                    break;

                case "-disable-read-commands":
                    arguments.DisableReadCommands = true;
                    break;

                case "-remove-engine-thread-block":
                    arguments.RemoveEngineThreadBlock = true;
                    break;

                default:
                    Logger.LogError($"Unrecognised command-line argument '{arg}'.");
                    break;
            }
        }

        return arguments;
    }
}
