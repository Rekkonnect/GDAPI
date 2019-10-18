using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a magenta dash orb.</summary>
    [ObjectID(OrbType.MagentaDashOrb)]
    public class MagentaDashOrb : Orb
    {
        /// <summary>The object ID of the magenta dash orb.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)OrbType.MagentaDashOrb;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaDashOrb"/> class.</summary>
        public MagentaDashOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="MagentaDashOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new MagentaDashOrb());
    }
}
