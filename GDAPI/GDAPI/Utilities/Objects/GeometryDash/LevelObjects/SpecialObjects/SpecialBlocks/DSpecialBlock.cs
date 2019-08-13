using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents the D special block.</summary>
    [ObjectID(SpecialBlockType.D)]
    public class DSpecialBlock : SpecialBlock
    {
        /// <summary>The object ID of the D special block.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)SpecialBlockType.D;

        /// <summary>Initializes a new instance of the <seealso cref="DSpecialBlock"/> class.</summary>
        public DSpecialBlock() : base() { }
    }
}
