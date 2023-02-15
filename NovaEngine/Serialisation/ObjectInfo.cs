namespace NovaEngine.Serialisation;

/// <summary>Represents the info about an object.</summary>
internal class ObjectInfo
{
    /*********
    ** Fields
    *********/
    /// <summary>Whether the non inlinable members and collection elements have had their references linked.</summary>
    private bool HaveReferencesBeenLinked;

    /// <summary>The deserialised elements of the object.</summary>
    /// <remarks>This is used to store all deserialised collection elements before they are added to the array (after all references have been linked).</remarks>
    private List<object?> CollectionElements { get; } = new();


    /*********
    ** Properties
    *********/
    /// <summary>The unique id representing the object.</summary>
    public uint Id { get; }

    /// <summary>The object this object info represents.</summary>
    public object UnderlyingObject { get; private set; }

    /// <summary>The type info of the object this represents.</summary>
    public TypeInfo TypeInfo { get; }

    /// <summary>If the object is a array, this will specify whether the elements are inlinable or non inlinable (whether <see cref="InlinableCollectionElements"/> or <see cref="NonInlinableCollectionElements"/>) stores the elements).</summary>
    public bool AreCollectionElementsInlinable { get; set; }

    /// <summary>The elements of the object, when it's an <see cref="Array"/> of inlinable objects.</summary>
    /// <remarks>This and <see cref="NonInlinableCollectionElements"/> will not ever be used at the same time.</remarks>
    public List<byte[]> InlinableCollectionElements { get; } = new();

    /// <summary>The elements of the object, when it's an <see cref="Array"/> of non inlinable objects.</summary>
    /// <remarks>This and <see cref="InlinableCollectionElements"/> will not ever be used at the same time.</remarks>
    public List<uint> NonInlinableCollectionElements { get; } = new();

    /// <summary>The fields of the object whose value can be inlined.</summary>
    /// <remarks>This is the byte representation of the field value.</remarks>
    public Dictionary<string, byte[]> InlinableFields { get; } = new();

    /// <summary>The properties of the object whose value can be inlined.</summary>
    /// <remarks>This is the byte representation of the property value.</remarks>
    public Dictionary<string, byte[]> InlinableProperties { get; } = new();

    /// <summary>The fields of the object whose value can not be inlined.</summary>
    /// <remarks>This is the unique id of the object info of the field value.</remarks>
    public Dictionary<string, uint> NonInlinableFields { get; set; } = new();

