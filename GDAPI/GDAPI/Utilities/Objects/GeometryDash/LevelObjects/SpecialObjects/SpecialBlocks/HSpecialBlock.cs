using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents the H special block.</summary>
    [ObjectID(SpecialBlockType.H)]
    public class HSpecialBlock : SpecialBlock
    {
        /// <summary>The object ID of the H special block.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)SpecialBlockType.H;

        /// <summary>Initializes a new instance of the <seealso cref="HSpecialBlock"/> class.</summary>
        public HSpecialBlock() : base() { }
    }
}
