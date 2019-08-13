using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>This enumeration provides values for the comparison of an Instant Count trigger.</summary>
    public enum InstantCountComparison : byte
    {
        /// <summary>Represents Equals on the comparison of an Instant Count trigger.</summary>
        Equals,
        /// <summary>Represents Larger on the comparison of an Instant Count trigger.</summary>
        Larger,
        /// <summary>Represents Smaller on the comparison of an Instant Count trigger.</summary>
        Smaller
    }
}
