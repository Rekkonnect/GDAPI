using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a primary Block ID.</summary>
    public interface IHasPrimaryBlockID : IHasPrimaryID<BlockID>
    {
        /// <summary>The primary Block ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.BlockAID, 0)]
        int PrimaryBlockID { get; set; }

        BlockID IHasPrimaryID<BlockID>.PrimaryID
        {
            get => PrimaryBlockID;
            set => PrimaryBlockID = value.ID;
        }
    }
}
