namespace NovaEditor.Controls;

/// <summary>Represents a group of panels with a tab selector.</summary>
public partial class PanelTabGroup : EditorPanelBase
{
    /*********
    ** Fields
    *********/
    /// <summary>The position of the mouse when the mouse button is first pressed.</summary>
    private Point RelativePosition;

    /// <summary>The cached position boundries of each tag in the tab group.</summary>
    private List<double> TabBoundries;

    /// <summary>The index into <see cref="TabBoundries"/> the mouse position currently falls into.</summary>
    private int CurrentBoundryIndex;


    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
    public PanelTabGroup()
    {
        InitializeComponent();
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when a mouse button is pressed.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        RelativePosition = Mouse.GetPosition((IInputElement)VisualTreeHelper.GetParent(this));

        // calculate tab boundries, when the mouse crosses one of these the tab being dragged will change index
        // these are cached as tabs can be variable width meaning the tab could go back and forth quickly if
        // the boundries are calculated each move event
        var tabPanel = VisualTreeHelper.GetParent((DependencyObject)sender);
        TabBoundries = Enumerable.Range(0, VisualTreeHelper.GetChildrenCount(tabPanel))
            .Select(childIndex => VisualTreeHelper.GetChild(tabPanel, childIndex))
            .Cast<TabItem>()
            .Select(tabItem => tabItem.TransformToAncestor((Visual)tabPanel).Transform(new()).X + tabItem.RenderSize.Width)
            .Order()
            .ToList();

        CurrentBoundryIndex = CalculateBoundyIndex(RelativePosition.X);

        ((IInputElement)sender).CaptureMouse();
    }

    /// <summary>Invoked when the mouse is moved.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (!((IInputElement)sender).IsMouseCaptured)
            return;

        var mousePosition = Mouse.GetPosition((IInputElement)VisualTreeHelper.GetParent(this));
        var boundryIndex = CalculateBoundyIndex(mousePosition.X);

        if (CurrentBoundryIndex != boundryIndex)
        {
            var viewModel = (PanelTabGroupViewModel)DataContext;
            viewModel.Panels.Move(CurrentBoundryIndex, boundryIndex);
            CurrentBoundryIndex = boundryIndex;
        }
    }

    /// <summary>Invoked when a mouse button is released.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e) => ((IInputElement)sender).ReleaseMouseCapture();

    /// <summary>Calculates the index into <see cref="TabBoundries"/> a specified value falls into.</summary>
    /// <param name="xPosition">The value to determine which boundry it falls in.</param>
    /// <returns>The index into <see cref="TabBoundries"/> <paramref name="xPosition"/> falls into.</returns>
    private int CalculateBoundyIndex(double xPosition)
    {
        var boundryIndex = 0;

        for (var i = 0; i < TabBoundries.Count - 1; i++)
        {
            if (TabBoundries[boundryIndex] >= xPosition)
                break;

            boundryIndex++;
        }

        return boundryIndex;
    }
}
