using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a Color 3 Color trigger.</summary>
    [ObjectID(TriggerType.Color3)]
    public class Color3ColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the Color 3 Color trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)TriggerType.Color3;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => 3;

        /// <summary>Initializes a new instance of the <seealso cref="Color3ColorTrigger"/> class.</summary>
        public Color3ColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="Color3ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public Color3ColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="Color3ColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new Color3ColorTrigger());
    }
}
