using NovaEngine.Maths;
using System;
using System.Runtime.InteropServices;

namespace NovaEngine.Platform.Windows
{
    /// <summary>Exposes neccessary User32.dll apis.</summary>
    internal static class User32
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Converts the client-area coordinates of the specified point to screen coordinates.</summary>
        /// <param name="window">A handle to the window whose client area is used for the conversion.</param>
        /// <param name="point">A reference to the structure that contains the client coordinates to be converted. The new screen coordinates are copied into this structure if the function succeeds.</param>
        /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool ClientToScreen(IntPtr window, ref Vector2I point);

        /// <summary>Confines the cursor to a rectangular area on the screen.</summary>
        /// <param name="rectangle">A pointer to the structure that contains the screen coordiantes of the upper-left and lower-right corners of the confining rectangle. If this parameter is <see langword="null"/>, the cursor is free to move anywhere on the screen.</param>
        /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public unsafe static extern bool ClipCursor(NativeRectangle* rectangle);

        /// <summary>Creates a window with an extended window style.</summary>
        /// <param name="exStyle">The extended window style of the window being created.</param>
        /// <param name="className">A class name that has previously been registered using the <see cref="RegisterClass(in NativeWindowClass)"/> method.</param>
        /// <param name="windowName">The name of the window.</param>
        /// <param name="style">The style of the window being created.</param>
        /// <param name="x">The initial horizontal position of the window.</param>
        /// <param name="y">The initial vertical position of the window.</param>
        /// <param name="width">The width, in device units, of the window.</param>
        /// <param name="height">The height, in device units, of the window.</param>
        /// <param name="windowParent">A handle to the parent window of the window being created.</param>
        /// <param name="menu">A handle to a menu.</param>
        /// <param name="handle">A handle to the application to be associated with the window.</param>
        /// <param name="lpParam">Pointer to a value to be passed to the window.</param>
        /// <returns>If the function succeedsm, the return value is a handle to the new window; otherwise, <see cref="IntPtr.Zero"/> is returned.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr CreateWindowEx(uint exStyle, string className, string windowName, WindowStyle style, int x, int y, int width, int height, IntPtr windowParent, IntPtr menu, IntPtr handle, IntPtr lpParam);

        /// <summary>Calls the default window procedure to provide basic default processing for any window message that an application does not process.</summary>
        /// <param name="window">A handle to the window procedure that received the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="wParam">Additional message information. The content of this parameter depends on the value of the <paramref name="message"/> parameter.</param>
        /// <param name="lParam">Additional message information. The content of this parameter depends on the value of the <paramref name="message"/> parameter.</param>
        /// <returns>TThe result of the message processing, which depends on the message sent.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr DefWindowProc(IntPtr window, Message message, IntPtr wParam, IntPtr lParam);

        /// <summary>Dispatches a message to a window procedure.</summary>
        /// <param name="message">The message information.</param>
        /// <returns>The value returned by the window procedure, which depends on the message being dispatched.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr DispatchMessage(in NativeMessage message);

        /// <summary>Retrieves the screen coordinates of the rectangle area to which the cursor is confined.</summary>
        /// <param name="rectangle">A reference to the <see cref="NativeRectangle"/> structure that receives the screen coordinates of the confining rectanlge. The structure receives the dimensions of the screen if the cursor is not confined to a rectangle.</param>
        /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool GetClipCursor(ref NativeRectangle rectangle);

        /// <summary>Retrieves information about the global cursor.</summary>
        /// <param name="cursorInfo">The structure to populate with the cursor infomation.</param>
        /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool GetCursorInfo(ref NativeCursorInfo cursorInfo);

        /// <summary>Retrieves the text of the specified window's title bar (if it has one).</summary>
        /// <param name="windowHandle">A handle to the window whose title is to be retrieved.</param>
        /// <param name="text">The window title.</param>
        /// <param name="maxCount">The max length of <paramref name="text"/>.</param>
        /// <returns>The length of the text, if the call was successful; otherwise, 0.</returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern int GetWindowText(IntPtr windowHandle, out string text, int maxCount);

