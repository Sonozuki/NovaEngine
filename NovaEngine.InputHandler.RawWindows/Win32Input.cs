using Rectangle = NovaEngine.Common.Windows.Native.Rectangle;

namespace NovaEngine.InputHandler.RawWindows;

/// <summary>Represents the raw Windows input handler.</summary>
public class Win32RawInput : IInputHandler
{
    /*********
    ** Fields
    *********/
    /// <summary>The current state of the mouse.</summary>
    private MouseState _MouseState;

    /// <summary>The old procedure of the window.</summary>
    private IntPtr OldWindowProcedure;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>The procedure of the window.</summary>
    private WindowProcedureDelegate WindowProcedure;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public bool CanUseOnPlatform => OperatingSystem.IsWindows();

    /// <inheritdoc/>
    public MouseState MouseState
    {
        get
        {
            var mouseState = _MouseState;
            _MouseState.PositionDelta = new(); // reset this as otherwise each frame will have the delta from when the mouse last moved, not the last frame
            return mouseState;
        }
        set => _MouseState = value;
    }

    /// <inheritdoc/>
    public KeyboardState KeyboardState { get; set; }

    /// <inheritdoc/>
    public bool IsCursorVisible
    {
        get
        {
            var cursorInfo = new CursorInfo
            {
                Size = (uint)Marshal.SizeOf<CursorInfo>()
            };

            User32.GetCursorInfo(ref cursorInfo);
            return cursorInfo.Flags == CursorState.Showing;
        }
        set
        {
            var displayCounter = User32.ShowCursor(value);
            if (value)
                while (displayCounter < 0)
                    displayCounter = User32.ShowCursor(true);
            else
                while (displayCounter >= 0)
                    displayCounter = User32.ShowCursor(false);
        }
    }

    /// <inheritdoc/>
    public unsafe bool IsCursorLocked
    {
        get
        {
            var clipRectangle = new Rectangle();
            User32.GetClipCursor(ref clipRectangle);

            var clientCoordinatesTopLeft = new Vector2I(clipRectangle.Left, clipRectangle.Top);
            User32.ScreenToClient(Program.MainWindow.Handle, ref clientCoordinatesTopLeft);

            return clientCoordinatesTopLeft == CursorClipTopLeft;
        }
        set
        {
            if (value)
            {
                var screenCoordinatesTopLeft = CursorClipTopLeft;
                User32.ClientToScreen(Program.MainWindow.Handle, ref screenCoordinatesTopLeft);
                var screenCoordinatesBottomRight = screenCoordinatesTopLeft + Vector2I.One;

                var clipRectangle = new Rectangle(screenCoordinatesTopLeft, screenCoordinatesBottomRight);
                User32.ClipCursor(&clipRectangle);
            }
            else
                User32.ClipCursor(null);
        }
    }

