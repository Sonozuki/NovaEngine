using NovaEngine.Extensions;
using NovaEngine.IO;
using NovaEngine.IO.Events;
using NovaEngine.Logging;
using NovaEngine.Maths;
using NovaEngine.Platform.Windows.Api;
using NovaEngine.Platform.Windows.Input;
using NovaEngine.Windowing.Events;
using System;
using System.Runtime.InteropServices;

namespace NovaEngine.Platform.Windows.Windowing
{
    /// <summary>Represents a Win32 window.</summary>
    public class Win32Window : PlatformWindowBase
    {
        /*********
        ** Events
        *********/
        /// <inheritdoc/>
        public override event Action<ResizeEventArgs>? Resize;

        /// <inheritdoc/>
        public override event Action? Closed;

        /// <inheritdoc/>
        public override event Action? FocusGained;

        /// <inheritdoc/>
        public override event Action? FocusLost;

        /// <inheritdoc/>
        public override event Action<MouseMoveEventArgs>? MouseMove;

        /// <inheritdoc/>
        public override event Action<MouseScrollEventArgs>? MouseScroll;

        /// <inheritdoc/>
        public override event Action<MouseButtonPressedEventArgs>? MouseButtonPressed;

        /// <inheritdoc/>
        public override event Action<MouseButtonReleasedEventArgs>? MouseButtonReleased;

        /// <inheritdoc/>
        public override event Action<KeyPressedEventArgs>? KeyPressed;

        /// <inheritdoc/>
        public override event Action<KeyReleasedEventArgs>? KeyReleased;


        /*********
        ** Fields
        *********/
        /// <summary>The procedure of the window.</summary>
        private readonly WindowProcedureDelegate WindowProcedure;


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public Win32Window(string title, Size size)
            : base(title, size)
        {
            // create window
            WindowProcedure = Procedure;

            var className = "NovaWindowClass";

            var windowClass = new NativeWindowClass()
            {
                WindowProcedure = WindowProcedure,
                Handle = Program.Handle,
                ClassName = className,
                Cursor = User32.LoadCursor(IntPtr.Zero, 32512) // normal 'arrow' cursor
            };

            User32.RegisterClass(in windowClass);

            Handle = User32.CreateWindowEx(0, className, Title, WindowStyle.OverlappedWindow, 0, 0, Size.Width, Size.Height, IntPtr.Zero, IntPtr.Zero, Program.Handle, IntPtr.Zero);
            if (Handle == IntPtr.Zero)
                return;

            // register input devices
            var rawInputDevices = new[]
            {
                new RawInputDevice(HidUsageGenericDesktop.Mouse, RawInputDeviceFlags.DevNotify, Handle),
                new RawInputDevice(HidUsageGenericDesktop.Keyboard, RawInputDeviceFlags.DevNotify, Handle)
            };

            if (!User32.RegisterRawInputDevices(rawInputDevices, (uint)rawInputDevices.Length, (uint)Marshal.SizeOf<RawInputDevice>()))
                throw new ApplicationException($"Failed to register raw input device: {Marshal.GetLastWin32Error()}.").Log(LogSeverity.Fatal);
        }

        /// <inheritdoc/>
        public override void SetTitle(string title) => User32.SetWindowText(Handle, title);

