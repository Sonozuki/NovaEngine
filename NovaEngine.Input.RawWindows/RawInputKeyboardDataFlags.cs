using System;

namespace NovaEngine.Input.RawWindows
{
    /// <summary>Flags for scan code information.</summary>
    [Flags]
    internal enum RawInputKeyboardDataFlags : ushort
    {
        /// <summary>The key is down.</summary>
        Make = 0x00,

        /// <summary>The key is up.</summary>
        Break = 0x01,

        /// <summary>The scan code has the E0 prefix.</summary>
        E0 = 0x02,

        /// <summary>The scan code has the E1 prefix.</summary>
        E1 = 0x04,
    }
}
