namespace NovaEditor.Controls.Panels;

/// <summary>Represents an editor panel.</summary>
/// <remarks>An editor panel refers to a control that can be docked.</remarks>
public abstract class EditorPanelBase : UserControl
{
    /*********
    ** Fields
    *********/
    /// <summary>The title of the panel.</summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(EditorPanelBase));


    /*********
    ** Properties
    *********/
    /// <summary>The title of the panel.</summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>The persistent settings of the panel.</summary>
    public NotificationDictionary<string, string> Settings { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="settings">The persistent settings of the panel.</param>
    protected EditorPanelBase(NotificationDictionary<string, string> settings)
    {
        Settings = settings;
    }
}
