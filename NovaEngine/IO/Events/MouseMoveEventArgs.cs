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
        /// <summary>The position of the mouse.</summary>
        public Vector2I MousePosition { get; }

        /// <summary>Whether <see cref="MousePosition"/> is relative to the current location.</summary>
        public bool IsRelative { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="mousePosition">The position of the mouse.</param>
        /// <param name="isRelative">Whether <paramref name="mousePosition"/> is relative to the current location.</param>
        public MouseMoveEventArgs(Vector2I mousePosition, bool isRelative)
        {
            MousePosition = mousePosition;
            IsRelative = isRelative;
        }
    }
}
