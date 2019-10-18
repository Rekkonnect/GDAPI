using System;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Attributes
{
    /// <summary>Marks the property or field as a mappable element in the object string with the corresponding key with an optional default value.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ObjectStringMappableAttribute : Attribute
    {
        /// <summary>The key the property represents in the object string.</summary>
        public int Key { get; }
        /// <summary>The default value of the property.</summary>
        public object DefaultValue { get; }
        /// <summary>Determines whether the default value of the property is variable across different object types.</summary>
        public bool IsDefaultValueVariable { get; }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectStringMappableAttribute"/> attribute.</summary>
        /// <param name="key">The key the property represents in the object string.</param>
        /// <param name="defaultValue">The default value of the property. If <see langword="null"/>, the object property has no default value.</param>
        /// <param name="isDefaultValueVariable">Determines whether the default value of the property is variable across different object types.</param>
        public ObjectStringMappableAttribute(int key, object defaultValue = null, bool isDefaultValueVariable = false)
        {
            Key = key;
            DefaultValue = defaultValue;
            IsDefaultValueVariable = isDefaultValueVariable;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectStringMappableAttribute"/> attribute.</summary>
        /// <param name="key">The key the property represents in the object string.</param>
        /// <param name="defaultValue">The default value of the property. If <see langword="null"/>, the object property has no default value.</param>
        /// <param name="isDefaultValueVariable">Determines whether the default value of the property is variable across different object types.</param>
        public ObjectStringMappableAttribute(ObjectProperty key, object defaultValue = null, bool isDefaultValueVariable = false) : this((int)key, defaultValue, isDefaultValueVariable) { }
    }
}