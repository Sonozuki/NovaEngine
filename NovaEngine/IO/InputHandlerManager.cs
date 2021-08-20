﻿using NovaEngine.External.Input;
using NovaEngine.IO.Fake;
using NovaEngine.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NovaEngine.IO
{
    /// <summary>Handles using difference input handlers.</summary>
    internal static class InputHandlerManager
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The input handler that is currently being used.</summary>
        public static IInputHandler CurrentInputHandler { get; }


        /*********
        ** Public Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. (If the current input handler is null, it's handled and the program is killed before it could cause any null references.)

        /// <summary>Initialises the class.</summary>
        static InputHandlerManager()
        {
            // ensure an input handler should actually be used
            if (!Program.HasProgramInstance)
            {
                CurrentInputHandler = new FakeInputHandler();
                return;
            }

            // get the current input handler object
            var inputHandlerFiles = Directory.GetFiles(Environment.CurrentDirectory, "NovaEngine.InputHandler.*.dll");
            var types = inputHandlerFiles
                .Select(inputHandlerFile => Assembly.LoadFrom(inputHandlerFile))
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.IsClass && !type.IsAbstract);

            foreach (var type in types)
            {
                // ensure type is a renderer
                if (!type.GetInterfaces().Contains(typeof(IInputHandler)))
                    continue;

                // try to create an instance
                var inputHandler = (IInputHandler?)Activator.CreateInstance(type);
                if (inputHandler == null)
                    continue;

                // set to the current input handler if it's usable
                if (!inputHandler.CanUseOnPlatform)
                    continue;

                CurrentInputHandler = inputHandler;
                Logger.Log($"Using input handler: {type.Assembly.ManifestModule.Name}", LogSeverity.Debug);
                break;
            }

            if (CurrentInputHandler == null)
                Logger.Log("The environment operating system doesn't have a supported input handler.", LogSeverity.Fatal);
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    }
}
