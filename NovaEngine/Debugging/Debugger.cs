namespace NovaEngine.Debugging;

/// <summary>Provides a way of interacting with debug values at runtime through the console.</summary>
public static class Debugger
{
    /*********
    ** Fields
    *********/
    /// <summary>The registered debug values.</summary>
    private static readonly List<DebugValueBase> DebugValues = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises the class.</summary>
    static Debugger()
    {
        CommandManager.Add("debug", "Provides a way of interacting with debug values at runtime.\n\nUsage: debug\nLists all the registered debug values.\n\nUsage: debug <debug_value>\nLists the documentation about a specific debug value\n- debug_value: The name of debug value to view the documentation of.\n\nUsage: debug <debug_value> <value>\nSets the value of a debug value.\n- debug_value: The name of the debug value to set the value of.\n- value: The value to set the debug value to.", DebugCommand);
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Adds a debug value that is editable through the 'debug' command.</summary>
    /// <typeparam name="T">The type of value being added.</typeparam>
    /// <param name="name">The name of the debug value.</param>
    /// <param name="documentation">The documentation of the debug value.</param>
    /// <param name="callback">The callback of the debug value.</param>
    /// <returns><see langword="true"/>, if the debug value was added successfully; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> or <paramref name="documentation"/> is <see langword="null"/> or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static bool AddDebugValue<T>(string name, string documentation, Action<T?> callback)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(documentation);
        ArgumentNullException.ThrowIfNull(callback);

        name = name.ToLower(G11n.Culture);
        if (DebugValues.Any(value => value.Name == name))
        {
            Logger.LogError($"Cannot add debug value '{name}' as it already exists.");
            return false;
        }

        DebugValues.Add(new DebugValue<T>(name, documentation, callback));
        return true;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>The command that's executed when "debug" is typed in the console.</summary>
    /// <param name="args">The command arguments.</param>
    private static void DebugCommand(string[] args)
    {
        var trimmedArguments = args.Where(argument => !string.IsNullOrEmpty(argument)).ToArray();

        if (trimmedArguments.Length == 0) // write out debug values list
        {
            Logger.LogHelp("All registered debug values are:");
            foreach (var debugValue in DebugValues.OrderBy(debugValue => debugValue.Name))
                Logger.LogHelp($"  {debugValue.Name}");
            Logger.LogHelp();
            Logger.LogHelp("For more information about a debug value, type: \"debug debug_value\".");
        }
        else if (trimmedArguments.Length == 1) // write the documentation of a specified debug value
        {
            if (!TryGetDebugValueByName(trimmedArguments[0].ToLower(G11n.Culture), out var debugValue))
                return;

            Logger.LogHelp($"{debugValue!.Name}: {debugValue.Documentation}");
        }
        else // set value of command
        {
            if (!TryGetDebugValueByName(trimmedArguments[0].ToLower(G11n.Culture), out var debugValue))
                return;

            try
            {
                debugValue!.InvokeCallback(string.Join(" ", trimmedArguments[1..]));
            }
            catch (Exception ex)
            {
                Logger.LogError($"Debug value crashed. Technical details:\n{ex}");
            }
        }

        // Retrieves a debug value from a name.
        bool TryGetDebugValueByName(string name, out DebugValueBase? debugValue)
        {
            debugValue = DebugValues.FirstOrDefault(value => value.Name == name);
            if (debugValue == null)
            {
                Logger.LogError($"No debug value with the name '{name}' could be found.");
                Logger.LogHelp($"Type 'debug' for a list of available debug values.");
                return false;
            }

            return true;
        }
    }
}
