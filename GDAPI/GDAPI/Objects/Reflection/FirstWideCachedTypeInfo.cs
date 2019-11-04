using GDAPI.Objects.KeyedObjects;
using System;

namespace GDAPI.Objects.Reflection
{
    /// <summary>Contains cached information about a <seealso cref="Type"/>.</summary>
    /// <typeparam name="TTypeKey">The type of the key of this <seealso cref="Type"/>.</typeparam>
    /// <typeparam name="TPropertyKey">The type of the key of the properties of this <seealso cref="Type"/>.</typeparam>
    public abstract class FirstWideCachedTypeInfo<TTypeKey, TPropertyKey> : CachedTypeInfo<TTypeKey[], TPropertyKey>, IFirstWideDoubleKeyedObject<TTypeKey, Type>
    {
        /// <summary>The non-array version of the first key by which this object type info is being addressed.</summary>
        public abstract TTypeKey ConvertedKey { get; }

        /// <summary>Initializes a new instance of the <seealso cref="FirstWideCachedTypeInfo{TTypeKey, TPropertyKey}"/> class.</summary>
        /// <param name="objectType">The object type whose info is being stored in this object.</param>
        public FirstWideCachedTypeInfo(Type objectType) : base(objectType) { }
    }
}
