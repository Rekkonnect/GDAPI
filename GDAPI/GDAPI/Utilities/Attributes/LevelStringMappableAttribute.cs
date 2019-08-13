using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Attributes
{
    /// <summary>Marks the property or field as a mappable element in the level string with the corresponding key.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class LevelStringMappableAttribute : Attribute
    {
        /// <summary>The key the property represents in the level string.</summary>
        public string Key { get; }

        /// <summary>Initializes a new instance of the <seealso cref="LevelStringMappableAttribute"/> class.</summary>
        /// <param name="key">The key the property represents in the level string.</param>
        public LevelStringMappableAttribute(string key)
        {
            Key = key;
        }
    }
}