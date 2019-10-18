using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents the H special block.</summary>
    [ObjectID(SpecialBlockType.H)]
    public class HSpecialBlock : SpecialBlock
    {
        /// <summary>The object ID of the H special block.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)SpecialBlockType.H;

        /// <summary>Initializes a new instance of the <seealso cref="HSpecialBlock"/> class.</summary>
        public HSpecialBlock() : base() { }
    }
}
