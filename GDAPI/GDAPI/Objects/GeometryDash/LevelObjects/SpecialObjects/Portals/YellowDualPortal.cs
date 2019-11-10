using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a yellow dual portal.</summary>
    [ObjectID(PortalType.YellowDual)]
    public class YellowDualPortal : Portal
    {
        /// <summary>The object ID of the yellow dual portal.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)PortalType.YellowDual;

        /// <summary>Initializes a new instance of the <seealso cref="YellowDualPortal"/> class.</summary>
        public YellowDualPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowDualPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowDualPortal());
    }
}
