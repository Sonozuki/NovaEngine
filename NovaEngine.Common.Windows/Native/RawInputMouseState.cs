using System;

namespace NovaEngine.Common.Windows.Native;

/// <summary>The transitions state of the mouse buttons.</summary>
[Flags]
public enum RawInputMouseState : ushort
{
    /// <summary>Left button changed to down.</summary>
    LeftButtonDown = 0x0001,

    /// <summary>Left button changed to up.</summary>
    LeftButtonUp = 0x0002,

    /// <summary>Right button changed to down.</summary>
    RightButtonDown = 0x0004,

    /// <summary>Right button changed to up.</summary>
    RightButtonUp = 0x0008,

    /// <summary>Middle button changed to down.</summary>
    MiddleButtonDown = 0x0010,

    /// <summary>Middle button changed to up.</summary>
    MiddleButtonUp = 0x0020,

    /// <summary>Left button changed to down.</summary>
    Button1Down = LeftButtonDown,

    /// <summary>Left button changed to up.</summary>
    Button1Up = LeftButtonUp,

    /// <summary>Right button changed to down.</summary>
    Button2Down = RightButtonDown,

    /// <summary>Right button changed to up.</summary>
    Button2Up = RightButtonUp,

    /// <summary>Middle button changed to down.</summary>
    Button3Down = MiddleButtonDown,

    /// <summary>Middle button changed to up.</summary>
    Button3Up = MiddleButtonUp,

    /// <summary>XButton 1 changed to down.</summary>
    Button4Down = 0x0040,

    /// <summary>XButton 1 changed to up.</summary>
    Button4Up = 0x0080,

    /// <summary>XButton 2 changed to down.</summary>
    Button5Down = 0x0100,

    /// <summary>XButton 2 changed to up.</summary>
    Button5Up = 0x0200,

    /// <summary>Raw input comes from a mouse wheel. The wheel delta is stored in <see cref="RawMouse.ButtonData"/>.</summary>
    Wheel = 0x0400,

    /// <summary>Raw input comes from a horizontal mouse wheel. The wheel delta is stored in <see cref="RawMouse.ButtonData"/>.</summary>
    HWheel = 0x0800
}
