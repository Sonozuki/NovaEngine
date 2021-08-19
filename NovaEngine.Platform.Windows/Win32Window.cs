﻿using NovaEngine.External.Platform;
using NovaEngine.Logging;
using NovaEngine.Maths;
using NovaEngine.Windowing;
using System;

namespace NovaEngine.Platform.Windows
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


        /*********
        ** Fields
        *********/
        /// <summary>The procedure of the window.</summary>
        private readonly WindowProcedureDelegate WindowProcedure;


        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public override string Title
        {
            get
            {
                User32.GetWindowText(this.Handle, out var text, 255);
                return text;
            }
            set => User32.SetWindowText(this.Handle, value ?? "");
        }

        /// <inheritdoc/>
        public override Size Size {  get; set; }


        /*********
        ** Public Methods
        *********/
        /// <inheritdoc/>
        public Win32Window(string title, Size size)
            : base(title, size)
        {
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

            this.Handle = User32.CreateWindowEx(0, className, Title, WindowStyle.OverlappedWindow, 0, 0, Size.Width, Size.Height, IntPtr.Zero, IntPtr.Zero, Program.Handle, IntPtr.Zero);
            if (this.Handle == IntPtr.Zero)
                return;
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
                            Size = new(widthHeight & 0x0000FFFF, widthHeight >> 16);
                            Resize?.Invoke(new ResizeEventArgs(oldSize, Size));

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
    }
}