namespace NovaEngine.InputHandler.RawWindows
{
    /// <summary>Contains information about the state of a keyboard.</summary>
    internal struct RawKeyboard
    {
        /*********
        ** Fields
        *********/
        /// <summary>Specifies the scan code associatied with a key press.</summary>
        public ushort MakeCode;

        /// <summary>Flags for scan code information.</summary>
        public RawInputKeyboardDataFlags Flags;

        /// <summary>Reserved; must be zero.</summary>
        private ushort Reserved;

        /// <summary>The corresponding <see cref="Input.VirtualKey"/> code.</summary>
        public VirtualKey VirtualKey;

        /// <summary>Corresponding window message, for example WM_KEYDOWN, WM_SYSKEYDOWN, and so forth.</summary>
        public uint Message;

        /// <summary>The device-specific additional information for the event.</summary>
        public ulong ExtraInformation;
    }
}
