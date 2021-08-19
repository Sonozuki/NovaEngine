namespace NovaEngine.External.Input
{
    /// <summary>Represents an input handler.</summary>
    public interface IInput
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether the renderer can be used on the current environment platform.</summary>
        public bool CanUseOnPlatform { get; }

        /// <summary>The current state of the mouse.</summary>
        public MouseState MouseState { get; }

        /// <summary>The current state of the keyboard.</summary>
        public KeyboardState KeyboardState {  get; }
    }
}
