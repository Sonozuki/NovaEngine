namespace NovaEditor.ViewModels;

/// <summary>Represents the view model for <see cref="PanelTabGroup"/>.</summary>
internal sealed class PanelTabGroupViewModel : DependencyObject
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
    public static readonly DependencyProperty ActivePanelProperty = DependencyProperty.Register(nameof(ActivePanel), typeof(EditorPanelBase), typeof(PanelTabGroupViewModel));


    /*********
    ** Properties
    *********/
    /// <summary>The panel currently active in the group.</summary>
    public EditorPanelBase ActivePanel
    {
        get => (EditorPanelBase)GetValue(ActivePanelProperty);
        set => SetValue(ActivePanelProperty, value);
    }

    /// <summary>The panels in the group.</summary>
    public ObservableCollection<EditorPanelBase> Panels { get; } = new();

    /// <summary>The command used to close the active tab.</summary>
    public ICommand CloseActivePanelCommand { get; set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public PanelTabGroupViewModel()
    {
        CloseActivePanelCommand = new RelayCommand(CloseActiveTab);

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

        ActivePanel = Panels[activeTabIndex];
    }
}
