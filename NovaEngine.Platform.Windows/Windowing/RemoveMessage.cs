namespace NovaEngine.Platform.Windows.Windowing
{
    /// <summary>Specifies how messages are to be handled.</summary>
    public enum RemoveMessage
    {
        /// <summary>Messages are not removed from the queue after processsing.</summary>
        NoRemove,

        /// <summary>Messages are removed from the queue after processing.</summary>
        Remove,

        /// <summary>Prevents the system from releasing any thread that is waiting for the called to go idle.</summary>
        /// <remarks>Combine this valud with either <see cref="NoRemove"/> or <see cref="Remove"/>.</remarks>
        NoYield
    }
}
