using System;
using System.Linq;
using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Information.GeometryDash
{
    /// <summary>Contains information about level object properties.</summary>
    public static class LevelObjectInformation
    {
        /// <summary>The highest parameter ID currently in the game.</summary>
        public const int ParameterCount = 118;

        private static readonly ObjectParameterTypeAttribute[] objectParameterAttributes = new ObjectParameterTypeAttribute[ParameterCount];
        private static readonly Type[] objectParameterAttributeTypes = new Type[ParameterCount];

        static LevelObjectInformation()
        {
            var type = typeof(ObjectParameter);
            var baseAttributeType = typeof(ObjectParameterTypeAttribute);
            var attributeTypes = baseAttributeType.Assembly.GetTypes().Where(t => t.BaseType == baseAttributeType);
            foreach (var n in Enum.GetNames(type))
            {
                var m = type.GetMember(n).FirstOrDefault();
                ObjectParameterTypeAttribute a = null;
                for (int i = 0; i < attributeTypes.Count() && a == null; i++)
                    a = m.GetCustomAttributes(baseAttributeType, false).FirstOrDefault() as ObjectParameterTypeAttribute;
                int value = (int)Enum.Parse(type, n);
                if (value > 0)
                {
                    objectParameterAttributes[value - 1] = a;
                    objectParameterAttributeTypes[value - 1] = a?.GetType();
                }
            }
        }

        /// <summary>Returns the <seealso cref="Type"/> of the attribute of the chosen parameter ID in the <seealso cref="ObjectParameter"/> enum.</summary>
        /// <param name="parameterID">The parameter ID to get the string type of.</param>
        public static ObjectParameterTypeAttribute GetParameterIDAttribute(int parameterID) => objectParameterAttributes[parameterID - 1];
        /// <summary>Returns the <seealso cref="Type"/> of the attribute of the chosen parameter ID in the <seealso cref="ObjectParameter"/> enum.</summary>
        /// <param name="parameterID">The parameter ID to get the string type of.</param>
        public static Type GetParameterIDAttributeType(int parameterID) => objectParameterAttributeTypes[parameterID - 1];
        /// <summary>Returns the <seealso cref="Type"/> of the chosen parameter ID in the <seealso cref="ObjectParameter"/> enum.</summary>
        /// <param name="parameterID">The parameter ID to get the string type of.</param>
        public static Type GetParameterIDType(int parameterID) => objectParameterAttributes[parameterID - 1].Type;
    }
}
