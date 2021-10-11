using System;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Functions.GeometryDash
{
    /// <summary>Provides functions for conversion between color triggers and target Color IDs.</summary>
    public static class ColorTriggerTypes
    {
        /// <summary>Returns the special Color ID the color trigger represents.</summary>
        /// <param name="trigger">The color trigger whose target special Color ID to retrieve.</param>
        public static SpecialColorID ConvertToSpecialColorID(TriggerType trigger)
        {
            return trigger switch
            {
                TriggerType.BG      => SpecialColorID.BG,
                TriggerType.GRND    => SpecialColorID.GRND,
                TriggerType.GRND2   => SpecialColorID.GRND2,
                TriggerType.Obj     => SpecialColorID.Obj,
                TriggerType.Line    => SpecialColorID.Line,
                TriggerType.ThreeDL => SpecialColorID.ThreeDL,
                _ => throw new ArgumentException("Invalid color trigger type"),
            };
        }
        /// <summary>Returns the Color ID the color trigger represents.</summary>
        /// <param name="trigger">The color trigger whose target Color ID to retrieve.</param>
        public static short ConvertToColorID(TriggerType trigger)
        {
            return trigger switch
            {
                TriggerType.Color1 => 1,
                TriggerType.Color2 => 2,
                TriggerType.Color3 => 3,
                TriggerType.Color4 => 4,
                _ => (short)ConvertToSpecialColorID(trigger),
            };
        }
    }
}
