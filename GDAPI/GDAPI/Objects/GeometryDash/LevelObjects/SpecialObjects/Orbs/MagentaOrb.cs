using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a magenta orb.</summary>
    [ObjectID(OrbType.MagentaOrb)]
    public class MagentaOrb : Orb
    {
        /// <summary>The object ID of the magenta orb.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)OrbType.MagentaOrb;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaOrb"/> class.</summary>
        public MagentaOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="MagentaOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new MagentaOrb());
    }
}
