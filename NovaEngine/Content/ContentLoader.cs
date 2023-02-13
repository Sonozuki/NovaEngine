namespace NovaEngine.Content;

/// <summary>Handles content loading.</summary>
public static class ContentLoader
{
    /*********
    ** Fields
    *********/
    /// <summary>The loaded content readers.</summary>
    private static readonly List<IContentReader> ContentReaders = new();


    /*********
    ** Public Methods
    *********/
    /// <summary>Initialises the class.</summary>
    static ContentLoader()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract);

        foreach (var type in types)
        {
            if (!type.IsAssignableTo(typeof(IContentReader)))
                continue;

            if (!type.GetCustomAttributes(false).Any(attribute => attribute.GetType() == typeof(ContentReaderAttribute)))
            {
                Logger.LogError($"ContentReader: {type.FullName} doesn't have a {nameof(ContentReaderAttribute)}");
                continue;
            }

            var reader = Activator.CreateInstance(type);
            if (reader != null)
                ContentReaders.Add((IContentReader)reader);
        }
    }

    /// <summary>Loads content from a file to a specified type.</summary>
    /// <typeparam name="T">The return type to load the file to.</typeparam>
    /// <param name="path">The relative path to the file to load.</param>
    /// <returns>The loaded file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> doesn't exist.</exception>
    /// <exception cref="ContentException">Thrown if a content reader for <typeparamref name="T"/> couldn't be found, or if the file was invalid.</exception>
    public static T Load<T>(string path) => (T)Load(path, typeof(T));

    /// <summary>Loads content from a file to a specified type.</summary>
    /// <param name="path">The relative path to the fileto load.</param>
    /// <param name="returnType">The return type to load the file to.</param>
    /// <returns>The loaded file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> doesn't exist.</exception>
    /// <exception cref="ContentException">Thrown if a content reader for <paramref name="returnType"/> couldn't be found, or if the file was invalid.</exception>
    public static object Load(string path, Type returnType)
    {
        path = Path.Combine(Constants.ContentDirectory, path);

        var file = path;
        if (string.IsNullOrEmpty(new FileInfo(file).Extension))
            file += Constants.ContentFileExtension;

        if (!File.Exists(file))
            throw new FileNotFoundException($"Cannot find file: {file}.");

        using var stream = GetFileContentStream(file, out var contentType);
        object? readObject = null;

        try
        {
            if (contentType.ToLower() == "serialised")
                readObject = Serialiser.Deserialise(stream);
            else
            {
                var contentReader = GetContentReader(returnType, contentType);
                if (contentReader == null)
                    throw new ContentException($"Cannot find content reader for object type: {returnType.FullName} and content type: {contentType}.");

                readObject = contentReader.Read(stream, returnType);
            }
        }
        catch (Exception ex)
        {
            throw new ContentException("Failed to read content from file.", ex);
        }

        return readObject ?? throw new ContentException("Content reader returned null.");
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Gets a stream to the content portion of a nova file.</summary>
    /// <param name="file">The file to get the content stream of.</param>
    /// <param name="contentType">The type of content in the stream.</param>
    /// <returns>A stream for the content portion of the file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if <paramref name="file"/> doesn't exist.</exception>
    /// <exception cref="ContentException">Thrown if <paramref name="file"/> doesn't have a valid header.</exception>
    private static Stream GetFileContentStream(string file, out string contentType)
    {
        if (!File.Exists(file))
            throw new FileNotFoundException($"Cannot find file: {file}.");

        using var fileStream = File.OpenRead(file);
        using var binaryReader = new BinaryReader(fileStream);

        var n = binaryReader.ReadByte();
        var o = binaryReader.ReadByte();
        var v = binaryReader.ReadByte();
        var a = binaryReader.ReadByte();

        if (n != 'N' || o != 'O' || v != 'V' || a != 'A')
            throw new ContentException($"{file} isn't a valid nova file.");

        var version = binaryReader.ReadByte(); // unused for now, should always be 1
        contentType = binaryReader.ReadString();

        var stream = new MemoryStream();
        binaryReader.BaseStream.CopyTo(stream);
        stream.Position = 0;
        return stream;
    }

    /// <summary>Gets an <see cref="IContentReader"/> for a specified object type and content type.</summary>
    /// <param name="returnType">The type of object the content reader must return.</param>
    /// <param name="contentType">The type of content file the reader must handle.</param>
    /// <returns>The content reader for <paramref name="returnType"/> and content type for <paramref name="contentType"/>, if one exists; otherwise, <see langword="null"/>.</returns>
    private static IContentReader? GetContentReader(Type returnType, string contentType)
    {
        foreach (var contentReader in ContentReaders)
        {
            var attribute = contentReader.GetType()
                .GetCustomAttributes(false)
                .First(attribute => attribute.GetType() == typeof(ContentReaderAttribute));
            if (attribute is not ContentReaderAttribute contentReaderAttribute)
                continue;

            if (contentReaderAttribute.Type.ToLower() == contentType.ToLower() && contentReaderAttribute.OutputTypes.Contains(returnType))
                return contentReader;
        }

        return null;
    }
}
