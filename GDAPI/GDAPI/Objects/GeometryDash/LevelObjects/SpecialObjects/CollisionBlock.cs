using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a collision block.</summary>
    [ObjectID(SpecialObjectType.CollisionBlock)]
    public class CollisionBlock : ConstantIDSpecialObject, IHasPrimaryBlockID
    {
        private short blockID;

        /// <summary>The object ID of the collision block.</summary>
        public override int ConstantObjectID => (int)SpecialObjectType.CollisionBlock;

        /// <summary>The Block ID of the collision block.</summary>
        [ObjectStringMappable(ObjectProperty.BlockID, 0)]
        public int BlockID
        {
            get => blockID;
            set => blockID = (short)value;
        }
        /// <summary>The Block ID of the collision block.</summary>
        public int PrimaryBlockID
        {
            get => BlockID;
            set => BlockID = value;
        }
        /// <summary>The Dynamic Block property of the collision block.</summary>
        [ObjectStringMappable(ObjectProperty.DynamicBlock, false)]
        public bool DynamicBlock
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="CollisionBlock"/> class.</summary>
        public CollisionBlock() : base() { }

        /// <summary>Returns a clone of this <seealso cref="CollisionBlock"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new CollisionBlock());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as CollisionBlock;
            c.blockID = blockID;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as CollisionBlock;
            return base.EqualsInherited(other)
                && blockID == z.blockID;
        }
    }
}
