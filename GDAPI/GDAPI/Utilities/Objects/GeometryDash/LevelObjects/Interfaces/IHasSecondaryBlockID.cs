using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a secondary Block ID.</summary>
    public interface IHasSecondaryBlockID
    {
        /// <summary>The secondary Block ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.BlockBID)]
        int SecondaryBlockID { get; set; }
    }
}
