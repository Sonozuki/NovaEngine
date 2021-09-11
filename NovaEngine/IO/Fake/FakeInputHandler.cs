using NovaEngine.External.Input;
using System;

namespace NovaEngine.IO.Fake
{
    /// <summary>Represents an input handler that is only used when nova is being used without a program instance.</summary>
    internal class FakeInputHandler : IInputHandler
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool CanUseOnPlatform => true;

        /// <inheritdoc/>
        public MouseState MouseState { get; set; }

        /// <inheritdoc/>
        public KeyboardState KeyboardState { get; set; }


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public void OnInitialise(IntPtr windowHandle) { }
    }
}
