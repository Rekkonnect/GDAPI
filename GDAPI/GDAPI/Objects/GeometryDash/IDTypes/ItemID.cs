using GDAPI.Enumerations.GeometryDash;
using System;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents an Item ID.</summary>
    public struct ItemID : IID, IEquatable<ItemID>
    {
        public int ID { get; set; }

        public LevelObjectIDType Type => LevelObjectIDType.Item;

        public ItemID(int value) => ID = value;

        public static bool operator ==(ItemID left, ItemID right) => left.Equals(right);
        public static bool operator !=(ItemID left, ItemID right) => !(left == right);

        public static implicit operator ItemID(int value) => new(value);
        public static explicit operator int(ItemID value) => value.ID;

        public bool Equals(ItemID other) => ID == other.ID;
        public override bool Equals(object obj) => ((ItemID)obj).ID == ID;
        public override int GetHashCode() => ((IID)this).GetHashCode();
        public override string ToString() => ID.ToString();
    }
}
