namespace NovaEngine;

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
            var stopwatch = Stopwatch.StartNew();
            while (!Program.MainWindow!.HasClosed)
            {
                Time.DeltaTime = (float)stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();
                Time.TotalTime += Time.DeltaTime;

                Input.Update();
                SceneManager.Update();

                Camera.Main?.Render(true);

                Program.MainWindow.ProcessEvents();

                Time.FrameCount++;
            }
        }
        catch (Exception ex)
        {
            Logger.LogFatal($"Unrecoverable error occured inside the main loop: {ex}");
        }
    }
}
