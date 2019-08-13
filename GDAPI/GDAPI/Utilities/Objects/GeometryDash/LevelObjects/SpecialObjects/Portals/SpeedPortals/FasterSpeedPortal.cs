using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Functions.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals
{
    /// <summary>Represents a faster speed portal in the game.</summary>
    [ObjectID(PortalType.FasterSpeed)]
    public class FasterSpeedPortal : SpeedPortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the faster speed portal.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)PortalType.FasterSpeed;

        /// <summary>The speed this speed portal sets.</summary>
        public override Speed Speed => Speed.Faster;

        /// <summary>Initializes a new instance of the <seealso cref="FasterSpeedPortal"/> class.</summary>
        public FasterSpeedPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="FasterSpeedPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new FasterSpeedPortal());
    }
}
