using NovaEngine.Maths;
using System;

namespace NovaEngine.Platform.Windows
{
    /// <summary>Constains global cursor information.</summary>
    internal struct NativeCursorInfo
    {
        /*********
        ** Fields
        *********/
        /// <summary>The size of the structure, in <see langword="byte"/>s.</summary>
        public uint Size;

        /// <summary>The state of the cursor.</summary>
        public CursorState Flags;

        /// <summary>A handle to the cursor.</summary>
        public IntPtr Cursor;

        /// <summary>The screen coordinates of the cursor.</summary>
        public Vector2I ScreenPosition;
    }
}
