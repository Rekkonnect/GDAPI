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
    /// <summary>Represents a Move trigger.</summary>
    [ObjectID(TriggerType.Move)]
    public class MoveTrigger : Trigger, IHasDuration, IHasEasing, IHasTargetGroupID, IHasSecondaryGroupID
    {
        private short targetGroupID, targetPosGroupID;
        private float duration = 0.5f, easingRate, moveX, moveY;

        /// <summary>The Object ID of the Move trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Move;

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
            get => TargetPosGroupID;
            set => TargetPosGroupID = value;
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
        /// <summary>The Move X property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.OffsetX, 0d)]
        public double MoveX
        {
            get => moveX;
            set => moveX = (float)value;
        }
        /// <summary>The Move Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.OffsetY, 0d)]
        public double MoveY
        {
            get => moveY;
            set => moveY = (float)value;
        }
        /// <summary>The Lock to Player X property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockToPlayerX, false)]
        public bool LockToPlayerX
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Lock to Player Y property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.LockToPlayerY, false)]
        public bool LockToPlayerY
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        /// <summary>The Enable Use Target property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.EnableUseTarget, false)]
        public bool EnableUseTarget
        {
            get => TriggerBools[5];
            set => TriggerBools[5] = value;
        }
        /// <summary>The Lock to Camera X property of the trigger.</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.LockToCameraX, false)]
        public bool LockToCameraX
        {
            get => TriggerBools[6];
            set => TriggerBools[6] = value;
        }
        /// <summary>The Lock to Camera Y property of the trigger.</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.LockToCameraY, false)]
        public bool LockToCameraY
        {
            get => TriggerBools[7];
            set => TriggerBools[7] = value;
        }
        /// <summary>The Target Pos Group ID property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetPosGroupID, 0)]
        public int TargetPosGroupID
        {
            get => targetPosGroupID;
            set => targetPosGroupID = (short)value;
        }
        /// <summary>The Target Pos coordinates property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetPosCoordinates, TargetPosCoordinates.Both)]
        public TargetPosCoordinates TargetPosCoordinates { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        public MoveTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="lockToPlayerX">The Lock to Player X property of the trigger.</param>
        /// <param name="lockToPlayerY">The Lock to Player Y property of the trigger.</param>
        public MoveTrigger(double duration, int targetGroupID, bool lockToPlayerX = false, bool lockToPlayerY = false)
            : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
            LockToPlayerX = lockToPlayerX;
            LockToPlayerY = lockToPlayerY;
        }
        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="moveX">The Move X of the trigger.</param>
        /// <param name="moveY">The Move Y of the trigger.</param>
        /// <param name="lockToPlayerX">The Lock to Player X property of the trigger.</param>
        /// <param name="lockToPlayerY">The Lock to Player Y property of the trigger.</param>
        public MoveTrigger(double duration, int targetGroupID, double moveX, double moveY, bool lockToPlayerX = false, bool lockToPlayerY = false)
            : this(duration, targetGroupID, lockToPlayerX, lockToPlayerY)
        {
            MoveX = moveX;
            MoveY = moveY;
        }
        /// <summary>Initializes a new instance of the <seealso cref="MoveTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="moveX">The Move X of the trigger.</param>
        /// <param name="moveY">The Move Y of the trigger.</param>
        /// <param name="easing">The easing of the trigger.</param>
        /// <param name="easingRate">The easing rate of the trigger.</param>
        /// <param name="lockToPlayerX">The Lock to Player X property of the trigger.</param>
        /// <param name="lockToPlayerY">The Lock to Player Y property of the trigger.</param>
        public MoveTrigger(double duration, int targetGroupID, Easing easing, double easingRate, double moveX, double moveY, bool lockToPlayerX = false, bool lockToPlayerY = false)
            : this(duration, targetGroupID, moveX, moveY, lockToPlayerX, lockToPlayerY)
        {
            Easing = easing;
            EasingRate = easingRate;
        }
        // Add more constructors? They seem to be many already

        /// <summary>Returns a clone of this <seealso cref="MoveTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new MoveTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as MoveTrigger;
            c.targetGroupID = targetGroupID;
            c.duration = duration;
            c.TargetPosGroupID = TargetPosGroupID;
            c.Easing = Easing;
            c.easingRate = easingRate;
            c.moveX = moveX;
            c.moveY = moveY;
            c.TargetPosCoordinates = TargetPosCoordinates;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as MoveTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && duration == z.duration
                && Easing == z.Easing
                && easingRate == z.easingRate
                && moveX == z.moveX
                && moveY == z.moveY
                && TargetPosCoordinates == z.TargetPosCoordinates;
        }
    }
}
