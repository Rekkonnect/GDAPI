namespace GDAPI.Objects.GeometryDash.EditorHelper
{
    /// <summary>Represents an object which contains information about the objects to create from an original object that are the outline decoration in the object set.</summary>
    public class OutlineCreationHelperInformation
    {
        /// <summary>The object ID of the original object which will be used to create the outline decoration object from.</summary>
        public int OriginalObjectID { get; set; }

        /// <summary>The object ID of the created object.</summary>
        public int CreatedObjectID { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="OutlineCreationHelperInformation"/> class.</summary>
        /// <param name="originalObjectID">The original object ID.</param>
        /// <param name="createdObjectID">The created object ID.</param>
        public OutlineCreationHelperInformation(int originalObjectID, int createdObjectID)
        {
            OriginalObjectID = originalObjectID;
            CreatedObjectID = createdObjectID;
        }
    }
}
