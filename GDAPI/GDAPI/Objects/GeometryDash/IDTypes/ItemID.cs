using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents an Item ID.</summary>
    public struct ItemID : IID
    {
        public int ID { get; set; }

        public LevelObjectIDType Type => LevelObjectIDType.Item;

        public ItemID(int value) => ID = value;

        public static implicit operator ItemID(int value) => new ItemID(value);
    }
}
