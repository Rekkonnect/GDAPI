using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a green size portal.</summary>
    [ObjectID(PortalType.GreenSize)]
    public class GreenSizePortal : Portal
    {
        /// <summary>The object ID of the green size portal.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)PortalType.GreenSize;

        /// <summary>Initializes a new instance of the <seealso cref="GreenSizePortal"/> class.</summary>
        public GreenSizePortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="GreenSizePortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GreenSizePortal());
    }
}
