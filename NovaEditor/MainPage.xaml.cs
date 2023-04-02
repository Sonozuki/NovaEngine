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

        var tabGroup1 = new PanelTabGroup();
        var activePanel1 = new InspectorPanel();
        (tabGroup1.BindingContext as PanelTabGroupViewModel).ActivePanel = activePanel1;
        (tabGroup1.BindingContext as PanelTabGroupViewModel).Panels.Add(activePanel1);
        (tabGroup1.BindingContext as PanelTabGroupViewModel).Panels.Add(new AssetsPanel());
        (tabGroup1.BindingContext as PanelTabGroupViewModel).Panels.Add(new HierarchyPanel());

        var tabGroup2 = new PanelTabGroup();
        var activePanel2 = new AssetsPanel();
        (tabGroup2.BindingContext as PanelTabGroupViewModel).ActivePanel = activePanel2;
        (tabGroup2.BindingContext as PanelTabGroupViewModel).Panels.Add(new HierarchyPanel());
        (tabGroup2.BindingContext as PanelTabGroupViewModel).Panels.Add(activePanel2);
        (tabGroup2.BindingContext as PanelTabGroupViewModel).Panels.Add(new InspectorPanel());

        var tabGroup3 = new PanelTabGroup();
        var activePanel3 = new HierarchyPanel();
        (tabGroup3.BindingContext as PanelTabGroupViewModel).ActivePanel = activePanel3;
        (tabGroup3.BindingContext as PanelTabGroupViewModel).Panels.Add(activePanel3);
        (tabGroup3.BindingContext as PanelTabGroupViewModel).Panels.Add(new AssetsPanel());
        (tabGroup3.BindingContext as PanelTabGroupViewModel).Panels.Add(new InspectorPanel());

        var tabGroup4 = new PanelTabGroup();
        var activePanel4 = new HierarchyPanel();
        (tabGroup4.BindingContext as PanelTabGroupViewModel).ActivePanel = activePanel4;
        (tabGroup4.BindingContext as PanelTabGroupViewModel).Panels.Add(activePanel4);
        (tabGroup4.BindingContext as PanelTabGroupViewModel).Panels.Add(new AssetsPanel());
        (tabGroup4.BindingContext as PanelTabGroupViewModel).Panels.Add(new InspectorPanel());

        var panelPair1 = new PanelPair();
        (panelPair1.BindingContext as PanelPairViewModel).Orientation = StackOrientation.Vertical;
        (panelPair1.BindingContext as PanelPairViewModel).Panels[0] = tabGroup3;
        (panelPair1.BindingContext as PanelPairViewModel).Panels[1] = tabGroup4;

        var panelPair2 = new PanelPair();
        (panelPair2.BindingContext as PanelPairViewModel).Orientation = StackOrientation.Horizontal;
        (panelPair2.BindingContext as PanelPairViewModel).Panels[0] = tabGroup1;
        (panelPair2.BindingContext as PanelPairViewModel).Panels[1] = panelPair1;

        (RootPanelPair.BindingContext as PanelPairViewModel).Orientation = StackOrientation.Vertical;
        (RootPanelPair.BindingContext as PanelPairViewModel).Panels[0] = panelPair2;
        (RootPanelPair.BindingContext as PanelPairViewModel).Panels[1] = tabGroup2;
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

