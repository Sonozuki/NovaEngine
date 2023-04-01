namespace NovaEditor;

/// <summary>The main page of the application.</summary>
public partial class MainPage : ContentPage
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public MainPage()
    {
        InitializeComponent();

        AddPanelsToViewMenu();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Adds all panels to the 'View' menu.</summary>
    private void AddPanelsToViewMenu()
    {
        foreach (var panel in PanelManager.GetAllPanels())
            ViewMenuBarItem.Add(new MenuFlyoutItem() { Text = panel.Title });
    }
}

