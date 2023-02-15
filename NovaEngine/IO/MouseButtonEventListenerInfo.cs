namespace NovaEngine.IO;

/// <summary>The info about the state of a mouse button to listen for, for input event handling.</summary>
internal class MouseButtonEventListenerInfo
{
    /*********
    ** Properties
    *********/
    /// <summary>The button to listen to.</summary>
    public MouseButton Button { get; }

    /// <summary>The type of press to listen to.</summary>
    public PressType PressType { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="button">The button to listen to.</param>
    /// <param name="pressType">The type of press to listen to.</param>
    public MouseButtonEventListenerInfo(MouseButton button, PressType pressType)
    {
        Button = button;
        PressType = pressType;
    }
}
