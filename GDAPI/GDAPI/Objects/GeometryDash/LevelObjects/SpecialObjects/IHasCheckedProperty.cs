using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a special object that has the checked property.</summary>
    public interface IHasCheckedProperty
    {
        /// <summary>Represents the Checked property of the special object.</summary>
        [ObjectStringMappable(ObjectParameter.SpecialObjectChecked, null, true)]
        bool Checked { get; set; }
    }
}
