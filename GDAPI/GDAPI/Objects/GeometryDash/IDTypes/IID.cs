using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents an ID.</summary>
    public interface IID
    {
        /// <summary>The ID value.</summary>
        int ID { get; set; }

        /// <summary>The type of the ID.</summary>
        LevelObjectIDType Type { get; }
    }
}
