namespace NovaEngine.IO;

/// <summary>The type of press for input events.</summary>
public enum PressType
{
    /// <summary>The event will be invoked the tick a button is pressed.</summary>
    Press,

    /// <summary>The event will be invoked the tick a button is pressed twice concurrently.</summary>
    DoublePress,

    /// <summary>The event will be invoked the tick a button is released.</summary>
    Release,

    /// <summary>The event will be invoked for every tick which the button is held down (this doesn't include the first frame, the one that <see cref="Press"/> will be invoked on).</summary>
    Hold
}
