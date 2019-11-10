using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a red orb.</summary>
    [ObjectID(OrbType.RedOrb)]
    public class RedOrb : Orb
    {
        /// <summary>The object ID of the red orb.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)OrbType.RedOrb;

        /// <summary>Initializes a new instance of the <seealso cref="RedOrb"/> class.</summary>
        public RedOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="RedOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RedOrb());
    }
}
