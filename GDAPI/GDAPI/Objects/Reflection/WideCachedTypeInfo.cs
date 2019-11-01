using GDAPI.Objects.DataStructures;
using System;

namespace GDAPI.Objects.Reflection
{
    /// <summary>Contains cached information about a <seealso cref="Type"/>.</summary>
    /// <typeparam name="TTypeKey">The type of the key of this <seealso cref="Type"/>.</typeparam>
    /// <typeparam name="TPropertyKey">The type of the key of the properties of this <seealso cref="Type"/>.</typeparam>
    public abstract class WideCachedTypeInfo<TTypeKey, TPropertyKey> : CachedTypeInfoBase<TPropertyKey>, IWideKeyedObject<TTypeKey>
    {
        /// <summary>The key that this object can be addressed by.</summary>
        public abstract TTypeKey[] Key { get; }

        /// <summary>Initializes a new instance of the <seealso cref="CachedTypeInfo{TTypeKey, TPropertyKey}"/> class.</summary>
        /// <param name="objectType">The object type whose info is being stored in this object.</param>
        public WideCachedTypeInfo(Type objectType) : base(objectType) { }
    }
}
