using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents a Color ID.</summary>
    public struct ColorID : IID
    {
        public int ID { get; set; }

        public LevelObjectIDType Type => LevelObjectIDType.Color;
        public bool IsSpecialColorID => ID >= 1000;

        public ColorID(int value) => ID = value;

        public static implicit operator ColorID(int value) => new ColorID(value);
    }
}
