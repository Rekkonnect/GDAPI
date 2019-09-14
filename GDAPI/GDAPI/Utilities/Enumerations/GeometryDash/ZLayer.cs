using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Objects.GeometryDash;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>This enumeration provides values for the Z Layer of a <see cref="LevelObject"/>.</summary>
    public enum ZLayer
    {
        /// <summary>Represents the value for the B4 Z Layer.</summary>
        B4 = -3,
        /// <summary>Represents the value for the B3 Z Layer.</summary>
        B3 = -1,
        /// <summary>Represents the value for the B2 Z Layer.</summary>
        B2 = 1,
        /// <summary>Represents the value for the B1 Z Layer.</summary>
        B1 = 3,
        /// <summary>Represents the value for the T1 Z Layer.</summary>
        T1 = 5,
        /// <summary>Represents the value for the T2 Z Layer.</summary>
        T2 = 7,
        /// <summary>Represents the value for the T3 Z Layer.</summary>
        T3 = 9,

        /// <summary>Represents the value for the Bot Z Layer. This has been renamed to B2 since 2.1.</summary>
        Bot = 1,
        /// <summary>Represents the value for the Mid Z Layer. This has been renamed to B1 since 2.1.</summary>
        Mid = 3,
        /// <summary>Represents the value for the Top Z Layer. This has been renamed to T1 since 2.1.</summary>
        Top = 5,
        /// <summary>Represents the value for the Top+ Z Layer. This has been renamed to T2 since 2.1.</summary>
        TopPlus = 7,

        /// <summary>Represents the absolute zero Z Layer which is between B1 and T1. This value is theoretical and should only be used for calculations.</summary>
        AbsoluteZero = 4,
    }
}
