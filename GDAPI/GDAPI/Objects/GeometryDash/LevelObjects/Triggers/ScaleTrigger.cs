using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Scale trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.Scale)]
    public class ScaleTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID, IHasSecondaryGroupID
    {
        private short targetGroupID, centerGroupID;
        private float duration = 0.5f, easingRate, scaleX, scaleY;

        /// <summary>The Object ID of the Scale trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Scale;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration, 0.5d)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The secondary Group ID of the trigger.</summary>
        public int SecondaryGroupID
        {
            get => CenterGroupID;
            set => CenterGroupID = value;
        }
        /// <summary>The easing of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Easing, Easing.None)]
        public Easing Easing { get; set; }
        /// <summary>The Move Y of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.EasingRate, 2d)]
        public double EasingRate
        {
            get => easingRate;
            set => easingRate = (float)value;
        }
        /// <summary>The Scaling Multiplier property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ScaleX, 1d)]
        public double ScaleX
        {
            get => scaleX;
            set => scaleX = (float)value;
        }
        /// <summary>The Scaling Multiplier property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ScaleY, 1d)]
        public double ScaleY
        {
            get => scaleY;
            set => scaleY = (float)value;
        }
        /// <summary>The Center Group ID property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CenterGroupID, 0)]
        public int CenterGroupID
        {
            get => centerGroupID;
            set => centerGroupID = (short)value;
        }
        /// <summary>The Lock Object Scale property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockObjectScale, false)]
        public bool LockObjectScale
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Only Move Scale property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.OnlyMoveScale, false)]
        public bool OnlyMoveScale
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ScaleTrigger"/> class.</summary>
        public ScaleTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ScaleTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        public ScaleTrigger(double duration, int targetGroupID)
             : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ScaleTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="scaling">The Scaling property of the trigger.</param>
        public ScaleTrigger(double duration, int targetGroupID, double scaling)
            : this(duration, targetGroupID)
        {
            Scaling = scaling;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ScaleTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="scaling">The Scaling property of the trigger.</param>
        /// <param name="easing">The easing of the trigger.</param>
        /// <param name="easingRate">The easing rate of the trigger.</param>
        public ScaleTrigger(double duration, int targetGroupID, Easing easing, double easingRate, double scaling)
            : this(duration, targetGroupID, scaling)
        {
            Easing = easing;
            EasingRate = easingRate;
        }

        /// <summary>Returns a clone of this <seealso cref="ScaleTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ScaleTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as ScaleTrigger;
            c.targetGroupID = targetGroupID;
            c.duration = duration;
            c.Easing = Easing;
            c.easingRate = easingRate;
            c.scaleX = scaleX;
            c.scaleY = scaleY;
            c.centerGroupID = centerGroupID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as ScaleTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && duration == z.duration
                && Easing == z.Easing
                && easingRate == z.easingRate
                && scaleX == z.scaleX
                && scaleY == z.scaleY
                && centerGroupID == z.centerGroupID;
        }
    }
}
