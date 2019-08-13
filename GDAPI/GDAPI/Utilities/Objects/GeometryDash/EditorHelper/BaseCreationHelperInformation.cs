using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.EditorHelper
{
    /// <summary>Represents an object which contains information about the objects to create from an original object that are the base decoration in the object set.</summary>
    public class BaseCreationHelperInformation
    {
        /// <summary>The object ID of the original object which will be used to create the base decoration object from.</summary>
        public int OriginalObjectID { get; set; }

        /// <summary>The object ID of the created object.</summary>
        public int CreatedObjectID { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="BaseCreationHelperInformation"/> class.</summary>
        /// <param name="originalObjectID">The original object ID.</param>
        /// <param name="createdObjectID">The created object ID.</param>
        public BaseCreationHelperInformation(int originalObjectID, int createdObjectID)
        {
            OriginalObjectID = originalObjectID;
            CreatedObjectID = createdObjectID;
        }
    }
}
