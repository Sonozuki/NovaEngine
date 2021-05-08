using NovaEngine.Maths;

namespace NovaEngine.Platform.Dummy
{
    /// <summary>Represents a platform that is only used when nova is being used without a program instance.</summary>
    public class DummyPlatform : IPlatform
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
        public PlatformWindowBase CreatePlatformWindow(string title, Size size) => new DummyWindow(title, size);
    }
}
