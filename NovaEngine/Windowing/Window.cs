using NovaEngine.External.Platform;
using NovaEngine.IO;
using NovaEngine.Maths;
using NovaEngine.Platform;
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


        /*********
        ** Fields
        *********/
        /// <summary>The platform specific window.</summary>
        private readonly PlatformWindowBase PlatformWindow;


        /*********
        ** Accessors
        *********/
        /// <summary>The handle of the window.</summary>
        public IntPtr Handle => PlatformWindow.Handle;

        /// <summary>The title of the window.</summary>
        public string Title
        {
            get => PlatformWindow.Title;
            set => PlatformWindow.Title = value;
        }

        /// <summary>The size of the window.</summary>
        public Vector2I Size
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
        public Window(string title, Vector2I size)
        {
            PlatformWindow = PlatformManager.CurrentPlatform.CreatePlatformWindow(title, size);

            PlatformWindow.Resize += (e) => Resize?.Invoke(e);
            PlatformWindow.LostFocus += () => Input.ResetInputState();
            PlatformWindow.Closed += () => HasClosed = true;
        }

        /// <summary>Processes all pending window events.</summary>
        public void ProcessEvents() => PlatformWindow.ProcessEvents();

        /// <summary>Shows the window.</summary>
        public void Show() => PlatformWindow.Show();
    }
}
