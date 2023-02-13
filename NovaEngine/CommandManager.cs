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
    ** Public Methods
    *********/
    /// <summary>Initialises the class.</summary>
    static CommandManager()
    {
        var standardInput = new StreamReader(Console.OpenStandardInput());
        ReadCommands(standardInput);

        Add("help", "Lists all registered command documentation.\n\nUsage: help\nLists all the registered commands.\n\nUsage: help <command>\nLists the documentation about a specific command\n- command: The name of command to view the documentation of.", HelpCommand);
        Add("qqq", "Closes the application.\n\nUsage: qqq\nCloses the application.", (_) => Environment.Exit(0));
    }

    /// <summary>Adds a command.</summary>
    /// <param name="name">The name of the command.</param>
    /// <param name="documentation">The documentation of the command.</param>
    /// <param name="callback">The callback of the command.</param>
    /// <returns><see langword="true"/>, if the command was added successfully; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is <see langword="null"/> or white space.</exception>
    /// <exception cref="ArgumentNullException">Thrown is <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static bool Add(string name, string documentation, Action<string[]> callback)
    {
        var command = new Command(name, documentation, callback);

        if (Commands.Any(c => c.Name.ToLower() == command.Name.ToLower()))
        {
            Logger.LogError($"Cannot add command: '{command.Name}' as it already exists.");
            return false;
        }

        Commands.Add(command);
        return true;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Reads a <see cref="StreamReader"/> and executes commands written to it.</summary>
    /// <param name="streamReader">The <see cref="StreamReader"/> to listen for commands on.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="streamReader"/> is <see langword="null"/>.</exception>
    private static async void ReadCommands(StreamReader streamReader)
    {
        if (streamReader == null)
            throw new ArgumentNullException(nameof(streamReader));

        while (true)
        {
            var input = await streamReader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(input))
                continue;

            var splitString = input.Split(' ');
            var commandName = splitString[0];
            var command = Commands.FirstOrDefault(command => command.Name.ToLower() == commandName.ToLower());
            if (command == null)
            {
                Logger.LogError($"No command with the name: '{commandName}' could be found.");
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
        // check if a specific command was requested
        if (args.Length > 0)
        {
            var requestedCommandName = args[0];
            var requestedCommand = Commands.FirstOrDefault(command => command.Name.ToLower() == requestedCommandName.ToLower());
            if (requestedCommand == null)
            {
                Logger.LogError($"No command with the name: '{requestedCommandName}' could be found.");
                Logger.LogHelp("Type 'help' for a list of available commands.");
                return;
            }

            Logger.LogHelp($"{requestedCommand.Name}: {requestedCommand.Documentation}");
        }
        else // write out command list
        {
            Logger.LogHelp("All registered commands are:");
            foreach (var command in Commands.OrderBy(command => command.Name))
                Logger.LogHelp($"  {command.Name}");
            Logger.LogHelp();
            Logger.LogHelp("For more information about a command, type: \"help command_name\".");
        }
    }
}
