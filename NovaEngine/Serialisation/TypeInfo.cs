using NovaEngine.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NovaEngine.Serialisation
{
    /// <summary>Represents the info about a type.</summary>
    internal class TypeInfo
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The underlying type this represents.</summary>
        public Type Type { get; }

        /// <summary>Whether the type can be inlined.</summary>
        public bool IsInlinable { get; }

        /// <summary>Whether the type is <see langword="unmanaged"/>.</summary>
        public bool IsUnmanaged { get; }

        /// <summary>The fields that should be serialised for the type.</summary>
        public List<FieldInfo> SerialisableFields { get; } = new();

        /// <summary>The properties that should be serialised for the type.</summary>
        public List<PropertyInfo> SerialisableProperties { get; } = new();

        /// <summary>The methods that will get invoked when the object is reconstructed.</summary>
        public List<MethodInfo> SerialiserCalledMethods { get; } = new();


        /*********
        ** Public Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="type">The object whose object info should be retrieved.</param>
        public TypeInfo(Type type)
        {
            Type = type;
            IsInlinable = type.IsInlinable();
            IsUnmanaged = type.IsUnmanaged();

            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            // fields
            SerialisableFields.AddRange(type.GetFields(bindingFlags)
                .Where(field => (field.IsPublic && !field.HasCustomAttribute<NonSerialisableAttribute>()) // serialise public fields without a non serialisable attribute
                             || (!field.IsPublic && field.HasCustomAttribute<SerialisableAttribute>()))); // serialise non public fields with a serialisable attribute

            // properties
            var properties = type.GetProperties(bindingFlags)
                .Where(property => property.CanRead && property.CanWriteForSerialisation() // property must be readable and writable
                               && (property.HasBackingField() || property.HasCustomAttribute<SerialisableAttribute>()) // must be an auto property, or property with a serialisable attribute
                               && ((property.GetMethod!.IsPublic && !property.HasCustomAttribute<NonSerialisableAttribute>()) // getter must either be public without a non serialisable attribute
                                   || (!property.GetMethod!.IsPublic && property.HasCustomAttribute<SerialisableAttribute>()))); // or be non public with a serialisable attribute
            foreach (var property in properties)
                if (property.HasBackingField()) // serialise the backing field directly (if it has one), instead of through the property
                    SerialisableFields.Add(property.GetBackingField()!);
                else
                    SerialisableProperties.Add(property);

            // callbacks
            SerialiserCalledMethods.AddRange(type.GetMethods(bindingFlags)
                .Where(method => method.HasCustomAttribute<SerialiserCalledAttribute>()));
        }
    }
}
