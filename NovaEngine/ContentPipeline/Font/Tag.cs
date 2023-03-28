namespace NovaEngine.ContentPipeline.Font;

/// <summary>Represents a 4-byte tag.</summary>
internal sealed class Tag
{
    /*********
    ** Properties
    *********/
    /// <summary>The value of the tag.</summary>
    public string Value { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="char1">The first character of the tag.</param>
    /// <param name="char2">The second character of the tag.</param>
    /// <param name="char3">The third character of the tag.</param>
    /// <param name="char4">The fourth character of the tag.</param>
    public Tag(byte char1, byte char2, byte char3, byte char4)
    {
        Value = Encoding.UTF8.GetString(new[] { char1, char2, char3, char4 });
    }


    /*********
    ** Public Methods
    *********/
    /// <inheritdoc/>
    public override string ToString() => Value;


    /*********
    ** Operators
    *********/
    /// <summary>Converts a tag to a string.</summary>
    /// <param name="tag">The tag to convert.</param>
    public static implicit operator string(Tag tag) => tag.Value;
}
