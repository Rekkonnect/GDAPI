using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Attributes
{
    /// <summary>Marks the property or field as a mappable element in the color string with the corresponding key.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ColorStringMappableAttribute : Attribute
    {
        /// <summary>The key the property represents in the color string.</summary>
        public int Key { get; }

        /// <summary>Initializes a new instance of the <seealso cref="ColorStringMappableAttribute"/> class.</summary>
        /// <param name="key">The key the property represents in the color string.</param>
        public ColorStringMappableAttribute(int key)
        {
            Key = key;
        }
    }
}