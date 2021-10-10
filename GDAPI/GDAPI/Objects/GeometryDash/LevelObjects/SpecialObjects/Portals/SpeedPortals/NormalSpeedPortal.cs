using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals
{
    /// <summary>Represents a normal speed portal in the game.</summary>
    [ObjectID(PortalType.NormalSpeed)]
    public class NormalSpeedPortal : SpeedPortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the normal speed portal.</summary>
        public override int ConstantObjectID => (int)PortalType.NormalSpeed;

        /// <summary>The speed this speed portal sets.</summary>
        public override Speed Speed => Speed.Normal;

        /// <summary>Initializes a new instance of the <seealso cref="NormalSpeedPortal"/> class.</summary>
        public NormalSpeedPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="NormalSpeedPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new NormalSpeedPortal());
    }
}
