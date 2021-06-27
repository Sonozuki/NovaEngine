using NovaEngine.IO.Events;
using NovaEngine.Maths;
using NovaEngine.Windowing.Events;
using System;

namespace NovaEngine.Platform
{
    /// <summary>Represents a platform specific window.</summary>
    public abstract class PlatformWindowBase
    {
        /*********
        ** Events
        *********/
        /// <summary>Invoked when the window is resized.</summary>
        public abstract event Action<ResizeEventArgs>? Resize;

        /// <summary>Invoked when the window is closed.</summary>
        public abstract event Action? Closed;

        /// <summary>Invoked when the window gains focus.</summary>
        public abstract event Action? FocusGained;

        /// <summary>Invoked when the window loses focus.</summary>
        public abstract event Action? FocusLost;

        /// <summary>Invoked when the mouse is moved.</summary>
        public abstract event Action<MouseMoveEventArgs>? MouseMove;

        /// <summary>Invoked when the mouse is scrolled.</summary>
        public abstract event Action<MouseScrollEventArgs>? MouseScroll;

        /// <summary>Invoked when a mouse button is pressed.</summary>
        public abstract event Action<MouseButtonPressedEventArgs>? MouseButtonPressed;

        /// <summary>Invoked when a mouse button is released.</summary>
        public abstract event Action<MouseButtonReleasedEventArgs>? MouseButtonReleased;

        /// <summary>Invoked when a key is pressed.</summary>
        public abstract event Action<KeyPressedEventArgs>? KeyPressed;

        /// <summary>Invoked when a key is released.</summary>
        public abstract event Action<KeyReleasedEventArgs>? KeyReleased;


        /*********
        ** Fields
        *********/
        /// <summary>The size of the window.</summary>
        protected Size _Size;

        /// <summary>The title of the window.</summary>
        private string _Title;


        /*********
        ** Accessors
        *********/
        /// <summary>The handle of the window.</summary>
        public IntPtr Handle { get; protected set; }

        /// <summary>The minimum size of the window.</summary>
        public Size MinSize { get; set; } = new(1280, 720); // TODO: don't hard code this

        /// <summary>The title of the window.</summary>
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value ?? string.Empty;
                SetTitle(_Title);
            }
        }

        /// <summary>The size of the window.</summary>
        public Size Size
        {
            get => _Size;
            set
            {
                _Size.Width = Math.Max(value.Width, MinSize.Width);
                _Size.Height = Math.Max(value.Height, MinSize.Height);

                SetSize(Size);
            }
        }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="size">The size of the window.</param>
        public PlatformWindowBase(string title, Size size)
        {
            _Title = title;
            _Size = size;
        }

        /// <summary>Sets the window title.</summary>
        /// <param name="title">The new title of the window.</param>
        public abstract void SetTitle(string title);

        /// <summary>Sets the window size.</summary>
        /// <param name="size">The new size of the window.</param>
        public abstract void SetSize(Size size);

        /// <summary>Sets the mouse position (relative to the window).</summary>
        /// <param name="x">The X position of the mouse.</param>
        /// <param name="y">The Y position of the mouse.</param>
        public abstract void SetMousePosition(int x, int y);

        /// <summary>Processes all pending window events.</summary>
        public abstract void ProcessEvents();

        /// <summary>Shows the window.</summary>
        public abstract void Show();

        /// <summary>Closes the window.</summary>
        public abstract void Close();
    }
}
