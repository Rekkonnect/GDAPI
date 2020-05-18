using System;

namespace GDAPI.Functions.Comparers
{
    /// <summary>Contains comparers for the <seealso cref="Range"/> struct.</summary>
    public static class RangeComparers
    {
        /// <summary>Compares the starts of two <seealso cref="Range"/>s with ascending (default) order. It ignores the <seealso cref="Index.IsFromEnd"/> property of the start <seealso cref="Index"/>.</summary>
        /// <param name="a">The first <seealso cref="Range"/> whose start to compare.</param>
        /// <param name="b">The second <seealso cref="Range"/> whose start to compare.</param>
        /// <returns>A result complying with the <seealso cref="IComparable{T}.CompareTo(T)"/> result table.</returns>
        public static int RangeStartAscendingComparer(Range a, Range b)
        {
            return a.Start.Value.CompareTo(b.Start.Value);
        }
        /// <summary>Compares the ends of two <seealso cref="Range"/>s with ascending (default) order. It ignores the <seealso cref="Index.IsFromEnd"/> property of the end <seealso cref="Index"/>.</summary>
        /// <param name="a">The first <seealso cref="Range"/> whose end to compare.</param>
        /// <param name="b">The second <seealso cref="Range"/> whose end to compare.</param>
        /// <returns>A result complying with the <seealso cref="IComparable{T}.CompareTo(T)"/> result table.</returns>
        public static int RangeEndAscendingComparer(Range a, Range b)
        {
            return a.End.Value.CompareTo(b.End.Value);
        }
        /// <summary>Compares the starts of two <seealso cref="Range"/>s with descending (opposite) order. It ignores the <seealso cref="Index.IsFromEnd"/> property of the start <seealso cref="Index"/>.</summary>
        /// <param name="a">The first <seealso cref="Range"/> whose start to compare.</param>
        /// <param name="b">The second <seealso cref="Range"/> whose start to compare.</param>
        /// <returns>A result complying with the <seealso cref="IComparable{T}.CompareTo(T)"/> result table.</returns>
        public static int RangeStartDescendingComparer(Range a, Range b) => -RangeStartAscendingComparer(a, b);
        /// <summary>Compares the ends of two <seealso cref="Range"/>s with descending (opposite) order. It ignores the <seealso cref="Index.IsFromEnd"/> property of the end <seealso cref="Index"/>.</summary>
        /// <param name="a">The first <seealso cref="Range"/> whose end to compare.</param>
        /// <param name="b">The second <seealso cref="Range"/> whose end to compare.</param>
        /// <returns>A result complying with the <seealso cref="IComparable{T}.CompareTo(T)"/> result table.</returns>
        public static int RangeEndDescendingComparer(Range a, Range b) => -RangeEndAscendingComparer(a, b);
        
        // TODO: Add implementations caring for the IsFromEnd properties
    }
}
