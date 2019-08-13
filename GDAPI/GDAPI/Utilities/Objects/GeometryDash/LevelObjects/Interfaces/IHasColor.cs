using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for the color.</summary>
    public interface IHasColor
    {
        /// <summary>The red part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Red)]
        int Red { get; set; }
        /// <summary>The green part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Green)]
        int Green { get; set; }
        /// <summary>The blue part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Blue)]
        int Blue { get; set; }
    }
}
