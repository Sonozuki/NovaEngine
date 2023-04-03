namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="PanelTabGroupGroup"/>.</summary>
internal sealed class PanelTabGroupGroupViewModel : BindableObject
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when the group is emptied.</summary>
    public event Action Emptied;


    /*********
    ** Fields
    *********/
    /// <summary>The orientation of the group.</summary>
    public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(PanelTabGroupGroupViewModel));


    /*********
    ** Properties
    *********/
    /// <summary>The orientation of the group.</summary>
    public StackOrientation Orientation
    {
        get => (StackOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>The panels in the group.</summary>
    public ObservableCollection<PanelBase> Panels { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public PanelTabGroupGroupViewModel()
    {
        Panels.CollectionChanged += (sender, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0] is PanelTabGroup panelTabGroup)
                    ((PanelTabGroupViewModel)panelTabGroup.BindingContext).Emptied += () => Panels.Remove(panelTabGroup);
                else if (e.NewItems[0] is PanelTabGroupGroup panelTabGroupGroup)
                    ((PanelTabGroupGroupViewModel)panelTabGroupGroup.BindingContext).Emptied += () => Panels.Remove(panelTabGroupGroup);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (!Panels.Any())
                    Emptied?.Invoke();

                // TODO: remove event handlers
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                // TODO: add/remove event handlers
            }
        };
    }
}
