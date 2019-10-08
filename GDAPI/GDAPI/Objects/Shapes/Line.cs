using System;
using GDAPI.Objects.General;

namespace GDAPI.Objects.Shapes
{
    /// <summary>Represents a line shape.</summary>
    public class Line : Shape
    {
        /// <summary>Gets the slope ratio of this line shape.</summary>
        public double SlopeRatio => Math.Tan(Rotation * Math.PI / 180);

        /// <summary>Initializes a new instance of the <seealso cref="Line"/> class.</summary>
        /// <param name="position">The position of the line.</param>
        /// <param name="rotation">The rotation of the line in degrees.</param>
        public Line(Point position, double rotation) : base(position, rotation) { }

        /// <summary>Determines whether a point is within the shape (assuming the center of the shape is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public override bool ContainsPoint(Point point) => point.Y == SlopeRatio * (point.X - Position.X) + Position.Y;

        /// <summary>Returns the distance between the center of the shape and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        protected override double CalculateRadiusAtRotation(double rotation) => rotation % 180 == Rotation % 180 ? double.PositiveInfinity : 0;
        /// <summary>Returns the maximum distance between the center of the shape and its edge.</summary>
        public override double GetMaxRadius() => double.PositiveInfinity;

        protected override bool EqualsInheritably(Shape shape) => true;
    }
}
