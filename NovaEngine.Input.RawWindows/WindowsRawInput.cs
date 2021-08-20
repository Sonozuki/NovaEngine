using NovaEngine.External.Input;
using System;

namespace NovaEngine.InputHandler.RawWindows
{
    /// <summary>Represents the raw Windows input handler.</summary>
    public class WindowsRawInput : IInputHandler
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool CanUseOnPlatform => OperatingSystem.IsWindows();

        /// <inheritdoc/>
        public MouseState MouseState { get; } = new();

        /// <inheritdoc/>
        public KeyboardState KeyboardState { get; } = new();


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public void OnInitialise(IntPtr windowHandle) { }
    }
}
