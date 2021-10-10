using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a trigger orb.</summary>
    [ObjectID(OrbType.TriggerOrb)]
    public class TriggerOrb : Orb, IHasTargetGroupID
    {
        private short targetGroupID;

        /// <summary>The object ID of the trigger orb.</summary>
        public override int ConstantObjectID => (int)OrbType.TriggerOrb;

        /// <summary>Represents the Target Group ID of the trigger orb.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>Represents the Activate Group property of the trigger orb.</summary>
        [ObjectStringMappable(ObjectProperty.ActivateGroup, false)]
        public bool ActivateGroup
        {
            get => SpecialObjectBools[3];
            set => SpecialObjectBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="TriggerOrb"/> class.</summary>
        public TriggerOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="TriggerOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new TriggerOrb());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as TriggerOrb;
            c.targetGroupID = targetGroupID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as TriggerOrb;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID;
        }
    }
}
