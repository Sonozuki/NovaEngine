using Rectangle = NovaEngine.Common.Windows.Native.Rectangle;

namespace NovaEngine.Common.Windows.Api;

/// <summary>Exposes necessary User32.dll apis.</summary>
public static class User32
{
    /*********
    ** Constants
    *********/
    /// <summary>Determines whether a <see cref="VirtualKey.Shift"/> is a <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/>.</summary>
    public const ushort ShiftRight = 0x36;


    /*********
    ** Public Methods
    *********/
    /// <summary>Retrieves the raw input from the specified device.</summary>
    /// <param name="rawInput">A handle to the <see cref="RawInput"/> structure. This comes from the lParam in <see cref="Message.Input"/>.</param>
    /// <param name="data">The raw input data.</param>
    /// <returns>The size, in <see langword="byte"/>s, of the data in <paramref name="data"/>.</returns>
    public unsafe static uint GetRawInputData(IntPtr rawInput, out RawInput data)
    {
        var size = (uint)Marshal.SizeOf<RawInput>();
        fixed (RawInput* dataPointer = &data)
            GetRawInputData(rawInput, GetRawInputDataCommand.Input, (IntPtr)dataPointer, ref size, (uint)Marshal.SizeOf<RawInputHeader>());
        return size;
    }

    /// <summary>Retrieves the raw input header from the specified device.</summary>
    /// <param name="rawInput">A handle to the <see cref="RawInput"/> structure. This comes from the lParam in <see cref="Message.Input"/>.</param>
    /// <param name="header">The raw header data.</param>
    /// <returns>The size, in <see langword="byte"/>s, of the data in <paramref name="header"/>.</returns>
    public unsafe static uint GetRawInputData(IntPtr rawInput, out RawInputHeader header)
    {
        var size = (uint)Marshal.SizeOf<RawInputHeader>();
        fixed (RawInputHeader* headerPointer = &header)
            GetRawInputData(rawInput, GetRawInputDataCommand.Header, (IntPtr)headerPointer, ref size, (uint)Marshal.SizeOf<RawInputHeader>());
        return size;
    }

    /// <summary>Changes an attribute of the specified window.</summary>
    /// <param name="window">A handle to the window and, indirectly, the class to which the window belongs.</param>
    /// <param name="index">The zero-based offset to the value to be set. Valid values are in the range zero through the number of <see langword="byte"/>s of extra window memory, minus the size of an <see langword="int"/>. To set any other value, specify one of the <see cref="WindowLongOffset"/> values.</param>
    /// <param name="newValue">The replacement value.</param>
    /// <returns>The previous value of the specified offset, if the call succeeds; otherwise, 0.</returns>
    public static IntPtr SetWindowLong(IntPtr window, WindowLongOffset index, IntPtr newValue)
    {
        // set the last error to zero, this is because SetWindowLongPtr will return 0 in the case of an error *or* if the previous value of the specified int is 0
        // this means we can call GetLastError to determine if an error actually occurred (GetLastError will return a non-zero value if an error occurred)
        Kernel32.SetLastError(0);

        var returnValue = SetWindowLongPtr(window, index, newValue);
        if (returnValue == IntPtr.Zero)
        {
            var error = Marshal.GetLastWin32Error();
            if (error != 0) // check if an error actually occurred
                throw new Win32Exception($"Failed to modify window attribute: {error}").Log(LogSeverity.Fatal);
        }

        return returnValue;
    }

    /// <summary>Calculates the required size of the window rectangle, based on the desired client-rectangle size.</summary>
    /// <param name="rectangle">A pointer to a <see cref="Rectangle"/> structure that contains the coordinates of the top-left and bottom-right corners of the desired client area. When the function returns, the structure contains the coordinates of the top-left and bottom-right corners of the window to accommodate the desired client area.</param>
    /// <param name="style">The window style of the window whose required size is to be calculated. Note that you cannot specify the <see cref="WindowStyle.Overlapped"/> style.</param>
    /// <param name="menu">Indicates whether the window has a menu.</param>
    /// <returns><see langword="true"/>; if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public unsafe static extern bool AdjustWindowRect(Rectangle* rectangle, WindowStyle style, bool menu);

    /// <summary>Passes message information to the specified window procedure.</summary>
    /// <param name="previousWindowProc">The previous window procedure to receive the message.</param>
    /// <param name="window">A handle to the window.</param>
    /// <param name="message">The message.</param>
    /// <param name="wParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
    /// <param name="lParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
    /// <returns>The result of the message processing which depends on the message.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern IntPtr CallWindowProc(IntPtr previousWindowProc, IntPtr window, Message message, IntPtr wParam, IntPtr lParam);

