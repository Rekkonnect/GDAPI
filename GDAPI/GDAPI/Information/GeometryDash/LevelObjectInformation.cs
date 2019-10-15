using System;
using System.Linq;
using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Information.GeometryDash
{
    /// <summary>Contains information about level object properties.</summary>
    public static class LevelObjectInformation
    {
        /// <summary>The highest property ID currently in the game.</summary>
        public const int PropertyCount = 118;

        private static readonly ObjectParameterTypeAttribute[] objectPropertyAttributes = new ObjectParameterTypeAttribute[PropertyCount];
        private static readonly Type[] objectPropertyAttributeTypes = new Type[PropertyCount];

        static LevelObjectInformation()
        {
            var type = typeof(ObjectProperty);
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
                    objectPropertyAttributes[value - 1] = a;
                    objectPropertyAttributeTypes[value - 1] = a?.GetType();
                }
            }
        }

        /// <summary>Returns the <seealso cref="Type"/> of the attribute of the chosen property ID in the <seealso cref="ObjectProperty"/> enum.</summary>
        /// <param name="parameterID">The property ID to get the string type of.</param>
        public static ObjectParameterTypeAttribute GetPropertyIDAttribute(int parameterID) => objectPropertyAttributes[parameterID - 1];
        /// <summary>Returns the <seealso cref="Type"/> of the attribute of the chosen property ID in the <seealso cref="ObjectProperty"/> enum.</summary>
        /// <param name="parameterID">The property ID to get the string type of.</param>
        public static Type GetPropertyIDAttributeType(int parameterID) => objectPropertyAttributeTypes[parameterID - 1];
        /// <summary>Returns the <seealso cref="Type"/> of the chosen property ID in the <seealso cref="ObjectProperty"/> enum.</summary>
        /// <param name="parameterID">The property ID to get the string type of.</param>
        public static Type GetPropertyIDType(int parameterID) => objectPropertyAttributes[parameterID - 1].Type;
    }
}
