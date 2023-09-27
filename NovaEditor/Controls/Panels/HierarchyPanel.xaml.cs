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

    // TODO: temp - the onloaded is only used as it's called after the SceneViewPanel's onloaded, once the below modifications have been done
    // then this can be moved back into the constructor
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // TODO: currently there is a race condition between the engine getting initialised and this panel retrieving the loaded scenes
        // this will probably need to be changed to loaded the engine before the workspace starts getting parsed, this way the scenes can be loaded
        // and the SceneViewPanel can just hook into the engine instance that is already been created
        Thread.Sleep(2000);

        // TODO: there may also need to be some sort of dependency system, e.g. if scene X is loaded which requires the HUD scene, the HUD
        // scene will automatically be loaded this needs some way of setting ofc and to be displayed. perhaps show all scenes in this panel
        // and you can select scenes to hide all others that aren't that scene or any of it's dependency scenes
        foreach (var scene in SceneManager.LoadedScenes)
            RootTreeView.Items.Add(CreateTreeViewItem(scene.Name, scene.RootGameObjects));
    }
}
