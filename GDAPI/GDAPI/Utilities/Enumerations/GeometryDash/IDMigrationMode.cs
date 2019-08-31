using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>Represents the mode of an ID migration operation.</summary>
    public enum IDMigrationMode
    {
        /// <summary>The Group ID migration mode.</summary>
        Groups,
        /// <summary>The Color ID migration mode.</summary>
        Colors,
        /// <summary>The Item ID migration mode.</summary>
        Items,
        /// <summary>The Block ID migration mode.</summary>
        Blocks,
    }
}
