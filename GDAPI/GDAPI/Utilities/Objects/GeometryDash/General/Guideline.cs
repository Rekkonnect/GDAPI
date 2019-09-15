namespace GDAPI.Utilities.Objects.GeometryDash.General
{
    /// <summary>Contains information about a level guideline.</summary>
    public class Guideline
    {
        /// <summary>The timestamp of the guideline.</summary>
        public double TimeStamp { get; set; }
        /// <summary>The color of the guideline.</summary>
        public GuidelineColor Color { get; set; }

        /// <summary>Determines whether the guideline color is transparent.</summary>
        public bool IsTransparent => Color.IsTransparent;
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
        public Guideline(double timeStamp, double color)
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

        /// <summary>Converts the <see cref="Guideline"/> to its string representation in the gamesave.</summary>
        public override string ToString() => $"{TimeStamp}~{Color}";
    }
}