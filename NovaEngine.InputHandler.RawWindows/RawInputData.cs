using System.Runtime.InteropServices;

namespace NovaEngine.InputHandler.RawWindows
{
    /// <summary>A structure that can be used to store one of: <see cref="RawMouse"/>, <see cref="RawKeyboard"/>, or <see cref="RawHid"/>.</summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct RawInputData
    {
        /*********
        ** Fields
        *********/
        /// <summary>The structure this is used if the data comes from a mouse.</summary>
        [FieldOffset(0)]
        public RawMouse Mouse;

        /// <summary>The structure that is used if the data comes from a keyboard.</summary>
        [FieldOffset(0)]
        public RawKeyboard Keyboard;

        /// <summary>The structure that is used if the data comes from a Human Interface Device (HID).</summary>
        [FieldOffset(0)]
        public RawHid Hid;
    }
}
