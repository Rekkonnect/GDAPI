using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Information.GeometryDash
{
    /// <summary>Contains information about the previous versions of the game.</summary>
    public static class PreviousVersions
    {
        // TODO: Prefer using a dictionary to make things easier

        /// <summary>The last available object ID of each version.</summary>
        public static readonly int[] VersionObjectIDLimits = { 41, 46, 47, 75, 105, 141, 199, 285, 505, 744, 1329, 1911 };
        /// <summary>The last available parameter ID of each version.</summary>
        public static readonly int[] VersionParameterIDLimits = { 10, 10, 10, 10, 10, 11, 13, 17, 17, 20, 66, 107 };
        /// <summary>The last available kA property in the level string of each version.</summary>
        public static readonly int[] LevelStringkALimits = { 1, 1, 1, 1, 1, 1, 1, 7, 10, 16, 18, 18 };
    }
}
