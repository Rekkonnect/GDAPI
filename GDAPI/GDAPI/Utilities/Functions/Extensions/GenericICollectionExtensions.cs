using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Functions.Extensions
{
    /// <summary>Contains extension functions for the <seealso cref="ICollection{T}"/> interface.</summary>
    public static class GenericICollectionExtensions
    {
        /// <summary>Merges the collection's lists into a single list.</summary>
        /// <typeparam name="T">The type of the elements in each list of the collection.</typeparam>
        /// <param name="l">The collection of lists to merge into a list.</param>
        public static List<T> Merge<T>(this ICollection<List<T>> l)
        {
            var result = new List<T>();
            for (int i = 0; i < l.Count; i++)
                result.AddRange(l.ElementAt(i));
            return result;
        }
    }
}
