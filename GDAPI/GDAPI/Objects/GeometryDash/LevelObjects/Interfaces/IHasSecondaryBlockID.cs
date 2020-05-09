using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a secondary Block ID.</summary>
    public interface IHasSecondaryBlockID : IHasSecondaryID
    {
        /// <summary>The secondary Block ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.BlockBID, 0)]
        int SecondaryBlockID { get; set; }

        int IHasSecondaryID.SecondaryID
        {
            get => SecondaryBlockID;
            set => SecondaryBlockID = value;
        }
    }
}
