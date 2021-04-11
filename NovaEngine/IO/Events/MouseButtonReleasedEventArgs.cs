using System;

namespace NovaEngine.IO.Events
{
    /// <summary>The event arguments for mouse button release events.</summary>
    public class MouseButtonReleasedEventArgs : EventArgs
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The mouse button that was released.</summary>
        public MouseButton Button { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="button">The mouse button that was released.</param>
        public MouseButtonReleasedEventArgs(MouseButton button)
        {
            Button = button;
        }
    }
}
