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

        /// <inheritdoc/>
        public unsafe bool IsCursorLocked
        {
            get
            {
                var clipRectangle = new NativeRectangle();
                User32.GetClipCursor(ref clipRectangle);

                var clientCoordinatesTopLeft = new Vector2I(clipRectangle.Left, clipRectangle.Top);
                User32.ScreenToClient(Program.MainWindow.Handle, ref clientCoordinatesTopLeft);

                return clientCoordinatesTopLeft == CursorClipTopLeft;
            }
            set
            {
                if (value)
                {
                    var screenCoordinatesTopLeft = CursorClipTopLeft;
                    User32.ClientToScreen(Program.MainWindow.Handle, ref screenCoordinatesTopLeft);
                    var screenCoordinatesBottomRight = screenCoordinatesTopLeft + Vector2I.One;

                    var clipRectangle = new NativeRectangle(screenCoordinatesTopLeft, screenCoordinatesBottomRight);
                    User32.ClipCursor(&clipRectangle);
                }
                else
                    User32.ClipCursor(null);
            }
        }

        /// <summary>The top left position of the clip rectangle, in client-area coordinates.</summary>
        private Vector2I CursorClipTopLeft => (Vector2I)((Vector2)Program.MainWindow.Size / 2f);


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public PlatformWindowBase CreatePlatformWindow(string title, Vector2I size) => new Win32Window(title, size);
    }
}