    /// <summary>The top left position of the clip rectangle, in client-area coordinates.</summary>
    private static Vector2I CursorClipTopLeft
    {
        get
        {
            var position = Program.MainWindow.Size.ToVector2<float>() / 2f;
            return new((int)position.X, (int)position.Y);
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public void OnInitialise(IntPtr windowHandle)
    {
        var rawInputDevices = new[]
        {
            new RawInputDevice(HidUsageGenericDesktop.Mouse, RawInputDeviceFlags.DevNotify, windowHandle),
            new RawInputDevice(HidUsageGenericDesktop.Keyboard, RawInputDeviceFlags.DevNotify, windowHandle)
        };

        if (!User32.RegisterRawInputDevices(rawInputDevices, (uint)rawInputDevices.Length, (uint)Marshal.SizeOf<RawInputDevice>()))
            throw new ApplicationException($"Failed to register raw input device: {Marshal.GetLastWin32Error()}.").Log(LogSeverity.Fatal);

        WindowProcedure = Procedure;
        OldWindowProcedure = User32.SetWindowLong(windowHandle, WindowLongOffset.WindowProcedure, Marshal.GetFunctionPointerForDelegate(WindowProcedure));
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>An application-defined method that processess messges sent to a window.</summary>
    /// <param name="window">A handle to the window.</param>
    /// <param name="message">The message.</param>
    /// <param name="wParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
    /// <param name="lParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
    /// <returns>The result of the message processing, which depends on the message sent.</returns>
    private IntPtr Procedure(IntPtr window, Message message, IntPtr wParam, IntPtr lParam)
    {
        try
        {
            switch (message)
            {
                case Message.Input:
                    {
                        if (User32.GetRawInputData(lParam, out RawInputHeader header) != Marshal.SizeOf<RawInputHeader>())
                            break;
                        if (User32.GetRawInputData(lParam, out RawInput rawInput) == 0)
                            break;

                        switch (header.Type)
                        {
                            case RawInputDeviceType.Mouse:
                                ProcessMouseInput(rawInput);
                                return IntPtr.Zero;
                            case RawInputDeviceType.Keyboard:
                                ProcessKeyboardInput(rawInput);
                                return IntPtr.Zero;
                            case RawInputDeviceType.Hid:
                                ProcessHidInput(rawInput);
                                return IntPtr.Zero;
                        }

                        break;
                    }
            }

            // if the message isn't processed, run the old procedure
            return User32.CallWindowProc(OldWindowProcedure, window, message, wParam, lParam);
        }
        catch (Exception ex)
        {
            Logger.LogFatal($"Unhandled exception occured in window procedure: {ex}");
            return IntPtr.Zero;
        }
    }

    /// <summary>Process a mouse input event.</summary>
    /// <param name="rawInput">The raw mouse input data.</param>
    private void ProcessMouseInput(RawInput rawInput)
    {
        var rawMouse = rawInput.Data.Mouse;

        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.LeftButtonDown))
            _MouseState[MouseButton.LeftButton] = true;
        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.LeftButtonUp))
            _MouseState[MouseButton.LeftButton] = false;

        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.MiddleButtonDown))
            _MouseState[MouseButton.MiddleButton] = true;
        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.MiddleButtonUp))
            _MouseState[MouseButton.MiddleButton] = false;

        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.RightButtonDown))
            _MouseState[MouseButton.RightButton] = true;
        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.RightButtonUp))
            _MouseState[MouseButton.RightButton] = false;

        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Button4Down))
            _MouseState[MouseButton.BackButton] = true;
        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Button4Up))
            _MouseState[MouseButton.BackButton] = false;

        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Button5Down))
            _MouseState[MouseButton.ForwardButton] = true;
        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Button5Up))
            _MouseState[MouseButton.ForwardButton] = false;

        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Wheel))
            _MouseState.Scroll += new Vector2<float>(0, rawMouse.ButtonData / 120f);
        if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.HWheel))
            _MouseState.Scroll += new Vector2<float>(rawMouse.ButtonData / 120f, 0);

        _MouseState.PositionDelta = new Vector2I(rawMouse.LastX, rawMouse.LastY);
        User32.GetCursorPos(ref _MouseState.Position); // delta follows the raw input meaning it can go out of sync with the cursor position, so get the position from Windows instead
        User32.ScreenToClient(Program.MainWindow.Handle, ref _MouseState.Position);
    }

    /// <summary>Process a keyboard input event.</summary>
    /// <param name="rawInput">The raw keyboard input data.</param>
    private void ProcessKeyboardInput(RawInput rawInput)
    {
        var rawKeyboard = rawInput.Data.Keyboard;
        var isE0BitSet = rawKeyboard.Flags.HasFlag(RawInputKeyboardDataFlags.E0);
        var convertedKey = Win32Utilities.ConvertVirtualKey(rawKeyboard.VirtualKey, isE0BitSet, rawKeyboard.MakeCode);

        var keyboardState = KeyboardState;
        keyboardState[convertedKey] = !rawKeyboard.Flags.HasFlag(RawInputKeyboardDataFlags.Break);
        KeyboardState = keyboardState;
    }

    /// <summary>Process a hid input event.</summary>
    /// <param name="rawInput">The raw hid input data.</param>
    private void ProcessHidInput(RawInput rawInput)
    {
        // TODO: implement
    }
}
