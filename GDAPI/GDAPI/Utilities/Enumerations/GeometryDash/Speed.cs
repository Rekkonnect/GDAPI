using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>Represents the player speed.</summary>
    public enum Speed : byte
    {
        /// <summary>Represents the default speed (normal).</summary>
        Default = 0,
        /// <summary>Represents the slow speed (&lt;).</summary>
        Slow = 1,
        /// <summary>Represents the normal speed (&rt;).</summary>
        Normal = 2,
        /// <summary>Represents the fast speed (&rt;&rt;).</summary>
        Fast = 3,
        /// <summary>Represents the faster speed (&rt;&rt;&rt;).</summary>
        Faster = 4,
        /// <summary>Represents the fastest speed (&rt;&rt;&rt;&rt;).</summary>
        Fastest = 5,
    }
}
