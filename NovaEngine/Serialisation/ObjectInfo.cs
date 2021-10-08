using NovaEngine.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NovaEngine.Serialisation
{
    /// <summary>Represents the info about an object.</summary>
    internal class ObjectInfo
    {
        /*********
        ** Fields
        *********/
        /// <summary>The type info of the object this represents.</summary>
        private readonly TypeInfo TypeInfo;

        /// <summary>Whether the non inlinable members and collection elements have had their references linked.</summary>
        private bool HaveReferencesBeenLinked;


        /*********
        ** Accessors
        *********/
        /// <summary>The unique id representing the object.</summary>
        public uint Id { get; }

        /// <summary>The object this object info represents.</summary>
        public object UnderlyingObject { get; private set; }

        /// <summary>The type of the collection.</summary>
        public CollectionType CollectionType { get; set; }

        /// <summary>If the object is a collection (inherits from <see cref="IEnumerable"/>), this will specify whether the elements are inlinable or non inlinable (whether <see cref="InlinableCollectionElements"/> or <see cref="NonInlinableCollectionElements"/>) stores the elements.</summary>
        public bool AreCollectionElementsInlinable { get; set; }

        /// <summary>The elements of the object, when it's an <see cref="IEnumerable"/> of inlinable objects.</summary>
        /// <remarks>This and <see cref="NonInlinableCollectionElements"/> will not ever be used at the same time.</remarks>
        public List<byte[]> InlinableCollectionElements { get; } = new();

        /// <summary>The elements of the object, when it's an <see cref="IEnumerable"/> of non inlinable objects.</summary>
        /// <remarks>This and <see cref="NonInlinableCollectionElements"/> will not ever be used at the same time.</remarks>
        public List<uint> NonInlinableCollectionElements { get; } = new();

        // fields and properties are split here purely in the interest of performance, regardless of how small the gain is
        // (as when looking for the member names, we don't need to look through both the fields and properties to find what we want)
        // this does have the slight disadvantage of decreasing the effienciency of the final serialised result, as now 2 more shorts
        // are used for specifiying the collection sizes, but using an additional 4 bytes per non inlinable object isn't that bad

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
        ** Public Methods
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

        /// <summary>Links the references of non inlinable objects, recursively.</summary>
        /// <param name="allObjectInfos">All the deserialised object infos.</param>
        /// <remarks>This will also invoke all methods in the object which have a <see cref="SerialiserCalledAttribute"/>.</remarks>
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

            if (!AreCollectionElementsInlinable)
            {
                var collectionElements = new List<object?>();
                foreach (var element in NonInlinableCollectionElements)
                {
                    var objectInfo = allObjectInfos.First(objectInfo => objectInfo.Id == element);
                    objectInfo.LinkReferences(allObjectInfos);

                    collectionElements.Add(objectInfo.UnderlyingObject);
                }
                AssignCollectionElements(collectionElements);
            }

            // invoke serialiser callback methods
            TypeInfo.SerialiserCalledMethods.ForEach(method => method.Invoke(UnderlyingObject, null));
        }

        /// <summary>Writes the object info to a stream.</summary>
        /// <param name="binaryWriter">The binary writer to write the object to.</param>
        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(TypeInfo.Type.FullName!);

            var isArray = UnderlyingObject is Array array;
            binaryWriter.Write(isArray);
            if (isArray)
                binaryWriter.Write((UnderlyingObject as Array)!.Length);

            binaryWriter.Write(Id);

            // members
            binaryWriter.Write(InlinableFields);
            binaryWriter.Write(InlinableProperties);
            binaryWriter.Write(NonInlinableFields);
            binaryWriter.Write(NonInlinableProperties);

            // collection elements
            var hasCollectionElements = InlinableCollectionElements.Count != 0 || NonInlinableCollectionElements.Count != 0;
            binaryWriter.Write(hasCollectionElements);
            if (hasCollectionElements)
            {
                binaryWriter.Write((byte)CollectionType);
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
            // get type name
            var typeName = binaryReader.ReadString();
            var isArray = binaryReader.ReadBoolean();
            if (isArray)
                typeName = typeName[..^2];

            // get underlying object type
            var type = SerialiserUtilities.GetAnyType(typeName)!;
            var typeInfo = allTypeInfos.Get(type);

            // create underlying object instance
            object underlyingObject;
            if (isArray)
                underlyingObject = Array.CreateInstance(type, binaryReader.ReadInt32());
            else
                underlyingObject = Activator.CreateInstance(type, true)!;

            var objectInfo = new ObjectInfo(binaryReader.ReadUInt32(), underlyingObject, typeInfo);

            // inlinable fields
            foreach (var kvp in binaryReader.ReadInlinableMembers())
                typeInfo.SerialisableFields.First(field => field.Name == kvp.Key)
                    .SetValue(objectInfo.UnderlyingObject, kvp.Value);

            // inlinable properties
            foreach (var kvp in binaryReader.ReadInlinableMembers())
                typeInfo.SerialisableProperties.First(field => field.Name == kvp.Key)
                    .SetValue(objectInfo.UnderlyingObject, kvp.Value);

            // non inlinable members
            objectInfo.NonInlinableFields = binaryReader.ReadNonInlinableMembers();
            objectInfo.NonInlinableProperties = binaryReader.ReadNonInlinableMembers();

            // collection elements
            if (binaryReader.ReadBoolean()) // hasCollectionElements
            {
                objectInfo.CollectionType = (CollectionType)binaryReader.ReadByte();
                objectInfo.AreCollectionElementsInlinable = binaryReader.ReadBoolean();
                if (objectInfo.AreCollectionElementsInlinable)
                    objectInfo.AssignCollectionElements(binaryReader.ReadInlinableObjects());
                else
                    objectInfo.NonInlinableCollectionElements.AddRange(binaryReader.ReadNonInlinableObjects());
            }

            return objectInfo;
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Assigns the elements of the collection</summary>
        /// <param name="elements">The elements to assign to the collection.</param>
        private void AssignCollectionElements(List<object?> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];

                switch (CollectionType)
                {
                    case CollectionType.Array:
                        (UnderlyingObject as Array)!.SetValue(element, i);
                        break;
                    case CollectionType.GenericList:
                    case CollectionType.GenericDictionary:
                        UnderlyingObject!.GetType().GetInterfaces().Single(@interface => @interface.Name == "ICollection`1").GetMethod("Add")!.Invoke(UnderlyingObject, new[] { element });
                        break;
                    case CollectionType.List:
                        (UnderlyingObject as IList)!.Add(element);
                        break;
                    case CollectionType.Dictionary:
                        (UnderlyingObject as IDictionary)!.Add(
                            element!.GetType().GetProperty("Key")!.GetValue(element)!,
                            element!.GetType().GetProperty("Value")!.GetValue(element)
                        );
                        break;
                }
            }
        }
    }
}
