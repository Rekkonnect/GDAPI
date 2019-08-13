using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a secondary Group ID.</summary>
    public interface IHasSecondaryGroupID
    {
        /// <summary>The secondary Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SecondaryGroupID)]
        int SecondaryGroupID { get; set; }
    }
}
