namespace GDAPI.Objects.GeometryDash.LevelObjects
{
    /// <summary>Represents an object whose object ID is constant.</summary>
    public abstract class ConstantIDObject : GeneralObject, IConstantIDObject
    {
        /// <summary>The Object ID of the object.</summary>
        public abstract int ConstantObjectID { get; }

        /// <summary>Initializes a new instance of the <seealso cref="ConstantIDObject"/> class.</summary>
        public ConstantIDObject()
        {
            ObjectID = ConstantObjectID;
        }
    }
}
