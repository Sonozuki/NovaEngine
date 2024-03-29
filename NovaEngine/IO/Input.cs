﻿namespace NovaEngine.IO;

/// <summary>The input system.</summary>
public static class Input
{
    /*********
    ** Fields
    *********/
    /// <summary>The mouse state of the previous tick.</summary>
    private static MouseState PreviousMouseState;

    /// <summary>The mouse state of the current tick.</summary>
    private static MouseState CurrentMouseState;

    /// <summary>The keyboard state of the previous tick.</summary>
    private static KeyboardState PreviousKeyboardState;

    /// <summary>The keyboard state of the current tick.</summary>
    private static KeyboardState CurrentKeyboardState;

    /// <summary>The mouse input event handlers.</summary>
    private static readonly Dictionary<MouseButtonEventListenerInfo, List<Action>> MouseButtonEventHandlers = new();

    /// <summary>The keyboard input event handlers.</summary>
    private static readonly Dictionary<KeyEventListenerInfo, List<Action>> KeyEventHandlers = new();


    /*********
    ** Properties
    *********/
    /// <summary>Whether the cursor is currently visible.</summary>
    public static bool IsCursorVisible
    {
        get => InputHandlerManager.CurrentInputHandler.IsCursorVisible;
        set => InputHandlerManager.CurrentInputHandler.IsCursorVisible = value;
    }

    /// <summary>Whether the cursor is currently locked.</summary>
    /// <remarks>Locking the cursor will automatically centralise it.</remarks>
    public static bool IsCursorLocked
    {
        get => InputHandlerManager.CurrentInputHandler.IsCursorLocked;
        set => InputHandlerManager.CurrentInputHandler.IsCursorLocked = value;
    }

    /// <summary>The position of the mouse, relative to the window.</summary>
    public static Vector2I MousePosition => CurrentMouseState.Position;

    /// <summary>The delta of the mouse position.</summary>
    /// <remarks>This does not necessarily line up with the cursor movement; for example, if the cursor is locked using <see cref="IsCursorLocked"/>, this will still record movements made despite the cursor not moving.</remarks>
    public static Vector2I MouseDelta => CurrentMouseState.PositionDelta;


    /*********
    ** Public Methods
    *********/
    /// <summary>Gets whether the mouse button was pressed this frame.</summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns><see langword="true"/>, if the mouse button was pressed this frame; otherwise, <see langword="false"/>.</returns>
    public static bool WasButtonPressed(MouseButton button) => !PreviousMouseState[button] && CurrentMouseState[button];

    /// <summary>Gets whether the mouse button is currently pressed.</summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns><see langword="true"/>, if the mouse button is currently pressed; otherwise, <see langword="false"/>.</returns>
    public static bool IsButtonPressed(MouseButton button) => CurrentMouseState[button];

    /// <summary>Gets whether the mouse button was released this frame.</summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns><see langword="true"/>, if the mouse button was released this frame; otherwise, <see langword="false"/>.</returns>
    public static bool WasButtonReleased(MouseButton button) => PreviousMouseState[button] && !CurrentMouseState[button];

    /// <summary>Gets whether the mouse button was is currently released.</summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns><see langword="true"/>, if the mouse button is currently released; otherwise, <see langword="false"/>.</returns>
    public static bool IsButtonReleased(MouseButton button) => !CurrentMouseState[button];

    /// <summary>Gets whether the key was pressed this frame.</summary>
    /// <param name="key">The key to check.</param>
    /// <returns><see langword="true"/>, if the key was pressed this frame; otherwise, <see langword="false"/>.</returns>
    public static bool WasKeyPressed(Key key) => !PreviousKeyboardState[key] && CurrentKeyboardState[key];

    /// <summary>Gets whether the key is currently pressed.</summary>
    /// <param name="key">The key to check.</param>
    /// <returns><see langword="true"/>, if the key is currently pressed; otherwise, <see langword="false"/>.</returns>
    public static bool IsKeyPressed(Key key) => CurrentKeyboardState[key];