    /// <summary>Converts the client-area coordinates of the specified point to screen coordinates.</summary>
    /// <param name="window">A handle to the window whose client area is used for the conversion.</param>
    /// <param name="point">A reference to the structure that contains the client coordinates to be converted. The new screen coordinates are copied into this structure if the function succeeds.</param>
    /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool ClientToScreen(IntPtr window, ref Vector2I point);

    /// <summary>Confines the cursor to a rectangular area on the screen.</summary>
    /// <param name="rectangle">A pointer to the structure that contains the screen coordinates of the upper-left and lower-right corners of the confining rectangle. If this parameter is <see langword="null"/>, the cursor is free to move anywhere on the screen.</param>
    /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public unsafe static extern bool ClipCursor(Rectangle* rectangle);

    /// <summary>Creates a window with an extended window style.</summary>
    /// <param name="exStyle">The extended window style of the window being created.</param>
    /// <param name="className">A class name that has previously been registered using the <see cref="RegisterClass(in WindowClass)"/> method.</param>
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
    /// <returns>A handle to the new window, if the call succeeds; otherwise, <see cref="IntPtr.Zero"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern IntPtr CreateWindowEx(uint exStyle, string className, string windowName, WindowStyle style, int x, int y, int width, int height, IntPtr windowParent, IntPtr menu, IntPtr handle, IntPtr lpParam);

    /// <summary>Calls the default window procedure to provide basic default processing for any window message that an application does not process.</summary>
    /// <param name="window">A handle to the window procedure that received the message.</param>
    /// <param name="message">The message.</param>
    /// <param name="wParam">Additional message information. The content of this parameter depends on the value of the <paramref name="message"/> parameter.</param>
    /// <param name="lParam">Additional message information. The content of this parameter depends on the value of the <paramref name="message"/> parameter.</param>
    /// <returns>The result of the message processing, which depends on the message sent.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern IntPtr DefWindowProc(IntPtr window, Message message, IntPtr wParam, IntPtr lParam);

    /// <summary>Destroys the specified window.</summary>
    /// <param name="windowHandle">A handle to the window to be destroyed.</param>
    /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool DestroyWindow(IntPtr windowHandle);

    /// <summary>Dispatches a message to a window procedure.</summary>
    /// <param name="message">The message information.</param>
    /// <returns>The value returned by the window procedure, which depends on the message being dispatched.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern IntPtr DispatchMessage(in Msg message);

    /// <summary>Retrieves the screen coordinates of the rectangle area to which the cursor is confined.</summary>
    /// <param name="rectangle">A reference to the <see cref="Rectangle"/> structure that receives the screen coordinates of the confining rectangle. The structure receives the dimensions of the screen if the cursor is not confined to a rectangle.</param>
    /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool GetClipCursor(ref Rectangle rectangle);

    /// <summary>Retrieves information about the global cursor.</summary>
    /// <param name="cursorInfo">The structure to populate with the cursor information.</param>
    /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool GetCursorInfo(ref CursorInfo cursorInfo);

    /// <summary>Retrieves the position of the mouse cursor, in screen coordinates.</summary>
    /// <param name="point">The structure to populate with the screen coordinates of the cursor.</param>
    /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool GetCursorPos(ref Vector2I point);

    /// <summary>Retrieves the raw input from the specified device.</summary>
    /// <param name="rawInput">A handle to the <see cref="RawInput"/> structure. This comes from the lParam in WM_INPUT.</param>
    /// <param name="command">The command flag.</param>
    /// <param name="data">A pointer to the data that comes from the <see cref="RawInput"/> structure. This depends on the value of <paramref name="command"/>. If <paramref name="data"/> is <see langword="null"/>, the required size of the butter is returning in <paramref name="size"/>.</param>
    /// <param name="size">The size, in <see langword="byte"/>s, of the data in <paramref name="data"/>.</param>
    /// <param name="sizeHeader">The size, in <see langword="byte"/>s, of the <see cref="RawInputHeader"/> structure.</param>
    /// <returns>If <paramref name="data"/> is <see langword="null"/> and the function is successful, the return value is 0. If <paramref name="data"/> is not <see langword="null"/> and the function is successful, the return value is the number of <see langword="byte"/>s copied into <paramref name="data"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern uint GetRawInputData(IntPtr rawInput, GetRawInputDataCommand command, [Out] IntPtr data, ref uint size, uint sizeHeader);

    /// <summary>Retrieves the text of the specified window's title bar (if it has one).</summary>
    /// <param name="windowHandle">A handle to the window whose title is to be retrieved.</param>
    /// <param name="text">The window title.</param>
    /// <param name="maxCount">The max length of <paramref name="text"/>.</param>
    /// <returns>The length of the text, if the call succeeds; otherwise, 0.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern int GetWindowText(IntPtr windowHandle, out string text, int maxCount);

