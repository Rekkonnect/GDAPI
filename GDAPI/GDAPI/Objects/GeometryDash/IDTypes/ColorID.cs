using GDAPI.Enumerations.GeometryDash;
using System;
using System.Globalization;

namespace GDAPI.Objects.GeometryDash.IDTypes
{
    /// <summary>Represents a Color ID.</summary>
    public struct ColorID : IID, IEquatable<ColorID>
    {
        public int ID { get; set; }

        public LevelObjectIDType Type => LevelObjectIDType.Color;

        /// <summary>Determines whether the ID is a Special Color ID.</summary>
        public bool IsSpecialColorID => ID >= 1000;
        /// <summary>Interprets the current Color ID as a <seealso cref="SpecialColorID"/>.</summary>
        public SpecialColorID AsSpecialColorID => (SpecialColorID)ID;

        public ColorID(int value) => ID = value;

        public static bool operator ==(ColorID left, ColorID right) => left.Equals(right);
        public static bool operator !=(ColorID left, ColorID right) => !(left == right);

        public static implicit operator ColorID(int value) => new ColorID(value);
        public static explicit operator int(ColorID value) => value.ID;

        public bool Equals(ColorID other) => ID == other.ID;
        public override bool Equals(object obj) => ((ColorID)obj).ID == ID;
        public override int GetHashCode() => ((IID)this).GetHashCode();
        public override string ToString() => ID.ToString();
    }
}
