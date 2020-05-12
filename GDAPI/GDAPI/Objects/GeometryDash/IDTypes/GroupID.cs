using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents a Group ID.</summary>
    public struct GroupID : IID
    {
        public int ID { get; set; }

        public LevelObjectIDType Type => LevelObjectIDType.Group;

        public GroupID(int value) => ID = value;

        public static implicit operator GroupID(int value) => new GroupID(value);
    }
}
