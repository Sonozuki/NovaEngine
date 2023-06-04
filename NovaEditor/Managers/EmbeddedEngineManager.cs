namespace NovaEditor.Managers;

/// <summary>Manages the embedded engine.</summary>
internal static class EmbeddedEngineManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Starts the embedded engine.</summary>
    /// <param name="hwndParent">A handle to the parent window.</param>
    /// <param name="width">The width of the engine window.</param>
    /// <param name="height">The height of the engine window.</param>
    public static void StartEngine(HandleRef hwndParent, int width, int height)
    {
        var arguments = new[]
        {
            "-disable-read-commands",
            "-remove-engine-thread-block",
            "-window-parent", hwndParent.Handle.ToString(G11n.Culture),
            "-width", width.ToString(G11n.Culture),
            "-height", height.ToString(G11n.Culture)
        };

        NovaEngine.Program.Main(arguments);
    }
}
