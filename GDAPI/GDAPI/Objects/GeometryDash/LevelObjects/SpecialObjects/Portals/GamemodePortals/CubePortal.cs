using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a cube portal.</summary>
    [ObjectID(PortalType.Cube)]
    public class CubePortal : GamemodePortal
    {
        /// <summary>The object ID of the cube portal.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)PortalType.Cube;
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public override Gamemode Gamemode => Gamemode.Cube;

        /// <summary>Initializes a new instance of the <seealso cref="CubePortal"/> class.</summary>
        public CubePortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="CubePortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new CubePortal());
    }
}
