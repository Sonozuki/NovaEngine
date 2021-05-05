namespace NovaEngine.IO
{
    /// <summary>The info about the state of a key to listen to for event handling.</summary>
    internal struct KeyEventListenerInfo
    {
        /*********
        ** Fields
        *********/
        /// <summary>The key to listen to.</summary>
        public Key Key;

        /// <summary>The modifier(s) to listen to.</summary>
        public Modifiers Modifiers;

        /// <summary>The type of press to listen to.</summary>
        public PressType PressType;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="key">The key to listen to.</param>
        /// <param name="modifiers">The modifier(s) to listen to.</param>
        /// <param name="pressType">The type of press to listen to.</param>
        public KeyEventListenerInfo(Key key, Modifiers modifiers, PressType pressType)
        {
            Key = key;
            Modifiers = modifiers;
            PressType = pressType;
        }
    }
}
