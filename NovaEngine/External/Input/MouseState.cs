using NovaEngine.IO;
using NovaEngine.Maths;
using System;

namespace NovaEngine.External.Input
{
    /// <summary>Represents the current state of the mouse.</summary>
    public struct MouseState
    {
        /*********
        ** Fields
        *********/
        /// <summary>The position of the mouse, relative to the window.</summary>
        public Vector2I Position;

        /// <summary>The amount the mouse has moved since the last update.</summary>
        /// <remarks>This does not necessarily line up with the cursor movement; for example, if the cursor is locked using <see cref="IO.Input.IsCursorLocked"/>, this will still record movements made despite the cursor not moving.</remarks>
        public Vector2I PositionDelta;

        /// <summary>The horizontal and vertical scroll values of the mouse.</summary>
        public Vector2 Scroll;

        /// <summary>The states of the mouse buttons.</summary>
        /// <remarks>Only the 5 low-order bits are used.</remarks>
        private byte ButtonStates;


        /*********
        ** Accessors
        *********/
        /// <summary>Gets or sets the pressed state of a specified button.</summary>
        /// <param name="button">The button whose state to get or set.</param>
        /// <returns>The pressed state of the specified button.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified button isn't a valid mouse button.</exception>
        public bool this[MouseButton button]
        {
            get
            {
                if ((int)button < 0 || (int)button > 4)
                    throw new ArgumentOutOfRangeException(nameof(button), "Must be between 0 => 4 (inclusive)");

                return (ButtonStates & (1 << (int)button)) != 0;
            }
            set
            {
                if ((int)button < 0 || (int)button > 4)
                    throw new ArgumentOutOfRangeException(nameof(button), "Must be between 0 => 4 (inclusive)");

                if (value)
                    ButtonStates |= (byte)(1 << (int)button);
                else
                    ButtonStates &= (byte)~(1 << (int)button);
            }
        }
    }
}
