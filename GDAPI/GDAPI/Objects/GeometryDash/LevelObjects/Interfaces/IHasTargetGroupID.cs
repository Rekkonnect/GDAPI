using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents an object which contains a definition for a target Group ID.</summary>
    public interface IHasTargetGroupID : IHasPrimaryID<GroupID>
    {
        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        int TargetGroupID { get; set; }

        GroupID IHasPrimaryID<GroupID>.PrimaryID
        {
            get => TargetGroupID;
            set => TargetGroupID = value.ID;
        }
    }
}
