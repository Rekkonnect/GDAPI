using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.General
{
    /// <summary>Represents a point in the editor.</summary>
    public struct Point
    {
        /// <summary>The <seealso cref="Point"/> at the center of the plane.</summary>
        public static Point Zero => new Point(0);

        /// <summary>The X location of the point.</summary>
        public double X;
        /// <summary>The Y location of the point.</summary>
        public double Y;

        /// <summary>Initializes a new instance of the <seealso cref="Point"/> struct.</summary>
        /// <param name="both">The value of both locations of the point.</param>
        public Point(double both)
        {
            X = Y = both;
        }
        /// <summary>Initializes a new instance of the <seealso cref="Point"/> struct.</summary>
        /// <param name="x">The X location of the point.</param>
        /// <param name="y">The Y location of the point.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Returns the distance from another point.</summary>
        /// <param name="p">The other point to calculate the distance from.</param>
        public double DistanceFrom(Point p)
        {
            var d = p - this;
            return Math.Sqrt(d.X * d.X + d.Y * d.Y);
        }
        // TODO: Use EML and convert the angle arguments from double to Angle.
        /// <summary>Gets the angle in radians formed by this and a specified point, in comparison to the x axis on the plane.</summary>
        /// <param name="p">The point to rotate.</param>
        public double GetAngle(Point p)
        {
            double distance = DistanceFrom(p);
            double xDistance = p.X - X;
            int sign = Math.Sign(p.Y - Y);
            if (sign == 0)
                sign = 1;
            return sign * Math.Acos(xDistance / distance);
        }
        /// <summary>Given this point A and a supplied point B, this returns a point C such that the angle CAB (A is the center) is equal to the angle that's specified.</summary>
        /// <param name="p">The point to rotate.</param>
        /// <param name="rotation">The angle of the rotation in degrees.</param>
        public Point Rotate(Point p, double rotation)
        {
            // TODO: Validate
            double distance = DistanceFrom(p);
            double rad = GetAngle(p) + rotation * Math.PI / 180;
            double x = Math.Cos(rad) * distance;
            double y = Math.Sin(rad) * distance;
            return this + new Point(x, y);
        }

        public static Point operator -(Point point) => new Point(-point.X, -point.Y);
        public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
        public static Point operator +(double left, Point right) => new Point(left + right.X, left + right.Y);
        public static Point operator +(Point left, double right) => new Point(left.X + right, left.Y + right);
        public static Point operator -(Point left, Point right) => new Point(left.X - right.X, left.Y - right.Y);
        public static Point operator -(double left, Point right) => new Point(left - right.X, left - right.Y);
        public static Point operator -(Point left, double right) => new Point(left.X - right, left.Y - right);
        public static Point operator *(int left, Point right) => new Point(left * right.X, left * right.Y);
        public static Point operator *(Point left, int right) => new Point(left.X * right, left.Y * right);
        public static Point operator *(double left, Point right) => new Point(left * right.X, left * right.Y);
        public static Point operator *(Point left, double right) => new Point(left.X * right, left.Y * right);
        public static Point operator /(int left, Point right) => new Point(left / right.X, left / right.Y);
        public static Point operator /(Point left, int right) => new Point(left.X / right, left.Y / right);
        public static Point operator /(double left, Point right) => new Point(left / right.X, left / right.Y);
        public static Point operator /(Point left, double right) => new Point(left.X / right, left.Y / right);
        public static bool operator ==(Point left, Point right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Point left, Point right) => left.X != right.X && left.Y != right.Y;
        public static bool operator <=(Point left, Point right) => left.X <= right.X && left.Y <= right.Y;
        public static bool operator >=(Point left, Point right) => left.X >= right.X && left.Y >= right.Y;
        public static bool operator <(Point left, Point right) => left.X < right.X && left.Y < right.Y;
        public static bool operator >(Point left, Point right) => left.X > right.X && left.Y > right.Y;

        public override string ToString() => $"({X}, {Y})";
    }
}
