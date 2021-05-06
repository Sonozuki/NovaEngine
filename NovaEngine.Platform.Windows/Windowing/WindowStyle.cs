using System;

namespace NovaEngine.Platform.Windows.Windowing
{
    /// <summary>The styles a window can have.</summary>
    [Flags]
    public enum WindowStyle : uint
    {
        /// <summary>The window has a title bar.</summary>
        Caption = 0x00C00000,

        /// <summary>The window has a maximise button. The <see cref="SysMenu"/> style must also be specified.</summary>
        MaximiseBox = 0x00010000,

        /// <summary>The window has a minimise button. The <see cref="SysMenu"/> style must also be specified.</summary>
        MinimiseBox = 0x00020000,

        /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
        Overlapped = 0x00000000,

        /// <summary>The window is an overlapped window.</summary>
        OverlappedWindow = Overlapped | Caption | SysMenu | ThickFrame | MinimiseBox | MaximiseBox,

        /// <summary>The window has a window menu on its title bar. The <see cref="Caption"/> style must also be specified.</summary>
        SysMenu = 0x00080000,

        /// <summary>The window has a sizing border.</summary>
        ThickFrame = 0x00040000,
    }
}
