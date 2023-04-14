namespace NovaEditor.Windows;

/// <summary>The main window of the application.</summary>
public partial class MainWindow : Window
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public MainWindow()
    {
        InitializeComponent();

        var tabGroup1 = new PanelTabGroup();
        var activePanel1 = new InspectorPanel();
        (tabGroup1.DataContext as PanelTabGroupViewModel).ActivePanel = activePanel1;
        (tabGroup1.DataContext as PanelTabGroupViewModel).Panels.Add(activePanel1);
        (tabGroup1.DataContext as PanelTabGroupViewModel).Panels.Add(new AssetsPanel());
        (tabGroup1.DataContext as PanelTabGroupViewModel).Panels.Add(new HierarchyPanel());

        var tabGroup2 = new PanelTabGroup();
        var activePanel2 = new AssetsPanel();
        (tabGroup2.DataContext as PanelTabGroupViewModel).ActivePanel = activePanel2;
        (tabGroup2.DataContext as PanelTabGroupViewModel).Panels.Add(new HierarchyPanel());
        (tabGroup2.DataContext as PanelTabGroupViewModel).Panels.Add(activePanel2);
        (tabGroup2.DataContext as PanelTabGroupViewModel).Panels.Add(new InspectorPanel());

        var tabGroup3 = new PanelTabGroup();
        var activePanel3 = new HierarchyPanel();
        (tabGroup3.DataContext as PanelTabGroupViewModel).ActivePanel = activePanel3;
        (tabGroup3.DataContext as PanelTabGroupViewModel).Panels.Add(activePanel3);
        (tabGroup3.DataContext as PanelTabGroupViewModel).Panels.Add(new AssetsPanel());
        (tabGroup3.DataContext as PanelTabGroupViewModel).Panels.Add(new InspectorPanel());

        var tabGroup4 = new PanelTabGroup();
        var activePanel4 = new HierarchyPanel();
        (tabGroup4.DataContext as PanelTabGroupViewModel).ActivePanel = activePanel4;
        (tabGroup4.DataContext as PanelTabGroupViewModel).Panels.Add(activePanel4);
        (tabGroup4.DataContext as PanelTabGroupViewModel).Panels.Add(new AssetsPanel());
        (tabGroup4.DataContext as PanelTabGroupViewModel).Panels.Add(new InspectorPanel());

        var panelTabGroupGroup1 = new PanelTabGroupGroup();
        (panelTabGroupGroup1.DataContext as PanelTabGroupGroupViewModel).Orientation = Orientation.Vertical;
        (panelTabGroupGroup1.DataContext as PanelTabGroupGroupViewModel).Panels.Add(tabGroup3);
        (panelTabGroupGroup1.DataContext as PanelTabGroupGroupViewModel).Panels.Add(tabGroup4);

        var panelTabGroupGroup2 = new PanelTabGroupGroup();
        (panelTabGroupGroup2.DataContext as PanelTabGroupGroupViewModel).Orientation = Orientation.Horizontal;
        (panelTabGroupGroup2.DataContext as PanelTabGroupGroupViewModel).Panels.Add(tabGroup1);
        (panelTabGroupGroup2.DataContext as PanelTabGroupGroupViewModel).Panels.Add(panelTabGroupGroup1);

        (RootPanelTabGroupGroup.DataContext as PanelTabGroupGroupViewModel).Orientation = Orientation.Vertical;
        (RootPanelTabGroupGroup.DataContext as PanelTabGroupGroupViewModel).Panels.Add(panelTabGroupGroup2);
        (RootPanelTabGroupGroup.DataContext as PanelTabGroupGroupViewModel).Panels.Add(tabGroup2);
    }
}
