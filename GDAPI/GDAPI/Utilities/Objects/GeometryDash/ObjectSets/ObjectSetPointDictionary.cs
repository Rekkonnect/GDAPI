using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.ObjectSets
{
    /// <summary>Represents a point dictionary for an <seealso cref="ObjectSet"/>. It directly inherits <seealso cref="Dictionary{TKey, TValue}"/>.</summary>
    /// <typeparam name="T">The type of the <see langword="enum"/> containing the points of the dictionary.</typeparam>
    public class ObjectSetPointDictionary<T> : Dictionary<T, ObjectGrid>
        where T : Enum
    {
        /// <summary>Initializes a new instance of the <seealso cref="ObjectSetPointDictionary{T}"/> class.</summary>
        public ObjectSetPointDictionary() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectSetPointDictionary{T}"/> class.</summary>
        public ObjectSetPointDictionary(Dictionary<T, ObjectGrid> dictionary) : base(dictionary) { }
    }
}
