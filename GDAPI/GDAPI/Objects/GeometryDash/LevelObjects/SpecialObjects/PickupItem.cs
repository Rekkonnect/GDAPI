using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Information.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a pickup item.</summary>
    public class PickupItem : SpecialObject, IHasTargetGroupID, IHasTargetItemID
    {
        private short targetGroupID, targetItemID;

        /// <summary>The valid object IDs of the special object.</summary>
        protected override int[] ValidObjectIDs => ObjectLists.PickupItemList;
        /// <summary>The name as a string of the special object.</summary>
        protected override string SpecialObjectTypeName => "pickup item";

        /// <summary>Represents the Pickup Mode property of the pickup item.</summary>
        [ObjectStringMappable(ObjectProperty.PickupMode, PickupItemPickupMode.None)]
        public PickupItemPickupMode PickupMode { get; set; }
        /// <summary>Represents the Target Item ID of the pickup item.</summary>
        [ObjectStringMappable(ObjectProperty.ItemID)]
        public int TargetItemID
        {
            get => targetItemID;
            set => targetItemID = (short)value;
        }
        /// <summary>Represents the Subtract Count property of the pickup item.</summary>
        [ObjectStringMappable(ObjectProperty.SubtractCount, false)]
        public bool SubtractCount
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }
        /// <summary>Represents the Target Group ID property of the pickup item.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>Represents the Enable Group property of the pickup item.</summary>
        [ObjectStringMappable(ObjectProperty.ActivateGroup, false)]
        public bool EnableGroup
        {
            get => SpecialObjectBools[1];
            set => SpecialObjectBools[1] = value;
        }

        /// <summary>Initializes a new empty instance of the <seealso cref="PickupItem"/> class. For internal use only.</summary>
        private PickupItem() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="PickupItem"/> class.</summary>
        /// <param name="objectID">The object ID of the pickup item.</param>
        public PickupItem(int objectID) : base(objectID) { }
        /// <summary>Initializes a new instance of the <seealso cref="PickupItem"/> class.</summary>
        /// <param name="objectID">The object ID of the pickup item.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public PickupItem(int objectID, double x, double y)
            : base(objectID, x, y) { }

        /// <summary>Returns a clone of this <seealso cref="PickupItem"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new PickupItem());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as PickupItem;
            c.PickupMode = PickupMode;
            c.targetItemID = targetItemID;
            c.targetGroupID = targetGroupID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as PickupItem;
            return base.EqualsInherited(other)
                && PickupMode == z.PickupMode
                && targetItemID == z.targetItemID
                && targetGroupID == z.targetGroupID;
        }
    }
}
