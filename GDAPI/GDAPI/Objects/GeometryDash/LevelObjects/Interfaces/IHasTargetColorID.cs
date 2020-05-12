using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents a trigger which contains a definition for a target Color ID.</summary>
    public interface IHasTargetColorID : IHasPrimaryID<ColorID>
    {
        /// <summary>The target Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetColorID, 0)]
        int TargetColorID { get; set; }

        ColorID IHasPrimaryID<ColorID>.PrimaryID
        {
            get => TargetColorID;
            set => TargetColorID = value.ID;
        }
    }
}
