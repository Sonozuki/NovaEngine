namespace NovaEditor.Controls;

/// <summary>Represents a button containing a glyph.</summary>
public partial class FontIconButton : Button
{
    /*********
    ** Fields
    *********/
    /// <summary>The glyph in the button.</summary>
    public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(nameof(Glyph), typeof(char), typeof(FontIconButton));


    /*********
    ** Properties
    *********/
    /// <summary>The glyph in the button.</summary>
    public char Glyph
    {
        get => (char)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    public FontIconButton()
    {
        InitializeComponent();
    }
}
