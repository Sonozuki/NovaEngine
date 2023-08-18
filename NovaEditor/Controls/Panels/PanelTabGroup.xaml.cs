namespace NovaEditor.Controls.Panels;

/// <summary>Represents a group of panels with a tab selector.</summary>
public partial class PanelTabGroup : EditorPanelBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the mouse when the mouse button is first pressed.</summary>
    private Point RelativePosition;

    /// <summary>The cached bounding box of all the tabs in the tab group.</summary>
    private Rect TabsBoundingBox;

    /// <summary>The cached position boundaries of each tag in the tab group.</summary>
    private List<double> TabBoundaries;

    /// <summary>The index into <see cref="TabBoundaries"/> the mouse position fell into last move event.</summary>
    private int OldBoundaryIndex;


    /*********
    ** Properties
    *********/
    /// <summary>The view model of the panel.</summary>
    public PanelTabGroupViewModel ViewModel { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public PanelTabGroup()
    {
        DataContext = ViewModel;
        InitializeComponent();
    }

    /// <summary>Constructs an instance.</summary>
    public PanelTabGroup(EditorPanelBase panel)
        : this()
    {
        ArgumentNullException.ThrowIfNull(panel);

        ViewModel.Panels.Add(panel);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when a mouse button is pressed.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        RelativePosition = Mouse.GetPosition(this);

        // calculate tab boundaries, when the mouse crosses one of these the tab being dragged will change index
        // these are cached as tabs can be variable width meaning the tab could go back and forth quickly if
        // the boundaries are calculated each move event
        var tabPanel = (TabPanel)VisualTreeHelper.GetParent((DependencyObject)sender);
        CalculateTabMetrics(tabPanel);

        OldBoundaryIndex = CalculateBoundaryIndex(RelativePosition.X);

        ((IInputElement)sender).CaptureMouse();
    }

    /// <summary>Invoked when the mouse is moved.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (!((IInputElement)sender).IsMouseCaptured)
            return;

        var mousePosition = Mouse.GetPosition(this);

        // if the tab is the only tab and the tab group is the root of a floating window, the window should be moved instead
        if (ViewModel.Panels.Count == 1 && Window.GetWindow((DependencyObject)sender) is FloatingPanelWindow floatingPanelWindow)
        {
            ((IInputElement)sender).ReleaseMouseCapture();
            floatingPanelWindow.DragMove();
            return;
        }

        // check for floating window creation
        if (!TabsBoundingBox.Contains(mousePosition))
        {
            var window = WindowManager.CreateFloatingPanelWindow((EditorPanelBase)PanelTabControl.SelectedItem, RenderSize);
            CloseSelectedPanel();

            ((IInputElement)sender).ReleaseMouseCapture();

            var thisWindow = Window.GetWindow(this);
            var screenMousePosition = thisWindow.PointToScreen(Mouse.GetPosition(thisWindow));
            window.Top = screenMousePosition.Y - mousePosition.Y;
            window.Left = screenMousePosition.X - mousePosition.X;

            window.Show();
            window.DragMove();
            return;
        }

        // check for tab reorganisation
        var boundaryIndex = CalculateBoundaryIndex(mousePosition.X);
        if (OldBoundaryIndex != boundaryIndex)
        {
            ViewModel.Panels.Move(OldBoundaryIndex, boundaryIndex);
            OldBoundaryIndex = boundaryIndex;
        }
    }

    /// <summary>Invoked when a mouse button is released.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e) => ((IInputElement)sender).ReleaseMouseCapture();

    /// <summary>Calculates the metrics of the tabs in a tab panel.</summary>
    /// <param name="tabPanel">The tab panel whose tab metrics should be calculated.</param>
    private void CalculateTabMetrics(TabPanel tabPanel)
    {
        var tabItems = Enumerable.Range(0, VisualTreeHelper.GetChildrenCount(tabPanel))
            .Select(childIndex => VisualTreeHelper.GetChild(tabPanel, childIndex))
            .Cast<TabItem>()
            .OrderBy(tabItem => CalculateTabTopLeft(tabItem).X).ToList();

        TabsBoundingBox = new(CalculateTabTopLeft(tabItems.First()), CalculateTabBottomRight(tabItems.Last()));

        TabBoundaries = tabItems.Skip(1)
            .Select(tabItem => CalculateTabTopLeft(tabItem).X)
            .ToList();

        // Calculates the top left point of a tab item.
        Point CalculateTabTopLeft(TabItem tabItem) => tabItem.TransformToAncestor(this).Transform(new());

        // Calculates the bottom right point of a tab item.
        Point CalculateTabBottomRight(TabItem tabItem)
        {
            var topLeft = CalculateTabTopLeft(tabItem);
            return new(topLeft.X + tabItem.RenderSize.Width,
                       topLeft.Y + tabItem.RenderSize.Height);
        }
    }

    /// <summary>Calculates the index into <see cref="TabBoundaries"/> a specified value falls into.</summary>
    /// <param name="xPosition">The value to determine which boundary it falls in.</param>
    /// <returns>The index into <see cref="TabBoundaries"/> <paramref name="xPosition"/> falls into.</returns>
    private int CalculateBoundaryIndex(double xPosition)
    {
        var boundaryIndex = 0;

        for (var i = 0; i < TabBoundaries.Count; i++)
        {
            if (TabBoundaries[boundaryIndex] >= xPosition)
                break;

            boundaryIndex++;
        }

        return boundaryIndex;
    }

    /// <summary>Closes the selected panel.</summary>
    private void CloseSelectedPanel() => ViewModel.Panels.Remove((EditorPanelBase)PanelTabControl.SelectedItem);

    /// <summary>Invoked when the close button is clicked.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnCloseButtonClick(object sender, RoutedEventArgs e) => CloseSelectedPanel();
}
