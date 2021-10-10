using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents the S special block.</summary>
    [ObjectID(SpecialBlockType.S)]
    public class SSpecialBlock : SpecialBlock
    {
        /// <summary>The object ID of the S special block.</summary>
        public override int ConstantObjectID => (int)SpecialBlockType.S;

        /// <summary>Initializes a new instance of the <seealso cref="SSpecialBlock"/> class.</summary>
        public SSpecialBlock() : base() { }
    }
}
