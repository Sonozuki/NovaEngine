using NovaEngine.Maths;
using NovaEngine.Rendering;
using NovaEngine.Windowing;
using System;
using System.Diagnostics;

namespace NovaEngine
{
    /// <summary>The application entry point.</summary>
    public static class Program
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The name of the application, more specifically the name of the executable, without the extension.</summary>
        public static string Name { get; private set; } = "Unknown";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. (When these are actually used, they'll never be null.)

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
            Name = Process.GetCurrentProcess().ProcessName;
            Handle = Process.GetCurrentProcess().Handle;

            InitialiseWindow();
            InitialiseRenderer();

            try
            {
                MainLoop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unrecoverable error occured in main loop: {ex}");
            }

            CleanUp();
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Initialises the application window.</summary>
        private static void InitialiseWindow()
        {
            MainWindow = new Window("NovaEngine", new Size(1280, 720)); // TODO: don't hardcode
            MainWindow.Resize += (e) => RendererManager.CurrentRenderer.OnWindowResize(e.NewSize);
        }

        /// <summary>Initialises the renderer.</summary>
        private static void InitialiseRenderer() => RendererManager.CurrentRenderer.OnInitialise(MainWindow!.Handle);

        /// <summary>Runs the main application loop.</summary>
        private static void MainLoop()
        {
            while (!MainWindow!.HasClosed)
            {
                RendererManager.CurrentRenderer.OnRenderFrame();

                MainWindow.ProcessEvents();
            }
        }

        /// <summary>Cleans up the application.</summary>
        private static void CleanUp() => RendererManager.CurrentRenderer.OnCleanUp();
    }
}
