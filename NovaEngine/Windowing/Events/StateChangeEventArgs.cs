using System;

namespace NovaEngine.Windowing.Events
{
    /// <summary>The event arguments for window state change events.</summary>
    public class StateChangeEventArgs : EventArgs
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The old state of the window.</summary>
        public WindowState OldState { get; }

        /// <summary>The new state of the window.</summary>
        public WindowState NewState { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="oldState">The old state of the window.</param>
        /// <param name="newState">The new state of the window.</param>
        public StateChangeEventArgs(WindowState oldState, WindowState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
