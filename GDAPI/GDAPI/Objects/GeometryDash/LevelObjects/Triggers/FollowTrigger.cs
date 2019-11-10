using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Follow trigger.</summary>
    [ObjectID(TriggerType.Follow)]
    public class FollowTrigger : Trigger, IHasDuration, IHasTargetGroupID, IHasSecondaryGroupID
    {
        private short targetGroupID, followGroupID;
        private float duration = 0.5f, xMod = 1, yMod = 1;

        /// <summary>The Object ID of the Follow trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)TriggerType.Follow;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectProperty.Duration, 0.5d)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The secondary Group ID of the trigger.</summary>
        public int SecondaryGroupID
        {
            get => FollowGroupID;
            set => FollowGroupID = value;
        }
        /// <summary>The X Mod property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.XMod, 1d)]
        public double XMod
        {
            get => xMod;
            set => xMod = (float)value;
        }
        /// <summary>The Y Mod property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.YMod, 1d)]
        public double YMod
        {
            get => yMod;
            set => yMod = (float)value;
        }
        /// <summary>The Follow Group ID property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.FollowGroupID, 0)]
        public int FollowGroupID
        {
            get => followGroupID;
            set => followGroupID = (short)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        public FollowTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public FollowTrigger(double duration, int targetGroupID)
            : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
        }
        /// <summary>Initializes a new instance of the <seealso cref="FollowTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="xMod">The X Mod of the trigger.</param>
        /// <param name="yMod">The Y Mod of the trigger.</param>
        public FollowTrigger(double duration, int targetGroupID, double xMod, double yMod)
            : this(duration, targetGroupID)
        {
            XMod = xMod;
            YMod = yMod;
        }

        /// <summary>Returns a clone of this <seealso cref="FollowTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new FollowTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as FollowTrigger;
            c.targetGroupID = targetGroupID;
            c.duration = duration;
            c.xMod = xMod;
            c.yMod = yMod;
            c.followGroupID = followGroupID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as FollowTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && duration == z.duration
                && xMod == z.xMod
                && yMod == z.yMod
                && followGroupID == z.followGroupID;
        }
    }
}
