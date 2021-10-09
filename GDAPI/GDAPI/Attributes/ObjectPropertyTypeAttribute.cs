using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.General;
using System;
using static GDAPI.Objects.GeometryDash.LevelObjects.Triggers.RandomTrigger;

namespace GDAPI.Attributes
{
    public enum ObjectPropertyTypeCode
    {
        Unknown = -1,
        None = 0,

        Int,
        Double,
        Bool,
        String,
        HSVAdjustment,
        ChancePoolInfo,
        IntArray,
        Easing,
        InstantCountComparison,
        PickupItemPickupMode,
        PulseMode,
        PulseTargetType,
        TargetPosCoordinates,
        TouchToggleMode,
        CustomParticleGrouping,
        CustomParticleProperty1
    }

    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public abstract class ObjectPropertyTypeAttribute : Attribute
    {
        /// <summary>The type code of the object property.</summary>
        public abstract ObjectPropertyTypeCode TypeCode { get; }
        /// <summary>The type of the object property.</summary>
        public abstract Type Type { get; }
        /// <summary>The name of the type of the object property.</summary>
        public string TypeName => Type.Name;
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class ObjectPropertyCustomTypeAttribute : ObjectPropertyTypeAttribute
    {
        private readonly ObjectPropertyTypeCode? code;
        private readonly Type? type;

        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => code ?? throw new InvalidOperationException("Attribute does not contain type code information.");
        /// <inheritdoc/>
        public override Type Type => type ?? throw new InvalidOperationException("Attribute does not contain type information.");

        public ObjectPropertyCustomTypeAttribute(ObjectPropertyTypeCode code)
            : this(code, null) { }
        public ObjectPropertyCustomTypeAttribute(Type type)
            : this(null, type) { }

        private ObjectPropertyCustomTypeAttribute(ObjectPropertyTypeCode? typeCode, Type? typeObject)
        {
            code = typeCode;
            type = typeObject;
        }
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyIntTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.Int;
        /// <inheritdoc/>
        public override Type Type => typeof(int);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyDoubleTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.Double;
        /// <inheritdoc/>
        public override Type Type => typeof(double);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyBoolTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.Bool;
        /// <inheritdoc/>
        public override Type Type => typeof(bool);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyStringTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.String;
        /// <inheritdoc/>
        public override Type Type => typeof(string);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyHSVAdjustmentTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.HSVAdjustment;
        /// <inheritdoc/>
        public override Type Type => typeof(HSVAdjustment);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyChancePoolInfoTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.ChancePoolInfo;
        /// <inheritdoc/>
        public override Type Type => typeof(ChancePoolInfo);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyIntArrayTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.IntArray;
        /// <inheritdoc/>
        public override Type Type => typeof(int[]);
    }
    #region Enumeration Types
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyEasingTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.Easing;
        /// <inheritdoc/>
        public override Type Type => typeof(Easing);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyInstantCountComparisonTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.InstantCountComparison;
        /// <inheritdoc/>
        public override Type Type => typeof(InstantCountComparison);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyPickupItemPickupModeTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.PickupItemPickupMode;
        /// <inheritdoc/>
        public override Type Type => typeof(PickupItemPickupMode);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyPulseModeTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.PulseMode;
        /// <inheritdoc/>
        public override Type Type => typeof(PulseMode);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyPulseTargetTypeTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.PulseTargetType;
        /// <inheritdoc/>
        public override Type Type => typeof(PulseTargetType);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyTargetPosCoordinatesTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.TargetPosCoordinates;
        /// <inheritdoc/>
        public override Type Type => typeof(TargetPosCoordinates);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyTouchToggleModeTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.TouchToggleMode;
        /// <inheritdoc/>
        public override Type Type => typeof(TouchToggleMode);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyCustomParticleGroupingTypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.CustomParticleGrouping;
        /// <inheritdoc/>
        public override Type Type => typeof(CustomParticleGrouping);
    }
    /// <summary>Declares the object property's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ObjectPropertyCustomParticleProperty1TypeAttribute : ObjectPropertyTypeAttribute
    {
        /// <inheritdoc/>
        public override ObjectPropertyTypeCode TypeCode => ObjectPropertyTypeCode.CustomParticleProperty1;
        /// <inheritdoc/>
        public override Type Type => typeof(CustomParticleProperty1);
    }
    #endregion
}
