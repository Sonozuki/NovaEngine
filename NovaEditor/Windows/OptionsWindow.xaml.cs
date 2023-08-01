namespace NovaEditor.Windows;

/// <summary>Represents the options window of the application.</summary>
public partial class OptionsWindow : Window
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public OptionsWindow()
    {
        InitializeComponent();

        foreach (var category in OptionsManager.RootOptionCategories)
            RootTreeView.Items.Add(CreateTreeViewItemFromCategory(category));
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates an equivalent <see cref="TreeViewItem"/> of an <see cref="OptionsCategory"/>.</summary>
    /// <param name="category">The category to use to create the <see cref="TreeViewItem"/>.</param>
    /// <returns>The <see cref="TreeViewItem"/> equivalent to <paramref name="category"/>.</returns>
    private TreeViewItem CreateTreeViewItemFromCategory(OptionsCategory category)
    {
        var treeViewItem = new TreeViewItem
        {
            Header = category.Name
        };

        foreach (var subCategory in category.SubCategories)
            treeViewItem.Items.Add(CreateTreeViewItemFromCategory(subCategory));

        return treeViewItem;
    }
}
