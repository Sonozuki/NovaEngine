namespace NovaEngine.External.Input
{
    /// <summary>Represents the current state of the keyboard.</summary>
    public struct KeyboardState
    {
        /*********
        ** Fields
        *********/
        /// <summary>The states of the first 64 keys.</summary>
        private ulong KeyStates1;

        /// <summary>The states of the second 64 keys.</summary>
        private ulong KeyStates2;


        /*********
        ** Accessors
        *********/
        /// <summary>Gets or sets the pressed state of a specified button.</summary>
        /// <param name="key">The key whose state to get or set.</param>
        /// <returns>The pressed state of the specified button.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified key isn't a valid key.</exception>
        public bool this[Key key]
        {
            get
            {
                if ((int)key < 0 || (int)key > 123)
                    throw new ArgumentOutOfRangeException(nameof(key), "Must be between 0 => 123 (inclusive)");

                if ((int)key < 64)
                    return (KeyStates1 & (1ul << (int)key)) != 0;
                else
                    return (KeyStates2 & (1ul << (int)key - 64)) != 0;
            }
            set
            {
                if ((int)key < 0 || (int)key > 123)
                    throw new ArgumentOutOfRangeException(nameof(key), "Must be between 0 => 123 (inclusive)");

                if ((int)key < 64)
                {
                    if (value)
                        KeyStates1 |= (1ul << (int)key);
                    else
                        KeyStates1 &= ~(1ul << (int)key);
                }
                else
                {
                    if (value)
                        KeyStates2 |= (1ul << (int)key - 64);
                    else
                        KeyStates2 &= ~(1ul << (int)key - 64);
                }
            }
        }
    }
}
