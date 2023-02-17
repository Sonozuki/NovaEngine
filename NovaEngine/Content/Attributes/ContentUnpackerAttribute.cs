namespace NovaEngine.Content.Attributes;

/// <summary>An aatribute used for specifying information about a content unpacker.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContentUnpackerAttribute : Attribute
{
    /*********
    ** Properties
    *********/
    /// <summary>The type of content the unpacker is for.</summary>
    /// <remarks>For example 'texture', this is used so an asset file can't be used with the wrong unpacker.</remarks>
    public string Type { get; }

    /// <summary>The extension the content unpacker will output to.</summary>
    public string Extension { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="type">The type of content the unpacker is for.</param>
    /// <param name="extension">The extension the content unpacker will output to.</param>
    public ContentUnpackerAttribute(string type, string extension)
    {
        Type = type;
        Extension = extension;
    }
}
