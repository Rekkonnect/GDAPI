using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.General.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents an object hitbox.</summary>
    public class Hitbox
    {
        /// <summary>The rotation of the hitbox.</summary>
        public double Rotation
        {
            get => Shape.Rotation;
            set => Shape.Rotation = value;
        }
        /// <summary>The position of the hitbox.</summary>
        public Point Position
        {
            get => Shape.Position;
            set => Shape.Position = value;
        }
        /// <summary>The X position of the hitbox.</summary>
        public double X
        {
            get => Shape.X;
            set => Shape.X = value;
        }
        /// <summary>The Y position of the hitbox.</summary>
        public double Y
        {
            get => Shape.Y;
            set => Shape.Y = value;
        }

        /// <summary>The shape of the hitbox.</summary>
        public Shape Shape { get; }
        /// <summary>The behavior of the hitbox.</summary>
        public HitboxBehavior Behavior { get; }

        /// <summary>Initializes a new instance of the <seealso cref="Hitbox"/> class.</summary>
        /// <param name="shape">The shape of the hitbox.</param>
        /// <param name="behavior">The behavior of the hitbox.</param>
        public Hitbox(Shape shape, HitboxBehavior behavior = HitboxBehavior.Platform)
        {
            Shape = shape;
            Behavior = behavior;
        }

        /// <summary>Returns the distance between the center of the hitbox and its edge.</summary>
        /// <param name="rotation">The rotation in degrees to get the distance at.</param>
        public double GetRadiusAtRotation(double rotation) => Shape.GetRadiusAtRotation(rotation);

        /// <summary>Determines whether a point is within this hitbox.</summary>
        /// <param name="p">The point to determine whether it's within this hitbox.</param> 
        public bool IsPointWithinHitbox(Point p) => Shape.ContainsPoint(p);

        /// <summary>Determines whether the provided hitbox is unnecessary. This evaluates to <see langword="true"/> if the provided hitbox is contained within this hitbox and has the same behavior, otherwise <see langword="false"/>.</summary>
        /// <param name="h">The hitbox to check whether it is unnecessary.</param>
        public bool IsUnnecessaryHitbox(Hitbox h) => h.Behavior == Behavior && Shape.ContainsShape(h.Shape);

        public static bool operator ==(Hitbox left, Hitbox right) => left.Behavior == right.Behavior && left.Position == right.Position && left.Rotation == right.Rotation && left.Shape == right.Shape;
        public static bool operator !=(Hitbox left, Hitbox right) => left.Behavior != right.Behavior || left.Position != right.Position || left.Rotation != right.Rotation || left.Shape != right.Shape;
    }

    /// <summary>Represents a hitbox behavior.</summary>
    public enum HitboxBehavior
    {
        /// <summary>Represents a hitbox that behaves as a platform, allowing the player to land on it.</summary>
        Platform,
        /// <summary>Represents a hitbox that behaves as a hazard, killing the player right as soon as they come in contact.</summary>
        Hazard,
        /// <summary>Represents a hitbox that triggers an effect as soon as the player contacts it.</summary>
        Trigger,
    }
}
