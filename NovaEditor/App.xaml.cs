namespace NovaEditor;

/// <summary>The application.</summary>
public partial class App : Application
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
