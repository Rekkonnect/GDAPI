using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GDAPI.Functions.Extensions
{
    /// <summary>Contains extension methods for objects.</summary>
    public static class ObjectExtensions
    {
        /// <summary>Gets property infos from an object.</summary>
        /// <param name="data">The object to get the property infos from.</param>
        /// <returns>An array of <see cref="PropertyInfo"/>.</returns>
        public static PropertyInfo[] GetPropertyInfos(this object data) 
            => data.GetType().GetProperties();

        /// <summary>Gets all of the property names from an object.</summary>
        /// <param name="data">The object to get the property names from.</param>
        /// <returns>An array of string with the property names.</returns>
        public static string[] GetPropertiesName(this object data) 
            => data.GetPropertyInfos().Select(o => o.Name).ToArray();

        /// <summary>Converts any object into a dictionary containing it's properties and values.</summary>
        /// <param name="data">The object to convert into a dictionary</param>
        public static Dictionary<string, object> ToDictionary(this object? data)
        {
            if (data == null)
                return new Dictionary<string, object>();
            
            var props = data.GetType().GetProperties();
            return props.ToDictionary(prop => prop.Name, prop => prop.GetValue(data));
        }
    }
}