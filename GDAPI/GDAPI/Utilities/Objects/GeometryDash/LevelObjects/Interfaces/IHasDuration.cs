using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for effect duration.</summary>
    public interface IHasDuration
    {
        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration)]
        double Duration { get; set; }
    }
}