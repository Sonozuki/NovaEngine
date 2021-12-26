namespace NovaEngine.Platform.Fake;

/// <summary>Represents a window that is only used when nova is being used without a program instance.</summary>
internal class FakeWindow : PlatformWindowBase
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
    ** Accessors
    *********/
    /// <inheritdoc/>
    public override string Title { get; set; }

    /// <inheritdoc/>
    public override Vector2I Size { get; set; }


    /*********
    ** Public Methods
    *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <inheritdoc/>
    public FakeWindow(string title, Vector2I size)
        : base(title, size) { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <inheritdoc/>
    public override void ProcessEvents() { }

    /// <inheritdoc/>
    public override void Show() { }
}
