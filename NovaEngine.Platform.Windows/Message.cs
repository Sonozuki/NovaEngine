namespace NovaEngine.Platform.Windows
{
    /// <summary>The messages a window procedure can receive.</summary>
    public enum Message
    {
        /// <summary>Sent to a window when it's being destroyed. It is sent after the window has been removed from the screen.</summary>
        Destroy = 0x0002,

        /// <summary>Sent to a window after its size has changed.</summary>
        Size = 0x0005,

        /// <summary>Sent to a window immediately before it loses the keyboard focus.</summary>
        KillFocus = 0x0008
    }
}
