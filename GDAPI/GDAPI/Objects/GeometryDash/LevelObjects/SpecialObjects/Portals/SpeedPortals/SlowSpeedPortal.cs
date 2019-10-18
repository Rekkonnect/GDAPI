using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals
{
    /// <summary>Represents a slow speed portal in the game.</summary>
    [ObjectID(PortalType.SlowSpeed)]
    public class SlowSpeedPortal : SpeedPortal, IHasCheckedProperty
    {
        /// <summary>The object ID of the slow speed portal.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)PortalType.SlowSpeed;

        /// <summary>The speed this speed portal sets.</summary>
        public override Speed Speed => Speed.Slow;

        /// <summary>Initializes a new instance of the <seealso cref="SlowSpeedPortal"/> class.</summary>
        public SlowSpeedPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="SlowSpeedPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new SlowSpeedPortal());
    }
}
