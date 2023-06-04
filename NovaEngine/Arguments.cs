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

    /// <summary>The parent to use when creating <see cref="Program.MainWindow"/>.</summary>
    public IntPtr WindowParent { get; private set; }

    // TODO: retrieve default values from a settings file
    /// <summary>The width of the window.</summary>
    public int Width { get; private set; } = 1280;

    /// <summary>The height of the window.</summary>
    public int Height { get; private set; } = 720;


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

                case "-window-parent":
                    if (!TryGetNextValue(args, ref i, out var windowParent))
                    {
                        Logger.LogError("Failed to retrieve value after '-window-parent'.");
                        break;
                    }

                    if (!IntPtr.TryParse(windowParent, out var windowParentPointer))
                    {
                        Logger.LogError($"Failed to parse '{windowParent}' as a pointer.");
                        break;
                    }

                    arguments.WindowParent = windowParentPointer;
                    break;

                case "-width":
                    if (!TryGetNextValue(args, ref i, out var widthString))
                    {
                        Logger.LogError("Failed to retrieve value after '-width'.");
                        break;
                    }

                    if (!int.TryParse(widthString, out var width))
                    {
                        Logger.LogError($"Failed to parse '{widthString}' as an integer.");
                        break;
                    }

                    arguments.Width = width;
                    break;

                case "-height":
                    if (!TryGetNextValue(args, ref i, out var heightString))
                    {
                        Logger.LogError("Failed to retrieve value after '-height'.");
                        break;
                    }

                    if (!int.TryParse(heightString, out var height))
                    {
                        Logger.LogError($"Failed to parse '{heightString}' as an integer.");
                        break;
                    }

                    arguments.Height = height;
                    break;

                default:
                    Logger.LogError($"Unrecognised command-line argument '{arg}'.");
                    break;
            }
        }

        return arguments;
    }

    /// <summary>Tries to get the next specified value.</summary>
    /// <param name="args">The complete list of command-line arguments being parsed.</param>
    /// <param name="index">The current index into <paramref name="args"/> being parsed.</param>
    /// <param name="value">The value that was retrieved, if one could be retrieved; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/>, if a value could be retrieved; otherwise, <see langword="false"/>.</returns>
    /// <remarks>If the next next value is parsable as an argument, <paramref name="index"/> won't be incremented and <see langword="false"/> will be returned.</remarks>
    private static bool TryGetNextValue(string[] args, ref int index, out string? value)
    {
        value = null;
        var nextIndex = index + 1;

        if (nextIndex >= args.Length)
            return false;

        var nextValue = args[nextIndex];
        if (nextValue.StartsWith("-", ignoreCase: false, G11n.Culture))
            return false;

        index++;
        value = nextValue;
        return true;
    }
}
