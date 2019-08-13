using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
{
    /// <summary>Represents the J special block.</summary>
    [ObjectID(SpecialBlockType.J)]
    public class JSpecialBlock : SpecialBlock
    {
        /// <summary>The object ID of the J special block.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)SpecialBlockType.J;

        /// <summary>Initializes a new instance of the <seealso cref="JSpecialBlock"/> class.</summary>
        public JSpecialBlock() : base() { }
    }
}
