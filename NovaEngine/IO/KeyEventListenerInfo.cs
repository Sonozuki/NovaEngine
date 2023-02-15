namespace NovaEngine.IO;

/// <summary>The info about the state of a key to listen for, for input event handling.</summary>
internal class KeyEventListenerInfo
{
    /*********
    ** Properties
    *********/
    /// <summary>The key to listen for.</summary>
    public Key Key { get; }

    /// <summary>The modifier(s) to listen for.</summary>
    public Modifiers Modifiers { get; }

    /// <summary>The type of press to listen for.</summary>
    public PressType PressType { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="key">The key to listen for.</param>
    /// <param name="modifiers">The modifier(s) to listen for.</param>
    /// <param name="pressType">The type of press to listen for.</param>
    public KeyEventListenerInfo(Key key, Modifiers modifiers, PressType pressType)
    {
        Key = key;
        Modifiers = modifiers;
        PressType = pressType;
    }
}
