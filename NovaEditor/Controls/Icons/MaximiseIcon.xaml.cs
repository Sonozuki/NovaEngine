namespace NovaEditor.Controls.Icons;

/// <summary>Represents the icon for the maximise button in <see cref="MainWindowTitleBar"/>.</summary>
public partial class MaximiseIcon : UserControl
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public MaximiseIcon()
    {
        InitializeComponent();
    }

    /*********
    ** Public Methods
    *********/
    /// <summary>Changes the icon to the restore window state icon.</summary>
    public void ChangeIconToRestore() => RootPath.Data = Geometry.Parse("F1M11.999,10.002L10.998,10.002 10.998,5.002 5.998,5.002 5.998,4.001 11.999,4.001z M10.002,11.999L4.001,11.999 4.001,5.998 10.002,5.998z M5.002,3L5.002,5.002 3,5.002 3,13 10.998,13 10.998,10.998 13,10.998 13,3z");

    /// <summary>Changes the icon to the maximise window state icon.</summary>
    public void ChangeIconToMaximise() => RootPath.Data = Geometry.Parse("F1M12,12L4,12 4,4 12,4z M3,13L13,13 13,3 3,3z");
}
