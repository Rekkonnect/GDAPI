using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a primary Item ID.</summary>
    public interface IHasPrimaryItemID : IHasPrimaryID
    {
        /// <summary>The primary Item ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ItemID, 0)]
        int PrimaryItemID { get; set; }

        int IHasPrimaryID.PrimaryID
        {
            get => PrimaryItemID;
            set => PrimaryItemID = value;
        }
    }
}
