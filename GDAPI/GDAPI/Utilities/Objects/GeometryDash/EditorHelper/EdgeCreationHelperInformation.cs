using GDAPI.Utilities.Objects.General.DataStructures;

namespace GDAPI.Utilities.Objects.GeometryDash.EditorHelper
{
    /// <summary>Represents an object which contains information about the objects to create from an original object that are the edge decoration in the object set.</summary>
    public class EdgeCreationHelperInformation
    {
        /// <summary>The object ID of the original object which will be used to create the edge decoration object from.</summary>
        public int OriginalObjectID { get; set; }

        /// <summary>The object IDs of the created object.</summary>
        public Directional<int> CreatedObjectIDs { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="EdgeCreationHelperInformation"/> class.</summary>
        /// <param name="originalObjectID">The original object ID.</param>
        /// <param name="createdObjectIDs">The created object IDs.</param>
        public EdgeCreationHelperInformation(int originalObjectID, Directional<int> createdObjectIDs)
        {
            OriginalObjectID = originalObjectID;
            CreatedObjectIDs = createdObjectIDs;
        }
    }
}
