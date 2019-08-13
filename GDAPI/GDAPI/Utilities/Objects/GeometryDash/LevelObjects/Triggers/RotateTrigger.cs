using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Rotate trigger.</summary>
    [ObjectID(TriggerType.Rotate)]
    public class RotateTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID, IHasSecondaryGroupID
    {
        private short targetGroupID, centerGroupID;
        private float duration = 0.5f, easingRate;

        /// <summary>The Object ID of the Rotate trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Rotate;

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
        /// <summary>The Degrees property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Degrees, 0)]
        public int Degrees { get; set; }
        /// <summary>The Times 360 property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Times360, 0)]
        public int Times360 { get; set; }
        /// <summary>The Lock Object Rotation property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockObjectRotation, false)]
        public bool LockObjectRotation
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Center Group ID property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CenterGroupID, 0)]
        public int CenterGroupID
        {
            get => centerGroupID;
            set => centerGroupID = (short)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        public RotateTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="lockObjectRotation">The Lock Object Rotation property of the trigger.</param>
        public RotateTrigger(double duration, int targetGroupID, bool lockObjectRotation = false)
            : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
            LockObjectRotation = lockObjectRotation;
        }
        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="degrees">The Degrees property of the trigger.</param>
        /// <param name="times360">The Times 360 property of the trigger.</param>
        /// <param name="lockObjectRotation">The Lock Object Rotation property of the trigger.</param>
        public RotateTrigger(double duration, int targetGroupID, int degrees, int times360, bool lockObjectRotation = false)
            : this(duration, targetGroupID, lockObjectRotation)
        {
            Degrees = degrees;
            Times360 = times360;
        }
        /// <summary>Initializes a new instance of the <seealso cref="RotateTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="degrees">The Degrees property of the trigger.</param>
        /// <param name="times360">The Times 360 property of the trigger.</param>
        /// <param name="easing">The easing of the trigger.</param>
        /// <param name="easingRate">The easing rate of the trigger.</param>
        /// <param name="lockObjectRotation">The Lock Object Rotation property of the trigger.</param>
        public RotateTrigger(double duration, int targetGroupID, Easing easing, double easingRate, int degrees, int times360, bool lockObjectRotation = false)
            : this(duration, targetGroupID, degrees, times360, lockObjectRotation)
        {
            Easing = easing;
            EasingRate = easingRate;
        }

        /// <summary>Returns a clone of this <seealso cref="RotateTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RotateTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as RotateTrigger;
            c.targetGroupID = targetGroupID;
            c.duration = duration;
            c.Easing = Easing;
            c.easingRate = easingRate;
            c.Degrees = Degrees;
            c.Times360 = Times360;
            c.centerGroupID = centerGroupID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as RotateTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && duration == z.duration
                && Easing == z.Easing
                && easingRate == z.easingRate
                && Degrees == z.Degrees
                && Times360 == z.Times360
                && centerGroupID == z.centerGroupID;
        }
    }
}
