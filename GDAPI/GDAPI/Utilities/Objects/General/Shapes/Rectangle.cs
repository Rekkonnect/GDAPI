using GDAPI.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a rectangular shape.</summary>
    public class Rectangle : WidthHeightShape, IHasWidth, IHasHeight
    {
        /// <summary>Initializes a new instance of the <seealso cref="Rectangle"/> class.</summary>
        /// <param name="position">The position of the rectangle.</param>
        /// <param name="rotation">The rotation of the rectangle in degrees.</param>
        /// <param name="both">The length of both dimensions of the rectangular shape.</param>
        public Rectangle(Point position, double rotation, double both) : base(position, rotation, both) { }
        /// <summary>Initializes a new instance of the <seealso cref="Rectangle"/> class.</summary>
        /// <param name="position">The position of the rectangle.</param>
        /// <param name="rotation">The rotation of the rectangle in degrees.</param>
        /// <param name="width">The width of the rectangular shape.</param>
        /// <param name="height">The height of the rectangular shape.</param>
        public Rectangle(Point position, double rotation, double width, double height) : base(position, rotation, width, height) { }
    }
}
