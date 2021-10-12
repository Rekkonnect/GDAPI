using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Pads
{
    /// <summary>Represents a blue pad.</summary>
    [ObjectID(PadType.BluePad)]
    public class BluePad : Pad
    {
        /// <summary>The object ID of the blue pad.</summary>
        public override int ConstantObjectID => (int)PadType.BluePad;

        /// <summary>Initializes a new instance of the <seealso cref="BluePad"/> class.</summary>
        public BluePad() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BluePad"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BluePad());
    }
}
