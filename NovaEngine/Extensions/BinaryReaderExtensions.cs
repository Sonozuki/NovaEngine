namespace NovaEngine.Extensions
{
    /// <summary>Extension methods for the <see cref="BinaryReader"/> class.</summary>
    internal static class BinaryReaderExtensions
    {
        /*********
        ** Internal Methods
        *********/
        /// <summary>Writes a collection of inlinable object buffers to the current stream and advances the position.</summary>
        /// <param name="binaryReader">The binary reader to read from.</param>
        /// <returns>The read inlinable object buffers.</returns>
        internal static List<object?> ReadInlinableObjects(this BinaryReader binaryReader)
        {
            var list = new List<object?>();

            var count = binaryReader.ReadUInt16();
            for (int i = 0; i < count; i++)
                list.Add(SerialiserUtilities.ReadInlinedValueFromStream(binaryReader));

            return list;
        }

        /// <summary>Writes a collection of non inlinable object ids to the current stream and advances the position.</summary>
        /// <param name="binaryReader">The binary reader to read from.</param>
        /// <returns>The read non inlinable object ids.</returns>
        internal static List<uint> ReadNonInlinableObjects(this BinaryReader binaryReader)
        {
            var list = new List<uint>();

            var count = binaryReader.ReadUInt16();
            for (int i = 0; i < count; i++)
                list.Add(binaryReader.ReadUInt32());

            return list;
        }

        /// <summary>Writes a collection of inlinable members to the current stream and advances the position.</summary>
        /// <param name="binaryReader">The binary reader to read from.</param>
        /// <returns>The read inlinable members.</returns>
        internal static Dictionary<string, object?> ReadInlinableMembers(this BinaryReader binaryReader)
        {
            var dictionary = new Dictionary<string, object?>();
            
            var count = binaryReader.ReadUInt16();
            for (int i = 0; i < count; i++)
                dictionary[binaryReader.ReadString()] = SerialiserUtilities.ReadInlinedValueFromStream(binaryReader);

            return dictionary;
        }

        /// <summary>Writes a collection of non inlinable members to the current stream and advances the position.</summary>
        /// <param name="binaryReader">The binary reader to read from.</param>
        /// <returns>The read non inlinable members.</returns>
        internal static Dictionary<string, uint> ReadNonInlinableMembers(this BinaryReader binaryReader)
        {
            var dictionary = new Dictionary<string, uint>();

            var count = binaryReader.ReadUInt16();
            for (int i = 0; i < count; i++)
                dictionary[binaryReader.ReadString()] = binaryReader.ReadUInt32();

            return dictionary;
        }
    }
}
