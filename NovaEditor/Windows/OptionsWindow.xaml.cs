namespace NovaEditor.Windows;

/// <summary>Represents the options window of the application.</summary>
public partial class OptionsWindow : Window
{
    /*********
    ** Properties
    *********/
    /// <summary>The view model of the window.</summary>
    public OptionsWindowViewModel ViewModel { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public OptionsWindow()
    {
        DataContext = ViewModel;
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
        treeViewItem.Selected += (_, _) => ViewModel.SelectedCategory = category;

        foreach (var subCategory in category.SubCategories)
            treeViewItem.Items.Add(CreateTreeViewItemFromCategory(subCategory));

        return treeViewItem;
    }
}
