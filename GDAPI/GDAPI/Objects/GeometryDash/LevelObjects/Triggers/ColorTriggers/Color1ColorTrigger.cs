using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a Color 1 Color trigger.</summary>
    [ObjectID(TriggerType.Color1)]
    public class Color1ColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the Color 1 Color trigger.</summary>
        public override int ConstantObjectID => (int)TriggerType.Color1;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => 1;

        /// <summary>Initializes a new instance of the <seealso cref="Color1ColorTrigger"/> class.</summary>
        public Color1ColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="Color1ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public Color1ColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="Color1ColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new Color1ColorTrigger());
    }
}
