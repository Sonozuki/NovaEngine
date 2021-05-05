using NovaEngine.IO;
using NovaEngine.IO.Events;
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
        /// <summary>Invoked when the window is resized.</summary>
        public override event Action<ResizeEventArgs>? Resize;

        /// <summary>Invoked when the window is closed.</summary>
        public override event Action? Closed;

        /// <summary>Invoked when the window gains focus.</summary>
        public override event Action? FocusGained;

        /// <summary>Invoked when the window loses focus.</summary>
        public override event Action? FocusLost;

        /// <summary>Invoked when the mouse is moved.</summary>
        public override event Action<MouseMoveEventArgs>? MouseMove;

        /// <summary>Invoked when a mouse button is pressed.</summary>
        public override event Action<MouseButtonPressedEventArgs>? MouseButtonPressed;

        /// <summary>Invoked when a mouse button is released.</summary>
        public override event Action<MouseButtonReleasedEventArgs>? MouseButtonReleased;

        /// <summary>Invoked when a key is pressed.</summary>
        public override event Action<KeyPressedEventArgs>? KeyPressed;

        /// <summary>Invoked when a key is released.</summary>
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

            User32.ShowWindow(Handle, CommandShow.Show);

            // register input devices
            var rawInputDevices = new[]
            {
                new RawInputDevice(HidUsageGenericDesktop.Mouse, RawInputDeviceFlags.DevNotify, Handle),
                new RawInputDevice(HidUsageGenericDesktop.Keyboard, RawInputDeviceFlags.DevNotify, Handle)
            };

            if (!User32.RegisterRawInputDevices(rawInputDevices, (uint)rawInputDevices.Length, (uint)Marshal.SizeOf<RawInputDevice>()))
                throw new ApplicationException($"Failed to register raw input device: {Marshal.GetLastWin32Error()}.");
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

                            switch (header.Type)
                            {
                                case RawInputDeviceType.Mouse:
                                    ProcessMouseInput(lParam);
                                    break;
                                case RawInputDeviceType.Keyboard:
                                    ProcessKeyboardInput(lParam);
                                    break;
                                case RawInputDeviceType.Hid:
                                    ProcessHidInput(lParam);
                                    break;
                            }

                            return IntPtr.Zero;
                        }
                }

                return User32.DefWindowProc(windowHandle, message, wParam, lParam);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception occured in window procedure: {ex}");
                return IntPtr.Zero;
            }
        }

        /// <summary>Process a mouse input event.</summary>
        /// <param name="lParam">The lParam from the procedure input message.</param>
        private void ProcessMouseInput(IntPtr lParam)
        {
            Console.WriteLine("Received mouse input event");
        }

        /// <summary>Process a keyboard input event.</summary>
        /// <param name="lParam">The lParam from the procedure input message.</param>
        private void ProcessKeyboardInput(IntPtr lParam)
        {
            Console.WriteLine("Received keyboard input event");
        }

        /// <summary>Process a hid input event.</summary>
        /// <param name="lParam">The lParam from the procedure input message.</param>
        private void ProcessHidInput(IntPtr lParam)
        {
            // TODO: implement
        }
    }
}
