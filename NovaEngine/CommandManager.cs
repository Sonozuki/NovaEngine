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
        // set console input stream to read commands
        var standardInput = new StreamReader(Console.OpenStandardInput());
        ReadCommands(standardInput);

        // add help command
        Add("help", "Lists all registered command documentation.\n\nUsage: help\nLists all the registered commands.\n\nUsage: help <command>\nLists the documentation about a specific command\n- command: The name of command to view the documentation of.", HelpCommand);
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

        // ensure command doesn't already exist
        if (Commands.Any(c => c.Name.ToLower() == command.Name.ToLower()))
        {
            Logger.LogError($"Cannot add command: '{command.Name}' as it already exists.");
            return false;
        }

        // add command
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

        // execute commands
        while (true)
        {
            var input = await streamReader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(input))
                continue;

            // ensure command exists
            var splitString = input.Split(' ');
            var commandName = splitString[0];
            var command = Commands.FirstOrDefault(command => command.Name.ToLower() == commandName.ToLower());
            if (command == null)
            {
                Logger.LogError($"No command with the name: '{commandName}' could be found.");
                Logger.LogHelp("Type 'help' for a list of available commands.");
                continue;
            }

            // execute command
            var commandArgs = splitString[1..].Where(argument => !string.IsNullOrEmpty(argument));

            try
            {
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
            // get the command being requested
            var requestedCommand = args[0];
            var command = Commands.FirstOrDefault(command => command.Name.ToLower() == requestedCommand.ToLower());

            // ensure command exists
            if (command == null)
            {
                Logger.LogError($"No command with the name: '{requestedCommand}' could be found.");
                Logger.LogHelp("Type 'help' for a list of available commands.");
                return;
            }

            Logger.LogHelp($"{command.Name}: {command.Documentation}");
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
