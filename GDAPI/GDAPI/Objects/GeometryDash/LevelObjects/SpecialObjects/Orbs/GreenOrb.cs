using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a green orb.</summary>
    [ObjectID(OrbType.GreenOrb)]
    public class GreenOrb : Orb
    {
        /// <summary>The object ID of the green orb.</summary>
        public override int ConstantObjectID => (int)OrbType.GreenOrb;

        /// <summary>Initializes a new instance of the <seealso cref="GreenOrb"/> class.</summary>
        public GreenOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="GreenOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GreenOrb());
    }
}
