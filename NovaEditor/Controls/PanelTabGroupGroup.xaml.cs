namespace NovaEditor.Controls;

/// <summary>Represents a group of <see cref="PanelTabGroup"/>s.</summary>
public partial class PanelTabGroupGroup : EditorPanelBase
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public PanelTabGroupGroup()
    {
        InitializeComponent();

        ((PanelTabGroupGroupViewModel)DataContext).Panels.CollectionChanged += OnCollectionChanged;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when the collection in the view model changes.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
            CreateGridChildren((EditorPanelBase)e.NewItems[0], addSplitter: MainGrid.Children.Count > 0);
        else if (e.Action == NotifyCollectionChangedAction.Remove)
            RemoveGridChildren(e.OldStartingIndex);
        else
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>Adds a child and grid splitter (and corresponding row/column definitions) to the grid.</summary>
    /// <param name="panel">The child to add.</param>
    /// <param name="addSplitter">Whether a splitter should be added.</param>
    private void CreateGridChildren(EditorPanelBase panel, bool addSplitter)
    {
        if (addSplitter)
        {
            AddDefinition(GridLength.Auto);
            AddSplitter();
        }

        AddDefinition(new(1, GridUnitType.Star));
        AddPanel(panel);
    }

    /// <summary>Removes a child and grid splitter (and corresponding row/column definitions) from the grid.</summary>
    /// <param name="index">The index of the child to remove.</param>
    private void RemoveGridChildren(int index)
    {
        RemoveChild();

        if (index < MainGrid.Children.Count) // check for a splitter after
            RemoveChild();

        // Removes a child and the corresponding row/column definition from the grid.
        void RemoveChild()
        {
            MainGrid.Children.RemoveAt(index);

            if (((PanelTabGroupGroupViewModel)DataContext).Orientation == Orientation.Horizontal)
                MainGrid.ColumnDefinitions.RemoveAt(index);
            else
                MainGrid.RowDefinitions.RemoveAt(index);
        }
    }

    /// <summary>Adds a grid splitter to the grid.</summary>
    private void AddSplitter()
    {
        var gridSplitter = new GridSplitter()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        if (((PanelTabGroupGroupViewModel)DataContext).Orientation == Orientation.Horizontal)
        {
            gridSplitter.Width = 5;
            gridSplitter.SetValue(Grid.ColumnProperty, MainGrid.Children.Count);
        }
        else
        {
            gridSplitter.Height = 5;
            gridSplitter.SetValue(Grid.RowProperty, MainGrid.Children.Count);
        }

        MainGrid.Children.Add(gridSplitter);
    }

    /// <summary>Adds a panel to the grid.</summary>
    /// <param name="panel">The panel to add to the grid.</param>
    private void AddPanel(EditorPanelBase panel)
    {
        if (((PanelTabGroupGroupViewModel)DataContext).Orientation == Orientation.Horizontal)
            panel.SetValue(Grid.ColumnProperty, MainGrid.Children.Count);
        else
            panel.SetValue(Grid.RowProperty, MainGrid.Children.Count);

        MainGrid.Children.Add(panel);
    }

    /// <summary>Adds a row/column definition to the grid.</summary>
    /// <param name="size">The size of the row/column.</param>
    private void AddDefinition(GridLength size)
    {
        if (((PanelTabGroupGroupViewModel)DataContext).Orientation == Orientation.Horizontal)
            MainGrid.ColumnDefinitions.Add(new() { Width = size });
        else
            MainGrid.RowDefinitions.Add(new() { Height = size });
    }
}
