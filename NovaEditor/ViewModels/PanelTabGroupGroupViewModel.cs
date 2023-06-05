namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="PanelTabGroupGroup"/>.</summary>
public sealed class PanelTabGroupGroupViewModel : DependencyObject
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
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(PanelTabGroupGroupViewModel));


    /*********
    ** Properties
    *********/
    /// <summary>The orientation of the group.</summary>
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>The panels in the group.</summary>
    public ObservableCollection<EditorPanelBase> Panels { get; } = new();


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
                    panelTabGroup.ViewModel.Emptied += () => Panels.Remove(panelTabGroup);
                else if (e.NewItems[0] is PanelTabGroupGroup panelTabGroupGroup)
                    panelTabGroupGroup.ViewModel.Emptied += () => Panels.Remove(panelTabGroupGroup);
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
