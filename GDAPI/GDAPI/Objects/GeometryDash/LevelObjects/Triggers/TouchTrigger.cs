using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Touch trigger.</summary>
    [ObjectID(TriggerType.Touch)]
    public class TouchTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;

        /// <summary>The Object ID of the Touch trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)TriggerType.Touch;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Hold Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.HoldMode, false)]
        public bool HoldMode
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Dual Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.DualMode, false)]
        public bool DualMode
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        /// <summary>The Toggle Mode property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ToggleMode, TouchToggleMode.Default)]
        public TouchToggleMode ToggleMode { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="TouchTrigger"/> class.</summary>
        public TouchTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="TouchTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="holdMode">The Hold Mode property of the trigger.</param>
        /// <param name="dualMode">The Dual Mode property of the trigger.</param>
        /// <param name="toggleMode">The Toggle Mode property of the trigger.</param>
        public TouchTrigger(int targetGroupID, bool holdMode = false, bool dualMode = false, TouchToggleMode toggleMode = TouchToggleMode.Default)
            : base()
        {
            TargetGroupID = targetGroupID;
            HoldMode = holdMode;
            DualMode = dualMode;
            ToggleMode = toggleMode;
        }

        /// <summary>Returns a clone of this <seealso cref="TouchTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new TouchTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as TouchTrigger;
            c.targetGroupID = targetGroupID;
            c.ToggleMode = ToggleMode;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as TouchTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && ToggleMode == z.ToggleMode;
        }
    }
}
