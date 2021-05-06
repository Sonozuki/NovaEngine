using NovaEngine.Maths;

namespace NovaEngine.IO.Events
{
    /// <summary>The event arguments for the mouse scroll event.</summary>
    public class MouseScrollEventArgs
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The new scroll value.</summary>
        public Vector2 Scroll { get; }

        /// <summary>Whether <see cref="Scroll"/> is relative to the current scroll value.</summary>
        public bool IsRelative { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="scroll">The new scroll value.</param>
        /// <param name="isRelative">Whether <paramref name="scroll"/> is relative to the current scroll value.</param>
        public MouseScrollEventArgs(Vector2 scroll, bool isRelative)
        {
            Scroll = scroll;
            IsRelative = isRelative;
        }
    }
}
