namespace NovaEngine.External.Input
{
    /// <summary>Represents an input handler.</summary>
    public interface IInputHandler
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether the input handler can be used on the current environment platform.</summary>
        public bool CanUseOnPlatform { get; }

        /// <summary>The current state of the mouse.</summary>
        public MouseState MouseState { get; set; }

        /// <summary>The current state of the keyboard.</summary>
        public KeyboardState KeyboardState { get; set; }

        /// <summary>Whether the cursor is currently visible.</summary>
        public bool IsCursorVisible { get; set; }

        /// <summary>Whether the cursor is currently locked.</summary>
        public bool IsCursorLocked { get; set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Invoked when the input handler should initialise itself.</summary>
        /// <param name="windowHandle">The handle to the application window.</param>
        public void OnInitialise(IntPtr windowHandle);
    }
}
