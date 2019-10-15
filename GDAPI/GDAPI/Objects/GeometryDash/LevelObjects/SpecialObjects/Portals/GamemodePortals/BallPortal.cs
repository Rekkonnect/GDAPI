using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a ball portal.</summary>
    [ObjectID(PortalType.Ball)]
    public class BallPortal : CheckableGamemodePortal
    {
        /// <summary>The object ID of the ball portal.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)PortalType.Ball;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Ball;

        /// <summary>Initializes a new instance of the <seealso cref="BallPortal"/> class.</summary>
        public BallPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BallPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BallPortal());
    }
}
