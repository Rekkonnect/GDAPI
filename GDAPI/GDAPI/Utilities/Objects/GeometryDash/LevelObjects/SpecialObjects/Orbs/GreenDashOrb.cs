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
    /// <summary>Represents a green dash orb.</summary>
    [ObjectID(OrbType.GreenDashOrb)]
    public class GreenDashOrb : Orb
    {
        /// <summary>The object ID of the green dash orb.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)OrbType.GreenDashOrb;

        /// <summary>Initializes a new instance of the <seealso cref="GreenDashOrb"/> class.</summary>
        public GreenDashOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="GreenDashOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GreenDashOrb());
    }
}
