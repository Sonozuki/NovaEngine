namespace NovaEngine.Platform.Windows;

/// <summary>Represents a Win32 window.</summary>
public class Win32Window : PlatformWindowBase
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
    ** Fields
    *********/
    /// <summary>The procedure of the window.</summary>
    private readonly WindowProcedureDelegate WindowProcedure;


    /*********
    ** Properties
    *********/
    /// <inheritdoc/>
    public override string Title
    {
        get
        {
#pragma warning disable CA1806 // Do not ignore method results
            User32.GetWindowText(Handle, out var text, 255);
#pragma warning restore CA1806 // Do not ignore method results
            return text;
        }
        set => User32.SetWindowText(Handle, value ?? "");
    }

    /// <inheritdoc/>
    public override Vector2I Size { get; set; }


    /*********
    ** Constructors
    *********/
    /// <inheritdoc/>
    public unsafe Win32Window(string title, Vector2I size)
    {
        WindowProcedure = Procedure;
        Size = size;

        var className = "nova";

        var windowClass = new WindowClass()
        {
            WindowProcedure = WindowProcedure,
            Handle = Program.Handle,
            ClassName = className,
            Cursor = User32.LoadCursor(IntPtr.Zero, 32512) // normal 'arrow' cursor
        };

        User32.RegisterClass(in windowClass);

        // adjust window size so client area is the specified size
        var rectangle = new Common.Windows.Native.Rectangle(Vector2I.Zero, Size);
        var style = WindowStyle.OverlappedWindow;
        if (Program.Arguments.WindowParent != 0)
            style |= WindowStyle.Child;

        if (!User32.AdjustWindowRect(&rectangle, style, false))
            throw new Win32Exception("Failed to adjust Win32 window client area.").Log(LogSeverity.Fatal);

        // change rectangle top left to be zero (so it's not off of the screen) // TODO: temp?
        rectangle.Right -= rectangle.Left;
        rectangle.Bottom -= rectangle.Top;
        rectangle.Left = 0;
        rectangle.Top = 0;

        Handle = User32.CreateWindowEx(0, className, title, style, rectangle.Left, rectangle.Top, rectangle.Right - rectangle.Left, rectangle.Bottom - rectangle.Top, Program.Arguments.WindowParent, IntPtr.Zero, Program.Handle, IntPtr.Zero);
        if (Handle == IntPtr.Zero)
            throw new Win32Exception("Failed to create Win32 window.").Log(LogSeverity.Fatal);
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override void ProcessEvents()
    {
        while (User32.PeekMessage(out var message, IntPtr.Zero, 0, 0, RemoveMessage.Remove))
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
    /// <summary>An application-defined method that processes messages sent to a window.</summary>
    /// <param name="windowHandle">A handle to the window.</param>
    /// <param name="message">The message.</param>
    /// <param name="wParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
    /// <param name="lParam">Additional message information. The contents of this parameter depend on the value of the <paramref name="message"/> parameter.</param>
    /// <returns>The result of the message processing, which depends on the message sent.</returns>
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
                        Resize?.Invoke(new(oldSize, Size));
                        return IntPtr.Zero;
                    }
                case Message.KillFocus:
                    {
                        LostFocus?.Invoke();
                        return IntPtr.Zero;
                    }
            }

            return User32.DefWindowProc(windowHandle, message, wParam, lParam);
        }
        catch (Exception ex)
        {
            Logger.LogFatal($"Unhandled exception occurred in window procedure: {ex}");
            return IntPtr.Zero;
        }
    }
}
