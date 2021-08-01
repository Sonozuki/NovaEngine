using NovaEngine.IO.Events;
using NovaEngine.Maths;
using NovaEngine.Windowing.Events;
using System;

namespace NovaEngine.Platform.Fake
{
    /// <summary>Represents a window that is only used when nova is being used without a program instance.</summary>
    internal class FakeWindow : PlatformWindowBase
    {
        /*********
        ** Events
        *********/
        /// <inheritdoc/>
        public override event Action<ResizeEventArgs>? Resize;

        /// <inheritdoc/>
        public override event Action? Closed;

        /// <inheritdoc/>
        public override event Action? FocusGained;

        /// <inheritdoc/>
        public override event Action? FocusLost;

        /// <inheritdoc/>
        public override event Action<MouseMoveEventArgs>? MouseMove;

        /// <inheritdoc/>
        public override event Action<MouseScrollEventArgs>? MouseScroll;

        /// <inheritdoc/>
        public override event Action<MouseButtonPressedEventArgs>? MouseButtonPressed;

        /// <inheritdoc/>
        public override event Action<MouseButtonReleasedEventArgs>? MouseButtonReleased;

        /// <inheritdoc/>
        public override event Action<KeyPressedEventArgs>? KeyPressed;

        /// <inheritdoc/>
        public override event Action<KeyReleasedEventArgs>? KeyReleased;


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public FakeWindow(string title, Size size)
            : base(title, size) { }

        /// <inheritdoc/>
        public override void Close() { }

        /// <inheritdoc/>
        public override void ProcessEvents() { }

        /// <inheritdoc/>
        public override void SetMousePosition(int x, int y) { }

        /// <inheritdoc/>
        public override void SetSize(Size size) { }

        /// <inheritdoc/>
        public override void Show() { }

        /// <inheritdoc/>
        public override void SetTitle(string title) { }
    }
}
