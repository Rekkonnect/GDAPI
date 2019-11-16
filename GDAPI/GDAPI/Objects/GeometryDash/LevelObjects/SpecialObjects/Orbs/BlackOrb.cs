using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents a black orb.</summary>
    [ObjectID(OrbType.BlackOrb)]
    public class BlackOrb : Orb
    {
        /// <summary>The object ID of the black orb.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)OrbType.BlackOrb;

        /// <summary>The Disable Rotation property of the orb, which does not exist. It serves the purpose of an exception reductor.</summary>
        [ExceptionReductor]
        [ObjectStringMappable(ObjectProperty.DisableRotation, false)]
        public bool DisableRotation
        {
            get => false;
            set { }
        }

        /// <summary>Initializes a new instance of the <seealso cref="BlackOrb"/> class.</summary>
        public BlackOrb() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlackOrb"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlackOrb());
    }
}
