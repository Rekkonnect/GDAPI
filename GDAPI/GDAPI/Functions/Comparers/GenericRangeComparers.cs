using System;

namespace GDAPI.Functions.Comparers
{
    /// <summary>Contains comparers for the <seealso cref="Range{T}"/> struct.</summary>
    public static class GenericRangeComparers
    {
        /// <summary>Compares the starts of two <seealso cref="Range{T}"/>s with ascending (default) order.</summary>
        /// <param name="a">The first <seealso cref="Range{T}"/> whose start to compare.</param>
        /// <param name="b">The second <seealso cref="Range{T}"/> whose start to compare.</param>
        /// <returns>A result complying with the <seealso cref="IComparable{T}.CompareTo(T)"/> result table.</returns>
        public static int RangeStartAscendingComparer<T>(Range<T> a, Range<T> b)
            where T : IComparable<T>, IEquatable<T>
        {
            return a.Begin.CompareTo(b.Begin);
        }
        /// <summary>Compares the ends of two <seealso cref="Range{T}"/>s with ascending (default) order.</summary>
        /// <param name="a">The first <seealso cref="Range{T}"/> whose end to compare.</param>
        /// <param name="b">The second <seealso cref="Range{T}"/> whose end to compare.</param>
        /// <returns>A result complying with the <seealso cref="IComparable{T}.CompareTo(T)"/> result table.</returns>
        public static int RangeEndAscendingComparer<T>(Range<T> a, Range<T> b)
            where T : IComparable<T>, IEquatable<T>
        {
            return a.End.CompareTo(b.End);
        }
        /// <summary>Compares the starts of two <seealso cref="Range{T}"/>s with descending (opposite) order.</summary>
        /// <param name="a">The first <seealso cref="Range{T}"/> whose start to compare.</param>
        /// <param name="b">The second <seealso cref="Range{T}"/> whose start to compare.</param>
        /// <returns>A result complying with the <seealso cref="IComparable{T}.CompareTo(T)"/> result table.</returns>
        public static int RangeStartDescendingComparer<T>(Range<T> a, Range<T> b)
            where T : IComparable<T>, IEquatable<T>
        {
            return -RangeStartAscendingComparer(a, b);
        }
        /// <summary>Compares the ends of two <seealso cref="Range{T}"/>s with descending (opposite) order.</summary>
        /// <param name="a">The first <seealso cref="Range{T}"/> whose end to compare.</param>
        /// <param name="b">The second <seealso cref="Range{T}"/> whose end to compare.</param>
        /// <returns>A result complying with the <seealso cref="IComparable{T}.CompareTo(T)"/> result table.</returns>
        public static int RangeEndDescendingComparer<T>(Range<T> a, Range<T> b)
            where T : IComparable<T>, IEquatable<T>
        {
            return -RangeEndAscendingComparer(a, b);
        }
    }
}
