﻿namespace NovaEditor.Windows;

/// <summary>
/// Interaction logic for FloatingPanelWindow.xaml
/// </summary>
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
    internal FloatingPanelWindow(EditorPanelBase content)
        : this(new PanelTabGroup(content)) { }

    /// <summary>Constructs an instance.</summary>
    /// <param name="content">The panel tab group the window should contain.</param>
    internal FloatingPanelWindow(PanelTabGroup content)
        : this()
    {
        ArgumentNullException.ThrowIfNull(content);

        Content = content;
        ((PanelTabGroupViewModel)content.DataContext).Emptied += OnEmptied;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Invoked when the containing panel tab group is emptied.</summary>
    private void OnEmptied()
    {
        ((PanelTabGroupViewModel)((PanelTabGroup)Content).DataContext).Emptied -= OnEmptied;
        Close();
    }
}
