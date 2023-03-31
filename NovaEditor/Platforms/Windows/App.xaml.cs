namespace NovaEditor.WinUI;

/// <summary>The Windows application entry point.</summary>
public partial class App : MauiWinUIApplication
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public App()
    {
        InitializeComponent();
    }


    /*********
    ** Protected Methods
    *********/
    /// <inheritdoc/>
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

