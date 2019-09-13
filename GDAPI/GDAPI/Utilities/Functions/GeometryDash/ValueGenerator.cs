using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Functions.GeometryDash
{
    /// <summary>Provides helpful functions for generating values for several object properties that are enumerated.</summary>
    public static class ValueGenerator
    {
        #region Easing
        public static int GenerateEasingValue(int easingType, bool enableIn, bool enableOut)
        {
            if (easingType > 0 && easingType < 7)
            {
                if (enableIn || enableOut)
                    return (easingType - 1) * 3 + 1 + (enableIn ? 0 : 2) + (enableOut ? 0 : 1);
                else
                    throw new ArgumentException("The easing in and out parameters were both false which is invalid.");
            }
            else if (easingType == 0)
                return 0;
            else
                throw new ArgumentException("The easing type value was beyond the easing type range.");
        }

        public static EasingType GenerateEasingType(int easingType, bool enableIn, bool enableOut)
        {
            if (easingType > 0 && easingType < 7)
            {
                if (enableIn || enableOut)
                    return (EasingType)(1 << (easingType + 3)) | (enableIn ? EasingType.In : EasingType.None) | (enableOut ? EasingType.Out : EasingType.None);
                else
                    throw new ArgumentException("The easing in and out parameters were both false which is invalid.");
            }
            else if (easingType == 0)
                return EasingType.None;
            else
                throw new ArgumentException("The easing type value was beyond the easing type range.");
        }
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
        /// <param name="zLayer">The <seealso cref="ZLayer"/> whole absolute layer to get.</param>
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