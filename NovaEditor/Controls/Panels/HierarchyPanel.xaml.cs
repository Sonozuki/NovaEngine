namespace NovaEditor.Controls.Panels;

/// <summary>Represents the panel used for managing the content of scenes.</summary>
public partial class HierarchyPanel : EditorPanelBase
{
    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="settings">The persistent settings of the panel.</param>
    public HierarchyPanel(NotificationDictionary<string, string> settings)
        : base(settings)
    {
        InitializeComponent();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates a tree view item from a scene.</summary>
    /// <param name="scene">The scene to create the tree view item from.</param>
    /// <returns>A tree view item representing <paramref name="scene"/>.</returns>
    private TreeViewItem CreateTreeViewItem(Scene scene)
    {
        var treeViewItem = new TreeViewItem
        {
            Header = scene.Name
        };

        foreach (var rootGameObject in scene.RootGameObjects)
            treeViewItem.Items.Add(CreateTreeViewItem(rootGameObject));

        return treeViewItem;
    }

    /// <summary>Creates a tree view item from a game object.</summary>
    /// <param name="gameObject">The game object to create the tree view item from.</param>
    /// <returns>A tree view item representing <paramref name="gameObject"/>.</returns>
    private TreeViewItem CreateTreeViewItem(GameObject gameObject)
    {
        var treeViewItem = new TreeViewItem
        {
            Header = gameObject.Name
        };
        treeViewItem.Selected += (_, e) => OnGameObjectTreeViewItemSelected(e, gameObject);

        foreach (var childGameObject in gameObject.Children)
            treeViewItem.Items.Add(CreateTreeViewItem(childGameObject));

        return treeViewItem;
    }

    /// <summary>Invoked when a game object in the hierarchy is selected.</summary>
    /// <param name="e">The event data.</param>
    /// <param name="gameObject">The game object that was selected.</param>
    private void OnGameObjectTreeViewItemSelected(RoutedEventArgs e, GameObject gameObject)
    {
        ProjectManager.SelectedGameObject = gameObject;
        e.Handled = true;
    }

    /// <summary>Invoked when the control has been initialised.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private async void OnInitialised(object sender, EventArgs e)
    {
        await NovaEngine.Program.ScenesLoadedTask.WaitAsync(TimeSpan.FromMinutes(1)).ConfigureAwait(true);

        // TODO: there may also need to be some sort of dependency system, e.g. if scene X is loaded which requires the HUD scene, the HUD
        // scene will automatically be loaded this needs some way of setting ofc and to be displayed. perhaps show all scenes in this panel
        // and you can select scenes to hide all others that aren't that scene or any of it's dependency scenes
        foreach (var scene in SceneManager.LoadedScenes)
            RootTreeView.Items.Add(CreateTreeViewItem(scene));
    }
}
