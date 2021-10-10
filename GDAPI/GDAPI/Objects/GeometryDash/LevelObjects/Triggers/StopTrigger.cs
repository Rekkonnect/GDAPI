using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Stop trigger.</summary>
    [ObjectID(TriggerType.Stop)]
    public class StopTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;

        /// <summary>The Object ID of the Stop trigger.</summary>
        public override int ConstantObjectID => (int)TriggerType.Stop;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="StopTrigger"/> class.</summary>
        public StopTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="StopTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public StopTrigger(int targetGroupID)
            : base()
        {
            TargetGroupID = targetGroupID;
        }

        /// <summary>Returns a clone of this <seealso cref="StopTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new StopTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as StopTrigger;
            c.targetGroupID = targetGroupID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as StopTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID;
        }
    }
}
