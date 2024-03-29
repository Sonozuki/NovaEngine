﻿namespace NovaEngine.ContentPipeline.Unpackers;

/// <summary>Defines how a nova file should be rewritten for a regular file.</summary>
public interface IContentUnpacker
{
    /*********
    ** Properties
    *********/
    /// <summary>The type of content the unpacker is for.</summary>
    /// <remarks>This must be identical to the type specified in the corresponding <see cref="IContentPacker"/>.</remarks>
    public string Type { get; }

    /// <summary>The extension the content unpacker will output to.</summary>
    public string Extension { get; }


    /*********
    ** Public Methods
    *********/
    /// <summary>Writes the a packed stream to an unpacked file.</summary>
    /// <param name="novaFileStream">The file stream of the packed nova file to unpack.</param>
    /// <param name="destinationFileStream">The file stream of the destination file to write the unpacked data to.</param>
    public void Write(FileStream novaFileStream, FileStream destinationFileStream);
}
