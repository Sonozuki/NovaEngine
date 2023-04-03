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

        var panelTabGroupGroup1 = new PanelTabGroupGroup();
        (panelTabGroupGroup1.BindingContext as PanelTabGroupGroupViewModel).Orientation = StackOrientation.Vertical;
        (panelTabGroupGroup1.BindingContext as PanelTabGroupGroupViewModel).Panels.Add(tabGroup3);
        (panelTabGroupGroup1.BindingContext as PanelTabGroupGroupViewModel).Panels.Add(tabGroup4);

        var panelTabGroupGroup2 = new PanelTabGroupGroup();
        (panelTabGroupGroup2.BindingContext as PanelTabGroupGroupViewModel).Orientation = StackOrientation.Horizontal;
        (panelTabGroupGroup2.BindingContext as PanelTabGroupGroupViewModel).Panels.Add(tabGroup1);
        (panelTabGroupGroup2.BindingContext as PanelTabGroupGroupViewModel).Panels.Add(panelTabGroupGroup1);

        (RootPanelTabGroupGroup.BindingContext as PanelTabGroupGroupViewModel).Orientation = StackOrientation.Vertical;
        (RootPanelTabGroupGroup.BindingContext as PanelTabGroupGroupViewModel).Panels.Add(panelTabGroupGroup2);
        (RootPanelTabGroupGroup.BindingContext as PanelTabGroupGroupViewModel).Panels.Add(tabGroup2);
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

