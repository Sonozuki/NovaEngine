namespace NovaEngine;

/// <summary>Manages console commands commands.</summary>
public static class CommandManager
{
    /*********
    ** Fields
    *********/
    /// <summary>The currently registed commands.</summary>
    private static readonly List<Command> Commands = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises the class.</summary>
    static CommandManager()
    {
        if (!Program.Arguments.DisableReadCommands)
            ReadCommands();

        Add("help", "Lists all registered command documentation.\n\nUsage: help\nLists all the registered commands.\n\nUsage: help <command>\nLists the documentation about a specific command\n- command: The name of command to view the documentation of.", HelpCommand);
        Add("qqq", "Closes the application.\n\nUsage: qqq\nCloses the application.", (_) => Environment.Exit(0));
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Adds a command.</summary>
    /// <param name="name">The name of the command.</param>
    /// <param name="documentation">The documentation of the command.</param>
    /// <param name="callback">The callback of the command.</param>
    /// <returns><see langword="true"/>, if the command was added successfully; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> or <paramref name="documentation"/> is <see langword="null"/> or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown is <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static bool Add(string name, string documentation, Action<string[]> callback)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(documentation);
        ArgumentNullException.ThrowIfNull(callback);

        name = name.ToLower(G11n.Culture);
        if (Commands.Any(c => c.Name == name))
        {
            Logger.LogError($"Cannot add command '{name}' as it already exists.");
            return false;
        }

        Commands.Add(new(name, documentation, callback));
        return true;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Reads any commands written to the console.</summary>
    private static async void ReadCommands()
    {
        using var streamReader = new StreamReader(Console.OpenStandardInput());

        while (true)
        {
            var input = await streamReader.ReadLineAsync().ConfigureAwait(true);
            if (string.IsNullOrWhiteSpace(input))
                continue;

            var splitString = input.Split(' ');
            var commandName = splitString[0];
            var command = Commands.FirstOrDefault(command => command.Name.ToLower(G11n.Culture) == commandName.ToLower(G11n.Culture));
            if (command == null)
            {
                Logger.LogError($"No command with the name '{commandName}' could be found.");
                Logger.LogHelp("Type 'help' for a list of available commands.");
                continue;
            }

            try
            {
                var commandArgs = splitString[1..].Where(argument => !string.IsNullOrEmpty(argument));
                command.Callback.Invoke(commandArgs.ToArray());
            }
            catch (Exception ex)
            {
                Logger.LogError($"Command crashed. Technical details:\n{ex}");
            }
        }
    }

    /// <summary>The command that's executed when "help" is typed in the console.</summary>
    /// <param name="args">The command arguments.</param>
    private static void HelpCommand(string[] args)
    {
        if (args.Length > 0)
        {
            var requestedCommandName = args[0].ToLower(G11n.Culture);
            var requestedCommand = Commands.FirstOrDefault(command => command.Name == requestedCommandName);
            if (requestedCommand == null)
            {
                Logger.LogError($"No command with the name: '{requestedCommandName}' could be found.");
                Logger.LogHelp("Type 'help' for a list of available commands.");
                return;
            }

            Logger.LogHelp($"{requestedCommand.Name}: {requestedCommand.Documentation}");
        }
        else
        {
            Logger.LogHelp("All registered commands are:");
            foreach (var command in Commands.OrderBy(command => command.Name))
                Logger.LogHelp($"  {command.Name}");
            Logger.LogHelp();
            Logger.LogHelp("For more information about a command, type: \"help command_name\".");
        }
    }
}
