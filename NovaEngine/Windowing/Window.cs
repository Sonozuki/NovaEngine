using NovaEngine.IO.Events;
using NovaEngine.Maths;
using NovaEngine.Platform;
using NovaEngine.Windowing.Events;
using System;

namespace NovaEngine.Windowing
{
    /// <summary>Represents a platform agnostic window.</summary>
    public class Window
    {
        /*********
        ** Events
        *********/
        /// <summary>Invoked when the window is resized.</summary>
        public event Action<ResizeEventArgs>? Resize;

        /// <summary>Invoked when the window is closed.</summary>
        public event Action? Closed;

        /// <summary>Invoked when the window gains focus.</summary>
        public event Action? FocusGained;

        /// <summary>Invoked when the window loses focus.</summary>
        public event Action? FocusLost;

        /// <summary>Invoked when state of the window changes.</summary>
        public event Action<StateChangeEventArgs>? StateChange;

        /// <summary>Invoked when the mouse is moved.</summary>
        public event Action<MouseMoveEventArgs>? MouseMove;

        /// <summary>Invoked when a mouse button is pressed.</summary>
        public event Action<MouseButtonPressedEventArgs>? MouseButtonPressed;

        /// <summary>Invoked when a mouse button is released.</summary>
        public event Action<MouseButtonReleasedEventArgs>? MouseButtonReleased;

        /// <summary>Invoked when a key is pressed.</summary>
        public event Action<KeyPressedEventArgs>? KeyPressed;

        /// <summary>Invoked when a key is released.</summary>
        public event Action<KeyReleasedEventArgs>? KeyReleased;


        /*********
        ** Fields
        *********/
        /// <summary>The platform specific window.</summary>
        private PlatformWindowBase PlatformWindow;


        /*********
        ** Accessors
        *********/
        /// <summary>The handle of the window.</summary>
        public IntPtr Handle => PlatformWindow.Handle;

        /// <summary>The minimum size of the window.</summary>
        public Size MinSize
        {
            get => PlatformWindow.MinSize;
            set => PlatformWindow.MinSize = value;
        }

        /// <summary>The title of the window.</summary>
        public string Title
        {
            get => PlatformWindow.Title;
            set => PlatformWindow.Title = value;
        }

        /// <summary>The size of the window.</summary>
        public Size Size
        {
            get => PlatformWindow.Size;
            set => PlatformWindow.Size = value;
        }

        /// <summary>Whether the window has closed.</summary>
        public bool HasClosed { get; private set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="size">The size of the window.</param>
        public Window(string title, Size size)
        {
            PlatformWindow = PlatformManager.CurrentPlatform.CreatePlatformWindow(title, size);

            PlatformWindow.Resize += (e) => Resize?.Invoke(e);
            PlatformWindow.Closed += () => { HasClosed = true; Closed?.Invoke(); };
            PlatformWindow.FocusGained += () => FocusGained?.Invoke();
            PlatformWindow.FocusLost += () => FocusLost?.Invoke();
            PlatformWindow.StateChange += (e) => StateChange?.Invoke(e);
            PlatformWindow.MouseMove += (e) => MouseMove?.Invoke(e);
            PlatformWindow.MouseButtonPressed += (e) => MouseButtonPressed?.Invoke(e);
            PlatformWindow.MouseButtonReleased += (e) => MouseButtonReleased?.Invoke(e);
            PlatformWindow.KeyPressed += (e) => KeyPressed?.Invoke(e);
            PlatformWindow.KeyReleased += (e) => KeyReleased?.Invoke(e);
        }

        /// <summary>Sets the mouse position (relative to the window).</summary>
        /// <param name="position">The position of the mouse.</param>
        public void SetMousePosition(Vector2I position) => SetMousePosition(position.X, position.Y);

        /// <summary>Sets the mouse position (relative to the window).</summary>
        /// <param name="x">The X position of the mouse.</param>
        /// <param name="y">The Y position of the mouse.</param>
        public void SetMousePosition(int x, int y) => PlatformWindow.SetMousePosition(x, y);

        /// <summary>Processes all pending window events.</summary>
        public void ProcessEvents() => PlatformWindow.ProcessEvents();

        /// <summary>Closes the window.</summary>
        public void Close() => PlatformWindow.Close();
    }
}
