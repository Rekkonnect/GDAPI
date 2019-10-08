using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents a special block (not to be confused with <seealso cref="SpecialObject"/>).</summary>
    public abstract class SpecialBlock : ConstantIDSpecialObject
    {
        /// <summary>The object ID of the special block.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID { get; }

        /// <summary>Initializes a new instance of the <seealso cref="SpecialBlock"/> class.</summary>
        public SpecialBlock() : base() { }
    }
}
