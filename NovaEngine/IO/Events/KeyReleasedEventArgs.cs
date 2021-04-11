using System;

namespace NovaEngine.IO.Events
{
    /// <summary>The event arguments for key release events.</summary>
    public class KeyReleasedEventArgs : EventArgs
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The key that was released.</summary>
        public Key Key { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="key">The key that was released.</param>
        public KeyReleasedEventArgs(Key key)
        {
            Key = key;
        }
    }
}
