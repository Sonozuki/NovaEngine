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

        /// <inheritdoc/>
        public bool IsCursorVisible
        {
            get
            {
                var cursorInfo = new NativeCursorInfo();
                cursorInfo.Size = (uint)Marshal.SizeOf<NativeCursorInfo>();
                User32.GetCursorInfo(ref cursorInfo);
                return cursorInfo.Flags == CursorState.Showing;
            }
            set
            {
                var displayCounter = User32.ShowCursor(value);
                if (value)
                    while (displayCounter < 0)
                        displayCounter = User32.ShowCursor(true);
                else
                    while (displayCounter >= 0)
                        displayCounter = User32.ShowCursor(false);
            }
        }


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
