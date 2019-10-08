using System;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash
{
    /// <summary>Rerpesents a speed segment in the level.</summary>
    public class SpeedSegment : IComparable<SpeedSegment>
    {
        /// <summary>The speed of the segment.</summary>
        public Speed Speed { get; set; }
        /// <summary>The starting X of the speed segment.</summary>
        public double X { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="SpeedSegment"/> class.</summary>
        /// <param name="speed">The speed of the speed segment.</param>
        /// <param name="x">The starting X of the speed segment.</param>
        public SpeedSegment(Speed speed, double x)
        {
            Speed = speed;
            X = x;
        }

        /// <summary>Compares this <seealso cref="SpeedSegment"/> object to another based on their X positions.</summary>
        /// <param name="other">The other <seealso cref="SpeedSegment"/> object to compare this to.</param>
        public int CompareTo(SpeedSegment other) => X.CompareTo(other.X);
    }
}
