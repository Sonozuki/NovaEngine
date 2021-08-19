using NovaEngine.IO;
using NovaEngine.Maths;
using System;
using System.Runtime.InteropServices;

namespace NovaEngine.Input.RawWindows
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
        [DllImport("User32.dll", SetLastError = true)]
        public static extern uint GetRawInputData(IntPtr rawInput, GetRawInputDataCommand command, [Out] IntPtr data, ref uint size, uint sizeHeader);

        /// <summary>Registers the devices that supply the raw input data.</summary>
        /// <param name="rawInputDevices">The devices that represent the raw input.</param>
        /// <param name="numDevices">The number of <see cref="RawInputDevice"/> strucures in <paramref name="rawInputDevices"/>.</param>
        /// <param name="size">The size, in <see langword="byte"/>s, of a <see cref="RawInputDevice"/> structure.</param>
        /// <returns><see langword="true"/> is the function succeeds; otherwise, <see langword="false"/>.</returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool RegisterRawInputDevices(RawInputDevice[] rawInputDevices, uint numDevices, uint size);
    }
}
