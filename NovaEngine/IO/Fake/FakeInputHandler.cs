using NovaEngine.External.Input;

namespace NovaEngine.IO.Fake
{
    /// <summary>Represents an input handler that is only used when nova is being used without a program instance.</summary>
    internal class FakeInputHandler : IInput
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool CanUseOnPlatform => true;

        /// <inheritdoc/>
        public MouseState MouseState => new();

        /// <inheritdoc/>
        public KeyboardState KeyboardState => new();
    }
}
