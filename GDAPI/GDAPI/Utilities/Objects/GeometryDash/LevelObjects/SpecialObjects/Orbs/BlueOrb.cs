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
    /// <summary>Represents a blue orb.</summary>
    [ObjectID(OrbType.BlueOrb)]
    public class BlueOrb : Orb
    {
        /// <summary>The object ID of the blue orb.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)OrbType.BlueOrb;

        /// <summary>Initializes a new instance of the <seealso cref="BlueOrb"/> class.</summary>
        public BlueOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlueOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlueOrb());
    }
}
