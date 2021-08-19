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


        /*********
        ** Accessors
        *********/
        /// <summary>The handle of the window.</summary>
        public IntPtr Handle { get; protected set; }

        /// <summary>The title of the window.</summary>
        public abstract string Title { get; set; }

        /// <summary>The size of the window.</summary>
        public abstract Size Size { get; set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="size">The size of the window.</param>
        public PlatformWindowBase(string title, Size size)
        {
            Title = title;
            Size = size;
        }

        /// <summary>Processes all pending window events.</summary>
        public abstract void ProcessEvents();

        /// <summary>Shows the window.</summary>
        public abstract void Show();
    }
}
