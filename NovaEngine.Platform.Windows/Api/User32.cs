using NovaEngine.Platform.Windows.Input;
using NovaEngine.Platform.Windows.Windowing;
using System;
using System.Runtime.InteropServices;

namespace NovaEngine.Platform.Windows.Api
{
    /// <summary>Exposes User32.dll apis.</summary>
    internal static class User32
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

        /// <summary>Retreives the raw input header from the specified device.</summary>
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
        /// <returns>The result of the message processing which depends on the message.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr DefWindowProc(IntPtr window, Message message, IntPtr wParam, IntPtr lParam);

        /// <summary>Dispatches a message to a window procedure.</summary>
        /// <param name="message">The message information.</param>
        /// <returns>The value returned by the window procedure, which depends on the message being dispatched.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr DispatchMessage(in NativeMessage message);

        /// <summary>Retreives the raw input from the specified device.</summary>
        /// <param name="rawInput">A handle to the <see cref="RawInput"/> structure. This comes from the lParam in WM_INPUT.</param>
        /// <param name="command">The command flag.</param>
        /// <param name="data">A pointer to the data that comes from the <see cref="RawInput"/> structure. This depends on the value of <paramref name="command"/>. If <paramref name="data"/> is <see langword="null"/>, the required size of the butter is returning in <paramref name="size"/>.</param>
        /// <param name="size">The size, in <see langword="byte"/>s, of the data in <paramref name="data"/>.</param>
        /// <param name="sizeHeader">The size, in <see langword="byte"/>s, of the <see cref="RawInputHeader"/> structure.</param>
        /// <returns>If <paramref name="data"/> is <see langword="null"/> and the function is successful, the return value is 0. If <paramref name="data"/> is not <see langword="null"/> and the function is successful, the return value is the number of <see langword="byte"/>s copied into <paramref name="data"/>.</returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern uint GetRawInputData(IntPtr rawInput, GetRawInputDataCommand command, [Out] IntPtr data, ref uint size, uint sizeHeader);

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

        /// <summary>Registers the devices that supply the raw input data.</summary>
        /// <param name="rawInputDevices">The devices that represent the raw input.</param>
        /// <param name="numDevices">The number of <see cref="RawInputDevice"/> strucures in <paramref name="rawInputDevices"/>.</param>
        /// <param name="size">The size, in <see langword="byte"/>s, of a <see cref="RawInputDevice"/> structure.</param>
        /// <returns><see langword="true"/> is the function succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool RegisterRawInputDevices(RawInputDevice[] rawInputDevices, uint numDevices, uint size);

        /// <summary>Changes the text of the specified window's title bar (if it has one).</summary>
        /// <param name="windowHandle">A handle to the window whose title is to be changed.</param>
        /// <param name="text">The new title.</param>
        /// <returns><see langword="true"/> if the title was changed successfully; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool SetWindowText(IntPtr windowHandle, string text);

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
