using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Functions.GeometryDash
{
    /// <summary>Provides functions for conversion between color triggers and target Color IDs.</summary>
    public static class ColorTriggerTypes
    {
        /// <summary>Returns the special Color ID the color trigger represents.</summary>
        /// <param name="trigger">The color trigger whose target special Color ID to retrieve.</param>
        public static SpecialColorID ConvertToSpecialColorID(TriggerType trigger)
        {
            switch (trigger)
            {
                case TriggerType.BG:
                    return SpecialColorID.BG;
                case TriggerType.GRND:
                    return SpecialColorID.GRND;
                case TriggerType.GRND2:
                    return SpecialColorID.GRND2;
                case TriggerType.Obj:
                    return SpecialColorID.Obj;
                case TriggerType.Line:
                    return SpecialColorID.Line;
                case TriggerType.ThreeDL:
                    return SpecialColorID.ThreeDL;
                default:
                    throw new ArgumentException("Invalid color trigger type");
            }
        }
        /// <summary>Returns the Color ID the color trigger represents.</summary>
        /// <param name="trigger">The color trigger whose target Color ID to retrieve.</param>
        public static short ConvertToColorID(TriggerType trigger)
        {
            switch (trigger)
            {
                case TriggerType.Color1:
                    return 1;
                case TriggerType.Color2:
                    return 2;
                case TriggerType.Color3:
                    return 3;
                case TriggerType.Color4:
                    return 4;
                case TriggerType.BG:
                    return (int)SpecialColorID.BG;
                case TriggerType.GRND:
                    return (int)SpecialColorID.GRND;
                case TriggerType.GRND2:
                    return (int)SpecialColorID.GRND2;
                case TriggerType.Obj:
                    return (int)SpecialColorID.Obj;
                case TriggerType.Line:
                    return (int)SpecialColorID.Line;
                case TriggerType.ThreeDL:
                    return (int)SpecialColorID.ThreeDL;
                default:
                    throw new ArgumentException("Invalid color trigger type");
            }
        }
    }
}
