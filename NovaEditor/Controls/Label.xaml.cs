namespace NovaEditor.Controls;

/// <summary>Represents a control that can display a <see langword="string"/>.</summary>
public partial class Label : UserControl
{
    /*********
    ** Fields
    *********/
    /// <summary>The text of the label.</summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(Label));


    /*********
    ** Properties
    *********/
    /// <summary>The text of the label.</summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public Label()
    {
        InitializeComponent();
    }
}
