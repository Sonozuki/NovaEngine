using System.IO;

namespace NovaEngine.ContentPipeline;

/// <summary>Interacts with the content pipeline to pack, read, and unpack data files.</summary>
public static class Content
{
    /*********
    ** Fields
    *********/
    /// <summary>The loaded content readers.</summary>
    private static readonly List<IContentReader> LoadedContentReaders = new();

    /// <summary>The loaded content packers.</summary>
    private static readonly List<IContentPacker> LoadedContentPackers = new();

    /// <summary>The loaded content unpackers.</summary>
    private static readonly List<IContentUnpacker> LoadedContentUnpackers = new();


    /*********
    ** Public Methods
    *********/
    /// <summary>Initialises the class.</summary>
    static Content()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract);

        foreach (var type in types)
        {
            if (type.IsAssignableTo(typeof(IContentReader)))
                if (TryCreatePipelineObjectInstance<IContentReader>(type, out var contentReader))
                    LoadedContentReaders.Add(contentReader!);

            if (type.IsAssignableTo(typeof(IContentPacker)))
                if (TryCreatePipelineObjectInstance<IContentPacker>(type, out var contentReader))
                    LoadedContentPackers.Add(contentReader!);

            if (type.IsAssignableTo(typeof(IContentUnpacker)))
                if (TryCreatePipelineObjectInstance<IContentUnpacker>(type, out var contentReader))
                    LoadedContentUnpackers.Add(contentReader!);
        }

        // Tries to create an instance of a specified type.
        static bool TryCreatePipelineObjectInstance<T>(Type type, out T? pipelineObject)
            where T : class
        {
            pipelineObject = null;

            try
            {
                pipelineObject = (T?)Activator.CreateInstance(type);
                if (pipelineObject == null)
                {
                    new ContentException($"Failed to create an instance of '{typeof(T)}' for the content pipeline.").Log(LogSeverity.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                new ContentException($"Failed to create an instance of '{typeof(T)}' for the content pipeline.", ex).Log(LogSeverity.Error);
                return false;
            }

            return true;
        }
    }

    /// <summary>Loads content from a file to a specified type.</summary>
    /// <typeparam name="T">The return type to load the file to.</typeparam>
    /// <param name="path">The relative path to the file to load.</param>
    /// <returns>The loaded file.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="path"/> is <see langword="null"/> or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> doesn't exist.</exception>
    /// <exception cref="ContentException">Thrown if a content reader for <typeparamref name="T"/> couldn't be found, or if the file was invalid.</exception>
    public static T Load<T>(string path) => (T)Load(path, typeof(T));

    /// <summary>Loads content from a file to a specified type.</summary>
    /// <param name="path">The relative path to the fileto load.</param>
    /// <param name="returnType">The return type to load the file to.</param>
    /// <returns>The loaded file.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="path"/> is <see langword="null"/> or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="returnType"/> is <see langword="null"/>.</exception>
    /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> doesn't exist.</exception>
    /// <exception cref="ContentException">Thrown if a content reader for <paramref name="returnType"/> couldn't be found, or if the file couldn't be opened, or if the file was invalid.</exception>
    public static object Load(string path, Type returnType)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentNullException.ThrowIfNull(returnType);

        path = Path.Combine(Constants.ContentDirectory, path);

        var file = path;
        if (string.IsNullOrEmpty(new FileInfo(file).Extension))
            file += Constants.ContentFileExtension;

        using var fileStream = OpenFile(file);
        if (!ReadHeader(fileStream, out var contentType))
            throw new ContentException($"'{file}' is not a valid nova file.");

        try
        {
            if (contentType == "serialised")
                return Serialiser.Deserialise(fileStream)!;
            else
            {
                var contentReader = GetContentReader(returnType, contentType)
                    ?? throw new ContentException($"Cannot find content reader for object type '{returnType}' and content type '{contentType}'.");
                
                return contentReader.Read(fileStream, returnType)
                    ?? throw new ContentException("Content reader returned null.");
            }
        }
        catch (Exception ex)
        {
            throw new ContentException("Failed to read content from file.", ex);
        }
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Attempts to open a file.</summary>
    /// <param name="file">The file to try and open.</param>
    /// <returns>The file stream of the file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file couldn't be found.</exception>
    /// <exception cref="ContentException">Thrown if the file could be found but couldn't be opened.</exception>
    private static Stream OpenFile(string file)
    {
        try
        {
            return File.OpenRead(file);
        }
        catch (FileNotFoundException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new ContentException("Failed to open file.", ex);
        }
    }

    /// <summary>Reads the header of a nova file.</summary>
    /// <param name="fileStream">The stream to a nova file to read the header of.</param>
    /// <param name="contentType">The type of content in the stream.</param>
    /// <returns><see langword="true"/>, if the header is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ReadHeader(Stream fileStream, out string contentType)
    {
        contentType = string.Empty;

        using var binaryReader = new BinaryReader(fileStream, Encoding.UTF8, leaveOpen: true);

        var n = binaryReader.ReadByte();
        var o = binaryReader.ReadByte();
        var v = binaryReader.ReadByte();
        var a = binaryReader.ReadByte();
        if (n != 'N' || o != 'O' || v != 'V' || a != 'A')
            return false;

        var version = binaryReader.ReadByte();
        if (version != 1)
            return false;

        contentType = binaryReader.ReadString();
        return true;
    }

    /// <summary>Retrieves an <see cref="IContentReader"/> for a specified object type and content type.</summary>
    /// <param name="returnType">The type the content reader must return.</param>
    /// <param name="contentType">The type of content file the reader must handle.</param>
    /// <returns>The content reader for <paramref name="returnType"/> and content type for <paramref name="contentType"/>, if one exists; otherwise, <see langword="null"/>.</returns>
    private static IContentReader? GetContentReader(Type returnType, string contentType)
    {
        foreach (var contentReader in LoadedContentReaders)
            if (contentReader.Type == contentType && contentReader.OutputTypes.Contains(returnType))
                return contentReader;

        return null;
    }
}
