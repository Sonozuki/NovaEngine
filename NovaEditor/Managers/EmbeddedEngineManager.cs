namespace NovaEditor.Managers;

/// <summary>Manages the embedded engine.</summary>
internal static class EmbeddedEngineManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Starts the embedded engine.</summary>
    /// <param name="hwndParent">A handle to the parent window.</param>
    public static void StartEngine(HandleRef hwndParent)
    {
        NovaEngine.Program.Main(new[] { "-disable-read-commands", "-remove-engine-thread-block", $"-window-parent", hwndParent.Handle.ToString(G11n.Culture) });
    }
}
