using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a primary Block ID.</summary>
    public interface IHasPrimaryBlockID
    {
        /// <summary>The primary Block ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.BlockAID, 0)]
        int PrimaryBlockID { get; set; }
    }
}
