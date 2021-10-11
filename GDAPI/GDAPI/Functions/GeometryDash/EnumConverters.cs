using System;
using GDAPI.Enumerations.GeometryDash;
using static GDAPI.Information.GeometryDash.LevelLengths;

namespace GDAPI.Functions.GeometryDash
{
    /// <summary>Provides functions to convert values based on level lengths.</summary>
    public static class EnumConverters
    {
        #region LevelLength
        /// <summary>Returns the <seealso cref="LevelLength"/> of a level from the seconds.</summary>
        /// <param name="seconds">The seconds of the length of the level to get the <seealso cref="LevelLength"/> value of.</param>
        public static LevelLength GetLevelLength(double seconds)
        {
            return seconds switch
            {
                >= MinXLLength     => LevelLength.XL,
                >= MinLongLength   => LevelLength.Long,
                >= MinMediumLength => LevelLength.Medium,
                >= MinSmallLength  => LevelLength.Small,
                _ => LevelLength.Tiny,
            };
        }
        /// <summary>Returns the respective time length range, given a <seealso cref="LevelLength"/>.</summary>
        /// <param name="length">The <seealso cref="LevelLength"/> whose respective time length range to return.</param>
        public static Range<double> GetLevelLengthRange(LevelLength length)
        {
            return length switch
            {
                LevelLength.XL     => XLLengthRange,
                LevelLength.Long   => LongLengthRange,
                LevelLength.Medium => MediumLengthRange,
                LevelLength.Small  => SmallLengthRange,
                _ => TinyLengthRange,
            };
        }
        #endregion

        #region LevelDifficulty
        /// <summary>Returns the <seealso cref="LevelDifficulty"/> of a level from the specified star rating.</summary>
        /// <param name="stars">The star rating of the level to get the <seealso cref="LevelDifficulty"/> value of.</param>
        public static LevelDifficulty GetLevelDifficulty(int stars)
        {
            return stars switch
            {
                1      => LevelDifficulty.Auto,
                2      => LevelDifficulty.Easy,
                3      => LevelDifficulty.Normal,
                4 or 5 => LevelDifficulty.Hard,
                6 or 7 => LevelDifficulty.Harder,
                8 or 9 => LevelDifficulty.Insane,
                10     => LevelDifficulty.Demon,
                _      => LevelDifficulty.NA,
            };
        }
        #endregion
    }
}
