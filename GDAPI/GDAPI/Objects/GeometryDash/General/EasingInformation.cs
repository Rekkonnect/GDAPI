using GDAPI.Enumerations.GeometryDash;
using static GDAPI.Functions.GeometryDash.ValueGenerator;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Contains detailed information about an easing.</summary>
    public struct EasingInformation
    {
        /// <summary>The easing method of the easing.</summary>
        public EasingMethod Method;
        /// <summary>Determines whether the easing has easing in.</summary>
        public bool EasingIn;
        /// <summary>Determines whether the easing has easing out.</summary>
        public bool EasingOut;

        /// <summary>Initializes a new instance of the <seealso cref="EasingInformation"/> struct.</summary>
        /// <param name="method">The easing method of the easing.</param>
        /// <param name="easingIn">Determines whether the easing has easing in.</param>
        /// <param name="easingOut">Determines whether the easing has easing out.</param>
        public EasingInformation(EasingMethod method, bool easingIn, bool easingOut)
        {
            Method = method;
            EasingIn = easingIn;
            EasingOut = easingOut;
        }
        /// <summary>Initializes a new instance of the <seealso cref="EasingInformation"/> struct.</summary>
        /// <param name="easing">The <seealso cref="Easing"/> whose information to retrieve.</param>
        public EasingInformation(Easing easing)
        {
            Method = GetEasingMethod(easing);
            EasingIn = HasEasingIn(easing);
            EasingOut = HasEasingOut(easing);
        }

        /// <summary>Generates the <seealso cref="Easing"/> that this information matches.</summary>
        public Easing ToEasing() => GenerateEasing(Method, EasingIn, EasingOut);
    }
}
