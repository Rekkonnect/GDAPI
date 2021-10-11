using System;
using System.Collections.Generic;

namespace GDAPI.Functions.Extensions
{
    /// <summary>Provides generic extension methods for lists.</summary>
    public static class GenericListExtensions
    {
        #region Cloning
        /// <summary>Clones a list.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list to clone.</param>
        public static List<T> Clone<T>(this List<T> l) => new(l);
        /// <summary>Clones a list of lists.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list of lists to clone.</param>
        public static List<List<T>> Clone<T>(this List<List<T>> l)
        {
            var result = new List<List<T>>();
            for (int i = 0; i < l.Count; i++)
                result.Add(new List<T>(l[i]));
            return result;
        }

        /// <summary>Clones a list, sorts by the default comparer and then returns the cloned list.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list to clone and sort.</param>
        public static List<T> CloneSort<T>(this List<T> l)
        {
            var result = l.Clone();
            result.Sort();
            return result;
        }
        /// <summary>Clones a list, sorts by a provided <seealso cref="IComparer{T}"/> and then returns the cloned list.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list to clone and sort.</param>
        /// <param name="comparer">The <seealso cref="IComparer{T}"/> implementation to use when comparing elements, or <see langword="null"/> to use the default comparer.</param>
        public static List<T> CloneSort<T>(this List<T> l, IComparer<T> comparer)
        {
            var result = l.Clone();
            result.Sort(comparer);
            return result;
        }
        /// <summary>Clones a list, sorts by a provided <seealso cref="Comparison{T}"/> and then returns the cloned list.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list to clone and sort.</param>
        /// <param name="comparison">The <seealso cref="Comparison{T}"/> to use when comparing elements.</param>
        public static List<T> CloneSort<T>(this List<T> l, Comparison<T> comparison)
        {
            var result = l.Clone();
            result.Sort(comparison);
            return result;
        }
        #endregion

        #region Contain Checks
        /// <summary>Determines whether the list contains all the elements of an other list.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list whose elements have to be contained on the other list.</param>
        /// <param name="containedList">The list other list to check.</param>
        public static bool ContainsAll<T>(this List<T> list, List<T> containedList)
        {
            if (containedList.Count != list.Count)
                return false;
            var tempList = list.Clone();
            var tempContained = containedList.Clone();
            for (int i = 0; i < tempContained.Count; i++)
                if (!tempList.Remove(tempContained[i]))
                    return false;
            return true;
        }
        /// <summary>Determines whether the list contains all the elements of an other list in any order.</summary>
        /// <typeparam name="T">The type of the list elements.</typeparam>
        /// <param name="l">The list whose elements have to be contained on the other list.</param>
        /// <param name="containedList">The list other list to check.</param>
        public static bool ContainsUnordered<T>(this List<T> list, List<T> containedList)
        {
            var tempList = containedList.Clone();
            var tempContained = containedList.Clone();
            for (int i = 0; i < tempList.Count; i++)
                tempList.Remove(tempContained[i]);
            return list.Count - containedList.Count == tempList.Count;
        }
        #endregion

        #region Intradimensional
        /// <summary>Gets the lengths of the list of arrays.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="l">The list of arrays to get the lengths of.</param>
        public static int[] GetLengths<T>(this List<T[]> l)
        {
            int[] lengths = new int[l.Count];
            for (int i = 0; i < l.Count; i++)
                lengths[i] = l[i].Length;
            return lengths;
        }
        /// <summary>Converts the list of arrays to a two-dimensional array.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="l">The list of arrays to convert.</param>
        public static T[,] ToTwoDimensionalArray<T>(this List<T[]> l)
        {
            var ar = new T[l.Count, l.GetLengths().Max()];
            for (int i = 0; i < l.Count; i++)
                for (int j = 0; j < l[i].Length; j++)
                    ar[i, j] = l[i][j];
            return ar;
        }
        #endregion
    }
}
