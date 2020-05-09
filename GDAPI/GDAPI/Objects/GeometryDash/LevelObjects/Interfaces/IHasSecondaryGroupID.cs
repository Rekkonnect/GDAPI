using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a secondary Group ID.</summary>
    public interface IHasSecondaryGroupID : IHasSecondaryID
    {
        /// <summary>The secondary Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.SecondaryGroupID, 0)]
        int SecondaryGroupID { get; set; }

        int IHasSecondaryID.SecondaryID
        {
            get => SecondaryGroupID;
            set => SecondaryGroupID = value;
        }
    }
}
