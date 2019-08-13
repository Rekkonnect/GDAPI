using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Information.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Pads
{
    /// <summary>Represents a pad.</summary>
    public abstract class Pad : OrbPad
    {
        /// <summary>Initializes a new instance of the <seealso cref="Pad"/> class.</summary>
        public Pad() : base() { }
    }
}
