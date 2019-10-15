using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for effect duration.</summary>
    public interface IHasDuration
    {
        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectProperty.Duration, 0.5d)]
        double Duration { get; set; }
    }
}