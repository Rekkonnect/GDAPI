using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a yellow gravity portal.</summary>
    [ObjectID(PortalType.YellowGravity)]
    public class YellowGravityPortal : Portal
    {
        /// <summary>The object ID of the yellow gravity portal.</summary>
        public override int ConstantObjectID => (int)PortalType.YellowGravity;

        /// <summary>Initializes a new instance of the <seealso cref="YellowGravityPortal"/> class.</summary>
        public YellowGravityPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowGravityPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowGravityPortal());
    }
}
