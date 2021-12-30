namespace NovaEngine.Common.Windows.Native;

/// <summary>Contains message information from a thread's message queue.</summary>
public struct Msg
{
    /*********
    ** Fields
    *********/
    /// <summary>A handle to the window whose window procedure receives the message. This is <see langword="null"/> when the message is a thread message.</summary>
    public IntPtr Handle;

    /// <summary>The message identifier.</summary>
    public uint Message;

    /// <summary>Additional information about the message. The exact meaning depends on the value of the <see cref="Message"/> member.</summary>
    public IntPtr WParam;

    /// <summary>Additional information about the message. The exact meaning depends on the value of the <see cref="Message"/> member.</summary>
    public IntPtr LParam;

    /// <summary>The time at which the message was posted.</summary>
    public ulong Time;

    /// <summary>The cursor position, in screen coordinates, when the message was posted.</summary>
    public Vector2I Point;
}
