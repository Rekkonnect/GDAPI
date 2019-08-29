using GDAPI.Utilities.Attributes;
using System;
using System.Reflection;
using System.Transactions;

namespace GDAPI.Utilities.Objects.General
{
    // The following TODOs contain instructions to be executed when self-compiling code can be made

    // TODO: Make this abstract
    public class PropertyAccessInfo
    {
        public PropertyInfo PropertyInfo { get; }
        public Type PropertyType { get; }

        protected Type GenericFunc, GenericAction;

        // TODO: Remove these
        public Delegate GetMethodDelegate { get; }
        public Delegate SetMethodDelegate { get; }

        public ObjectStringMappableAttribute ObjectStringMappableAttribute { get; }
        public int? Key => ObjectStringMappableAttribute?.Key;
            
        public PropertyAccessInfo(PropertyInfo info)
        {
            PropertyInfo = info;
            // You have to be kidding me right now
            var propertyType = PropertyType = info.PropertyType;
            var objectType = info.DeclaringType;
            GenericFunc = typeof(Func<,>).MakeGenericType(objectType, propertyType);
            GenericAction = typeof(Action<,>).MakeGenericType(objectType, propertyType);
            ObjectStringMappableAttribute = info.GetCustomAttribute<ObjectStringMappableAttribute>();

            // TODO: Remove these
            GetMethodDelegate = info.GetGetMethod().CreateDelegate(GenericFunc);
            SetMethodDelegate = info.GetSetMethod()?.CreateDelegate(GenericAction);
        }

        // TODO: Make these abstract
        public virtual object Get(object instance) => GetMethodDelegate?.DynamicInvoke(instance);
        public virtual void Set(object instance, object newValue) => SetMethodDelegate?.DynamicInvoke(instance, newValue);
    }
    public class PropertyAccessInfo<TObject, TProperty> : PropertyAccessInfo
    {
        public Func<TObject, TProperty> GetMethod { get; }
        public Action<TObject, TProperty> SetMethod { get; }

        public PropertyAccessInfo(PropertyInfo info)
            : base(info)
        {
            GetMethod = info.GetGetMethod().CreateDelegate(GenericFunc) as Func<TObject, TProperty>;
            SetMethod = info.GetSetMethod()?.CreateDelegate(GenericAction) as Action<TObject, TProperty>;
        }

        public override object Get(object instance) => GetMethod((TObject)instance);
        public override void Set(object instance, object newValue) => SetMethod?.Invoke((TObject)instance, (TProperty)newValue);
    }
}
