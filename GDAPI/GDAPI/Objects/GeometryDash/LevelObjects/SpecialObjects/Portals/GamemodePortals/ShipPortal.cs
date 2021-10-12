using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a ship portal.</summary>
    [ObjectID(PortalType.Ship)]
    public class ShipPortal : CheckableGamemodePortal
    {
        /// <summary>The object ID of the ship portal.</summary>
        public override int ConstantObjectID => (int)PortalType.Ship;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Ship;

        /// <summary>Initializes a new instance of the <seealso cref="ShipPortal"/> class.</summary>
        public ShipPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="ShipPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ShipPortal());
    }
}
