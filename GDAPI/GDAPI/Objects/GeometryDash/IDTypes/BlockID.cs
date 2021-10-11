using GDAPI.Enumerations.GeometryDash;
using System;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents a Block ID.</summary>
    public struct BlockID : IID, IEquatable<BlockID>
    {
        public int ID { get; set; }

        public LevelObjectIDType Type => LevelObjectIDType.Block;

        public BlockID(int value) => ID = value;

        public static bool operator ==(BlockID left, BlockID right) => left.Equals(right);
        public static bool operator !=(BlockID left, BlockID right) => !(left == right);

        public static implicit operator BlockID(int value) => new(value);
        public static explicit operator int(BlockID value) => value.ID;

        public bool Equals(BlockID other) => ID == other.ID;
        public override bool Equals(object obj) => ((BlockID)obj).ID == ID;
        public override int GetHashCode() => ((IID)this).GetHashCode();
        public override string ToString() => ID.ToString();
    }
}
