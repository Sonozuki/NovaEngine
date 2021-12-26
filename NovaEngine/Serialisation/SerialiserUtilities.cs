namespace NovaEngine.Serialisation
{
    /// <summary>Contains useful methods for the serialiser.</summary>
    internal static class SerialiserUtilities
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Retrieves all the object infos that make up an object (including all recursive children).</summary>
        /// <param name="object">The object whose object info should be retrieved.</param>
        /// <param name="allObjectInfos">The collection to add all the object infos that make up <paramref name="object"/>. This is used to flatten circular references.</param>
        /// <param name="allTypeInfos">The collection to add all the cached type infos to, this includes inlinable types.</param>
        /// <returns>The id that uniquely identifies the object info that was created which represents <paramref name="object"/>.</returns>
        public static uint FlattenObject(object @object, List<ObjectInfo> allObjectInfos, TypeInfos allTypeInfos)
        {
            // get all serialisable members
            var type = @object.GetType();
            var typeInfo = allTypeInfos.Get(type);

            // create the object info representing the object
            var id = allObjectInfos.Count == 0 ? 1 : allObjectInfos.Last().Id + 1;
            var objectInfo = new ObjectInfo(id, @object, typeInfo);
            allObjectInfos.Add(objectInfo);

            // retrieve collection elements (if the object is an IEnumerable)
            if (@object is IEnumerable iEnumerable)
            {
                (objectInfo.CollectionType, objectInfo.AreCollectionElementsInlinable) = @object switch
                {
                    Array =>
                        (CollectionType.Array, @object.GetType().GetElementType()?.IsInlinable() ?? false),
                    object when @object?.GetType().GetInterfaces().Any(@interface => @interface.Name == "IList`1") ?? false =>
                        (CollectionType.GenericList, @object.GetType().GetInterfaces().First(@interface => @interface.Name == "IList`1").GetGenericArguments()[0].IsInlinable()),
                    object when @object?.GetType().GetInterfaces().Any(@interface => @interface.Name == "IDictionary`2") ?? false =>
                        (CollectionType.GenericDictionary, false),
                    object when @object?.GetType().GetInterfaces().Contains(typeof(IList)) ?? false =>
                        (CollectionType.List, false),
                    object when @object?.GetType().GetInterfaces().Contains(typeof(IDictionary)) ?? false =>
                        (CollectionType.Dictionary, false),
                    _ => throw new NotSupportedException($"Type: {@object!.GetType()} isn't a supported {nameof(IEnumerable)} type.")
                };

                if (objectInfo.AreCollectionElementsInlinable)
                    objectInfo.InlinableCollectionElements.AddRange(iEnumerable.OfType<object>().Select(element => SerialiserUtilities.ConvertInlinableValueToBuffer(element, allTypeInfos.Get(element.GetType()))));
                else
                    objectInfo.NonInlinableCollectionElements.AddRange(iEnumerable.OfType<object>().Select(element => GetObjectInfoIdByObject(element)));
            }

            // flatten/inline all serialisable member values
            foreach (var field in typeInfo.SerialisableFields)
            {
                var memberTypeInfo = allTypeInfos.Get(field.FieldType);
                var value = field.GetValue(@object);

                if (value == null || memberTypeInfo.IsInlinable)
                    objectInfo.InlinableFields[field.Name] = SerialiserUtilities.ConvertInlinableValueToBuffer(value, memberTypeInfo);
                else
                    objectInfo.NonInlinableFields[field.Name] = GetObjectInfoIdByObject(value);
            }

            foreach (var property in typeInfo.SerialisableProperties)
            {
                var memberTypeInfo = allTypeInfos.Get(property.PropertyType);
                var value = property.GetValue(@object);

                if (value == null || memberTypeInfo.IsInlinable)
                    objectInfo.InlinableProperties[property.Name] = SerialiserUtilities.ConvertInlinableValueToBuffer(value, memberTypeInfo);
                else
                    objectInfo.NonInlinableProperties[property.Name] = GetObjectInfoIdByObject(value);
            }

            return id;

            // Retrieves the id of an object info for a specified object
            uint GetObjectInfoIdByObject(object @object)
            {
                // check if an object info has already been created for the object
                var objectInfo = allObjectInfos.FirstOrDefault(objectInfo => object.ReferenceEquals(objectInfo.UnderlyingObject, @object));
                if (objectInfo != null)
                    return objectInfo.Id;
                else
                    return SerialiserUtilities.FlattenObject(@object, allObjectInfos, allTypeInfos);
            }
        }

        /// <summary>Converts an object which is inlinable to a buffer.</summary>
        /// <param name="value">The value to convert to a buffer.</param>
        /// <param name="typeInfo">The type info for the type of <paramref name="value"/>.</param>
        /// <returns>The byte array representation of <paramref name="value"/>.</returns>
        /// <remarks><paramref name="value"/> must be <see cref="TypeExtensions.IsInlinable(Type)"/>, it's the callers responibility to ensure that is the case.</remarks>
        public unsafe static byte[] ConvertInlinableValueToBuffer(object? value, TypeInfo typeInfo)
        {
            if (value == null)
                return new[] { (byte)0 }; // the first byte is zero if the value is null

            return value switch
            {
                bool @bool => CreateInlinedBuffer(InlinedValueType.Bool, BitConverter.GetBytes(@bool)),
                sbyte @sbyte => new[] { (byte)InlinedValueType.SByte, (byte)@sbyte },
                byte @byte => new[] { (byte)InlinedValueType.Byte, @byte },
                char @char => CreateInlinedBuffer(InlinedValueType.Char, BitConverter.GetBytes(@char)),
                short @short => CreateInlinedBuffer(InlinedValueType.Short, BitConverter.GetBytes(@short)),
                ushort @ushort => CreateInlinedBuffer(InlinedValueType.UShort, BitConverter.GetBytes(@ushort)),
                int @int => CreateInlinedBuffer(InlinedValueType.Int, BitConverter.GetBytes(@int)),
                uint @uint => CreateInlinedBuffer(InlinedValueType.UInt, BitConverter.GetBytes(@uint)),
                long @long => CreateInlinedBuffer(InlinedValueType.Long, BitConverter.GetBytes(@long)),
                ulong @ulong => CreateInlinedBuffer(InlinedValueType.ULong, BitConverter.GetBytes(@ulong)),
                float @float => CreateInlinedBuffer(InlinedValueType.Float, BitConverter.GetBytes(@float)),
                double @double => CreateInlinedBuffer(InlinedValueType.Double, BitConverter.GetBytes(@double)),
                decimal @decimal => CreateInlinedBuffer(InlinedValueType.Decimal, MemoryMarshal.Cast<int, byte>(decimal.GetBits(@decimal).AsSpan()).ToArray()),
                string @string => CreateInlinedBufferForString(@string),
                var @enum when value.GetType().IsEnum => CreateInlinedBufferForEnum(@enum),
                _ => CreateInlinedBufferForUnmanagedObject(value, typeInfo)
            };

            // Creates a buffer representing a type and type data
            byte[] CreateInlinedBuffer(InlinedValueType valueType, byte[] value)
            {
                var buffer = new byte[value.Length + 1];
                buffer[0] = (byte)valueType;
                Array.Copy(value, 0, buffer, 1, value.Length);
                return buffer;
            }

            // Creates a buffer representing a null-terminated string
            byte[] CreateInlinedBufferForString(string @string)
            {
                var stringBuffer = Encoding.UTF8.GetBytes(@string);

                var buffer = new byte[1 + stringBuffer.Length + 1];
                buffer[0] = (byte)InlinedValueType.String;
                Array.Copy(stringBuffer, 0, buffer, 1, stringBuffer.Length);
                return buffer;
            }

            // Creates a buffer representing an enum
            byte[] CreateInlinedBufferForEnum(object @enum)
            {
                var typeNameBuffer = Encoding.UTF8.GetBytes(@enum.GetType().FullName!);
                var enumValueBuffer = Encoding.UTF8.GetBytes(@enum.ToString()!);

                var buffer = new byte[1 + typeNameBuffer.Length + 1 + enumValueBuffer.Length + 1];
                buffer[0] = (byte)InlinedValueType.Enum;
                Array.Copy(typeNameBuffer, 0, buffer, 1, typeNameBuffer.Length);
                Array.Copy(enumValueBuffer, 0, buffer, 1 + typeNameBuffer.Length + 1, enumValueBuffer.Length);
                return buffer;
            }

            // Creates a buffer representing an unmanaged type
            byte[] CreateInlinedBufferForUnmanagedObject(object @unmanagedType, TypeInfo typeInfo)
            {
                var typeNameBuffer = Encoding.UTF8.GetBytes(typeInfo.Type.FullName!);
                var objectSize = Marshal.SizeOf(value);

                // populate buffer with object
                var buffer = new byte[1 + typeNameBuffer.Length + 1 + sizeof(int) + objectSize];
                buffer[0] = (byte)InlinedValueType.Unmanaged;
                fixed (byte* bufferPointer = buffer)
                {
                    var offset = 1;

                    // type name
                    Marshal.Copy(typeNameBuffer, 0, (IntPtr)bufferPointer + offset, typeNameBuffer.Length);
                    offset += typeNameBuffer.Length + 1;

                    // object size
                    Unsafe.CopyBlock(bufferPointer + offset, &objectSize, sizeof(int));
                    offset += sizeof(int);

                    // object data
                    Marshal.StructureToPtr(value!, (IntPtr)bufferPointer + offset, true);
                }
                return buffer;
            }
        }

        /// <summary>Reads an inlined value from a stream.</summary>
        /// <param name="binaryReader">The binary reader to use to read from the stream.</param>
        /// <returns>The inlined object.</returns>
        /// <exception cref="SerialisationException">Thrown if an invalid value type was specified in the stream.</exception>
        public unsafe static object? ReadInlinedValueFromStream(BinaryReader binaryReader)
        {
            var valueType = binaryReader.ReadByte();
            if (valueType == 0)
                return null;

            return valueType switch
            {
                (byte)InlinedValueType.Bool => binaryReader.ReadBoolean(),
                (byte)InlinedValueType.SByte => binaryReader.ReadSByte(),
                (byte)InlinedValueType.Byte => binaryReader.ReadByte(),
                (byte)InlinedValueType.Char => BitConverter.ToChar(binaryReader.ReadBytes(2)),
                (byte)InlinedValueType.Short => binaryReader.ReadInt16(),
                (byte)InlinedValueType.UShort => binaryReader.ReadUInt16(),
                (byte)InlinedValueType.Int => binaryReader.ReadInt32(),
                (byte)InlinedValueType.UInt => binaryReader.ReadUInt32(),
                (byte)InlinedValueType.Long => binaryReader.ReadInt64(),
                (byte)InlinedValueType.ULong => binaryReader.ReadUInt64(),
                (byte)InlinedValueType.Float => binaryReader.ReadSingle(),
                (byte)InlinedValueType.Double => binaryReader.ReadDouble(),
                (byte)InlinedValueType.Decimal => binaryReader.ReadDecimal(),
                (byte)InlinedValueType.String => ReadStringFromStream(),
                (byte)InlinedValueType.Enum => ReadEnumFromStream(),
                (byte)InlinedValueType.Unmanaged => ReadUnmanagedObjectFromStream(),
                _ => throw new SerialisationException("Invalid value type when reading inlined value.")
            };
            
            // Reads a null-terminated string from the stream
            string ReadStringFromStream()
            {
                var bytes = new List<byte>();

                var nextByte = (byte)0;
                while ((nextByte = binaryReader.ReadByte()) != 0)
                    bytes.Add(nextByte);

                return Encoding.UTF8.GetString(bytes.ToArray());
            }

            // Reads an enum from the stream
            object ReadEnumFromStream() => Enum.Parse(SerialiserUtilities.GetAnyType(ReadStringFromStream())!, ReadStringFromStream());

            // Reads an unmanaged type from the stream
            object ReadUnmanagedObjectFromStream()
            {
                var typeName = ReadStringFromStream();
                var objectSize = binaryReader.ReadInt32();
                var buffer = binaryReader.ReadBytes(objectSize);
                fixed (byte* bufferPointer = buffer)
                    return Marshal.PtrToStructure((IntPtr)bufferPointer, SerialiserUtilities.GetAnyType(typeName)!)!;
            }
        }

        /// <summary>Retrieves the type with a specified full name looking in all loaded assemblies.</summary>
        /// <param name="typeName">The name of the type to retrieve.</param>
        /// <returns>The type with a full name of <paramref name="typeName"/>.</returns>
        public static Type? GetAnyType(string typeName)
        {
            // try looking for the type in the engine dll and core library
            var type = Type.GetType(typeName);
            if (type != null)
                return type;

            // if the type is outside either of those, look in all loaded assemblies
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                    return type;
            }

            return null;
        }
    }
}
