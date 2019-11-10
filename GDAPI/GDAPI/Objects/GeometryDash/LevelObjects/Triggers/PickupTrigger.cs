using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Pickup trigger.</summary>
    [ObjectID(TriggerType.Pickup)]
    public class PickupTrigger : Trigger, IHasTargetItemID
    {
        private short targetItemID;

        /// <summary>The Object ID of the Pickup trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)TriggerType.Pickup;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ItemID, 0)]
        public int TargetItemID
        {
            get => targetItemID;
            set => targetItemID = (short)value;
        }
        /// <summary>The Count property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.Count, 0)]
        public int Count { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="PickupTrigger"/> class.</summary>
        public PickupTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="PickupTrigger"/> class.</summary>
        /// <param name="targetItemID">The target Item ID of the trigger.</param>
        /// <param name="count">The Count property of the trigger.</param>
        public PickupTrigger(int targetItemID, int count)
            : base()
        {
            TargetItemID = targetItemID;
            Count = count;
        }

        /// <summary>Returns a clone of this <seealso cref="PickupTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new PickupTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as PickupTrigger;
            c.targetItemID = targetItemID;
            c.Count = Count;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as PickupTrigger;
            return base.EqualsInherited(other)
                && targetItemID == z.targetItemID
                && Count == z.Count;
        }
    }
}
