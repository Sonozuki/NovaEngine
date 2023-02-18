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

    /// <summary>Packs content from a non-nova file to a nova file.</summary>
    /// <param name="fileToPack">The non-nova file to pack into a nova file.</param>
    /// <param name="destinationFile">The nova file to create/overwrite.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="fileToPack"/> or <paramref name="destinationFile"/> is <see langword="null"/> or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown if <paramref name="fileToPack"/> doesn't exist.</exception>
    /// <exception cref="ContentException">Thrown if <paramref name="destinationFile"/> couldn't be created or if the content packer failed to pack the file.</exception>
    public static void Pack(string fileToPack, string destinationFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileToPack);
        ArgumentException.ThrowIfNullOrEmpty(destinationFile);

        using var fileStream = OpenFile(fileToPack);
        var extension = new FileInfo(fileToPack).Extension.TrimStart('.');

        if (string.IsNullOrEmpty(new FileInfo(destinationFile).Extension))
            destinationFile = Path.ChangeExtension(destinationFile, ".nova");
        using var novaFileStream = CreateFile(destinationFile);

        try
        {
            var contentPacker = GetContentPacker(extension)
                ?? throw new ContentException($"Cannot find content packer for extension '{extension}'.");

            var contentStream = contentPacker.Write(fileStream);
            contentStream.Position = 0;
            
            WriteHeader(novaFileStream, contentPacker.Type);
            contentStream.CopyTo(novaFileStream);
        }
        catch (Exception ex)
        {
            throw new ContentException("Failed to pack content.", ex);
        }
    }

    /// <summary>Packs content from an object to a nova file.</summary>
    /// <typeparam name="T">The type of object to serialise into the nova file.</typeparam>
    /// <param name="value">The object to serialise into the nova file.</param>
    /// <param name="destinationFile">The nova file to create/overwrite.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="destinationFile"/> is <see langword="null"/> or empty.</exception>
    /// <exception cref="ContentException">Thrown if <paramref name="destinationFile"/> couldn't be created or if the serialiser failed to pack the file.</exception>
    public static void Pack<T>(T value, string destinationFile)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentException.ThrowIfNullOrEmpty(destinationFile);

        if (string.IsNullOrEmpty(new FileInfo(destinationFile).Extension))
            destinationFile = Path.ChangeExtension(destinationFile, ".nova");
        using var novaFileStream = CreateFile(destinationFile);

        try
        {
            WriteHeader(novaFileStream, "serialised");
            Serialiser.Serialise(novaFileStream, value);
        }
        catch (Exception ex)
        {
            throw new ContentException("Failed to pack content.", ex);
        }
    }

    /// <summary>Unpacks content from a nova file to a non-nova file.</summary>
    /// <param name="fileToUnpack">The nova file to unpack.</param>
    /// <param name="destinationFile">The non-nova file to unpack to.<br/>If an extension is specified then only an unpacker that outputs to that extension will be selected; otherwise, any unpacker that is able to parse the nova file contents according to the header will be used.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="fileToUnpack"/> or <paramref name="destinationFile"/> is <see langword="null"/> or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown if <paramref name="fileToUnpack"/> doesn't exist.</exception>
    /// <exception cref="ContentException">Thrown if <paramref name="fileToUnpack"/> couldn't be opened, or if <paramref name="destinationFile"/> couldn't be created, or if an unpacker couldn't be found, or if the unpacker failed to unpack the file.</exception>
    public static void Unpack(string fileToUnpack, string destinationFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileToUnpack);
        ArgumentException.ThrowIfNullOrEmpty(destinationFile);

        using var fileToUnpackStream = OpenFile(fileToUnpack);
        using var destinationFileStream = CreateFile(destinationFile);

        if (!ReadHeader(fileToUnpackStream, out var contentType))
            throw new ContentException($"'{fileToUnpack}' is not a valid nova file.");

        var extension = GetExtension(destinationFile);

        try
        {
            var contentUnpacker = GetContentUnpacker(contentType, extension) ??
                throw new ContentException($"Cannot find content unpacker for content type '{contentType}'{(string.IsNullOrEmpty(extension) ? "" : $" and extension '{extension}'")}.");

            contentUnpacker.Write(fileToUnpackStream, destinationFileStream);
        }
        catch (Exception ex)
        {
            throw new ContentException("Failed to pack content.", ex);
        }
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Retrieves the file extension of a file.</summary>
    /// <param name="file">The file to retrieve the extension of.</param>
    /// <returns>The extension of the file, without the leading '.'.</returns>
    /// <exception cref="ContentException">Thrown if the extension couldn't be retrieved.</exception>
    private static string GetExtension(string file)
    {
        try
        {
            return new FileInfo(file).Extension.TrimStart('.');
        }
        catch (Exception ex)
        {
            throw new ContentException($"Failed to get file extension of '{file}'.", ex);
        }
    }

    /// <summary>Attempts to open a file.</summary>
    /// <param name="file">The file to try and open.</param>
    /// <returns>The file stream of the file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file couldn't be found.</exception>
    /// <exception cref="ContentException">Thrown if the file could be found but couldn't be opened.</exception>
    private static FileStream OpenFile(string file)
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

    /// <summary>Attempts to create/overwrite a file.</summary>
    /// <param name="file">The file to try and create.</param>
    /// <returns>The file stream of the file.</returns>
    /// <exception cref="ContentException">Thrown if the file or containing directory couldn't be created.</exception>
    private static FileStream CreateFile(string file)
    {
        var directory = Path.GetDirectoryName(file) ?? throw new ContentException($"Couldn't retrieve directory of path '{file}'.");

        try
        {
            Directory.CreateDirectory(directory);
            return File.Create(file);
        }
        catch (Exception ex)
        {
            throw new ContentException("Failed to create file.", ex);
        }
    }

    /// <summary>Reads the header of a nova file.</summary>
    /// <param name="fileStream">The stream to a nova file to read the header of.</param>
    /// <param name="contentType">The type of content in the stream.</param>
    /// <returns><see langword="true"/>, if the header is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ReadHeader(FileStream fileStream, out string contentType)
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

    /// <summary>Writes a nova file header to a stream.</summary>
    /// <param name="fileStream">The file stream to write the header to.</param>
    /// <param name="contentType">The content type of the nova file.</param>
    private static void WriteHeader(FileStream fileStream, string contentType)
    {
        using var binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8, leaveOpen: true);

        binaryWriter.Write("NOVA"u8);
        binaryWriter.Write((byte)1);
        binaryWriter.Write(contentType);
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

    /// <summary>Retrieves an <see cref="IContentPacker"/> for a specified file extension.</summary>
    /// <param name="extension">The file extension the content packer must be able to handle.</param>
    /// <returns>The content packer for <paramref name="extension"/>, if one exists; otherwise, <see langword="null"/>.</returns>
    private static IContentPacker? GetContentPacker(string extension)
    {
        extension = extension.ToLower(G11n.Culture);
        foreach (var contentPacker in LoadedContentPackers)
            if (contentPacker.Extensions.Any(packerExtension => packerExtension.ToLower(G11n.Culture) == extension))
                return contentPacker;

        return null;
    }

    /// <summary>Retrieves an <see cref="IContentUnpacker"/> for a specified content type and file extension.</summary>
    /// <param name="contentType">The type of content the unpacker must handle.</param>
    /// <param name="extension">The file extension the unpacker should output as, or an empty string for any extension.</param>
    /// <returns>The content unpacker for <paramref name="extension"/> and content type for <paramref name="contentType"/>, if one exists; otherwise, <see langword="null"/>.</returns>
    private static IContentUnpacker? GetContentUnpacker(string contentType, string extension)
    {
        extension = extension.ToLower(G11n.Culture);
        foreach (var contentUnpacker in LoadedContentUnpackers)
            if (contentUnpacker.Type == contentType && (string.IsNullOrEmpty(extension) || contentUnpacker.Extension.ToLower(G11n.Culture) == extension))
                return contentUnpacker;

        return null;
    }
}
