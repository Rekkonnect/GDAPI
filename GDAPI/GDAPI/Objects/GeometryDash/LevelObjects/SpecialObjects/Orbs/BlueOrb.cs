using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a blue orb.</summary>
    [ObjectID(OrbType.BlueOrb)]
    public class BlueOrb : Orb
    {
        /// <summary>The object ID of the blue orb.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)OrbType.BlueOrb;

        /// <summary>Initializes a new instance of the <seealso cref="BlueOrb"/> class.</summary>
        public BlueOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlueOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlueOrb());
    }
}
