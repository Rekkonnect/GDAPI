using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GDAPI.Utilities.Information.GeometryDash.LevelLengths;

namespace GDAPI.Utilities.Functions.GeometryDash
{
    /// <summary>Provides functions to convert values based on level lengths.</summary>
    public static class EnumConverters
    {
        /// <summary>Returns the <seealso cref="LevelLength"/> of a level from the seconds.</summary>
        /// <param name="seconds">The seconds of the length of the level to get the <seealso cref="LevelLength"/> value of.</param>
        public static LevelLength GetLevelLength(double seconds)
        {
            if (seconds >= MinXLLength)
                return LevelLength.XL;
            if (seconds >= MinLongLength)
                return LevelLength.Long;
            if (seconds >= MinMediumLength)
                return LevelLength.Medium;
            if (seconds >= MinSmallLength)
                return LevelLength.Small;
            return LevelLength.Tiny;
        }
        /// <summary>Returns the respective time length range, given a <seealso cref="LevelLength"/>.</summary>
        /// <param name="length">The <seealso cref="LevelLength"/> whose respective time length range to return.</param>
        public static Range<double> GetLevelLengthRange(LevelLength length)
        {
            switch (length)
            {
                case LevelLength.XL:
                    return XLLengthRange;
                case LevelLength.Long:
                    return LongLengthRange;
                case LevelLength.Medium:
                    return MediumLengthRange;
                case LevelLength.Small:
                    return SmallLengthRange;
                case LevelLength.Tiny:
                default:
                    return TinyLengthRange;
            }
        }
    }
}
