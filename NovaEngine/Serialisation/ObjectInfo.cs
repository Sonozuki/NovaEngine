using NovaEngine.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NovaEngine.Serialisation
{
    /// <summary>Represents metadata about an <see langword="object"/>.</summary>
    internal class ObjectInfo
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The id of the object.</summary>
        public Guid Id { get; }

        /// <summary>The value of the object.</summary>
        public object? Value { get; private set; }

        /// <summary>Whether the serialiser is able to inline the value of the object.</summary>
        public bool IsInlinable { get; private set; }

        /// <summary>The members of <see cref="Value"/>, pointing to the ids of the <see cref="ObjectInfo"/> representations of the member values.</summary>
        public Dictionary<string, Guid> Members { get; } = new();

        /// <summary>The type of collection the object who this object info is representing (if it's a collection.)</summary>
        public CollectionType CollectionType { get; private set; }

        /// <summary>The ids of the object infos representing the objects that are stored in the collection (if it's a collection.)</summary>
        public List<Guid> Collection { get; } = new();


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="value">The value of the object.</param>
        /// <param name="allObjectInfos">All the object infos of the object being converted. This is used to flatten circular references.</param>
        public ObjectInfo(object? value, List<ObjectInfo> allObjectInfos)
        {
            allObjectInfos.Add(this);

            Id = Guid.NewGuid();
            Value = value;
            IsInlinable = Value?.GetType().IsInlinable() ?? true;

            if (!IsInlinable)
            {
                // retrieve the items to add
                var items = new List<(string? Name, object Value)>();

                // add items stored in collection
                if (Value is IEnumerable)
                {
                         if (Value is Array)                                                                                  CollectionType = CollectionType.Array;
                    else if (Value?.GetType().GetInterfaces().Any(@interface => @interface.Name == "IList`1") ?? false)       CollectionType = CollectionType.GenericList;
                    else if (Value?.GetType().GetInterfaces().Contains(typeof(IList)) ?? false)                               CollectionType = CollectionType.List;
                    else if (Value?.GetType().GetInterfaces().Any(@interface => @interface.Name == "IDictionary`1") ?? false) CollectionType = CollectionType.GenericDictionary;
                    else if (Value?.GetType().GetInterfaces().Contains(typeof(IDictionary)) ?? false)                         CollectionType = CollectionType.Dictionary;
                    else throw new SerialisationException($"Failed to serialise collection of type: {Value?.GetType()}");
                    
                    items.AddRange((Value as IEnumerable)!.OfType<object>().Select(item => ((string?)null, item)));
                }

                // add members
                items.AddRange(Value!.GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(field => (field.IsPublic && !field.HasCustomAttribute<NonSerialisableAttribute>())
                                 || (!field.IsPublic && field.HasCustomAttribute<SerialisableAttribute>()))
                    .Select(field => ((string?)field.Name, field.GetValue(Value)!)));

                items.AddRange(Value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(property => property.CanRead && property.CanWriteForSerialisation()
                                    && (property.HasBackingField() || property.HasCustomAttribute<SerialisableAttribute>()) // if a property doesn't have a backing field, it must have the Serialisable attribute
                                    && ((property.GetMethod!.IsPublic && !property.HasCustomAttribute<NonSerialisableAttribute>()) 
                                        || (!property.GetMethod!.IsPublic && property.HasCustomAttribute<SerialisableAttribute>())))
                    .Select(property => 
                        (Name: property.HasBackingField() ? (string?)property.GetBackingFieldName() : property.Name, 
                        Value: property.HasBackingField() ? property.GetBackingField()!.GetValue(Value)! : property.GetValue(Value)!)));

                // add the items to the object info
                foreach (var item in items)
                {
                    var objectInfo = allObjectInfos.FirstOrDefault(objectInfo => object.ReferenceEquals(item.Value, objectInfo.Value));
                    if (objectInfo == null)
                        objectInfo = new(item.Value, allObjectInfos);

                    if (item.Name == null)
                        Collection.Add(objectInfo.Id);
                    else
                        Members[item.Name] = objectInfo.Id;
                }
            }
        }

        /// <summary>Writes the object info to a binary writer.</summary>
        /// <param name="binaryWriter">The binary writer to write the object info to.</param>
        /// <param name="allObjectInfos">All the object infos of the object being converted. This is used to inline values properly.</param>
        public void Write(BinaryWriter binaryWriter, List<ObjectInfo> allObjectInfos)
        {
            binaryWriter.Write(Id.ToString("N"));

            binaryWriter.Write(IsInlinable);
            if (IsInlinable)
                binaryWriter.Write(Value);
            else
            {
                // write type
                var typeName = Value!.GetType().FullName!;
                binaryWriter.Write(typeName);

                // write array length // TODO: the length is written twice for arrays, only write it once
                var isArray = typeName.EndsWith("[]");
                binaryWriter.Write(isArray);
                if (isArray)
                    binaryWriter.Write((Value as Array)!.Length);

                // write collection info
                binaryWriter.Write((byte)CollectionType);
                binaryWriter.Write(Collection.Count);
                foreach (var item in Collection)
                    WriteValue(item);

                // write member info
                binaryWriter.Write(Members.Count);
                foreach (var member in Members)
                {
                    binaryWriter.Write(member.Key); // name
                    WriteValue(member.Value); // value
                }
            }

            // Writes the value of an object
            // This will write the value if it's inlinable; otherwise, the id
            void WriteValue(Guid objectToWrite)
            {
                var @object = allObjectInfos.Single(objectInfo => objectInfo.Id == objectToWrite);
                binaryWriter.Write(@object.IsInlinable); // TODO: optimise this so it's not specified on each object (needed for members, not for collections)
                if (@object.IsInlinable)
                    binaryWriter.Write(@object.Value);
                else
                    binaryWriter.Write(objectToWrite.ToString("N"));
            }
        }

        /// <summary>Reads the object info from a binary reader.</summary>
        /// <param name="binaryReader">The binary reader to read the object info from.</param>
        /// <param name="allObjectInfos">All the object infos of the object being converted. This is used to inline values properly.</param>
        public static void Read(BinaryReader binaryReader, List<ObjectInfo> allObjectInfos)
        {
            var objectInfo = new ObjectInfo(new Guid(binaryReader.ReadString()));
            allObjectInfos.Add(objectInfo);

            objectInfo.IsInlinable = binaryReader.ReadBoolean();
            if (objectInfo.IsInlinable)
                objectInfo.Value = binaryReader.ReadObject();
            else
            {
                // retrieve type
                var typeName = binaryReader.ReadString();

                // create object
                if (binaryReader.ReadBoolean()) // isArray
                    objectInfo.Value = Array.CreateInstance(Type.GetType(typeName[..^2])!, binaryReader.ReadInt32());
                else
                    objectInfo.Value = Activator.CreateInstance(Type.GetType(typeName)!, true);

                // retrieve collection info
                objectInfo.CollectionType = (CollectionType)binaryReader.ReadByte();
                var length = binaryReader.ReadInt32();
                for (int i = 0; i < length; i++)
                    objectInfo.Collection.Add(ReadValue());

                // retrieve member info
                length = binaryReader.ReadInt32();
                for (int i = 0; i < length; i++)
                    objectInfo.Members[binaryReader.ReadString()] = ReadValue();
            }

            // Reads the value of an object
            // This will create an object info for the value if it's inlined
            Guid ReadValue()
            {
                if (binaryReader.ReadBoolean()) // isInlined
                {
                    var objectInfo = new ObjectInfo(binaryReader.ReadObject(), allObjectInfos);
                    return objectInfo.Id;
                }
                else
                    return new Guid(binaryReader.ReadString());
            }
        }


        /*********
        ** Private Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="id">The id of the object.</param>
        private ObjectInfo(Guid id)
        {
            Id = id;
        }
    }
}
