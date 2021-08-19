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
        public Size OldSize { get; }

        /// <summary>The new size of the window.</summary>
        public Size NewSize { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="oldSize">The old size of the window.</param>
        /// <param name="newSize">The new size of the window.</param>
        public ResizeEventArgs(Size oldSize, Size newSize)
        {
            OldSize = oldSize;
            NewSize = newSize;
        }
    }
}
