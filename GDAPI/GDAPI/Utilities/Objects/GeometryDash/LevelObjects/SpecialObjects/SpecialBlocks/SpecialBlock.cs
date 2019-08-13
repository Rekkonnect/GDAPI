using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.SpecialBlocks
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
