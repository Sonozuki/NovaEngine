using NovaEngine.Maths;
using System;

namespace NovaEngine.Windowing
{
    /// <summary>The event arguments for window resize events.</summary>
    public class ResizeEventArgs : EventArgs
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The old size of the window.</summary>
        public Vector2I OldSize { get; }

        /// <summary>The new size of the window.</summary>
        public Vector2I NewSize { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="oldSize">The old size of the window.</param>
        /// <param name="newSize">The new size of the window.</param>
        public ResizeEventArgs(Vector2I oldSize, Vector2I newSize)
        {
            OldSize = oldSize;
            NewSize = newSize;
        }
    }
}
