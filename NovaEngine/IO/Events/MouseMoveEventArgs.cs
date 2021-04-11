using NovaEngine.Maths;
using System;

namespace NovaEngine.IO.Events
{
    /// <summary>The event arguments for mouse move events.</summary>
    public class MouseMoveEventArgs : EventArgs
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The old position of the mouse.</summary>
        public Vector2I OldMousePosition { get; }

        /// <summary>The new position of the mouse.</summary>
        public Vector2I NewMousePosition { get; }

        /// <summary>The delta of the mouse position.</summary>
        public Vector2I MouseDelta => NewMousePosition - OldMousePosition;


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="oldMousePosition">The old state of the window.</param>
        /// <param name="newMousePosition">The new state of the window.</param>
        public MouseMoveEventArgs(Vector2I oldMousePosition, Vector2I newMousePosition)
        {
            OldMousePosition = oldMousePosition;
            NewMousePosition = newMousePosition;
        }
    }
}
