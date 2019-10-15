using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents the D special block.</summary>
    [ObjectID(SpecialBlockType.D)]
    public class DSpecialBlock : SpecialBlock
    {
        /// <summary>The object ID of the D special block.</summary>
        [ObjectStringMappable(ObjectProperty.ID)]
        public override int ObjectID => (int)SpecialBlockType.D;

        /// <summary>Initializes a new instance of the <seealso cref="DSpecialBlock"/> class.</summary>
        public DSpecialBlock() : base() { }
    }
}
