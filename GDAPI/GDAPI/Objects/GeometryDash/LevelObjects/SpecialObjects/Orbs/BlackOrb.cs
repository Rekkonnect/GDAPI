using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a black orb.</summary>
    [ObjectID(OrbType.BlackOrb)]
    public class BlackOrb : Orb
    {
        /// <summary>The object ID of the black orb.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)OrbType.BlackOrb;

        /// <summary>Initializes a new instance of the <seealso cref="BlackOrb"/> class.</summary>
        public BlackOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlackOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlackOrb());
    }
}
