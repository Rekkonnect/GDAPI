using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a yellow gravity portal.</summary>
    [ObjectID(PortalType.YellowGravity)]
    public class YellowGravityPortal : Portal
    {
        /// <summary>The object ID of the yellow gravity portal.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)PortalType.YellowGravity;

        /// <summary>Initializes a new instance of the <seealso cref="YellowGravityPortal"/> class.</summary>
        public YellowGravityPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowGravityPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowGravityPortal());
    }
}
