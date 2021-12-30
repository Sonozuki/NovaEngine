namespace NovaEngine.Common.Windows.Native;

/// <summary>Contains information about the state of the mouse.</summary>
[StructLayout(LayoutKind.Explicit)]
public struct RawMouse
{
    /*********
    ** Fields
    *********/
    /// <summary>The mouse state.</summary>
    [FieldOffset(0)]
    public RawMouseFlags Flags;

    /// <summary>The transition state of the mouse buttons.</summary>
    [FieldOffset(4)]
    public RawInputMouseState ButtonFlags;

    /// <summary>If <see cref="ButtonFlags"/> has <see cref="RawInputMouseState.Wheel"/> or <see cref="RawInputMouseState.HWheel"/>, this member specifies the distance the wheel is rotated.</summary>
    [FieldOffset(6)]
    public ushort ButtonData;

    /// <summary>The raw state of the mouse buttons.</summary>
    [FieldOffset(8)]
    public uint RawButtons;

    /// <summary>The motion in the X direction. This is signed relative motion or absolute motion, depending on the value of <see cref="Flags"/>.</summary>
    [FieldOffset(12)]
    public int LastX;

    /// <summary>The motion in the Y direction. This is signed relative motion or absolute motion, depending on the value of <see cref="Flags"/>.</summary>
    [FieldOffset(16)]
    public int LastY;

    /// <summary>The device-specific additional information for the event.</summary>
    [FieldOffset(20)]
    public uint ExtraInformation;
}
