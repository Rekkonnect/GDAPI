using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a robot portal.</summary>
    [ObjectID(PortalType.Robot)]
    public class RobotPortal : GamemodePortal
    {
        /// <summary>The object ID of the robot portal.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)PortalType.Robot;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Robot;

        /// <summary>Initializes a new instance of the <seealso cref="RobotPortal"/> class.</summary>
        public RobotPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="RobotPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RobotPortal());
    }
}
