namespace NovaEditor.Controls;

/// <summary>Represents a button containing a label.</summary>
public partial class TextButton : Button
{
    /*********
    ** Fields
    *********/
    /// <summary>The text in the button.</summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextButton));


    /*********
    ** Properties
    *********/
    /// <summary>The text in the button.</summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public TextButton()
    {
        InitializeComponent();
    }
}
