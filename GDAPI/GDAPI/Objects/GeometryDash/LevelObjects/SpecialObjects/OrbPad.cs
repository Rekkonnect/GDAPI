using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents an orb or a pad object.</summary>
    public abstract class OrbPad : ConstantIDSpecialObject, IHasCheckedProperty
    {
        /// <summary>Represents the Switch Player Direction property of the orb or pad.</summary>
        [ObjectStringMappable(ObjectProperty.SwitchPlayerDirection, false)]
        public bool SwitchPlayerDirection
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }
        /// <summary>Represents the Checked property of the orb or pad.</summary>
        [ObjectStringMappable(ObjectProperty.SpecialObjectChecked, false)]
        public bool Checked
        {
            get => SpecialObjectBools[1];
            set => SpecialObjectBools[1] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="OrbPad"/> class.</summary>
        public OrbPad() : base() { }
    }
}
