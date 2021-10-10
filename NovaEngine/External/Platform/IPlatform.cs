using NovaEngine.Maths;

namespace NovaEngine.External.Platform
{
    /// <summary>Represents a platform.</summary>
    public interface IPlatform
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether this represents the current environment platform.</summary>
        public bool IsCurrentPlatform { get; }

        /// <summary>Whether the cursor is currently visible.</summary>
        public bool IsCursorVisible { get; set; }

        /// <summary>Whether the cursor is currently locked.</summary>
        public bool IsCursorLocked { get; set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Creates a platform specific window.</summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="size">The size of the window.</param>
        /// <returns>A platform specific window.</returns>
        public PlatformWindowBase CreatePlatformWindow(string title, Vector2I size);
    }
}
