namespace NovaEngine.Platform.Windows;

/// <summary>Represents the Windows platform.</summary>
public class WindowsPlatform : IPlatform
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public bool IsCurrentPlatform => OperatingSystem.IsWindows();


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public PlatformWindowBase CreatePlatformWindow(string title, Vector2I size) => new Win32Window(title, size);
}
