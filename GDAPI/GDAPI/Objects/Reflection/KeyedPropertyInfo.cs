using GDAPI.Objects.KeyedObjects;
using System;
using System.Reflection;

namespace GDAPI.Objects.Reflection
{
    /// <summary>Contains information about a property which can be accessed by key.</summary>
    public abstract class KeyedPropertyInfo<TKey> : IKeyedObject<TKey>
    {
        /// <summary>The <seealso cref="System.Reflection.PropertyInfo"/> object that is associated with this property.</summary>
        public PropertyInfo PropertyInfo { get; }
        /// <summary>The type of the property.</summary>
        public Type PropertyType { get; }

        /// <summary>The type of the getter function.</summary>
        protected Type GenericFunc;
        /// <summary>The type of the setter function.</summary>
        protected Type GenericAction;

        // TODO: Remove these
        /// <summary>The delegate of the get method.</summary>
        public Delegate GetMethodDelegate { get; }
        /// <summary>The delegate of the set method.</summary>
        public Delegate SetMethodDelegate { get; }

        /// <summary>The key that this property can be addressed by.</summary>
        public abstract TKey Key { get; }

        /// <summary>Initializes a new instance of the <seealso cref="KeyedPropertyInfo{TKey}"/> class.</summary>
        /// <param name="info">The <seealso cref="System.Reflection.PropertyInfo"/> from which to retrieve the property info.</param>
        public KeyedPropertyInfo(PropertyInfo info)
        {
            PropertyInfo = info;
            // You have to be kidding me right now
            var propertyType = PropertyType = info.PropertyType;
            var objectType = info.DeclaringType;
            GenericFunc = typeof(Func<,>).MakeGenericType(objectType, propertyType);
            GenericAction = typeof(Action<,>).MakeGenericType(objectType, propertyType);

            // TODO: Remove these
            GetMethodDelegate = info.GetGetMethod()?.CreateDelegate(GenericFunc);
            SetMethodDelegate = info.GetSetMethod()?.CreateDelegate(GenericAction);
        }

        // TODO: Make these abstract
        /// <summary>Gets the value of the property for an object instance.</summary>
        /// <param name="instance">The instance whose property's value to get.</param>
        public virtual object Get(object instance) => GetMethodDelegate?.DynamicInvoke(instance);
        /// <summary>Sets the value of the property for an object instance.</summary>
        /// <param name="instance">The instance whose property's value to set.</param>
        /// <param name="newValue">The new value to set to the property.</param>
        public virtual void Set(object instance, object newValue) => SetMethodDelegate?.DynamicInvoke(instance, newValue);
    }
}
