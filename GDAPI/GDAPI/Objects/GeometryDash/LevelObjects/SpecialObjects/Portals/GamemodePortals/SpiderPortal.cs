using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a spider portal.</summary>
    [ObjectID(PortalType.Spider)]
    public class SpiderPortal : CheckableGamemodePortal
    {
        /// <summary>The object ID of the spider portal.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)PortalType.Spider;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Spider;

        /// <summary>Initializes a new instance of the <seealso cref="SpiderPortal"/> class.</summary>
        public SpiderPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="SpiderPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new SpiderPortal());
    }
}
