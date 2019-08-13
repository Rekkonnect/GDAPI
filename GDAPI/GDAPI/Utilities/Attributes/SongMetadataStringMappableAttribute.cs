using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Attributes
{
    /// <summary>Marks the property or field as a mappable element in the song metadata string with the corresponding key.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class SongMetadataStringMappableAttribute : Attribute
    {
        /// <summary>The key the property represents in the song metadata string.</summary>
        public int Key { get; }

        /// <summary>Initializes a new instance of the <seealso cref="SongMetadataStringMappableAttribute"/> class.</summary>
        /// <param name="key">The key the property represents in the song metadata string.</param>
        public SongMetadataStringMappableAttribute(int key)
        {
            Key = key;
        }
    }
}