using System;

namespace NovaEngine.IO.Events
{
    /// <summary>The event arguments for key press events.</summary>
    public class KeyPressedEventArgs : EventArgs
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The key that was pressed.</summary>
        public Key Key { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="key">The key that was pressed.</param>
        public KeyPressedEventArgs(Key key)
        {
            Key = key;
        }
    }
}
