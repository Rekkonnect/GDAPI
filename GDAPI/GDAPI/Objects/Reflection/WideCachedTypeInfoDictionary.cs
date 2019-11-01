using GDAPI.Objects.DataStructures;

namespace GDAPI.Objects.Reflection
{
    /// <summary>Represents a dictionary of <seealso cref="CachedTypeInfo{TTypeKey, TPropertyKey}"/> objects that also allows for <seealso cref="WideCachedTypeInfo{TTypeKey, TPropertyKey}"/> objects to be added.</summary>
    /// <typeparam name="TTypeKey">The type of the key that the type returns.</typeparam>
    /// <typeparam name="TPropertyKey">The type of the key that the type's properties return.</typeparam>
    public class WideCachedTypeInfoDictionary<TTypeKey, TPropertyKey> : WideKeyedObjectDictionary<TTypeKey, CachedTypeInfo<TTypeKey, TPropertyKey>>
    {
        /// <summary>Initializes a new instance of the <seealso cref="WideCachedTypeInfoDictionary{TTypeKey, TPropertyKey}"/> class.</summary>
        public WideCachedTypeInfoDictionary() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="WideCachedTypeInfoDictionary{TTypeKey, TPropertyKey}"/> class.</summary>
        /// <param name="d">The dictionary to initialize this dictionary out of.</param>
        public WideCachedTypeInfoDictionary(WideCachedTypeInfoDictionary<TTypeKey, TPropertyKey> d) : base(d) { }
        // TODO: Add more constructors if necessary
    }
}
