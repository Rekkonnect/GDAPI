using GDAPI.Enumerations.GeometryDash;
using System.Globalization;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents a Group ID.</summary>
    public struct GroupID : IID
    {
        public int ID { get; set; }

        public LevelObjectIDType Type => LevelObjectIDType.Group;

        public GroupID(int value) => ID = value;

        public static bool operator ==(GroupID left, GroupID right) => left.Equals(right);
        public static bool operator !=(GroupID left, GroupID right) => !(left == right);

        public static implicit operator GroupID(int value) => new GroupID(value);
        public static explicit operator int(GroupID value) => value.ID;

        public bool Equals(GroupID other) => ID == other.ID;
        public override bool Equals(object obj) => ((GroupID)obj).ID == ID;
        public override int GetHashCode() => ((IID)this).GetHashCode();
        public override string ToString() => ID.ToString(CultureInfo.InvariantCulture);
    }
}
