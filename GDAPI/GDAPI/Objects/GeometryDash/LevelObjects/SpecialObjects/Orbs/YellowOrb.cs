using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a yellow orb.</summary>
    [ObjectID(OrbType.YellowOrb)]
    public class YellowOrb : Orb
    {
        /// <summary>The object ID of the yellow orb.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)OrbType.YellowOrb;

        /// <summary>Initializes a new instance of the <seealso cref="YellowOrb"/> class.</summary>
        public YellowOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowOrb());
    }
}
