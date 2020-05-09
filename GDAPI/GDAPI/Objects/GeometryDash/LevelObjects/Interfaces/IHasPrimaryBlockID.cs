using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a primary Block ID.</summary>
    public interface IHasPrimaryBlockID : IHasPrimaryID
    {
        /// <summary>The primary Block ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.BlockAID, 0)]
        int PrimaryBlockID { get; set; }

        int IHasPrimaryID.PrimaryID
        {
            get => PrimaryBlockID;
            set => PrimaryBlockID = value;
        }
    }
}
