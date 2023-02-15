namespace NovaEngine;

/// <summary>Provides time information.</summary>
public static class Time
{
    /*********
    ** Properties
    *********/
    /// <summary>The number of seconds that the previous frame took to process.</summary>
    public static float DeltaTime { get; internal set; }

    /// <summary>The number of seconds that has elapsed since the start of the application.</summary>
    public static float TotalTime { get; internal set; }

    /// <summary>The number of frames that have been processed since the start of the application.</summary>
    public static uint FrameCount { get; internal set; }
}
