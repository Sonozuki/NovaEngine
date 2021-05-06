namespace NovaEngine.IO
{
    /// <summary>The info about the state of a mouse button to listen to for event handling.</summary>
    internal struct MouseButtonEventListenerInfo
    {
        /*********
        ** Fields
        *********/
        /// <summary>The button to listen to.</summary>
        public MouseButton Button;

        /// <summary>The type of press to listen to.</summary>
        public PressType PressType;


        /*********
        ** Public Methods
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
}
