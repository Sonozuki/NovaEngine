using System;

namespace NovaEngine.IO.Events
{
    /// <summary>The event arguments for mouse button press events.</summary>
    public class MouseButtonPressedEventArgs : EventArgs
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The mouse button that was pressed.</summary>
        public MouseButton Button { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="button">The mouse button that was pressed.</param>
        public MouseButtonPressedEventArgs(MouseButton button)
        {
            Button = button;
        }
    }
}
