namespace NovaEngine.Common.Windows.Native;

/// <summary>Mouse indicator flags.</summary>
[Flags]
public enum RawMouseFlags : ushort
{
    /// <summary>Mouse movement data is relativie to the last mouse position.</summary>
    MoveRelative = 0x00,

    /// <summary>Mouse movement data is based on absolute position.</summary>
    MoveAbsolute = 0x01,

    /// <summary>Mouse coordinates are mapped to the vitual desktop (for a multiple monitor system).</summary>
    VirtualDesktop = 0x02,

    /// <summary>Mouse attributes changed; application needs to query the mouse attributes.</summary>
    AttributesChanged = 0x04,

    /// <summary>The mouse movement event was not coalesced.</summary>
    MoveNoCoalesce = 0x08
}
