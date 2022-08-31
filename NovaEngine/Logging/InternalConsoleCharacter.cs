namespace NovaEngine.Logging;

/// <summary>Represents a character in an <see cref="InternalConsoleString"/>.</summary>
internal class InternalConsoleCharacter
{
    /*********
    ** Accessors
    *********/
    /// <summary>The underying character.</summary>
    public char Character { get; }

    /// <summary>The colour of the character.</summary>
    public Colour Colour { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="character">The underlying character.</param>
    /// <param name="colour">The colour of the character.</param>
    public InternalConsoleCharacter(char character, Colour colour)
    {
        Character = character;
        Colour = colour;
    }
}
