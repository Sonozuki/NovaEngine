﻿namespace NovaEngine.IO.Fake;

/// <summary>Represents an input handler that is only used when nova is being used without a program instance.</summary>
internal sealed class FakeInputHandler : IInputHandler
{
    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public bool CanUseOnPlatform => true;

    /// <inheritdoc/>
    public MouseState MouseState { get; set; }

    /// <inheritdoc/>
    public KeyboardState KeyboardState { get; set; }

    /// <inheritdoc/>
    public bool IsCursorVisible { get; set; }

    /// <inheritdoc/>
    public bool IsCursorLocked { get; set; }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public void OnInitialise(IntPtr windowHandle) { }
}
