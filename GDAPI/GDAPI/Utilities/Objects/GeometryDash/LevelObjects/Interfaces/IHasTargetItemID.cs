using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a target Item ID.</summary>
    public interface IHasTargetItemID
    {
        /// <summary>The target Item ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ItemID, 0)]
        int TargetItemID { get; set; }
    }
}
