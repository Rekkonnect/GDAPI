using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a Color 2 Color trigger.</summary>
    [ObjectID(TriggerType.Color2)]
    public class Color2ColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the Color 2 Color trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)TriggerType.Color2;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => 2;

        /// <summary>Initializes a new instance of the <seealso cref="Color2ColorTrigger"/> class.</summary>
        public Color2ColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="Color2ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public Color2ColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="Color2ColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new Color2ColorTrigger());
    }
}
