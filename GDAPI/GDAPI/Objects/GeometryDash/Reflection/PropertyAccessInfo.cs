using System;
using System.Reflection;
using GDAPI.Attributes;
using GDAPI.Objects.Reflection;

namespace GDAPI.Objects.GeometryDash.Reflection
{
    // The following TODOs contain instructions to be executed when self-compiling code can be made
    // Should also change KeyedPropertyInfo according to its TODOs

    // TODO: Make this abstract
    /// <summary>Contains cached information about a property aiming to improve performance while using reflective code.</summary>
    public class PropertyAccessInfo : KeyedPropertyInfo<int?>
    {
        /// <summary>The <seealso cref="Attributes.ObjectStringMappableAttribute"/> of the property.</summary>
        public ObjectStringMappableAttribute ObjectStringMappableAttribute { get; }
        /// <summary>The property key that is associated with this property.</summary>
        public override int? Key => ObjectStringMappableAttribute?.Key;

        /// <summary>Initializes a new instance of the <seealso cref="PropertyAccessInfo"/> class.</summary>
        /// <param name="info">The <seealso cref="PropertyInfo"/> from which to retrieve the property info.</param>
        public PropertyAccessInfo(PropertyInfo info)
            : base(info)
        {
            ObjectStringMappableAttribute = info.GetCustomAttribute<ObjectStringMappableAttribute>();
        }
    }
    /// <summary>Contains cached information about a property aiming to improve performance while using reflective code. This is defined so that it be used in self-generated and compiled code, aiming for maximized performance. Manual usage of this class indicates severe masochism.</summary>
    public class PropertyAccessInfo<TObject, TProperty> : PropertyAccessInfo
    {
        /// <summary>The delegate of the get method.</summary>
        public Func<TObject, TProperty> GetMethod { get; }
        /// <summary>The delegate of the set method.</summary>
        public Action<TObject, TProperty> SetMethod { get; }

        /// <summary>Initializes a new instance of the <seealso cref="PropertyAccessInfo"/> class.</summary>
        /// <param name="info">The <seealso cref="PropertyInfo"/> from which to retrieve the property info.</param>
        public PropertyAccessInfo(PropertyInfo info)
            : base(info)
        {
            GetMethod = info.GetGetMethod().CreateDelegate(GenericFunc) as Func<TObject, TProperty>;
            SetMethod = info.GetSetMethod()?.CreateDelegate(GenericAction) as Action<TObject, TProperty>;
        }

        /// <summary>Gets the value of the property for an object instance.</summary>
        /// <param name="instance">The instance whose property's value to get.</param>
        public override object Get(object instance) => GetMethod((TObject)instance);
        /// <summary>Sets the value of the property for an object instance.</summary>
        /// <param name="instance">The instance whose property's value to set.</param>
        /// <param name="newValue">The new value to set to the property.</param>
        public override void Set(object instance, object newValue) => SetMethod?.Invoke((TObject)instance, (TProperty)newValue);
    }
}
