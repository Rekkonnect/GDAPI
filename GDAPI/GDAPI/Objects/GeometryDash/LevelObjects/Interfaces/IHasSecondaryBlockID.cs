using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a secondary Block ID.</summary>
    public interface IHasSecondaryBlockID : IHasSecondaryID<BlockID>
    {
        /// <summary>The secondary Block ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.BlockBID, 0)]
        int SecondaryBlockID { get; set; }

        BlockID IHasSecondaryID<BlockID>.SecondaryID
        {
            get => SecondaryBlockID;
            set => SecondaryBlockID = value.ID;
        }
    }
}
