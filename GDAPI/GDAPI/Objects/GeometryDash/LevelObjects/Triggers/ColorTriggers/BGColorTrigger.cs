using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a BG Color trigger.</summary>
    [ObjectID(TriggerType.BG)]
    public class BGColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the BG Color trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)TriggerType.BG;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => (int)SpecialColorID.BG;

        /// <summary>Initializes a new instance of the <seealso cref="BGColorTrigger"/> class.</summary>
        public BGColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="BGColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public BGColorTrigger(float duration, bool tintGround = false)
            : base(duration, false, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="BGColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BGColorTrigger());
    }
}
