using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Objects.General;

namespace GDAPI.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a circular shape.</summary>
    public class Circle : Shape, IHasRadius
    {
        /// <summary>Determines whether the Rotation property of the <seealso cref="Shape"/> affects this shape in any way.</summary>
        protected override bool IsRotationUseful => false;

        /// <summary>The radius of the circular shape.</summary>
        public double Radius { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="Circle"/> class.</summary>
        /// <param name="position">The position of the shape.</param>
        /// <param name="radius">The radius of the circular shape.</param>
        public Circle(Point position, double radius) : base(position, 0) => Radius = radius;

        /// <summary>Returns the distance between the center of the circular shape and its edge. The rotation parameter is ignored for obvious mathematical reasons.</summary>
        /// <param name="rotation">A useless parameter.</param>
        protected override double CalculateRadiusAtRotation(double rotation) => Radius;
        /// <summary>Returns the maximum distance between the center of the shape and its edge.</summary>
        public override double GetMaxRadius() => Radius;

        /// <summary>Determines whether a point is within the shape (assuming the center of the shape is <seealso cref="Point.Zero"/>).</summary>
        /// <param name="point">The point's location.</param>
        public override bool ContainsPoint(Point point) => point.DistanceFrom(Position) <= Radius;

        protected override bool EqualsInheritably(Shape shape)
        {
            var other = shape as Circle;
            return Radius == other.Radius;
        }
    }
}
