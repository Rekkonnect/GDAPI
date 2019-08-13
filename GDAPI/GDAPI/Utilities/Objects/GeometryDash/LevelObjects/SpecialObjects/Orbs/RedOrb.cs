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
    /// <summary>Represents a red orb.</summary>
    [ObjectID(OrbType.RedOrb)]
    public class RedOrb : Orb
    {
        /// <summary>The object ID of the red orb.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)OrbType.RedOrb;

        /// <summary>Initializes a new instance of the <seealso cref="RedOrb"/> class.</summary>
        public RedOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="RedOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RedOrb());
    }
}
