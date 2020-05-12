using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a primary Item ID.</summary>
    public interface IHasPrimaryItemID : IHasPrimaryID<ItemID>
    {
        /// <summary>The primary Item ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ItemID, 0)]
        int PrimaryItemID { get; set; }

        ItemID IHasPrimaryID<ItemID>.PrimaryID
        {
            get => PrimaryItemID;
            set => PrimaryItemID = value.ID;
        }
    }
}
