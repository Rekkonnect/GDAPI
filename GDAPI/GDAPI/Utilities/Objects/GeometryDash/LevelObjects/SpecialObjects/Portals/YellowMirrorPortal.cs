using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a yellow mirror portal.</summary>
    [ObjectID(PortalType.YellowMirror)]
    public class YellowMirrorPortal : Portal
    {
        /// <summary>The object ID of the yellow mirror portal.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)PortalType.YellowMirror;

        /// <summary>Initializes a new instance of the <seealso cref="YellowMirrorPortal"/> class.</summary>
        public YellowMirrorPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowMirrorPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowMirrorPortal());
    }
}
