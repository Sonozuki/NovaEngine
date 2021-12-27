namespace NovaEngine.Common.Windows.Native;

/// <summary>Controls how a window should be shown.</summary>
public enum CommandShow
{
    /// <summary>Hides the window and activates another window.</summary>
    Hide = 0,

    /// <summary>Activates and displays a window. If the window is minimised or maximised, the system restores it to its original size and position.</summary>
    ShowNormal = 1,

    /// <summary>Activates the window and displays it as a minimised window.</summary>
    ShowMinimised = 2,

    /// <summary>Maximises the specified window.</summary>
    Maximise = 3,

    /// <summary>Activates the window and displays it as a maximised window.</summary>
    ShowMaximised = 3,

    /// <summary>Displays a window in its most recent size and position. This value is similar to <see cref="ShowNormal"/>, except that the window is not activated.</summary>
    ShowNoActivate = 4,

    /// <summary>Activates the window and displays it in its current size and position.</summary>
    Show = 5,

    /// <summary>Minimises the specified window and activates the next top-level window in the Z order.</summary>
    Minimise = 6,

    /// <summary>Displays the window as a minimised window. This valid is similar to <see cref="ShowMinimised"/>, except the window is not activated.</summary>
    ShowMinNoActive = 7,

    /// <summary>Displays the window in its current size and position. This value is similar to <see cref="ShowNormal"/>, except that the window is not activated.</summary>
    ShowNA = 8,

    /// <summary>Activates and displays the window. If the window is minimised or maximised, the system restores it to its original size and position.</summary>
    Restore = 9,

    // ShowDefault = 10

    /// <summary>Minimises a window, even if the thread that owns the window is not responding. This flag should only be used when minimising windows from a different thread.</summary>
    ForceMinimise = 11,
}
