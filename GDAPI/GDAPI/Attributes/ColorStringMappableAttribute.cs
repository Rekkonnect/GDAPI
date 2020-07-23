using System;

namespace GDAPI.Attributes
{
    /// <summary>Marks the property or field as a mappable element in the color string with the corresponding key.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ColorStringMappableAttribute : Attribute
    {
        /// <summary>The key the property represents in the color string.</summary>
        public int Key { get; }

        /// <summary>Initializes a new instance of the <seealso cref="ColorStringMappableAttribute"/> attribute.</summary>
        /// <param name="key">The key the property represents in the color string.</param>
        public ColorStringMappableAttribute(int key)
        {
            Key = key;
        }

        /// <summary>A key getter that is useful as a selector function.</summary>
        /// <param name="attribute">The attribute instance whose key to get.</param>
        /// <returns>The key of the provided attribute instance.</returns>
        public static int GetKey(ColorStringMappableAttribute attribute) => attribute.Key;
    }
}