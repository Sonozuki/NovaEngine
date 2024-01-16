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
    ** Fields
    *********/
    /// <summary>The index of the selected panel in the tab control.</summary>
    public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(PanelTabGroupViewModel), new(SelectedIndexPropertyChanged));

    /// <summary>The persistent settings of the panel.</summary>
    private readonly NotificationDictionary<string, string> Settings;


    /*********
    ** Properties
    *********/
    /// <summary>The index of the selected panel in the tab control.</summary>
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    /// <summary>The panels in the group.</summary>
    public ObservableCollection<EditorPanelBase> Panels { get; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="settings">The settings of the panel.</param>
    public PanelTabGroupViewModel(NotificationDictionary<string, string> settings)
    {
        Settings = settings;
        if (Settings.TryGetValue(nameof(SelectedIndex), out var selectedIndex))
            SelectedIndex = int.Parse(selectedIndex, G11n.Culture);

        Panels.CollectionChanged += (sender, e) =>
        {
            if (!Panels.Any())
                Emptied?.Invoke();
        };
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when <see cref="SelectedIndexProperty"/> is changed.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private static void SelectedIndexPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        (sender as PanelTabGroupViewModel).Settings[nameof(SelectedIndex)] = ((int)e.NewValue).ToString(G11n.Culture);
    }
}
