using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a secondary Group ID.</summary>
    public interface IHasSecondaryGroupID : IHasSecondaryID<GroupID>
    {
        /// <summary>The secondary Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.SecondaryGroupID, 0)]
        int SecondaryGroupID { get; set; }

        GroupID IHasSecondaryID<GroupID>.SecondaryID
        {
            get => SecondaryGroupID;
            set => SecondaryGroupID = value.ID;
        }
    }
}
