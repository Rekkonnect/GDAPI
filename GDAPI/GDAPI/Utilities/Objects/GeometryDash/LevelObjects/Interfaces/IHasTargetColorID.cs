using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a target Color ID.</summary>
    public interface IHasTargetColorID
    {
        /// <summary>The target Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetColorID)]
        int TargetColorID { get; set; }
    }
}