    /// <summary>The properties of the object whose value can not be inlined.</summary>
    /// <remarks>This is the unique id of the object info of the property value.</remarks>
    public Dictionary<string, uint> NonInlinableProperties { get; set; } = new();


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="id">The unique id representing the object.</param>
    /// <param name="underlyingObject">The object this object info represents.</param>
    /// <param name="typeInfo">The type info of the object this represents.</param>
    public ObjectInfo(uint id, object underlyingObject, TypeInfo typeInfo)
    {
        Id = id;
        UnderlyingObject = underlyingObject;
        TypeInfo = typeInfo;
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Links the references of non inlinable objects, recursively.</summary>
    /// <param name="allObjectInfos">All the deserialised object infos.</param>
    public void LinkReferences(List<ObjectInfo> allObjectInfos)
    {
        if (HaveReferencesBeenLinked)
            return;
        HaveReferencesBeenLinked = true;

        // link references of fields then collection elements
        foreach (var field in NonInlinableFields)
        {
            var objectInfo = allObjectInfos.First(objectInfo => objectInfo.Id == field.Value);
            objectInfo.LinkReferences(allObjectInfos);

            TypeInfo.SerialisableFields.First(f => f.Name == field.Key)
                .SetValue(UnderlyingObject, objectInfo.UnderlyingObject);
        }

        foreach (var property in NonInlinableProperties)
        {
            var objectInfo = allObjectInfos.First(objectInfo => objectInfo.Id == property.Value);
            objectInfo.LinkReferences(allObjectInfos);

            TypeInfo.SerialisableProperties.First(p => p.Name == property.Key)
                .SetValue(UnderlyingObject, objectInfo.UnderlyingObject);
        }

        foreach (var element in NonInlinableCollectionElements)
        {
            var objectInfo = allObjectInfos.First(objectInfo => objectInfo.Id == element);
            objectInfo.LinkReferences(allObjectInfos);

            CollectionElements.Add(objectInfo.UnderlyingObject);
        }
        AssignCollectionElements();
    }

    /// <summary>Writes the object info to a stream.</summary>
    /// <param name="binaryWriter">The binary writer to write the object to.</param>
    public void Write(BinaryWriter binaryWriter)
    {
        binaryWriter.Write(TypeInfo.Type.FullName!);

        var isArray = UnderlyingObject is Array;
        binaryWriter.Write(isArray);
        if (isArray)
            binaryWriter.Write((UnderlyingObject as Array)!.Length);

        binaryWriter.Write(Id);

        binaryWriter.Write(InlinableFields);
        binaryWriter.Write(InlinableProperties);
        binaryWriter.Write(NonInlinableFields);
        binaryWriter.Write(NonInlinableProperties);

        var hasCollectionElements = InlinableCollectionElements.Count != 0 || NonInlinableCollectionElements.Count != 0;
        binaryWriter.Write(hasCollectionElements);
        if (hasCollectionElements)
        {
            binaryWriter.Write(AreCollectionElementsInlinable);
            if (AreCollectionElementsInlinable)
                binaryWriter.Write(InlinableCollectionElements);
            else
                binaryWriter.Write(NonInlinableCollectionElements);
        }
    }

    /// <summary>Reads an object info from a stream.</summary>
    /// <param name="binaryReader">The binary reader to read the object from.</param>
    /// <param name="allTypeInfos">The collection to add all the cached type infos to.</param>
    public static ObjectInfo Read(BinaryReader binaryReader, TypeInfos allTypeInfos)
    {
        var typeName = binaryReader.ReadString();
        var type = SerialiserUtilities.GetAnyType(typeName)!;
        var typeInfo = allTypeInfos.Get(type);

        object underlyingObject;
        if (binaryReader.ReadBoolean()) // isArray
        {
            var elementType = SerialiserUtilities.GetAnyType(typeName[..^2])!; // remove the "[]" on the type name
            underlyingObject = Array.CreateInstance(elementType, binaryReader.ReadInt32());
        }
        else
            underlyingObject = RuntimeHelpers.GetUninitializedObject(type);

        foreach (var methodInfo in typeInfo.SerialiserCallbacks.OnDeserialisingMethods)
            try
            {
                methodInfo.Invoke(underlyingObject, null);
            }
            catch (Exception ex)
            {
                Logger.LogError($"OnDeserialising callback crashed. Technical details:\n{ex}");
            }

        var objectInfo = new ObjectInfo(binaryReader.ReadUInt32(), underlyingObject, typeInfo);

        foreach (var kvp in binaryReader.ReadInlinableMembers())
            typeInfo.SerialisableFields.First(field => field.Name == kvp.Key)
                .SetValue(objectInfo.UnderlyingObject, kvp.Value);

        foreach (var kvp in binaryReader.ReadInlinableMembers())
            typeInfo.SerialisableProperties.First(field => field.Name == kvp.Key)
                .SetValue(objectInfo.UnderlyingObject, kvp.Value);

        objectInfo.NonInlinableFields = binaryReader.ReadNonInlinableMembers();
        objectInfo.NonInlinableProperties = binaryReader.ReadNonInlinableMembers();

        if (binaryReader.ReadBoolean()) // hasCollectionElements
        {
            objectInfo.AreCollectionElementsInlinable = binaryReader.ReadBoolean();
            if (objectInfo.AreCollectionElementsInlinable)
                objectInfo.CollectionElements.AddRange(binaryReader.ReadInlinableObjects());
            else
                objectInfo.NonInlinableCollectionElements.AddRange(binaryReader.ReadNonInlinableObjects());
        }

        return objectInfo;
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Assigns the elements of the collection</summary>
    private void AssignCollectionElements()
    {
        if (CollectionElements.Count == 0)
            return;

        if (UnderlyingObject is not Array array)
            throw new SerialisationException("A non array object contains collection elements.");

        for (int i = 0; i < CollectionElements.Count; i++)
            array.SetValue(CollectionElements[i], i);
    }
}
