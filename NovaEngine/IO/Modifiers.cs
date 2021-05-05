using System;

namespace NovaEngine.IO
{
    /// <summary>The modifier keys for input events.</summary>
    [Flags]
    public enum Modifiers
    {
        /// <summary>No modifiers.</summary>
        None = 0,

        /// <summary>Either 'Ctrl' key.</summary>
        Ctrl = 1 << 0,

        /// <summary>Either 'Shift' key.</summary>
        Shift = 1 << 1,

        /// <summary>Either 'Alt' key.</summary>
        Alt = 1 << 2
    }
}
