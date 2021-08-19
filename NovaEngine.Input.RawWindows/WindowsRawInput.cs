using NovaEngine.External.Input;

namespace NovaEngine.Input.RawWindows
{
    /// <summary>Represents the raw Windows input handler.</summary>
    public class WindowsRawInput : IInput
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool CanUseOnPlatform { get; }

        /// <inheritdoc/>
        public MouseState MouseState { get; }

        /// <inheritdoc/>
        public KeyboardState KeyboardState { get; }
    }
}
