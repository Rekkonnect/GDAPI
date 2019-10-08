using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents an Obj Color trigger.</summary>
    [ObjectID(TriggerType.Obj)]
    public class ObjColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the Obj Color trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Obj;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => (int)SpecialColorID.Obj;

        /// <summary>Initializes a new instance of the <seealso cref="ObjColorTrigger"/> class.</summary>
        public ObjColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ObjColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public ObjColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="ObjColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ObjColorTrigger());
    }
}
