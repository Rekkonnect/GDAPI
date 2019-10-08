using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a <seealso cref="SpecialObject"/> whose object ID is constant. The reason it exists is because multiple class inheritance is not allowed in C#.</summary>
    public abstract class ConstantIDSpecialObject : SpecialObject
    {
        /// <summary>The Object ID of the object.</summary>
        // IMPORTANT: If we want to change the object IDs of objects through some function, this has to be reworked
        [ObjectStringMappable(ObjectParameter.ID)]
        public new abstract int ObjectID { get; }

        /// <summary>Initializes a new instance of the <seealso cref="ConstantIDSpecialObject"/> class.</summary>
        public ConstantIDSpecialObject()
        {
            base.ObjectID = ObjectID;
        }
    }
}
