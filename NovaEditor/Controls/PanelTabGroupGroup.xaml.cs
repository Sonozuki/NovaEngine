#pragma warning disable CS0618 // Type or member is obsolete

namespace NovaEditor.Controls;

/// <summary>Represents a group of <see cref="PanelTabGroup"/>s.</summary>
public partial class PanelTabGroupGroup : PanelBase
{
    /*********
    ** Constructor
    *********/
    /// <summary>Constructs an instance.</summary>
	public PanelTabGroupGroup()
    {
        InitializeComponent();

        ((PanelTabGroupGroupViewModel)BindingContext).Panels.CollectionChanged += OnCollectionChanged;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when the collection in the view model changes.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        var viewModel = (PanelTabGroupGroupViewModel)BindingContext;

        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            if (MainStackLayout.Any())
                MainStackLayout.Add(CreateResizeBoxView(viewModel.Orientation));

            MainStackLayout.Add(new ContentView
            {
                Content = (View)e.NewItems[0],
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            });
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            var index = e.OldStartingIndex;
            MainStackLayout.RemoveAt(index);

            if (index < MainStackLayout.Count) // check for a box view after
                MainStackLayout.RemoveAt(index);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>Creates a resize box view of a specific orientation.</summary>
    /// <param name="orientation">The orientation to create the resize box view for.</param>
    /// <returns>The created box view.</returns>
    private BoxView CreateResizeBoxView(StackOrientation orientation)
    {
        var boxView = new BoxView();

        if (orientation == StackOrientation.Horizontal)
        {
            boxView.WidthRequest = 6;
            boxView.VerticalOptions = LayoutOptions.FillAndExpand;

            CursorManager.SetHoverCursor(boxView, Cursor.ResiveHorizontal);
        }
        else
        {
            boxView.HorizontalOptions = LayoutOptions.FillAndExpand;
            boxView.HeightRequest = 6;

            CursorManager.SetHoverCursor(boxView, Cursor.ResiveVertical);
        }

        return boxView;
    }
}
