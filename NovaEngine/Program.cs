﻿using Debugger = NovaEngine.Debugging.Debugger;

namespace NovaEngine;

/// <summary>The application entry point.</summary>
public static class Program
{
    /*********
    ** Fields
    *********/
    /// <summary>The completion source for <see cref="MainWindowTask"/>.</summary>
    private static readonly TaskCompletionSource<Window> MainWindowTaskCompletionSource = new();

    /// <summary>The completion source for <see cref="ScenesLoadedTask"/>.</summary>
    private static readonly TaskCompletionSource ScenesLoadedTaskCompletionSource = new();


    /*********
    ** Properties
    *********/
    /// <summary>Whether the program has been ran.</summary>
    /// <remarks>This is used to determine if the application is being run from an executable or if the api is being used without running the app.<br/>This is important as we don't want to load up an entire renderer and platform when something as trivial as a texture is being created for content packing etc.</remarks>
    public static bool HasProgramInstance { get; private set; }

    /// <summary>The name of the application, more specifically the name of the executable, without the extension.</summary>
    public static string Name { get; private set; } = "Unknown";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

    /// <summary>The command-line arguments of the application.</summary>
    public static Arguments Arguments { get; private set; }

    /// <summary>The main window of the application.</summary>
    public static Window MainWindow { get; private set; }

    /// <summary>A task used for retrieving the main window once it has been created.</summary>
    public static Task<Window> MainWindowTask => MainWindowTaskCompletionSource.Task;
    
    /// <summary>A task used for determining when the scenes have been loaded.</summary>
    public static Task ScenesLoadedTask => ScenesLoadedTaskCompletionSource.Task;

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
        ArgumentNullException.ThrowIfNull(args);
        Arguments = Arguments.Parse(args);

        try
        {
            HasProgramInstance = true;
            Name = Process.GetCurrentProcess().ProcessName;
            Handle = Process.GetCurrentProcess().Handle;

            var engineThread = new Thread(() =>
            {
                if (!InitialiseEngine())
                    return;

                Content.EnsureDirectoryExists();
                LoadInitialScenes();
                ScenesLoadedTaskCompletionSource.SetResult();

                if (!Arguments.HideWindow)
                    MainWindow.Show();

                ApplicationLoop.Run();

                RendererManager.CurrentRenderer.PrepareDispose();
                SceneManager.GizmosScene.Dispose();
                foreach (var scene in SceneManager.LoadedScenes)
                    scene.Dispose();

                Texture1D.Undefined.Dispose();
                Texture2D.Undefined.Dispose();
                RendererManager.CurrentRenderer.Dispose();
            });
            engineThread.Start();

            if (!Arguments.RemoveEngineThreadBlock)
                engineThread.Join();
        }
        catch (Exception ex)
        {
            Logger.LogFatal($"Unrecoverable error occurred outside the main loop: {ex}");
        }
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Initialises the core engine managers.</summary>
    /// <returns><see langword="true"/>, if the engine was successfully initialised; otherwise, <see langword="false"/>.</returns>
    private static bool InitialiseEngine()
    {
        if (PlatformManager.CurrentPlatform == null)
            return false; // manager has already created a fatal log
        MainWindow = new Window("NovaEngine", new(Arguments.Width, Arguments.Height));
        MainWindowTaskCompletionSource.SetResult(MainWindow);

        if (InputHandlerManager.CurrentInputHandler == null)
            return false; // manager has already created a fatal log
        InputHandlerManager.CurrentInputHandler.OnInitialise(MainWindow.Handle);

        if (RendererManager.CurrentRenderer == null)
            return false; // manager has already created a fatal log
        RendererManager.CurrentRenderer.OnInitialise(MainWindow.Handle);
        Logger.LogDebug($"Using graphics device: {RendererManager.CurrentRenderer.DeviceName}");

        // force run some class initialisers to add their commands / event handlers
        RuntimeHelpers.RunClassConstructor(typeof(CommandManager).TypeHandle);
        RuntimeHelpers.RunClassConstructor(typeof(Debugger).TypeHandle);

        return true;
    }

    /// <summary>Loads the initial scenes.</summary>
    private static void LoadInitialScenes()
    {
        if (Content.DoesFileExist("InitialScenes"))
        {
            var initialScenes = Content.Load<string[]>("InitialScenes");
            foreach (var scene in initialScenes)
                SceneManager.LoadScene(scene);
        }
    }
}
