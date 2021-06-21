using NovaEngine.Content.Attributes;
using NovaEngine.Content.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NovaEngine.Content
{
    /// <summary>Handles content loading.</summary>
    public class ContentLoader
    {
        /*********
        ** Fields
        *********/
        /// <summary>The loaded content readers.</summary>
        private static readonly List<IContentReader> ContentReaders = new();

        
        /*********
        ** Accessors
        *********/
        /// <summary>The root content directory.</summary>
        public static string RootContentDirectory => Path.Combine(Environment.CurrentDirectory, "Data");

        /// <summary>The file extension for content files.</summary>
        public static string ContentFileExtension => ".nova";


        /*********
        ** Public Methods
        *********/
        /// <summary>Initialises the class.</summary>
        static ContentLoader()
        {
            // load all content pipeline types from assemblies
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract);

            foreach (var type in types)
            {
                if (!type.GetInterfaces().Contains(typeof(IContentReader)))
                    continue;

                // ensure the attribute exists
                if (!type.GetCustomAttributes(false).Any(attribute => attribute.GetType() == typeof(ContentReaderAttribute)))
                {
                    Console.WriteLine($"ContentReader: {type.FullName} doesn't have a {nameof(ContentReaderAttribute)}");
                    continue;
                }

                // create reader
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
        /// <exception cref="ContentException">Thrown if a content reader for <typeparamref name="T"/> couldn't be found, or if the file was invalid.</exception>
        public static object Load(string path, Type returnType)
        {
            // convert path from relative to absolute
            path = Path.Combine(RootContentDirectory, path);

            // add file extension if needed
            var file = path;
            if (string.IsNullOrEmpty(new FileInfo(file).Extension))
                file += ContentFileExtension;

            // ensure file exists
            if (!File.Exists(file))
                throw new FileNotFoundException($"Cannot find file: {file}.");

            // create a file content stream and read an object from it
            using (var stream = GetFileContentStream(file, out var contentType))
            {
                // find a valid content reader
                var contentReader = GetContentReader(returnType, contentType);
                if (contentReader == null)
                    throw new ContentException($"Cannot find content reader for object type: {returnType.FullName} and content type: {contentType}.");

                try
                {
                    var @object = contentReader.Read(stream, returnType);
                    if (@object == null)
                        throw new ContentException("Cotent reader returned null.");

                    return @object;
                }
                catch (Exception ex)
                {
                    throw new ContentException("Failed to read content from file.", ex);
                }
            }
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
            // ensure file exists
            if (!File.Exists(file))
                throw new FileNotFoundException($"Cannot find file: {file}.");

            // create binary reader for file
            using var fileStream = File.OpenRead(file);
            using var binaryReader = new BinaryReader(fileStream);

            // validate file
            var n = binaryReader.ReadByte();
            var o = binaryReader.ReadByte();
            var v = binaryReader.ReadByte();
            var a = binaryReader.ReadByte();

            if (n != 'N' || o != 'O' || v != 'V' || a != 'A')
                throw new ContentException($"{file} isn't a valid nova file.");

            // read header
            var version = binaryReader.ReadByte(); // unused for now, should always be 1
            contentType = binaryReader.ReadString();

            // create content stream
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
}