        /// <summary>Loads the specified cursor resource.</summary>
        /// <param name="handle">A handle to an instance of the module whose executable file contains the cursor to be loaded.<br/>To use a system cursor, <see cref="IntPtr.Zero"/> must be specified.</param>
        /// <param name="cursorName">The name of the cursor resource to be loaded.</param>
        /// <returns>A handle to the newly loaded cursor if successful; otherwise, <see cref="IntPtr.Zero"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr LoadCursor(IntPtr handle, int cursorName);

        /// <summary>Dispatches incoming sent messages, chest the thread message queue for a posted message, and retrieves the message (if any exist).</summary>
        /// <param name="message">The reveived message information.</param>
        /// <param name="window">A handle to the window whose messages are to be retrieved.</param>
        /// <param name="messageFilterMin">The value of the first message in the range of messages to be examined.</param>
        /// <param name="messageFilterMax">The value of the last message in the range of messages to be examined.</param>
        /// <param name="removeMessage">How the messages are to be handled.</param>
        /// <returns><see langword="true"/> if a message is available; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool PeekMessage(out NativeMessage message, IntPtr window, uint messageFilterMin, uint messageFilterMax, RemoveMessage removeMessage);

        /// <summary>Indicates to the system that a thread has made a request to terminate (quit).</summary>
        /// <param name="exitCode">The application exit code.</param>
        [DllImport("User32", SetLastError = true)]
        public static extern void PostQuitMessage(int exitCode);

        /// <summary>Registers a window class for subsequent calls to the <see cref="CreateWindowEx(uint, string, string, WindowStyle, int, int, int, int, IntPtr, IntPtr, IntPtr, IntPtr)"/> method.</summary>
        /// <param name="windowClass">The window class to register.</param>
        /// <returns>If the function succeeds, the return value is a class atom that uniquely identifies the class being registered; otherwise, zero is returned.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern ushort RegisterClass(in NativeWindowClass windowClass);

        /// <summary>Converts the screen coordinates of the specified point to client-area coordinates.</summary>
        /// <param name="window">A handle to the window whose client area is used for the conversion.</param>
        /// <param name="point">A reference to the structure that contains the screen coordinates to be converted. The new client coordinates are copied into this structure if the function succeeds.</param>
        /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool ScreenToClient(IntPtr window, ref Vector2I point);

        /// <summary>Changes the text of the specified window's title bar (if it has one).</summary>
        /// <param name="windowHandle">A handle to the window whose title is to be changed.</param>
        /// <param name="text">The new title.</param>
        /// <returns><see langword="true"/> if the title was changed successfully; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool SetWindowText(IntPtr windowHandle, string text);

        /// <summary>Displays or hides the cursor.</summary>
        /// <param name="show"><see langword="true"/>, to increase the internal display count by one; otherwise, decrement by one.</param>
        /// <returns>The new display count.</returns>
        /// <remarks>Whether the cursor is shown depends on the value of the internal display count, this means this may need to be called multiple times.</remarks>
        [DllImport("User32", SetLastError = true)]
        public static extern int ShowCursor(bool show);

        /// <summary>Sets the specified window's show state.</summary>
        /// <param name="window">A handle to the window.</param>
        /// <param name="commandShow">The command to determine how the window is to be shown.</param>
        /// <returns><see langword="true"/> if the window was previously visible; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr window, CommandShow commandShow);

        /// <summary>Translates virtual-key messages into character messages.</summary>
        /// <param name="message">The message information that was retrieved from the calling thread's message queue by using the <see cref="PeekMessage(out NativeMessage, IntPtr, uint, uint, RemoveMessage)"/> method.</param>
        /// <returns><see langword="true"/> if the message is translated; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool TranslateMessage(in NativeMessage message);
    }
}
