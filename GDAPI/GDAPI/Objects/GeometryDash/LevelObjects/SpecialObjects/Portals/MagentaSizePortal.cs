using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a green magenta portal.</summary>
    [ObjectID(PortalType.MagentaSize)]
    public class MagentaSizePortal : Portal
    {
        /// <summary>The object ID of the magenta size portal.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)PortalType.MagentaSize;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaSizePortal"/> class.</summary>
        public MagentaSizePortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="MagentaSizePortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new MagentaSizePortal());
    }
}
