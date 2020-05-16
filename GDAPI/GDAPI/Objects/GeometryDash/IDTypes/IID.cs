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

        /// <summary>Gets the hash code of the ID, based on the ID value and its type</summary>
        sealed int GetHashCode() => ID.GetHashCode() ^ ((int)Type << 28);
    }
}
