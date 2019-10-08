using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals
{
    /// <summary>Represents a fast speed portal in the game.</summary>
    [ObjectID(PortalType.FastSpeed)]
    public class FastSpeedPortal : SpeedPortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the fast speed portal.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)PortalType.FastSpeed;

        /// <summary>The speed this speed portal sets.</summary>
        public override Speed Speed => Speed.Fast;

        /// <summary>Initializes a new instance of the <seealso cref="FastSpeedPortal"/> class.</summary>
        public FastSpeedPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="FastSpeedPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new FastSpeedPortal());
    }
}
