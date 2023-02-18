namespace NovaEngine.ContentPipeline.Packers;

/// <summary>Defines how a file should be rewritten for a nova file.</summary>
public interface IContentPacker
{
    /*********
    ** Properties
    *********/
    /// <summary>The type of content the packer is for.</summary>
    /// <remarks>This will be used to find a corresponding <see cref="IContentReader"/> or <see cref="IContentPacker"/>.</remarks>
    public string Type { get; }

    /// <summary>The extensions the content packer can handle.</summary>
    public string[] Extensions { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Rewrites a file to a <see cref="Stream"/> (the stream that will be written to the nova file).</summary>
    /// <param name="fileStream">The file stream of the file to pack.</param>
    /// <returns>The file packed to a <see cref="Stream"/>.</returns>
    public Stream Write(FileStream fileStream);
}
