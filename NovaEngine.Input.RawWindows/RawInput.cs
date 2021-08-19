﻿namespace NovaEngine.Input.RawWindows
{
    /// <summary>Contains the raw input from a device.</summary>
    internal struct RawInput
    {
        /*********
        ** Fields
        *********/
        /// <summary>The raw input header.</summary>
        public RawInputHeader Header;

        /// <summary>The raw input data.</summary>
        public RawInputData Data;
    }
}
