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
    /// <summary>Represents a yellow orb.</summary>
    [ObjectID(OrbType.YellowOrb)]
    public class YellowOrb : Orb
    {
        /// <summary>The object ID of the yellow orb.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)OrbType.YellowOrb;

        /// <summary>Initializes a new instance of the <seealso cref="YellowOrb"/> class.</summary>
        public YellowOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowOrb());
    }
}