        /// <inheritdoc/>
        public override void SetSize(Size size)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void SetMousePosition(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void ProcessEvents()
        {
            NativeMessage message;
            while (User32.PeekMessage(out message, IntPtr.Zero, 0, 0, RemoveMessage.Remove))
            {
                User32.TranslateMessage(in message);
                User32.DispatchMessage(in message);
            }
        }

        /// <inheritdoc/>
        public override void Show() => User32.ShowWindow(Handle, CommandShow.Show);

        /// <inheritdoc/>
        public override void Close()
        {
            throw new NotImplementedException();
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>An application-defined method that processess messges sent to a window.</summary>
        /// <param name="windowHandle">A handle to the window.</param>
        /// <param name="message">The message.</param>
        /// <param name="wParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
        /// <param name="lParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
        /// <returns>The result of the message processing and depends on the message sent.</returns>
        private IntPtr Procedure(IntPtr windowHandle, Message message, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                switch (message)
                {
                    case Message.Destroy:
                        {
                            Closed?.Invoke();
                            User32.PostQuitMessage(0);

                            return IntPtr.Zero;
                        }
                    case Message.Size:
                        {
                            var oldSize = Size;
                            var widthHeight = lParam.ToInt32();
                            _Size.Width = widthHeight & 0x0000FFFF;
                            _Size.Height = widthHeight >> 16;
                            Resize?.Invoke(new ResizeEventArgs(oldSize, _Size));

                            return IntPtr.Zero;
                        }
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
                                    break;
                                case RawInputDeviceType.Keyboard:
                                    ProcessKeyboardInput(rawInput);
                                    break;
                                case RawInputDeviceType.Hid:
                                    ProcessHidInput(rawInput);
                                    break;
                            }

                            return IntPtr.Zero;
                        }
                }

                return User32.DefWindowProc(windowHandle, message, wParam, lParam);
            }
            catch (Exception ex)
            {
                Logger.Log($"Unhandled exception occured in window procedure: {ex}", LogSeverity.Fatal);
                return IntPtr.Zero;
            }
        }

        /// <summary>Process a mouse input event.</summary>
        /// <param name="rawInput">The raw mouse input data.</param>
        private void ProcessMouseInput(RawInput rawInput)
        {
            var rawMouse = rawInput.Data.Mouse;
            var button = MouseButton.LeftButton;
            var isPressing = false;

            // buttons
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.LeftButtonDown))
                (button, isPressing) = (MouseButton.LeftButton, true);
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.LeftButtonUp))
                (button, isPressing) = (MouseButton.LeftButton, false);

            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.MiddleButtonDown))
                (button, isPressing) = (MouseButton.MiddleButton, true);
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.MiddleButtonUp))
                (button, isPressing) = (MouseButton.MiddleButton, false);

            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.RightButtonDown))
                (button, isPressing) = (MouseButton.RightButton, true);
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.RightButtonUp))
                (button, isPressing) = (MouseButton.RightButton, false);

            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Button4Down))
                (button, isPressing) = (MouseButton.BackButton, true);
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Button4Up))
                (button, isPressing) = (MouseButton.BackButton, false);

            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Button5Down))
                (button, isPressing) = (MouseButton.ForwardButton, true);
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Button5Up))
                (button, isPressing) = (MouseButton.ForwardButton, false);

            if (isPressing)
                MouseButtonPressed?.Invoke(new(button));
            else
                MouseButtonReleased?.Invoke(new(button));

            // scroll wheel
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Wheel))
                MouseScroll?.Invoke(new(new(0, rawMouse.ButtonData / 120f), true));
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.HWheel))
                MouseScroll?.Invoke(new(new(rawMouse.ButtonData / 120f, 0), true));

            // position
            MouseMove?.Invoke(new(new(rawMouse.LastX, rawMouse.LastY), !rawMouse.Flags.HasFlag(RawMouseFlags.MoveAbsolute)));
        }

        /// <summary>Process a keyboard input event.</summary>
        /// <param name="rawInput">The raw keyboard input data.</param>
        private void ProcessKeyboardInput(RawInput rawInput)
        {
            var rawKeyboard = rawInput.Data.Keyboard;
            var isE0BitSet = rawKeyboard.Flags.HasFlag(RawInputKeyboardDataFlags.E0);
            var convertedKey = Win32Utilties.ConvertVirtualKey(rawKeyboard.VirtualKey, isE0BitSet, rawKeyboard.MakeCode);

            if (rawKeyboard.Flags.HasFlag(RawInputKeyboardDataFlags.Break))
                KeyReleased?.Invoke(new(convertedKey));
            else
                KeyPressed?.Invoke(new(convertedKey));
        }

        /// <summary>Process a hid input event.</summary>
        /// <param name="rawInput">The raw hid input data.</param>
        private void ProcessHidInput(RawInput rawInput)
        {
            // TODO: implement
        }
    }
}
