namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="PanelTabGroup"/>.</summary>
public sealed class PanelTabGroupViewModel : DependencyObject
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when the group is emptied.</summary>
    public event Action Emptied;


    /*********
    ** Properties
    *********/
    /// <summary>The panels in the group.</summary>
    public ObservableCollection<EditorPanelBase> Panels { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public PanelTabGroupViewModel()
    {
        Panels.CollectionChanged += (sender, e) =>
        {
            if (!Panels.Any())
                Emptied?.Invoke();
        };
    }
}
