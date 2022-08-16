namespace NovaEngine.Serialisation;

/// <summary>The binary serialiser.</summary>
public static class Serialiser
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Serialises an object to a byte array.</summary>
    /// <param name="object">The object to serialise to a byte array.</param>
    /// <returns><paramref name="object"/>, serialised as a byte array.</returns>
    public static byte[] Serialise(object @object)
    {
        var memoryStream = new MemoryStream();
        Serialise(memoryStream, @object);
        return memoryStream.ToArray();
    }

    /// <summary>Serialises an object to a stream.</summary>
    /// <param name="stream">The stream to serialise <paramref name="object"/> to.</param>
    /// <param name="object">The object to serialise to <paramref name="stream"/>.</param>
    public static void Serialise(Stream stream, object? @object)
    {
        try
        {
            using var binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true);
            binaryWriter.Write((byte)1); // version

            // check if the root object can be inlined
            TypeInfo? objectTypeInfo = null;
            var isRootValueInlinable = @object == null || (objectTypeInfo = new TypeInfo(@object.GetType())).IsInlinable;
            binaryWriter.Write(isRootValueInlinable);
            
            if (isRootValueInlinable)
                binaryWriter.Write(SerialiserUtilities.ConvertInlinableValueToBuffer(@object, objectTypeInfo!)); // ignoring that objectTypeInfo can be null, it's only null if @object is too (in which case it isn't used, so doesn't matter)
            else
            {
                // write all object infos to the stream
                var allObjectInfos = new List<ObjectInfo>();
                SerialiserUtilities.FlattenObject(@object!, allObjectInfos, new());

                binaryWriter.Write(allObjectInfos.Count);
                foreach (var objectInfo in allObjectInfos)
                    objectInfo.Write(binaryWriter);
            }
        }
        catch (Exception ex)
        {
            throw new SerialisationException("Error occured when serialising the object.", ex);
        }
    }

    /// <summary>Deserialises an object from a byte array.</summary>
    /// <typeparam name="T">The type of the object to deserialise.</typeparam>
    /// <param name="array">The byte array to deserialise the object from.</param>
    /// <returns>The deserialised object.</returns>
    public static T? Deserialise<T>(byte[] array) => Deserialise<T>(new MemoryStream(array));

    /// <summary>Deserialises an object from a byte array.</summary>
    /// <param name="array">The byte array to deserialise the object from.</param>
    /// <returns>The deserialised object.</returns>
    public static object? Deserialise(byte[] array) => Deserialise(new MemoryStream(array));

    /// <summary>Deserialises an object from a stream.</summary>
    /// <typeparam name="T">The type of the object to deserialise.</typeparam>
    /// <param name="stream">The stream to deserialise the object from.</param>
    /// <returns>The deserialised object.</returns>
    public static T? Deserialise<T>(Stream stream) => (T?)Deserialise(stream);

    /// <summary>Deserialises an object from a stream.</summary>
    /// <param name="stream">The stream to deserialise the object from.</param>
    /// <returns>The deserialised object.</returns>
    public static object? Deserialise(Stream stream)
    {
        try
        {
            using var binaryReader = new BinaryReader(stream);
            binaryReader.ReadByte(); // version (always 1)

            // check if the root object has been inlined
            if (binaryReader.ReadBoolean())
                return SerialiserUtilities.ReadInlinedValueFromStream(binaryReader);

            // reconstruct the non inlined object
            var allObjectInfos = new List<ObjectInfo>();
            var allTypeInfos = new TypeInfos();

            var count = binaryReader.ReadInt32();
            for (int i = 0; i < count; i++)
                allObjectInfos.Add(ObjectInfo.Read(binaryReader, allTypeInfos));

            // link references of and retrieve root object
            allObjectInfos[0].LinkReferences(allObjectInfos); // this will link all references of child objects as required

            // invoke OnDeserialised methods
            var onDeserialisedCallbacks = new List<(ObjectInfo ObjectInfo, MethodInfo MethodInfo)>();
            foreach (var objectInfo in allObjectInfos)
                foreach (var methodInfo in objectInfo.TypeInfo.SerialiserCallbacks.OnDeserialisedMethods)
                    onDeserialisedCallbacks.Add((objectInfo, methodInfo));

            foreach (var callback in onDeserialisedCallbacks.OrderBy(callback => callback.MethodInfo.GetCustomAttribute<OnDeserialisedAttribute>()!.Priority))
                try
                {
                    callback.MethodInfo.Invoke(callback.ObjectInfo.UnderlyingObject, null);
                }
                catch (Exception ex)
                {
                    Logger.LogError($"OnDeserialised callback crashed. Technical details:\n{ex}");
                }

            return allObjectInfos[0].UnderlyingObject;
        }
        catch (Exception ex)
        {
            throw new SerialisationException("Error occured when deserialising the object.", ex);
        }
    }
}
