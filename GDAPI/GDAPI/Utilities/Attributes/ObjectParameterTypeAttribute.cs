using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Triggers.RandomTrigger;

namespace GDAPI.Utilities.Attributes
{
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public abstract class ObjectParameterTypeAttribute : Attribute
    {
        /// <summary>The type of the object parameter.</summary>
        public abstract Type Type { get; }
        /// <summary>The name of the type of the object parameter.</summary>
        public abstract string TypeName { get; }
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterIntTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<int>
    {
        private static Type type = typeof(int);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterDoubleTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<double>
    {
        private static Type type = typeof(double);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterBoolTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<bool>
    {
        private static Type type = typeof(bool);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterStringTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<string>
    {
        private static Type type = typeof(string);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterHSVAdjustmentTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<HSVAdjustment>
    {
        private static Type type = typeof(HSVAdjustment);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterChancePoolInfoTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<ChancePoolInfo>
    {
        private static Type type = typeof(ChancePoolInfo);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterIntArrayTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<int[]>
    {
        private static Type type = typeof(int[]);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    #region Enumeration Types
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterEasingTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<Easing>
    {
        private static Type type = typeof(Easing);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterInstantCountComparisonTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<InstantCountComparison>
    {
        private static Type type = typeof(InstantCountComparison);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterPickupItemPickupModeTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<PickupItemPickupMode>
    {
        private static Type type = typeof(PickupItemPickupMode);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterPulseModeTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<PulseMode>
    {
        private static Type type = typeof(PulseMode);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterPulseTargetTypeTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<PulseTargetType>
    {
        private static Type type = typeof(PulseTargetType);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterTargetPosCoordinatesTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<TargetPosCoordinates>
    {
        private static Type type = typeof(TargetPosCoordinates);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterTouchToggleModeTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<TouchToggleMode>
    {
        private static Type type = typeof(TouchToggleMode);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterCustomParticleGroupingTypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<CustomParticleGrouping>
    {
        private static Type type = typeof(CustomParticleGrouping);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    /// <summary>Declares the object parameter's data type.</summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ObjectParameterCustomParticleProperty1TypeAttribute : ObjectParameterTypeAttribute, IGenericAttribute<CustomParticleProperty1>
    {
        private static Type type = typeof(CustomParticleProperty1);
        /// <summary>The type of the object parameter.</summary>
        public override Type Type => type;
        /// <summary>The name of the type of the object parameter.</summary>
        public override string TypeName => type.Name;
    }
    #endregion
}
