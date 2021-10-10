using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a yellow mirror portal.</summary>
    [ObjectID(PortalType.YellowMirror)]
    public class YellowMirrorPortal : Portal
    {
        /// <summary>The object ID of the yellow mirror portal.</summary>
        public override int ConstantObjectID => (int)PortalType.YellowMirror;

        /// <summary>Initializes a new instance of the <seealso cref="YellowMirrorPortal"/> class.</summary>
        public YellowMirrorPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowMirrorPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowMirrorPortal());
    }
}
