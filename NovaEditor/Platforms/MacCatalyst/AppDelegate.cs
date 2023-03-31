using Foundation;

namespace NovaEditor;

/// <summary>The MacOS application delegate.</summary>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    /*********
    ** Protected Methods
    *********/
    /// <inheritdoc/>
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
