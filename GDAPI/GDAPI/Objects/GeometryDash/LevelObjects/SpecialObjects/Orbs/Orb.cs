using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Orbs
{
    /// <summary>Represents an orb.</summary>
    public abstract class Orb : OrbPad
    {
        /// <summary>Represents the Multi Activate property of the orb.</summary>
        [ObjectStringMappable(ObjectParameter.MultiActivate, false)]
        public bool MultiActivate
        {
            get => SpecialObjectBools[2];
            set => SpecialObjectBools[2] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="Orb"/> class.</summary>
        public Orb() : base() { }
    }
}
