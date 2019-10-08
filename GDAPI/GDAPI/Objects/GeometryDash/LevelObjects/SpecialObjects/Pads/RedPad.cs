using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Pads
{
    /// <summary>Represents a red pad.</summary>
    [ObjectID(PadType.RedPad)]
    public class RedPad : Pad
    {
        /// <summary>The object ID of the red pad.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)PadType.RedPad;

        /// <summary>Initializes a new instance of the <seealso cref="RedPad"/> class.</summary>
        public RedPad() : base() { }

        /// <summary>Returns a clone of this <seealso cref="RedPad"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RedPad());
    }
}
