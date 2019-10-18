using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Pads
{
    /// <summary>Represents a yellow pad.</summary>
    [ObjectID(PadType.YellowPad)]
    public class YellowPad : Pad
    {
        /// <summary>The object ID of the yellow pad.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)PadType.YellowPad;

        /// <summary>Initializes a new instance of the <seealso cref="YellowPad"/> class.</summary>
        public YellowPad() : base() { }

        /// <summary>Returns a clone of this <seealso cref="YellowPad"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new YellowPad());
    }
}
