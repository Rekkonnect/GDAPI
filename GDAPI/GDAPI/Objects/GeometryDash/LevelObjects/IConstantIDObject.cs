namespace GDAPI.Objects.GeometryDash.LevelObjects
{
    /// <summary>Represents an object type whose ID is constant.</summary>
    public interface IConstantIDObject
    {
        /// <summary>The Object ID of the object.</summary>
        public abstract int ConstantObjectID { get; }
    }
}
