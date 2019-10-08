using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a blue gravity portal.</summary>
    [ObjectID(PortalType.BlueGravity)]
    public class BlueGravityPortal : Portal
    {
        /// <summary>The object ID of the blue gravity portal.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)PortalType.BlueGravity;

        /// <summary>Initializes a new instance of the <seealso cref="BlueGravityPortal"/> class.</summary>
        public BlueGravityPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlueGravityPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlueGravityPortal());
    }
}
