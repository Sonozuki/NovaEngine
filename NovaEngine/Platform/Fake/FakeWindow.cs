#pragma warning disable CS0067 // Event is never used

namespace NovaEngine.Platform.Fake;

/// <summary>Represents a window that is only used when nova is being used without a program instance.</summary>
internal sealed class FakeWindow : PlatformWindowBase
{
    /*********
    ** Events
    *********/
    /// <inheritdoc/>
    public override event Action<ResizeEventArgs>? Resize;

    /// <inheritdoc/>
    public override event Action? LostFocus;

    /// <inheritdoc/>
    public override event Action? Closed;


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public override string Title { get; set; } = "";

    /// <inheritdoc/>
    public override Vector2I Size { get; set; }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override void ProcessEvents() { }

    /// <inheritdoc/>
    public override void Show() { }
}
