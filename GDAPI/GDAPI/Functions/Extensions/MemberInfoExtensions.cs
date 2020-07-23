using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GDAPI.Functions.Extensions
{
    /// <summary>Contains extension methods for the <seealso cref="MemberInfo"/> class.</summary>
    public static class MemberInfoExtensions
    {
        // TODO: To Garyon
        #region MemberInfo
        /// <summary>Determines whether a member has a custom attribute of the specified type.</summary>
        /// <typeparam name="T">The type of the attribute to find.</typeparam>
        /// <param name="member">The member whose attribute to find.</param>
        /// <param name="firstInstance">The first instance of the attribute that is found, if any, otherwise <see langword="null"/>.</param>
        /// <returns>A value indicating whether the requested attribute was found.</returns>
        public static bool HasCustomAttribute<T>(this MemberInfo member, out T firstInstance)
            where T : Attribute
        {
            firstInstance = member.GetCustomAttribute<T>();
            return firstInstance != null;
        }
        /// <summary>Determines whether a member has custom attributes of the specified type.</summary>
        /// <typeparam name="T">The type of the attributes to find.</typeparam>
        /// <param name="member">The member whose attributes to find.</param>
        /// <param name="instances">A collection of instances of the attributes that were found. If no attributes were found, the collection is empty.</param>
        /// <returns>A value indicating whether the requested attribute was found at least once.</returns>
        public static bool HasCustomAttributes<T>(this MemberInfo member, out IEnumerable<T> instances)
            where T : Attribute
        {
            instances = member.GetCustomAttributes<T>();
            return instances.Any();
        }
        #endregion

        #region IEnumerable<MemberInfo>
        /// <summary>Determines whether a member has a custom attribute of the specified type.</summary>
        /// <typeparam name="T">The type of the attribute to find.</typeparam>
        /// <param name="members">The members whose attributes to find.</param>
        /// <returns>A dictionary mapping the found instances of the specified attribute to their respective members.</returns>
        public static Dictionary<T, MemberInfo> MapCustomAttributesToMembers<T>(this IEnumerable<MemberInfo> members)
            where T : Attribute
        {
            var result = new Dictionary<T, MemberInfo>();
            foreach (var m in members)
                if (m.HasCustomAttributes<T>(out var attributes))
                    foreach (var a in attributes)
                        result.Add(a, m);
            return result;
        }
        /// <summary>Determines whether a member has a custom attribute of the specified type.</summary>
        /// <typeparam name="TAttribute">The type of the attribute to find.</typeparam>
        /// <typeparam name="TResult">The resulting type of the keys in the dictionary.</typeparam>
        /// <param name="members">The members whose attributes to find.</param>
        /// <param name="selector">The selector that returns an object of the resulting key type from the specified attribute.</param>
        /// <returns>A dictionary mapping the found instances of the specified attribute to their respective members.</returns>
        public static Dictionary<TResult, MemberInfo> MapCustomAttributesToMembers<TAttribute, TResult>(this IEnumerable<MemberInfo> members, Func<TAttribute, TResult> selector)
            where TAttribute : Attribute
        {
            var result = new Dictionary<TResult, MemberInfo>();
            foreach (var m in members)
                if (m.HasCustomAttributes<TAttribute>(out var attributes))
                    foreach (var a in attributes)
                        result.Add(selector(a), m);
            return result;
        }
        /// <summary>Determines whether a member has a custom attribute of the specified type.</summary>
        /// <typeparam name="TMemberInfo">The type of the provided collection of <seealso cref="MemberInfo"/>.</typeparam>
        /// <typeparam name="TAttribute">The type of the attribute to find.</typeparam>
        /// <typeparam name="TResult">The resulting type of the keys in the dictionary.</typeparam>
        /// <param name="members">The members whose attributes to find.</param>
        /// <param name="selector">The selector that returns an object of the resulting key type from the specified attribute.</param>
        /// <returns>A dictionary mapping the found instances of the specified attribute to their respective members.</returns>
        public static Dictionary<TResult, TMemberInfo> MapCustomAttributesToMembers<TMemberInfo, TAttribute, TResult>(this IEnumerable<TMemberInfo> members, Func<TAttribute, TResult> selector)
            where TMemberInfo : MemberInfo
            where TAttribute : Attribute
        {
            var result = new Dictionary<TResult, TMemberInfo>();
            foreach (var m in members)
                if (m.HasCustomAttributes<TAttribute>(out var attributes))
                    foreach (var a in attributes)
                        result.Add(selector(a), m);
            return result;
        }
        #endregion
    }
}
