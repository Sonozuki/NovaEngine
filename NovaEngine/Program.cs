using NovaEngine.Content;
using NovaEngine.Graphics;
using NovaEngine.IO;
using NovaEngine.Logging;
using NovaEngine.Platform;
using NovaEngine.Rendering;
using NovaEngine.SceneManagement;
using NovaEngine.Windowing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Debugger = NovaEngine.Debugging.Debugger;

namespace NovaEngine
{
    /// <summary>The application entry point.</summary>
    public static class Program
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether the program has been ran.</summary>
        /// <remarks>This is used to determine if the application is being run from an executable or if the api is being used without running the app.<br/>This is important as we don't want to load up an entire renderer and platform when something as trivial as a texture is being created for content packing etc.</remarks>
        public static bool HasProgramInstance { get; private set; }

        /// <summary>The name of the application, more specifically the name of the executable, without the extension.</summary>
        public static string Name { get; private set; } = "Unknown";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

        /// <summary>The main window of the application.</summary>
        public static Window MainWindow { get; private set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

        /// <summary>A handle to the application.</summary>
        public static IntPtr Handle { get; private set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>The application entry point.</summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            try
            {
                HasProgramInstance = true;

                Name = Process.GetCurrentProcess().ProcessName;
                Handle = Process.GetCurrentProcess().Handle;

                // initialise window
                if (PlatformManager.CurrentPlatform == null)
                    return; // fatal log has already been created at this point
                MainWindow = new Window("NovaEngine", new(1280, 720)); // TODO: don't hardcode

                // initialise input handler
                if (InputHandlerManager.CurrentInputHandler == null)
                    return; // fatal log has already been created at this point
                InputHandlerManager.CurrentInputHandler.OnInitialise(MainWindow.Handle);

                // initialise renderer
                if (RendererManager.CurrentRenderer == null)
                    return; // fatal log has already been created at this point
                RendererManager.CurrentRenderer.OnInitialise(MainWindow.Handle);

                // force run some class initialisers to add their commands / event handlers
                RuntimeHelpers.RunClassConstructor(typeof(CommandManager).TypeHandle);
                RuntimeHelpers.RunClassConstructor(typeof(Debugger).TypeHandle);

                // initialise initial scenes
                var initialScenes = ContentLoader.Load<List<string>>("InitialScenes");
                foreach (var scene in initialScenes)
                    SceneManager.LoadScene(scene);

                MainWindow.Show();

                // main loop
                ApplicationLoop.Run();

                // clean up
                foreach (var scene in SceneManager.LoadedScenes)
                    scene.Dispose();

                Texture2D.Undefined.Dispose();
                RendererManager.CurrentRenderer.Dispose();
            }
            catch (Exception ex)
            {
                Logger.LogFatal($"Unrecoverable error occured outside the main loop: {ex}");
            }
        }
    }
}
