using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a blue dual portal.</summary>
    [ObjectID(PortalType.BlueDual)]
    public class BlueDualPortal : Portal
    {
        /// <summary>The object ID of the blue dual portal.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)PortalType.BlueDual;

        /// <summary>Initializes a new instance of the <seealso cref="BlueDualPortal"/> class.</summary>
        public BlueDualPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlueDualPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlueDualPortal());
    }
}
