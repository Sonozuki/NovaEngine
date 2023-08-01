namespace NovaEditor.Options;

/// <summary>Represents an option.</summary>
public class Option
{
    /*********
    ** Properties
    *********/
    /// <summary>The type of the value of the option.</summary>
    public Type Type { get; }

    /// <summary>The text to use when displaying the option.</summary>
    public string Text { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="type">The type of the value of the option.</param>
    /// <param name="text">The text to use when displaying the option.</param>
    public Option(Type type, string text)
    {
        Type = type;
        Text = text;
    }
}
