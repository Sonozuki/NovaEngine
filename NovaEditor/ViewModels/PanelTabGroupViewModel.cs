namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="PanelTabGroup"/>.</summary>
internal sealed class PanelTabGroupViewModel : BindableObject
{
    /*********
    ** Events
    *********/
    /// <summary>Invoked when the group is emptied.</summary>
    public event Action Emptied;


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

    /// <summary>The command used to close the active tab.</summary>
    public ICommand CloseActivePanel { get; set; }

    /// <summary>The command used to change the active tab.</summary>
    public ICommand ChangeActiveTabCommand { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public PanelTabGroupViewModel()
    {
        CloseActivePanel = new RelayCommand(CloseActiveTab);
        ChangeActiveTabCommand = new RelayCommand<PanelBase>(ChangeActiveTab);

        Panels.CollectionChanged += (sender, e) =>
        {
            if (!Panels.Any())
                Emptied?.Invoke();
        };
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Closes the active tab.</summary>
    private void CloseActiveTab()
    {
        if (!Panels.Any()) // if the user repeatedly closes tabs fast enough, then sometimes a click registers after the last tab was closed before the UI is updated
            return;

        if (Panels.Count == 1)
        {
            Panels.RemoveAt(0);
            return;
        }

        var activeTabIndex = Panels.IndexOf(ActivePanel);
        Panels.RemoveAt(activeTabIndex);

        if (activeTabIndex == Panels.Count)
            activeTabIndex--;

        ChangeActiveTab(Panels[activeTabIndex]);
    }

    /// <summary>Changes the active tab of the group.</summary>
    /// <param name="panel">The panel to set as the active panel.</param>
    private void ChangeActiveTab(PanelBase panel)
    {
        ActivePanel = panel;

        // resetting the collection is required so the UI rerenders the tabs to reset the style for the active tab
        // invoking a reset isn't great but a property change notification wasn't making it get rerendered
        Panels.GetType().GetMethod("OnCollectionReset", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(Panels, null);
    }
}
