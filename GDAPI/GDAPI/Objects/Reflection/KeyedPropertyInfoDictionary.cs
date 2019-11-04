using GDAPI.Objects.DataStructures;

namespace GDAPI.Objects.Reflection
{
    /// <summary>Represents a dictionary of <seealso cref="KeyedPropertyInfo{TKey}"/> objects.</summary>
    /// <typeparam name="TKey">The type of the key that the <seealso cref="KeyedPropertyInfo{TKey}"/> returns.</typeparam>
    public class KeyedPropertyInfoDictionary<TKey> : KeyedObjectDictionary<TKey, KeyedPropertyInfo<TKey>>
    {
        /// <summary>Initializes a new instance of the <seealso cref="KeyedPropertyInfoDictionary{TKey}"/> class.</summary>
        public KeyedPropertyInfoDictionary() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="KeyedPropertyInfoDictionary{TKey}"/> class.</summary>
        /// <param name="dictionary">The dictionary to initialize this dictionary out of.</param>
        public KeyedPropertyInfoDictionary(KeyedPropertyInfoDictionary<TKey> dictionary) : base(dictionary) { }
    }
}
