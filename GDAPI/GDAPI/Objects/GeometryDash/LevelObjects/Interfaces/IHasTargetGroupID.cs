using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a target Group ID.</summary>
    public interface IHasTargetGroupID : IHasPrimaryID
    {
        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        int TargetGroupID { get; set; }
        
        int IHasPrimaryID.PrimaryID
        {
            get => TargetGroupID;
            set => TargetGroupID = value;
        }
    }
}
