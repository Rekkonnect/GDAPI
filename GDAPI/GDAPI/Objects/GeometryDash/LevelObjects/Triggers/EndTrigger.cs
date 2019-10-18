using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents an End trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.End)]
    public class EndTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;

        /// <summary>The Object ID of the End trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)TriggerType.End;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Reversed property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.Reversed, false)]
        public bool Reversed
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Lock Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.LockY, false)]
        public bool LockY
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="EndTrigger"/> class.</summary>
        public EndTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="EndTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="reversed">The Reversed of the trigger.</param>
        public EndTrigger(int targetGroupID, bool reversed = false, bool lockY = false)
             : base()
        {
            TargetGroupID = targetGroupID;
            Reversed = reversed;
            LockY = lockY;
        }

        /// <summary>Returns a clone of this <seealso cref="EndTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new EndTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as EndTrigger;
            c.targetGroupID = targetGroupID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as EndTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID;
        }
    }
}
