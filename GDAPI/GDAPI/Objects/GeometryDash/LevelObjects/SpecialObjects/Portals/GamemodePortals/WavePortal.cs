using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a wave portal.</summary>
    [ObjectID(PortalType.Wave)]
    public class WavePortal : CheckableGamemodePortal
    {
        /// <summary>The object ID of the wave portal.</summary>
        public override int ConstantObjectID => (int)PortalType.Wave;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Wave;

        /// <summary>Initializes a new instance of the <seealso cref="WavePortal"/> class.</summary>
        public WavePortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="WavePortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new WavePortal());
    }
}
