﻿using System.IO;

namespace NovaEngine.Content.Unpackers
{
    /// <summary>Defines how a nova file should be rewritten for a regular file.</summary>
    public interface IContentUnpacker
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The type of content the unpacker is for.</summary>
        /// <remarks>For example 'texture', this is used so an asset file can't be used with the wrong unpacker.</remarks>
        public string Type { get; }

        /// <summary>The extension the content unpacker will output to.</summary>
        public string Extension { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Writes the a packed stream to an unpacked file.</summary>
        /// <param name="stream">The packed stream to convert.</param>
        /// <param name="destinationFile">The file that should be created/overwrote with the unpacked content.</param>
        public void Write(Stream stream, string destinationFile);
    }
}
