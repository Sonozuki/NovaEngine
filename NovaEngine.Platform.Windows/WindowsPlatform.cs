using NovaEngine.External.Platform;
using NovaEngine.Maths;
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
        public PlatformWindowBase CreatePlatformWindow(string title, Vector2I size) => new Win32Window(title, size);

        /// <inheritdoc/>
        public Vector2I GetCursorPosition()
        {
            Vector2I point = default;
            User32.GetCursorPos(ref point);
            User32.ScreenToClient(Program.MainWindow.Handle, ref point);
            return point;
        }
    }
}
