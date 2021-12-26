namespace NovaEngine.Platform.Fake;

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
    public PlatformWindowBase CreatePlatformWindow(string title, Vector2I size) => new FakeWindow(title, size);
}
