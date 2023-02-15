namespace NovaEngine.Content.Attributes;

/// <summary>An aatribute used for specifying information about a content reader.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ContentReaderAttribute : Attribute
{
    /*********
    ** Accesors
    *********/
    /// <summary>The type of content the reader is for.</summary>
    /// <remarks>For example 'texture', this is used so an asset file can't be used with the wrong reader.</remarks>
    public string Type { get; }

    /// <summary>The types of valid output for the content reader.</summary>
    public Type[] OutputTypes { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="type">The type of content the reader is for.</param>
    /// <param name="outputTypes">The types of valid output for the content reader.</param>
    public ContentReaderAttribute(string type, params Type[] outputTypes)
    {
        Type = type;
        OutputTypes = outputTypes;
    }
}
