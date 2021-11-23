using NovaEngine.Common.Windows.Api;
using NovaEngine.Common.Windows.Native;
using NovaEngine.External.Platform;
using NovaEngine.Maths;
using System;
using System.Runtime.InteropServices;

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
        public PlatformWindowBase CreatePlatformWindow(string title, Vector2I size) => new Win32Window(title, size);
    }
}
