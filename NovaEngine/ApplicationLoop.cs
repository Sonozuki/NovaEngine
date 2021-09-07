using NovaEngine.Components;
using NovaEngine.IO;
using NovaEngine.Logging;
using NovaEngine.SceneManagement;
using System;

namespace NovaEngine
{
    /// <summary>Responsible for the main frame loop of the engine.</summary>
    public static class ApplicationLoop
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether updating components has been paused.</summary>
        public static bool IsComponentUpdatingPaused { get; set; }


        /*********
        ** Internal Methods
        *********/
        /// <summary>Starts running the application loop.</summary>
        internal static void Run()
        {
            try
            {
                while (!Program.MainWindow!.HasClosed)
                {
                    Input.Update();

                    SceneManager.LoadedScenes.ForEach(scene => scene.Update());

                    Camera.Main?.Render(true);

                    Program.MainWindow.ProcessEvents();
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Unrecoverable error occured inside the main loop: {ex}", LogSeverity.Fatal);
            }
        }
    }
}
