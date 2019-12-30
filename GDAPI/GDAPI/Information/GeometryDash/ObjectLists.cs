using System.Collections.Generic;
using System.Linq;

namespace GDAPI.Information.GeometryDash
{
    /// <summary>Contains information about the objects in the game.</summary>
    public static class ObjectLists
    {
        /// <summary>The total count of different objects in the game (not including the different color triggers that have been deprecated).</summary>
        public const int TotalDifferentObjectCount = 1607;

        /// <summary>The object IDs of all the triggers.</summary>
        public static readonly int[] TriggerList = { 29, 30, 32, 33, 105, 744, 899, 900, 901, 915, 1006, 1007, 1049, 1268, 1346, 1347, 1520, 1585, 1595, 1611, 1612, 1613, 1616, 1811, 1812, 1814, 1815, 1817, 1818, 1819 };
        /// <summary>The object IDs of all the gamemode portals.</summary>
        public static readonly int[] ManipulationPortalList = { 13, 47, 111, 1331 };
        /// <summa
