using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents an Animate trigger.</summary>
    [ObjectID(TriggerType.Animate)]
    public class AnimateTrigger : Trigger, IHasTargetGroupID
    {
        private byte animationID;
        private short targetGroupID;

        /// <summary>The Object ID of the Animate trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)TriggerType.Animate;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Animation ID property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.AnimationID, 0)]
        public int AnimationID
        {
            get => animationID;
            set => animationID = (byte)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="AnimateTrigger"/> class.</summary>
        public AnimateTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="AnimateTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="animationID">The Animation ID property of the trigger.</summary>
        public AnimateTrigger(int targetGroupID, int animationID)
            : base()
        {
            TargetGroupID = targetGroupID;
            AnimationID = animationID;
        }

        /// <summary>Returns a clone of this <seealso cref="AnimateTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new AnimateTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as AnimateTrigger;
            c.animationID = animationID;
            c.targetGroupID = targetGroupID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as AnimateTrigger;
            return base.EqualsInherited(other)
                && animationID == z.animationID
                && targetGroupID == z.targetGroupID;
        }
    }
}
