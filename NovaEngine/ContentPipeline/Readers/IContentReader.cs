namespace NovaEngine.ContentPipeline.Readers;

/// <summary>Defines how content should be read from a nova file.</summary>
public interface IContentReader
{
    /*********
    ** Accesors
    *********/
    /// <summary>The type of content the reader is for.</summary>
    /// <remarks>This must be identical to the type specified in the corresponding <see cref="IContentPacker"/>.</remarks>
    public string Type { get; }

    /// <summary>The types of valid output for the content reader.</summary>
    public Type[] OutputTypes { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Reads content from a <see cref="Stream"/>.</summary>
    /// <param name="novaFileStream">The fille stream of the nova file to read the content from.</param>
    /// <param name="outputType">The type to output. This will always be one of the types specified in <see cref="OutputTypes"/>.</param>
    /// <returns>The content read from the stream.</returns>
    public object? Read(FileStream novaFileStream, Type outputType);
}
