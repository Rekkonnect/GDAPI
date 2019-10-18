using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a green dash orb.</summary>
    [ObjectID(OrbType.GreenDashOrb)]
    public class GreenDashOrb : Orb
    {
        /// <summary>The object ID of the green dash orb.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)OrbType.GreenDashOrb;

        /// <summary>Initializes a new instance of the <seealso cref="GreenDashOrb"/> class.</summary>
        public GreenDashOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="GreenDashOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new GreenDashOrb());
    }
}
