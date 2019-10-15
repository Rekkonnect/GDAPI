using System;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;
using static GDAPI.Objects.GeometryDash.LevelObjects.Triggers.RandomTrigger;

namespace GDAPI.Attributes
{
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public abstract class ObjectPropertyTypeAttribute : Attribute
    {
        /// <summary>The type of the object property.</summary>
        public abstract Type Type { get; }
        /// <summary>The name of the type of the object property.</summary>
        public abstract string TypeName { get; }
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyIntTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<int>
    {
        private static Type type = typeof(int);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyDoubleTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<double>
    {
        private static Type type = typeof(double);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyBoolTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<bool>
    {
        private static Type type = typeof(bool);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyStringTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<string>
    {
        private static Type type = typeof(string);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyHSVAdjustmentTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<HSVAdjustment>
    {
        private static Type type = typeof(HSVAdjustment);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyChancePoolInfoTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<RandomTrigger.ChancePoolInfo>
    {
        private static Type type = typeof(ChancePoolInfo);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyIntArrayTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<int[]>
    {
        private static Type type = typeof(int[]);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    #region Enumeration Types
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyEasingTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<Easing>
    {
        private static Type type = typeof(Easing);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyInstantCountComparisonTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<InstantCountComparison>
    {
        private static Type type = typeof(InstantCountComparison);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyPickupItemPickupModeTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<PickupItemPickupMode>
    {
        private static Type type = typeof(PickupItemPickupMode);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyPulseModeTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<PulseMode>
    {
        private static Type type = typeof(PulseMode);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyPulseTargetTypeTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<PulseTargetType>
    {
        private static Type type = typeof(PulseTargetType);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyTargetPosCoordinatesTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<TargetPosCoordinates>
    {
        private static Type type = typeof(TargetPosCoordinates);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyTouchToggleModeTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<TouchToggleMode>
    {
        private static Type type = typeof(TouchToggleMode);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyCustomParticleGroupingTypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<CustomParticleGrouping>
    {
        private static Type type = typeof(CustomParticleGrouping);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectPropertyCustomParticleProperty1TypeAttribute : ObjectPropertyTypeAttribute, IGenericAttribute<CustomParticleProperty1>
    {
        private static Type type = typeof(CustomParticleProperty1);
        /// <summary>The type of the object property.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object property.</summary>
        public override string TypeName => type.Name;
    }
    #endregion
}
