namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="PanelTabGroup"/>.</summary>
internal sealed class PanelTabGroupViewModel : BindableObject
{
    /*********
    ** Fields
    *********/
    /// <summary>The panel currently active in the group.</summary>
    public static readonly BindableProperty ActivePanelProperty = BindableProperty.Create(nameof(ActivePanel), typeof(PanelBase), typeof(PanelTabGroupViewModel));


    /*********
    ** Properties
    *********/
    /// <summary>The panel currently active in the group.</summary>
    public PanelBase ActivePanel
    {
        get => (PanelBase)GetValue(ActivePanelProperty);
        set => SetValue(ActivePanelProperty, value);
    }

    /// <summary>The panels in the group.</summary>
    public ObservableCollection<PanelBase> Panels { get; } = new();
}
