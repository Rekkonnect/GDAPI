using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>Represents the player's gamemode.</summary>
    public enum Gamemode : byte
    {
        /// <summary>The cube gamemode.</summary>
        Cube,
        /// <summary>The ship gamemode.</summary>
        Ship,
        /// <summary>The ball gamemode.</summary>
        Ball,
        /// <summary>The UFO gamemode.</summary>
        UFO,
        /// <summary>The wave gamemode (also known as dart).</summary>
        Wave,
        /// <summary>The robot gamemode.</summary>
        Robot,
        /// <summary>The spider gamemode.</summary>
        Spider,
    }
}
