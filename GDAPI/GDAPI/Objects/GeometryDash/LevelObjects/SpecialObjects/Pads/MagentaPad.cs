using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Pads
{
    /// <summary>Represents a magenta pad.</summary>
    [ObjectID(PadType.MagentaPad)]
    public class MagentaPad : Pad
    {
        /// <summary>The object ID of the magenta pad.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)PadType.MagentaPad;

        /// <summary>Initializes a new instance of the <seealso cref="MagentaPad"/> class.</summary>
        public MagentaPad() : base() { }

        /// <summary>Returns a clone of this <seealso cref="MagentaPad"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new MagentaPad());
    }
}
