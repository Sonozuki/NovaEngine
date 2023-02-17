namespace NovaEngine.Content.Attributes;

/// <summary>An aatribute used for specifying information about a content packer.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ContentPackerAttribute : Attribute
{
    /*********
    ** Properties
    *********/
    /// <summary>The type of content the packer is for.</summary>
    /// <remarks>For example 'texture', this is used so an asset file can't be used with the wrong reader.</remarks>
    public string Type { get; }

    /// <summary>The extensions the content packer can handle.</summary>
    public string[] Extensions { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="type">The type of content the packer is for.</param>
    /// <param name="extensions">The extensions the content packer can handle.</param>
    public ContentPackerAttribute(string type, params string[] extensions)
    {
        Type = type;
        Extensions = extensions;
    }
}
