using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for the color.</summary>
    public interface IHasColor
    {
        /// <summary>The red part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Red, 255)]
        int Red { get; set; }
        /// <summary>The green part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Green, 255)]
        int Green { get; set; }
        /// <summary>The blue part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Blue, 255)]
        int Blue { get; set; }
    }
}
