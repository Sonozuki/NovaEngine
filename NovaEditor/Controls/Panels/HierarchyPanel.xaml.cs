namespace NovaEditor.Controls.Panels;

/// <summary>Represents the panel used for managing the content of scenes.</summary>
public partial class HierarchyPanel : EditorPanelBase
{
    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    public HierarchyPanel()
    {
        InitializeComponent();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates a <see cref="TreeViewItem"/> with a name and child game objects.</summary>
    /// <param name="name">The header of the <see cref="TreeViewItem"/>.</param>
    /// <param name="childGameObjects">The children of the <see cref="TreeViewItem"/>.</param>
    /// <returns>The <see cref="TreeViewItem"/> with the specified name and children.</returns>
    private TreeViewItem CreateTreeViewItem(string name, IEnumerable<GameObject> childGameObjects)
    {
        var treeViewItem = new TreeViewItem
        {
            Header = name
        };

        foreach (var childGameObject in childGameObjects)
            treeViewItem.Items.Add(CreateTreeViewItem(childGameObject.Name, childGameObject.Children));

        return treeViewItem;
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
            RootTreeView.Items.Add(CreateTreeViewItem(scene.Name, scene.RootGameObjects));
    }
}