    /// <summary>Gets whether the key was released this frame.</summary>
    /// <param name="key">The key to check.</param>
    /// <returns><see langword="true"/>, if the key was released this frame; otherwise, <see langword="false"/>.</returns>
    public static bool WasKeyReleased(Key key) => PreviousKeyboardState[key] && !CurrentKeyboardState[key];

    /// <summary>Gets whether the key was is currently released.</summary>
    /// <param name="key">The key to check.</param>
    /// <returns><see langword="true"/>, if the key is currently released; otherwise, <see langword="false"/>.</returns>
    public static bool IsKeyReleased(Key key) => !CurrentKeyboardState[key];

    /// <summary>Adds a mouse button event handler.</summary>
    /// <param name="button">The button to listen for.</param>
    /// <param name="pressType">The type of press to listen for.</param>
    /// <param name="callback">The callback to invoke.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static void AddMouseButtonHandler(MouseButton button, PressType pressType, Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        var listenerInfo = new MouseButtonEventListenerInfo(button, pressType);
        if (MouseButtonEventHandlers.TryGetValue(listenerInfo, out var eventHandler))
            eventHandler.Add(callback);
        else
            MouseButtonEventHandlers[listenerInfo] = new() { callback };
    }

    /// <summary>Removes a mouse button event handler.</summary>
    /// <param name="button">The button to stop listening for.</param>
    /// <param name="pressType">The type of the press to stop listening for.</param>
    /// <param name="callback">The callback that should no longer be invoked.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static void RemoveMouseButtonHandler(MouseButton button, PressType pressType, Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        var listenerInfo = new MouseButtonEventListenerInfo(button, pressType);
        if (MouseButtonEventHandlers.TryGetValue(listenerInfo, out var eventHandlers))
            eventHandlers.Remove(callback);
    }

    /// <summary>Adds a key event handler.</summary>
    /// <param name="key">The key to listen for.</param>
    /// <param name="pressType">The type of press to listen for.</param>
    /// <param name="callback">The callback to invoke.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static void AddKeyHandler(Key key, PressType pressType, Action callback) => AddKeyHandler(key, Modifiers.None, pressType, callback);

    /// <summary>Adds a key event handler.</summary>
    /// <param name="key">The key to listen for.</param>
    /// <param name="modifiers">The modifier keys to listen for.</param>
    /// <param name="pressType">The type of press to listen for.</param>
    /// <param name="callback">The callback to invoke.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static void AddKeyHandler(Key key, Modifiers modifiers, PressType pressType, Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        var listenerInfo = new KeyEventListenerInfo(key, modifiers, pressType);
        if (KeyEventHandlers.TryGetValue(listenerInfo, out var eventHandlers))
            eventHandlers.Add(callback);
        else
            KeyEventHandlers[listenerInfo] = new() { callback };
    }

    /// <summary>Removes a mouse button event handler.</summary>
    /// <param name="key">The key to stop listening for.</param>
    /// <param name="pressType">The type of the press to stop listening for.</param>
    /// <param name="callback">The callback that should no longer be invoked.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static void RemoveKeyHandler(Key key, PressType pressType, Action callback) => AddKeyHandler(key, Modifiers.None, pressType, callback);

    /// <summary>Removes a mouse button event handler.</summary>
    /// <param name="key">The key to stop listening for.</param>
    /// <param name="modifiers">The modifier keys to stop listening for.</param>
    /// <param name="pressType">The type of the press to stop listening for.</param>
    /// <param name="callback">The callback that should no longer be invoked.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is <see langword="null"/>.</exception>
    public static void RemoveKeyHandler(Key key, Modifiers modifiers, PressType pressType, Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        var listenerInfo = new KeyEventListenerInfo(key, modifiers, pressType);
        if (KeyEventHandlers.TryGetValue(listenerInfo, out var eventHandlers))
            eventHandlers.Remove(callback);
    }


    /*********
    ** Internal Methods
    *********/
    /// <summary>Updates the state of the input system, as well as calling any input event handlers.</summary>
    internal static void Update()
    {
        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = InputHandlerManager.CurrentInputHandler.MouseState;

        PreviousKeyboardState = CurrentKeyboardState;
        CurrentKeyboardState = InputHandlerManager.CurrentInputHandler.KeyboardState;

        Enum.GetValues<MouseButton>().ToList().ForEach(button =>
        {
            switch (button)
            {
                case MouseButton when WasButtonPressed(button): InvokeMouseButtonEventHandlers(button, PressType.Press); break;
                case MouseButton when IsButtonPressed(button): InvokeMouseButtonEventHandlers(button, PressType.Hold); break;
                case MouseButton when WasButtonReleased(button): InvokeMouseButtonEventHandlers(button, PressType.Release); break;
            }
        });

        var modifiers = CurrentModifiers();
        Enum.GetValues<Key>().ToList().ForEach(key =>
        {
            switch (key)
            {
                case Key when WasKeyPressed(key): InvokeKeyEventHandlers(key, modifiers, PressType.Press); break;
                case Key when IsKeyPressed(key): InvokeKeyEventHandlers(key, modifiers, PressType.Hold); break;
                case Key when WasKeyReleased(key): InvokeKeyEventHandlers(key, modifiers, PressType.Release); break;
            }
        });
    }

    /// <summary>Resets the state of the input handler.</summary>
    internal static void ResetInputState()
    {
        CurrentMouseState = InputHandlerManager.CurrentInputHandler.MouseState = new();
        CurrentKeyboardState = InputHandlerManager.CurrentInputHandler.KeyboardState = new();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invokes mouse button event handlers that are listening for a specified button and press type.</summary>
    /// <param name="button">The button the event handlers to invoke are listening for.</param>
    /// <param name="pressType">The press type the event handlers to invoke are listening for.</param>
    private static void InvokeMouseButtonEventHandlers(MouseButton button, PressType pressType) =>
        MouseButtonEventHandlers
            .Where(eventHandler => eventHandler.Key.Button == button && eventHandler.Key.PressType == pressType).ToList()
            .ForEach(eventHandler => eventHandler.Value.ForEach(callback => callback.Invoke()));

    /// <summary>Invokes key event handlers that are listening for a specified key, modifier(s), and press type.</summary>
    /// <param name="key">The key the event handlers to invoke are listening for.</param>
    /// <param name="modifiers">The modifier(s) the event handlers to invoke are listening for.</param>
    /// <param name="pressType">The press type the event handlers to invoke are listening for.</param>
    private static void InvokeKeyEventHandlers(Key key, Modifiers modifiers, PressType pressType) =>
        KeyEventHandlers
            .Where(eventHandler => eventHandler.Key.Key == key && eventHandler.Key.Modifiers == modifiers && eventHandler.Key.PressType == pressType).ToList()
            .ForEach(eventHandler => eventHandler.Value.ForEach(callback => callback.Invoke()));

    /// <summary>Retrieves the currently pressed modifier keys.</summary>
    /// <returns>The currently pressed modifier keys.</returns>
    private static Modifiers CurrentModifiers()
    {
        var modifiers = Modifiers.None;
        if (IsKeyPressed(Key.LeftShift) || IsKeyPressed(Key.RightShift))
            modifiers |= Modifiers.Shift;
        if (IsKeyPressed(Key.LeftControl) || IsKeyPressed(Key.RightControl))
            modifiers |= Modifiers.Ctrl;
        if (IsKeyPressed(Key.LeftAlt) || IsKeyPressed(Key.RightAlt))
            modifiers |= Modifiers.Alt;
        return modifiers;
    }
}
