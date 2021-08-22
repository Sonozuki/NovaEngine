using System;

namespace NovaEngine.InputHandler.RawWindows
{
    /// <summary>Contains the header information that is part of the raw input data.</summary>
    internal struct RawInputHeader
    {
        /*********
        ** Fields
        *********/
        /// <summary>The type of raw input.</summary>
        public RawInputDeviceType Type;

        /// <summary>The size, in <see langword="byte"/>s, of the enture input packet of data, This includes <see cref="RawInput"/> plus possible extra input reports in the <see cref="RawHid"/> variable length array.</summary>
        public int Size;

        /// <summary>A handle to the device generating the raw input data.</summary>
        public IntPtr Device;

        /// <summary>The value passed in the Param parameter of the WM_INPUT message.</summary>
        public IntPtr Param;
    }
}
