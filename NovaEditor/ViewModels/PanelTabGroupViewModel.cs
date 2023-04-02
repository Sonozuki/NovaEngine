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

    /// <summary>The command used to change the active tab.</summary>
    public ICommand ChangeActiveTabCommand { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public PanelTabGroupViewModel()
    {
        ChangeActiveTabCommand = new RelayCommand<PanelBase>(ChangeActiveTab);
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Changes the active tab of the group.</summary>
    /// <param name="panel">The panel to set as the active panel.</param>
    private void ChangeActiveTab(PanelBase panel)
    {
        ActivePanel = panel;

        // resetting the collection is required so the UI rerenders the tabs to reset the style for the active tab
        Panels.GetType().GetMethod("OnCollectionReset", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(Panels, null);
    }
}
