using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents a Block ID.</summary>
    public struct BlockID : IID
    {
        public int ID { get; set; }

        public LevelObjectIDType Type => LevelObjectIDType.Block;

        public BlockID(int value) => ID = value;

        public static implicit operator BlockID(int value) => new BlockID(value);
    }
}
