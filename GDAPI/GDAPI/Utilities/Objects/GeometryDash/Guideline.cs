using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash
{
    /// <summary>Contains information about a level guideline.</summary>
    public class Guideline
    {
        /// <summary>Represents the value of the orange color in the guideline.</summary>
        public const double Orange = 0.8d;
        /// <summary>Represents the value of the yellow color in the guideline.</summary>
        public const double Yellow = 0.9d;
        /// <summary>Represents the value of the green color in the guideline.</summary>
        public const double Green = 1.0d;
        /// <summary>Represents the value of the transparent color in the guideline. It is a hidden game feature, where invisible guidelines may be created.</summary>
        public const double Transparent = 0.7d;

        /// <summary>The timestamp of the guideline.</summary>
        public double TimeStamp { get; set; }
        /// <summary>The color of the guideline.</summary>
        public double Color { get; set; }

        /// <summary>Creates a new empty instance of the <seealso cref="Guideline"/> class.</summary>
        public Guideline() { }
        /// <summary>Creates a new instance of the <seealso cref="Guideline"/> class.</summary>
        /// <param name="timeStamp">The timestamp of the guideline.</param>
        /// <param name="color">The color of the guideline.</param>
        public Guideline(double timeStamp, double color)
        {
            TimeStamp = timeStamp;
            Color = color;
        }
        /// <summary>Creates a new instance of the <seealso cref="Guideline"/> class.</summary>
        /// <param name="timeStamp">The timestamp of the guideline.</param>
        /// <param name="color">The color of the guideline.</param>
        public Guideline(int timeStamp, int color)
        {
            TimeStamp = timeStamp;
            Color = color;
        }
        /// <summary>Creates a new instance of the <seealso cref="Guideline"/> class.</summary>
        /// <param name="timeStamp">The timestamp of the guideline.</param>
        /// <param name="color">The color of the guideline.</param>
        public Guideline(float timeStamp, float color)
        {
            TimeStamp = timeStamp;
            Color = color;
        }
        /// <summary>Creates a new instance of the <seealso cref="Guideline"/> class.</summary>
        /// <param name="timeStamp">The timestamp of the guideline.</param>
        /// <param name="color">The color of the guideline.</param>
        public Guideline(decimal timeStamp, decimal color)
        {
            TimeStamp = (double)timeStamp;
            Color = (double)color;
        }

        /// <summary>Converts the <see cref="Guideline"/> to its string representation in the gamesave.</summary>
        public override string ToString() => $"{TimeStamp}~{Color}";
    }
}