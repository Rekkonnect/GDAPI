using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a target Color ID.</summary>
    public interface IHasTargetColorID
    {
        /// <summary>The target Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetColorID, 0)]
        int TargetColorID { get; set; }
    }
}
