using NovaEngine.Maths;
using NovaEngine.Platform.Windows.Windowing;
using System;

namespace NovaEngine.Platform.Windows
{
    /// <summary>Represents the Windows platform.</summary>
    public class WindowsPlatform : IPlatform
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool IsCurrentPlatform => OperatingSystem.IsWindows();


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public PlatformWindowBase CreatePlatformWindow(string title, Size size) => new Win32Window(title, size);
    }
}
