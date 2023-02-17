namespace NovaEngine.Logging;

/// <summary>Represents a string in <see cref="InternalConsole"/>.</summary>
internal sealed class InternalConsoleString
{
    /*********
    ** Properties
    *********/
    /// <summary>The characters of the string.</summary>
    public List<InternalConsoleCharacter> Characters { get; }

    /// <summary>The length of the string.</summary>
    public int Length => Characters.Count;


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="characters">The characters of the string.</param>
    public InternalConsoleString(IEnumerable<InternalConsoleCharacter> characters)
    {
        Characters = characters.ToList();
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Splits the string on a character.</summary>
    /// <param name="char">The character to split the string on.</param>
    /// <returns>The string split on each instance of <paramref name="char"/>.</returns>
    public List<InternalConsoleString> Split(char @char)
    {
        var strings = new List<InternalConsoleString>();

        var currentString = new List<InternalConsoleCharacter>();
        foreach (var character in Characters)
        {
            if (character.Character == @char)
            {
                strings.Add(new(currentString));
                currentString = new();
            }
            else
                currentString.Add(character);
        }
        strings.Add(new(currentString));

        return strings;
    }


    /*********
    ** Operators
    *********/
    /// <summary>Concatenates two strings together.</summary>
    /// <param name="left">The left string.</param>
    /// <param name="right">The right string.</param>
    /// <returns>The result of the concatenation.</returns>
    public static InternalConsoleString operator +(InternalConsoleString left, InternalConsoleString right) => new(left.Characters.Concat(right.Characters));
}
