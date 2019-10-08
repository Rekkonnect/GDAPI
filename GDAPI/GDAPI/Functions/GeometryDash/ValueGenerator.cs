using System;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.PropertyAnalysis;

namespace GDAPI.Functions.GeometryDash
{
    /// <summary>Provides helpful functions for generating values for several object properties that are enumerated.</summary>
    public static class ValueGenerator
    {
        #region Easing
        private const EasingMethod LastEasingMethod = EasingMethod.Back;

        /// <summary>Generates an easing value from the easing type properties.</summary>
        /// <param name="easingMethod">The easing method.</param>
        /// <param name="enableIn">Determines whether the easing will be applied at the start of the transformation.</param>
        /// <param name="enableOut">Determines whether the easing will be applied at the end of the transformation.</param>
        public static int GenerateEasingValue(EasingMethod easingMethod, bool enableIn, bool enableOut) => GenerateEasingType(easingMethod, enableIn, enableOut, EasingToEasingValue);
        /// <summary>Generates an <seealso cref="Easing"/> from the easing type properties.</summary>
        /// <param name="easingMethod">The easing method.</param>
        /// <param name="enableIn">Determines whether the easing will be applied at the start of the transformation.</param>
        /// <param name="enableOut">Determines whether the easing will be applied at the end of the transformation.</param>
        public static Easing GenerateEasing(EasingMethod easingMethod, bool enableIn, bool enableOut) => (Easing)GenerateEasingType(easingMethod, enableIn, enableOut, EasingToEasingValue);
        /// <summary>Generates an <seealso cref="EasingType"/> from the easing type properties.</summary>
        /// <param name="easingMethod">The easing method.</param>
        /// <param name="enableIn">Determines whether the easing will be applied at the start of the transformation.</param>
        /// <param name="enableOut">Determines whether the easing will be applied at the end of the transformation.</param>
        public static EasingType GenerateEasingType(EasingMethod easingMethod, bool enableIn, bool enableOut) => GenerateEasingType(easingMethod, enableIn, enableOut, EasingToEasingType);

        /// <summary>Gets the <seealso cref="EasingMethod"/> of the <seealso cref="Easing"/>.</summary>
        /// <param name="easing">The <seealso cref="Easing"/> whose <seealso cref="EasingMethod"/> to retrieve.</param>
        public static EasingMethod GetEasingMethod(Easing easing) => easing > Easing.None ? (EasingMethod)(((int)easing - 1) / 3 + 1) : EasingMethod.None;
        /// <summary>Determines whether an <seealso cref="Easing"/> has easing in.</summary>
        /// <param name="easing">The <seealso cref="Easing"/> to analyze.</param>
        public static bool HasEasingIn(Easing easing) => ((int)easing - 1) % 3 != 2;
        /// <summary>Determines whether an <seealso cref="Easing"/> has easing out.</summary>
        /// <param name="easing">The <seealso cref="Easing"/> to analyze.</param>
        public static bool HasEasingOut(Easing easing) => ((int)easing - 1) % 3 != 1;

        private static int EasingToEasingValue(EasingMethod easingMethod, bool enableIn, bool enableOut) => ((int)easingMethod - 1) * 3 + 1 + (enableIn ? 0 : 2) + (enableOut ? 0 : 1);
        private static EasingType EasingToEasingType(EasingMethod easingMethod, bool enableIn, bool enableOut) => (EasingType)(1 << ((int)easingMethod + 3)) | (enableIn ? EasingType.In : EasingType.None) | (enableOut ? EasingType.Out : EasingType.None);

        private static T GenerateEasingType<T>(EasingMethod easingMethod, bool enableIn, bool enableOut, EasingTypeConverter<T> converter, T defaultValue = default)
        {
            if (easingMethod > EasingMethod.None && easingMethod <= LastEasingMethod)
            {
                if (enableIn || enableOut)
                    return converter(easingMethod, enableIn, enableOut);
                else
                    throw new ArgumentException("The easing in and out parameters were both false which is invalid.");
            }
            else if (easingMethod == EasingMethod.None)
                return defaultValue;
            else
                throw new ArgumentException("The easing type value was beyond the easing type range.");
        }

        private delegate T EasingTypeConverter<T>(EasingMethod easingMethod, bool enableIn, bool enableOut);
        #endregion

        #region Z Layer
        /// <summary>Generates a <seealso cref="ZLayer"/> value from a <seealso cref="ZLayerPosition"/> and a layer on the relative position.</summary>
        /// <param name="position">The <seealso cref="ZLayerPosition"/>.</param>
        /// <param name="layer">The layer on the relative position.</param>
        public static ZLayer GenerateZLayer(ZLayerPosition position, int layer)
        {
            var result = (int)position * 2 * layer + ZLayer.B1;
            if (position == ZLayerPosition.Bottom)
                result += 2;
            return result;
        }
        /// <summary>Generates a <seealso cref="ZLayer"/> value from a <seealso cref="ZLayerInformation"/>.</summary>
        /// <param name="zLayerInformation">The <seealso cref="ZLayerInformation"/> object containing information about the Z Layer.</param>
        public static ZLayer GenerateZLayer(ZLayerInformation zLayerInformation) => GenerateZLayer(zLayerInformation.Position, zLayerInformation.Layer);

        /// <summary>Gets the <seealso cref="ZLayerPosition"/> of the specified <seealso cref="ZLayer"/>.</summary>
        /// <param name="zLayer">The <seealso cref="ZLayer"/> whose <seealso cref="ZLayerPosition"/> to get.</param>
        public static ZLayerPosition GetZLayerPosition(ZLayer zLayer) => zLayer < ZLayer.T1 ? ZLayerPosition.Bottom : ZLayerPosition.Top;
        /// <summary>Gets the absolute layer of the <seealso cref="ZLayer"/>.</summary>
        /// <param name="zLayer">The <seealso cref="ZLayer"/> whose absolute layer to get.</param>
        public static int GetAbsoluteZLayer(ZLayer zLayer)
        {
            var position = GetZLayerPosition(zLayer);
            var layer = (zLayer - ZLayer.B1) / 2;
            if (position == ZLayerPosition.Bottom)
                layer++;
            return layer;
        }
        #endregion
    }
}
