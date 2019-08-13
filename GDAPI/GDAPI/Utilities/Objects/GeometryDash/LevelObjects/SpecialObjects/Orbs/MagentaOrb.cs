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
    /// <summary>Represents a magenta orb.</summary>
    [ObjectID(OrbType.MagentaOrb)]
    public class MagentaOrb : Orb
    {
        /// <summary>The object ID of the magenta orb.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)OrbType.MagentaOrb;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaOrb"/> class.</summary>
        public MagentaOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="MagentaOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new MagentaOrb());
    }
}
