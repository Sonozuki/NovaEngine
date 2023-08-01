namespace NovaEditor.Options;

/// <summary>Indicates a member in a class with an <see cref="OptionsAttribute"/> is an option.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public sealed class OptionAttribute : Attribute
{
    /*********
    ** Properties
    *********/
    /// <summary>The text to use when displaying the option.</summary>
    /// <remarks>
    /// The location of the text depends on the type of the option, for example:<br/>
    /// If the option is a <see langword="bool"/>, then the text will be to the right of the checkbox.<br/>
    /// If the option is an <see cref="Enum"/>, then the text will be to the left of the combo box.
    /// </remarks>
    public string Text { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="text">The text to use when displaying the option.</param>
    public OptionAttribute(string text)
    {
        Text = text;
    }
}
