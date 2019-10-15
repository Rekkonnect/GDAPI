using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a GRND2 Color trigger.</summary>
    [ObjectID(TriggerType.GRND2)]
    public class GRND2ColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the GRND2 Color trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)TriggerType.GRND2;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => (int)SpecialColorID.GRND2;

        /// <summary>Initializes a new instance of the <seealso cref="GRND2ColorTrigger"/> class.</summary>
        public GRND2ColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="GRND2ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public GRND2ColorTrigger(float duration, bool tintGround = false)
            : base(duration, false, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="GRND2ColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GRND2ColorTrigger());
    }
}
