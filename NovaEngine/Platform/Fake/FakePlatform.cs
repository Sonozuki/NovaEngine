using NovaEngine.External.Platform;
using NovaEngine.Maths;

namespace NovaEngine.Platform.Fake
{
    /// <summary>Represents a platform that is only used when nova is being used without a program instance.</summary>
    internal class FakePlatform : IPlatform
    {
        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool IsCurrentPlatform => true;


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public PlatformWindowBase CreatePlatformWindow(string title, Size size) => new FakeWindow(title, size);

        /// <inheritdoc/>
        public Vector2I GetCursorPosition() => new();
    }
}
