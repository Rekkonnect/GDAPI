using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Count trigger.</summary>
    [ObjectID(TriggerType.Count)]
    public class CountTrigger : Trigger, IHasTargetGroupID, IHasPrimaryItemID
    {
        private short targetGroupID, itemID;

        int IHasPrimaryID.PrimaryID
        {
            get => (this as IHasTargetGroupID).PrimaryID;
            set => (this as IHasTargetGroupID).PrimaryID = value;
        }

        /// <summary>The Object ID of the Count trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)TriggerType.Count;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Item ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ItemID, 0)]
        public int ItemID
        {
            get => itemID;
            set => itemID = (short)value;
        }
        /// <summary>The primary Item ID of the trigger.</summary>
        public int PrimaryItemID
        {
            get => ItemID;
            set => ItemID = value;
        }
        /// <summary>The Target Count property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.Count, 0)]
        public int TargetCount { get; set; }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ActivateGroup, false)]
        public bool ActivateGroup
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Multi Activate property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.MultiActivate, false)]
        public bool MultiActivate
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        
        /// <summary>Initializes a new instance of the <seealso cref="CountTrigger"/> class.</summary>
        public CountTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="CountTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="itemID">The Item ID of the trigger.</param>
        /// <param name="count">The Target Count property of the trigger.</param>
        /// <param name="activateGroup">The Activate Group property of the trigger.</param>
        /// <param name="multiActivate">The Multi Activate property of the trigger.</param>
        public CountTrigger(int targetGroupID, int itemID, int targetCount, bool activateGroup = false, bool multiActivate = false)
            : base()
        {
            TargetGroupID = targetGroupID;
            ItemID = itemID;
            TargetCount = targetCount;
            ActivateGroup = activateGroup;
            MultiActivate = multiActivate;
        }

        /// <summary>Returns a clone of this <seealso cref="CountTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new CountTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as CountTrigger;
            c.targetGroupID = targetGroupID;
            c.itemID = itemID;
            c.TargetCount = TargetCount;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as CountTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && itemID == z.itemID
                && TargetCount == z.TargetCount;
        }
    }
}
