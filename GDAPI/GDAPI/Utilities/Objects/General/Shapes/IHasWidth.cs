using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.General.Shapes
{
    /// <summary>Represents a shape that has a width property.</summary>
    public interface IHasWidth
    {
        /// <summary>The width of the shape.</summary>
        double Width { get; set; }
    }
}
