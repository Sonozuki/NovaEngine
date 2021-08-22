using NovaEngine.Extensions;
using NovaEngine.IO;
using NovaEngine.Logging;
using NovaEngine.Maths;
using System;
using System.Runtime.InteropServices;

namespace NovaEngine.InputHandler.RawWindows
{
    /// <summary>Exposes neccessary User32.dll apis.</summary>
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

        /// <summary>Changes an attribute of the specified window.</summary>
        /// <param name="window">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="index">The zero-based offset to the value to be set. Valid values are in the range zero through the number of <see langword="byte"/>s of extra window memory, minus the size of an <see langword="int"/>. To set any other value, specify one of the <see cref="WindowLongOffset"/> values.</param>
        /// <param name="newValue">The replacement value.</param>
        /// <returns>If the method succeeds, the return value is the previous value of the specified offset; otherwise, zero.</returns>
        public static IntPtr SetWindowLong(IntPtr window, WindowLongOffset index, IntPtr newValue)
        {
            // set the last error to zero, this is because SetWindowLongPtr will return 0 in the case of an error *or* if the previous value of the specified int is 0
            // this means we can call GetLastError to determine if an error actually occured (GetLastError will return a non-zero value if an error occured
            Kernel32.SetLastError(0);

            var returnValue = SetWindowLongPtr(window, index, newValue);
            if (returnValue == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                if (error != 0) // check if an error actually occured
                    throw new ApplicationException($"Failed to modify window attribute: {error}").Log(LogSeverity.Fatal);
            }

            return returnValue;
        }

        /// <summary>Passes message information to the specified window procedure.</summary>
        /// <param name="previousWindowProc">The previous window procedure to receive the message.</param>
        /// <param name="window">A handle to the window.</param>
        /// <param name="message">The message.</param>
        /// <param name="wParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
        /// <param name="lParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
        /// <returns>The result of the message processing which depends on the message.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr CallWindowProc(IntPtr previousWindowProc, IntPtr window, Message message, IntPtr wParam, IntPtr lParam);

        /// <summary>Calls the default window procedure to provide basic default processing for any window message that an application does not process.</summary>
        /// <param name="window">A handle to the window procedure that received the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="wParam">Additional message information. The content of this parameter depends on the value of the <paramref name="message"/> parameter.</param>
        /// <param name="lParam">Additional message information. The content of this parameter depends on the value of the <paramref name="message"/> parameter.</param>
        /// <returns>The result of the message processing which depends on the message.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr DefWindowProc(IntPtr window, Message message, IntPtr wParam, IntPtr lParam);

        /// <summary>Retrieves the position of the mouse cursor, in screen coordinates.</summary>
        /// <param name="point">The structure to populate with the screen coordinates of the cursor.</param>
        /// <returns><see langword="true"/>, if the call succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool GetCursorPos(ref Vector2I point);

        /// <summary>Retreives the raw input from the specified device.</summary>
        /// <param name="rawInput">A handle to the <see cref="RawInput"/> structure. This comes from the lParam in WM_INPUT.</param>
        /// <param name="command">The command flag.</param>
        /// <param name="data">A pointer to the data that comes from the <see cref="RawInput"/> structure. This depends on the value of <paramref name="command"/>. If <paramref name="data"/> is <see langword="null"/>, the required size of the butter is returning in <paramref name="size"/>.</param>
        /// <param name="size">The size, in <see langword="byte"/>s, of the data in <paramref name="data"/>.</param>
        /// <param name="sizeHeader">The size, in <see langword="byte"/>s, of the <see cref="RawInputHeader"/> structure.</param>
        /// <returns>If <paramref name="data"/> is <see langword="null"/> and the function is successful, the return value is 0. If <paramref name="data"/> is not <see langword="null"/> and the function is successful, the return value is the number of <see langword="byte"/>s copied into <paramref name="data"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern uint GetRawInputData(IntPtr rawInput, GetRawInputDataCommand command, [Out] IntPtr data, ref uint size, uint sizeHeader);

        /// <summary>Registers the devices that supply the raw input data.</summary>
        /// <param name="rawInputDevices">The devices that represent the raw input.</param>
        /// <param name="numDevices">The number of <see cref="RawInputDevice"/> strucures in <paramref name="rawInputDevices"/>.</param>
        /// <param name="size">The size, in <see langword="byte"/>s, of a <see cref="RawInputDevice"/> structure.</param>
        /// <returns><see langword="true"/> is the function succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32", SetLastError = true)]
        public static extern bool RegisterRawInputDevices(RawInputDevice[] rawInputDevices, uint numDevices, uint size);

        /// <summary>Changes an attribute of the specified window.</summary>
        /// <param name="window">A handle to the window and, indirectly, athe class to which the window belongs.</param>
        /// <param name="index">The zero-based offset to the value to be set. Valid values are in the range zero through the number of <see langword="byte"/>s of extra window memory, minus the size of am <see cref="IntPtr"/>. To set any other value, specify one of the <see cref="WindowLongOffset"/> values.</param>
        /// <param name="newValue">The replacement values.</param>
        /// <returns>If the method succeeds, the return value is the previous value of the specified offset; otherwise, zero.</returns>
        /// <remarks>This is only available on 64bit Windows.</remarks>
        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr SetWindowLongPtr(IntPtr window, WindowLongOffset index, IntPtr newValue);
    }
}
