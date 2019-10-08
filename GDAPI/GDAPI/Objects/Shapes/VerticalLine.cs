using GDAPI.Objects.General;

namespace GDAPI.Objects.Shapes
{
    /// <summary>Represents a vertical line shape.</summary>
    public class VerticalLine : Line
    {
        /// <summary>Initializes a new instance of the <seealso cref="VerticalLine"/> class.</summary>
        /// <param name="position">The position of the vertical line.</param>
        public VerticalLine(Point position) : base(position, 90) { }

        /// <summary>Determines whether a point is within the shape (assuming the center of the shape is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public override bool ContainsPoint(Point point) => point.X == Position.X;
    }
}
