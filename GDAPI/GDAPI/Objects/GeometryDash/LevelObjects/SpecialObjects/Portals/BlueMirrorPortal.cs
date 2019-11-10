using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a blue mirror portal.</summary>
    [ObjectID(PortalType.BlueMirror)]
    public class BlueMirrorPortal : Portal
    {
        /// <summary>The object ID of the blue mirror portal.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)PortalType.BlueMirror;

        /// <summary>Initializes a new instance of the <seealso cref="BlueMirrorPortal"/> class.</summary>
        public BlueMirrorPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlueMirrorPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlueMirrorPortal());
    }
}
