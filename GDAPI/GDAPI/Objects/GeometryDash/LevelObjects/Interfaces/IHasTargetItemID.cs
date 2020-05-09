using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a target Item ID.</summary>
    public interface IHasTargetItemID : IHasPrimaryID
    {
        /// <summary>The target Item ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ItemID, 0)]
        int TargetItemID { get; set; }

        int IHasPrimaryID.PrimaryID
        {
            get => TargetItemID;
            set => TargetItemID = value;
        }
    }
}
