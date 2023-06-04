namespace NovaEditor.Controls;

/// <summary>Represents a NovaEngine window host.</summary>
public class EngineHost : HwndHost
{
    /*********
    ** Fields
    *********/
    /// <summary>The width of the child window.</summary>
    private readonly int Width;

    /// <summary>The height of the child window.</summary>
    private readonly int Height;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="width">The width of the child window.</param>
    /// <param name="height">The height of the child window.</param>
    public EngineHost(double width, double height)
    {
        Width = (int)width;
        Height = (int)height;
    }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Creates the window to be hosted.</summary>
    /// <param name="hwndParent">The handle of the parent window.</param>
    /// <returns>The handle to the child Win32 window.</returns>
    protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    {
        EmbeddedEngineManager.StartEngine(hwndParent, Width, Height);
        var engineWindow = NovaEngine.Program.MainWindowTask.Result.Handle;
        return new HandleRef(this, engineWindow);
    }

    /// <summary>Destroys the hosted window.</summary>
    /// <param name="hwnd">A structure that contains the window handle.</param>
    protected override void DestroyWindowCore(HandleRef hwnd) => User32.DestroyWindow(hwnd.Handle);
}
