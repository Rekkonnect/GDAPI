using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents an object which contains a definition for a copied Color ID.</summary>
    public interface IHasCopiedColorID : IHasSecondaryID<ColorID>
    {
        /// <summary>The copied Color ID of the object.</summary>
        [ObjectStringMappable(ObjectProperty.CopiedColorID, 0)]
        int CopiedColorID { get; set; }

        ColorID IHasSecondaryID<ColorID>.SecondaryID
        {
            get => CopiedColorID;
            set => CopiedColorID = value.ID;
        }
    }
}
