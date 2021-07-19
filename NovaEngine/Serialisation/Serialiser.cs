using NovaEngine.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NovaEngine.Serialisation
{
    /// <summary>The binary serialiser.</summary>
    public static class Serialiser
    {
        /*********
        ** Public Methods
        *********/
        /// <summary>Serialises an object to a stream.</summary>
        /// <param name="stream">The stream to serialise <paramref name="object"/> to.</param>
        /// <param name="object">The object to serialise to <paramref name="stream"/>.</param>
        public static void Serialise(Stream stream, object @object)
        {
            try
            {
                var allObjectInfos = new List<ObjectInfo>();
                _ = new ObjectInfo(@object, allObjectInfos); // this is only used to populate the object infos collection

                // write objects to stream
                using (var binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    var nonInlinableObjectInfos = allObjectInfos.Where(objectInfo => !objectInfo.IsInlinable); // inlinable object infos shouldn't be written to the stream
                    binaryWriter.Write(nonInlinableObjectInfos.Count());
                    foreach (var objectInfo in nonInlinableObjectInfos)
                        objectInfo.Write(binaryWriter, allObjectInfos);
                }
            }
            catch (Exception ex)
            {
                throw new SerialisationException("Error occured when serialising the object.", ex);
            }
        }

        /// <summary>Deserialises an object from a stream.</summary>
        /// <typeparam name="T">The type of the object to deserialise.</typeparam>
        /// <param name="stream">The stream to deserialise the object from.</param>
        /// <returns>The deserialised object.</returns>
        public static T? Deserialise<T>(Stream stream) => (T?)Deserialise(stream, typeof(T));

        /// <summary>Deserialises an object from a stream.</summary>
        /// <param name="stream">The stream to deserialise the object from.</param>
        /// <param name="returnType">The type of the object to deserialise.</param>
        /// <returns>The deserialised object.</returns>
        public static object? Deserialise(Stream stream, Type returnType)
        {
            try
            {
                var allObjectInfos = new List<ObjectInfo>();

                // create all objects
                using (var binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    var length = binaryReader.ReadInt32();
                    for (int i = 0; i < length; i++)
                        ObjectInfo.Read(binaryReader, allObjectInfos);
                }
                allObjectInfos.Reverse(); // reverse to populate the members for dependencies first

                // populate objects, this isn't done when creating the objects to ensure every object has been created (needed when linking the references)
                foreach (var objectInfo in allObjectInfos.Where(objectInfo => objectInfo.Members.Count > 0 || objectInfo.Collection.Count > 0))
                {
                    // populate members
                    foreach (var member in objectInfo.Members)
                    {
                        // get the object to set from the file
                        var objectInfoToSet = allObjectInfos.FirstOrDefault(objectInfo => objectInfo.Id == member.Value);
                        if (objectInfoToSet == null)
                            throw new SerialisationException($"File doesn't contain an object with an id of: {member.Value}. File seems to be corrupt.");
                        var objectToSetMemberValueTo = objectInfoToSet.Value;

                        // set the member to the retrieved object
                        var fieldInfo = objectInfo.Value!.GetType().GetFieldRecursive(member.Key, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        var propertyInfo = objectInfo.Value!.GetType().GetPropertyRecursive(member.Key, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        if (fieldInfo != null)
                            fieldInfo.SetValue(objectInfo.Value, objectToSetMemberValueTo);
                        else if (propertyInfo != null)
                            propertyInfo.SetValue(objectInfo.Value, objectToSetMemberValueTo);
                        else
                            throw new MissingMemberException(objectInfo.Value.GetType().FullName, member.Key);
                    }

                    // add objects to the collection (if the object being populated is a collection)
                    for (int i = 0; i < objectInfo.Collection.Count; i++)
                    {
                        var item = objectInfo.Collection[i];

                        // get the object to set from the file
                        var objectInfoToSet = allObjectInfos.FirstOrDefault(objectInfo => objectInfo.Id == item);
                        if (objectInfoToSet == null)
                            throw new SerialisationException($"File doesn't contain an object with an id of: {item}. File seems to be corrupt.");
                        var objectToSetMemberValueTo = objectInfoToSet.Value;

                        if (objectInfo.CollectionType == CollectionType.Array)
                            (objectInfo.Value as Array)!.SetValue(objectToSetMemberValueTo, i);
                        else if (objectInfo.CollectionType == CollectionType.GenericList || objectInfo.CollectionType == CollectionType.GenericDictionary)
                            objectInfo.Value!.GetType().GetInterfaces().Single(@interface => @interface.Name == "ICollection`1").GetMethod("Add")!.Invoke(objectInfo.Value, new[] { objectToSetMemberValueTo });
                        else if (objectInfo.CollectionType == CollectionType.List)
                            (objectInfo.Value as IList)!.Add(objectToSetMemberValueTo);
                        else if (objectInfo.CollectionType == CollectionType.Dictionary)
                            (objectInfo.Value as IDictionary)!.Add(objectToSetMemberValueTo!.GetType().GetProperty("Key")!.GetValue(objectToSetMemberValueTo)!, objectToSetMemberValueTo!.GetType().GetProperty("Value")!.GetValue(objectToSetMemberValueTo));
                    }
                }

                return Convert.ChangeType(allObjectInfos.Last().Value, returnType); // the root object is last as the collection was reversed
            }
            catch (Exception ex)
            {
                throw new SerialisationException("Error occured when deserialising the object.", ex);
            }
        }
    }
}
