using NovaEngine.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NovaEngine
{
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
            Console.SetIn(standardInput);

            ReadCommands(standardInput);

            // add help command
            Add("help", "Lists all registered command documentation.\n\nUsage: help\nLists all the registered commands.\n\nUsage: help <command>\nLists the documentation about a specific command\n- command: The name of command to view the documentation of.", HelpCommand);
        }

        /// <summary>Adds a command.</summary>
        /// <param name="command">The command to add.</param>
        /// <returns><see langword="true"/>, if the command was added successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown is <paramref name="command"/> is <see langword="null"/>.</exception>
        public static bool Add(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            // ensure command doesn't already exist
            if (Commands.Any(c => c.Name.ToLower() == command.Name.ToLower()))
            {
                Logger.Log($"Cannot add command: '{command.Name}' as it already exists.", LogSeverity.Error);
                return false;
            }

            // add command
            Commands.Add(command);
            return true;
        }

        /// <summary>Adds a command.</summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="documentation">The documentation of the command.</param>
        /// <param name="callback">The callback of the command.</param>
        /// <returns><see langword="true"/>, if the command was added successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is <see langword="null"/> or white space.</exception>
        /// <exception cref="ArgumentNullException">Thrown is <paramref name="callback"/> is <see langword="null"/>.</exception>
        public static bool Add(string name, string documentation, Action<string[]> callback) => Add(new(name, documentation, callback));


        /*********
        ** Private Methods
        *********/
        /// <summary>Reads a <see cref="StreamReader"/> and executes commands written to it.</summary>
        /// <param name="streamReader">The <see cref="StreamReader"/> to listen for commands on.</param>
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
                    Logger.Log($"No command with the name: '{commandName}' could be found.", LogSeverity.Error);
                    Logger.Log("Type 'help' for a list of available commands.", LogSeverity.Help);
                    continue;
                }

                // execute command
                var commandArgs = splitString[1..].Where(argument => !string.IsNullOrEmpty(argument));
                command.Callback.Invoke(commandArgs.ToArray());
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
                var command = Commands.Where(command => command.Name.ToLower() == requestedCommand.ToLower()).FirstOrDefault();

                // ensure command exists
                if (command == null)
                {
                    Logger.Log($"No command with the name: '{requestedCommand}' could be found.", LogSeverity.Error);
                    Logger.Log("Type 'help' for a list of available commands.", LogSeverity.Help);
                    return;
                }

                // log command documentation
                Logger.Log($"{command.Name}: {command.Documentation}", LogSeverity.Help);
            }
            else // write out command list
            {
                Logger.Log("All registered commands are:", LogSeverity.Help);
                foreach (var command in Commands)
                    Logger.Log($"  {command.Name}", LogSeverity.Help);
                Logger.Log(severity: LogSeverity.Help);
                Logger.Log("For more information about a command, type: \"help command_name\".", LogSeverity.Help);
            }
        }
    }
}