    /// <summary>Loads the specified cursor resource.</summary>
    /// <param name="handle">A handle to an instance of the module whose executable file contains the cursor to be loaded.<br/>To use a system cursor, <see cref="IntPtr.Zero"/> must be specified.</param>
    /// <param name="cursorName">The name of the cursor resource to be loaded.</param>
    /// <returns>A handle to the newly loaded cursor, if the call succeeds; otherwise, <see cref="IntPtr.Zero"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern IntPtr LoadCursor(IntPtr handle, int cursorName);

    /// <summary>Dispatches incoming sent messages, chest the thread message queue for a posted message, and retrieves the message (if any exist).</summary>
    /// <param name="message">The received message information.</param>
    /// <param name="window">A handle to the window whose messages are to be retrieved.</param>
    /// <param name="messageFilterMin">The value of the first message in the range of messages to be examined.</param>
    /// <param name="messageFilterMax">The value of the last message in the range of messages to be examined.</param>
    /// <param name="removeMessage">How the messages are to be handled.</param>
    /// <returns><see langword="true"/>, if a message is available; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool PeekMessage(out Msg message, IntPtr window, uint messageFilterMin, uint messageFilterMax, RemoveMessage removeMessage);

    /// <summary>Indicates to the system that a thread has made a request to terminate (quit).</summary>
    /// <param name="exitCode">The application exit code.</param>
    [DllImport("User32", SetLastError = true)]
    public static extern void PostQuitMessage(int exitCode);

    /// <summary>Registers a window class for subsequent calls to the <see cref="CreateWindowEx(uint, string, string, WindowStyle, int, int, int, int, IntPtr, IntPtr, IntPtr, IntPtr)"/> method.</summary>
    /// <param name="windowClass">The window class to register.</param>
    /// <returns>A class atom that uniquely identifies the class being registered, if the call succeeds; otherwise, 0.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern ushort RegisterClass(in WindowClass windowClass);

    /// <summary>Registers the devices that supply the raw input data.</summary>
    /// <param name="rawInputDevices">The devices that represent the raw input.</param>
    /// <param name="numDevices">The number of <see cref="RawInputDevice"/> structures in <paramref name="rawInputDevices"/>.</param>
    /// <param name="size">The size, in <see langword="byte"/>s, of a <see cref="RawInputDevice"/> structure.</param>
    /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool RegisterRawInputDevices(RawInputDevice[] rawInputDevices, uint numDevices, uint size);

    /// <summary>Converts the screen coordinates of the specified point to client-area coordinates.</summary>
    /// <param name="window">A handle to the window whose client area is used for the conversion.</param>
    /// <param name="point">A reference to the structure that contains the screen coordinates to be converted. The new client coordinates are copied into this structure if the function succeeds.</param>
    /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool ScreenToClient(IntPtr window, ref Vector2I point);

    /// <summary>Changes an attribute of the specified window.</summary>
    /// <param name="window">A handle to the window and, indirectly, the class to which the window belongs.</param>
    /// <param name="index">The zero-based offset to the value to be set. Valid values are in the range zero through the number of <see langword="byte"/>s of extra window memory, minus the size of am <see cref="IntPtr"/>. To set any other value, specify one of the <see cref="WindowLongOffset"/> values.</param>
    /// <param name="newValue">The replacement values.</param>
    /// <returns>The previous value of the specified offset, if the call succeeds; otherwise, 0.</returns>
    /// <remarks>This is only available on 64-bit Windows.</remarks>
    [DllImport("User32", SetLastError = true)]
    public static extern IntPtr SetWindowLongPtr(IntPtr window, WindowLongOffset index, IntPtr newValue);

    /// <summary>Changes the text of the specified window's title bar (if it has one).</summary>
    /// <param name="windowHandle">A handle to the window whose title is to be changed.</param>
    /// <param name="text">The new title.</param>
    /// <returns><see langword="true"/>, if the title was changed successfully; otherwise, <see langword="false"/>.</returns>
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
    /// <returns><see langword="true"/>, if the window was previously visible; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool ShowWindow(IntPtr window, CommandShow commandShow);

    /// <summary>Translates virtual-key messages into character messages.</summary>
    /// <param name="message">The message information that was retrieved from the calling thread's message queue by using the <see cref="PeekMessage(out Msg, IntPtr, uint, uint, RemoveMessage)"/> method.</param>
    /// <returns><see langword="true"/>, if the message is translated; otherwise, <see langword="false"/>.</returns>
    [DllImport("User32", SetLastError = true)]
    public static extern bool TranslateMessage(in Msg message);
}
