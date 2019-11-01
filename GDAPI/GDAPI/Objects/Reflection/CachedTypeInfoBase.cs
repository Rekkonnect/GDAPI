using System;
using System.Reflection;

namespace GDAPI.Objects.Reflection
{
    /// <summary>Contains cached information about a <seealso cref="Type"/>.</summary>
    /// <typeparam name="TPropertyKey">The type of the key of the properties of this <seealso cref="Type"/>.</typeparam>
    public abstract class CachedTypeInfoBase<TPropertyKey>
    {
        /// <summary>The object type whose info is being stored in this object.</summary>
        public Type ObjectType { get; protected set; }

        /// <summary>The default parameterless constructor of the type.</summary>
        public ConstructorInfo Constructor { get; protected set; }
        /// <summary>The properties of this type.</summary>
        public KeyedPropertyInfoDictionary<TPropertyKey> Properties { get; protected set; }

        /// <summary>Initializes a new instance of the <seealso cref="CachedTypeInfoBase{TPropertyKey}"/> class.</summary>
        /// <param name="objectType">The object type whose info is being stored in this object.</param>
        public CachedTypeInfoBase(Type objectType)
        {
            ObjectType = objectType;
            Constructor = objectType.GetConstructor(Type.EmptyTypes);
            var properties = objectType.GetProperties();
            Properties = new KeyedPropertyInfoDictionary<TPropertyKey>();
            for (int i = 0; i < properties.Length; i++)
            {
                var p = properties[i];
                Properties.Add(CreateProperty(p));
            }
        }

        /// <summary>Returns a <seealso cref="KeyedPropertyInfo{TKey}"/> instance from a <seealso cref="PropertyInfo"/> object.</summary>
        /// <param name="p">The <seealso cref="PropertyInfo"/> object based on which to create the returning <seealso cref="KeyedPropertyInfo{TKey}"/>.</param>
        protected abstract KeyedPropertyInfo<TPropertyKey> CreateProperty(PropertyInfo p);
    }
}
