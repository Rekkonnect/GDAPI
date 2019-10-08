using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a Color 4 Color trigger.</summary>
    [ObjectID(TriggerType.Color4)]
    public class Color4ColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the Color 4 Color trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Color4;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => 4;

        /// <summary>Initializes a new instance of the <seealso cref="Color4ColorTrigger"/> class.</summary>
        public Color4ColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="Color4ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public Color4ColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="Color4ColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new Color4ColorTrigger());
    }
}
