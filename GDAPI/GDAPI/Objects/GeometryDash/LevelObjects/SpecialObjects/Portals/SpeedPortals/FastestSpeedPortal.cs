using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals
{
    /// <summary>Represents a fastest speed portal in the game.</summary>
    [ObjectID(PortalType.FastestSpeed)]
    public class FastestSpeedPortal : SpeedPortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the fastest speed portal.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)PortalType.FastestSpeed;

        /// <summary>The speed this speed portal sets.</summary>
        public override Speed Speed => Speed.Faster;

        /// <summary>Initializes a new instance of the <seealso cref="FastestSpeedPortal"/> class.</summary>
        public FastestSpeedPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="FastestSpeedPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new FastestSpeedPortal());
    }
}
