using System;

namespace GDAPI.Enumerations.GeometryDash
{
    /// <summary>Provides values for the easing types.</summary>
    [Flags]
    public enum EasingType
    {
        // Easing Types
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

        // Easing Combinations
        EaseInOut = Ease | InOut,
        EaseIn = Ease | In,
        EaseOut = Ease | Out,
        ElasticInOut = Elastic | InOut,
        ElasticIn = Elastic | In,
        ElasticOut = Elastic | Out,
        BounceInOut = Bounce | InOut,
        BounceIn = Bounce | In,
        BounceOut = Bounce | Out,
        ExponentialInOut = Exponential | InOut,
        ExponentialIn = Exponential | In,
        ExponentialOut = Exponential | Out,
        SineInOut = Sine | InOut,
        SineIn = Sine | In,
        SineOut = Sine | Out,
        BackInOut = Back | InOut,
        BackIn = Back | In,
        BackOut = Back | Out,
    }
}