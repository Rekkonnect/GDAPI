using System;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Contains information about a level guideline.</summary>
    public class Guideline : IComparable<Guideline>, IEquatable<Guideline>
    {
        /// <summary>The timestamp of the guideline.</summary>
        public double TimeStamp { get; set; }
        /// <summary>The color of the guideline.</summary>
        public GuidelineColor Color { get; set; }

        /// <summary>Determines whether the guideline color is orange.</summary>
        public bool IsOrange => Color.IsOrange;
        /// <summary>Determines whether the guideline color is yellow.</summary>
        public bool IsYellow => Color.IsYellow;
        /// <summary>Determines whether the guideline color is green.</summary>
        public bool IsGreen => Color.IsGreen;

        /// <summary>Creates a new empty instance of the <seealso cref="Guideline"/> class.</summary>
        public Guideline() { }
        /// <summary>Creates a new instance of the <seealso cref="Guideline"/> class.</summary>
        /// <param name="timeStamp">The timestamp of the guideline.</param>
        /// <param name="color">The color of the guideline.</param>
        public Guideline(double timeStamp, GuidelineColor color)
        {
            TimeStamp = timeStamp;
            Color = color;
        }
        /// <summary>Creates a new instance of the <seealso cref="Guideline"/> class.</summary>
        /// <param name="timeStamp">The timestamp of the guideline.</param>
        /// <param name="color">The color of the guideline.</param>
        public Guideline(float timeStamp, GuidelineColor color)
        {
            TimeStamp = timeStamp;
            Color = color;
        }
        /// <summary>Creates a new instance of the <seealso cref="Guideline"/> class.</summary>
        /// <param name="timeStamp">The timestamp of the guideline.</param>
        /// <param name="color">The color of the guideline.</param>
        public Guideline(decimal timeStamp, GuidelineColor color)
        {
            TimeStamp = (double)timeStamp;
            Color = color;
        }

        /// <summary>Compares this <seealso cref="Guideline"/> to another primarily based on their time stamps and secondarily on their colors.</summary>
        /// <param name="other">The other <seealso cref="Guideline"/> to compare this to.</param>
        public int CompareTo(Guideline other)
        {
            int result = TimeStamp.CompareTo(other.TimeStamp);
            if (result == 0)
                return Color.CompareTo(other.Color);
            return result;
        }

        public static bool operator ==(Guideline left, Guideline right) => left.TimeStamp == right.TimeStamp && left.Color == right.Color;
        public static bool operator !=(Guideline left, Guideline right) => left.TimeStamp != right.TimeStamp || left.Color != right.Color;

        public bool Equals(Guideline other) => this == other;
        public override int GetHashCode() => HashCode.Combine(TimeStamp, Color);
        public override bool Equals(object? obj) => obj is Guideline g && Equals(g);
        /// <summary>Converts the <see cref="Guideline"/> to its string representation in the gamesave.</summary>
        public override string ToString() => $"{TimeStamp}~{Color}";
    }
}