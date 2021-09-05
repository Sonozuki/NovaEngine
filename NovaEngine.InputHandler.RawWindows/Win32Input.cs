﻿using NovaEngine.Extensions;
using NovaEngine.External.Input;
using NovaEngine.IO;
using NovaEngine.Logging;
using NovaEngine.Maths;
using System;
using System.Runtime.InteropServices;

namespace NovaEngine.InputHandler.RawWindows
{
    /// <summary>Represents the raw Windows input handler.</summary>
    public class Win32RawInput : IInputHandler
    {
        /*********
        ** Fields
        *********/
        /// <summary>The current state of the mouse.</summary>
        private MouseState _MouseState;

        /// <summary>The current state of the keyboard.</summary>
        private KeyboardState _KeyboardState;

        /// <summary>The old procedure of the window.</summary>
        private IntPtr OldWindowProcedure;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>The procedure of the window.</summary>
        private WindowProcedureDelegate WindowProcedure;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.



        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public bool CanUseOnPlatform => OperatingSystem.IsWindows();

        /// <inheritdoc/>
        public MouseState MouseState => _MouseState;

        /// <inheritdoc/>
        public KeyboardState KeyboardState => _KeyboardState;


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
                Logger.Log($"Unhandled exception occured in window procedure: {ex}", LogSeverity.Fatal);
                return IntPtr.Zero;
            }
        }

        /// <summary>Process a mouse input event.</summary>
        /// <param name="rawInput">The raw mouse input data.</param>
        private void ProcessMouseInput(RawInput rawInput)
        {
            var rawMouse = rawInput.Data.Mouse;

            // buttons
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

            // scroll wheels
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.Wheel))
                _MouseState.Scroll += new Vector2(0, rawMouse.ButtonData / 120f);
            if (rawMouse.ButtonFlags.HasFlag(RawInputMouseState.HWheel))
                _MouseState.Scroll += new Vector2(rawMouse.ButtonData / 120f, 0);

            // position
            if (rawMouse.Flags.HasFlag(RawMouseFlags.MoveAbsolute))
                _MouseState.Position = new(rawMouse.LastX, rawMouse.LastY);
            else
                _MouseState.Position += new Vector2I(rawMouse.LastX, rawMouse.LastY);
        }

        /// <summary>Process a keyboard input event.</summary>
        /// <param name="rawInput">The raw keyboard input data.</param>
        private void ProcessKeyboardInput(RawInput rawInput)
        {
            var rawKeyboard = rawInput.Data.Keyboard;
            var isE0BitSet = rawKeyboard.Flags.HasFlag(RawInputKeyboardDataFlags.E0);
            var convertedKey = Win32Utilities.ConvertVirtualKey(rawKeyboard.VirtualKey, isE0BitSet, rawKeyboard.MakeCode);

            _KeyboardState[convertedKey] = !rawKeyboard.Flags.HasFlag(RawInputKeyboardDataFlags.Break);
        }

        /// <summary>Process a hid input event.</summary>
        /// <param name="rawInput">The raw hid input data.</param>
        private void ProcessHidInput(RawInput rawInput)
        {
            // TODO: implement
        }
    }
}