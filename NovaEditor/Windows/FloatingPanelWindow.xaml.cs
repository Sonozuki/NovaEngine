namespace NovaEditor.Windows;

/// <summary>Represents the floating <see cref="EditorPanelBase"/> window.</summary>
public partial class FloatingPanelWindow : Window
{
    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    internal FloatingPanelWindow()
    {
        InitializeComponent();
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="content">The panel the window should contain.</param>
    /// <param name="size">The size of the content of the window.</param>
    internal FloatingPanelWindow(EditorPanelBase content, Size size)
        : this(new PanelTabGroup(content) { Width = size.Width, Height = size.Height }) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="content">The panel tab group the window should contain.</param>
    internal FloatingPanelWindow(PanelTabGroup content)
        : this()
    {
        ArgumentNullException.ThrowIfNull(content);

        Content = content;
        content.ViewModel.Emptied += Close;
    }
}
