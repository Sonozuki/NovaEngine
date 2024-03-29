﻿namespace NovaEngine.Platform.Fake;

/// <summary>Represents a platform that is only used when nova is being used without a program instance.</summary>
internal sealed class FakePlatform : IPlatform
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public bool IsCurrentPlatform => true;


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public PlatformWindowBase CreatePlatformWindow(string title, Vector2I size) => new FakeWindow();
}
