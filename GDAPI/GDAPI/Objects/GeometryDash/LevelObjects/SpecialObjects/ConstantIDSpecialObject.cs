namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a <seealso cref="SpecialObject"/> whose object ID is constant. The reason it exists is because multiple class inheritance is not allowed in C#.</summary>
    public abstract class ConstantIDSpecialObject : SpecialObject, IConstantIDObject
    {
        /// <summary>The Object ID of the object.</summary>
        public abstract int ConstantObjectID { get; }

        /// <summary>Initializes a new instance of the <seealso cref="ConstantIDSpecialObject"/> class.</summary>
        public ConstantIDSpecialObject()
        {
            ObjectID = ConstantObjectID;
        }
    }
}
