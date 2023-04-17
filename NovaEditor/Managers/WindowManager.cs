namespace NovaEditor.Managers;

/// <summary>Manages window creation.</summary>
internal static class WindowManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Creates a window containing a panel.</summary>
    /// <param name="content">The panel the window should contain.</param>
    /// <param name="size">The size of the window.</param>
    public static void CreateFloatingPanelWindow(EditorPanelBase content, Size size) =>
        new FloatingPanelWindow(content)
        {
            Width = size.Width,
            Height = size.Height
        }.Show();
}
