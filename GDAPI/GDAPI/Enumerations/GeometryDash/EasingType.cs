using System;

namespace GDAPI.Enumerations.GeometryDash
{
    /// <summary>Provides values for the easing types.</summary>
    [Flags]
    public enum EasingType
    {
        None = 0,
        Ease = 1 << 2,
        Elastic = 1 << 3,
        Bounce = 1 << 4,
        Exponential = 1 << 5,
        Sine = 1 << 6,
        Back = 1 << 7,

        // Easing Modifiers
        In = 1,
        Out = 2,
        InOut = In | Out,
    }
}