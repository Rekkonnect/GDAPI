using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a primary Item ID.</summary>
    public interface IHasPrimaryItemID
    {
        /// <summary>The primary Item ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ItemID, 0)]
        int PrimaryItemID { get; set; }
    }
}
