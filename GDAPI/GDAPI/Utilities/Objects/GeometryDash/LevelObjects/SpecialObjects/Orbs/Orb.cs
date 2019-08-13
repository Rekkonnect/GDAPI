using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Information.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents an orb.</summary>
    public abstract class Orb : OrbPad
    {
        /// <summary>Represents the Multi Activate property of the orb.</summary>
        [ObjectStringMappable(ObjectParameter.MultiActivate)]
        public bool MultiActivate
        {
            get => SpecialObjectBools[2];
            set => SpecialObjectBools[2] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="Orb"/> class.</summary>
        public Orb() : base() { }
    }
}
