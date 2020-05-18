using GDAPI.Functions.Comparers;
using System;
using System.Collections.Generic;

namespace GDAPI.Functions.Extensions
{
    /// <summary>Provides extension methods for the <seealso cref="Range{T}"/> struct.</summary>
    public static class RangeExtensions
    {
        #region Range
        /// <summary>Sorts the <seealso cref="Range"/>s by their starting value, merges them and returns the resulting list. The <seealso cref="Index.IsFromEnd"/> properties of the start and end indices are ignored. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range> SortAndMerge(this List<Range> ranges) => ranges.SortByStartValue().MergeRanges();
        /// <summary>Sorts the <seealso cref="Range"/>s by their starting value in ascending order and returns the resulting list. The <seealso cref="Index.IsFromEnd"/> properties of the start and end indices are ignored. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range> SortByStartValue(this List<Range> ranges)
        {
            return ranges.CloneSort(RangeComparers.RangeStartAscendingComparer);
        }
        /// <summary>Merges the <seealso cref="Range"/>s and returns the resulting list. The <seealso cref="Index.IsFromEnd"/> properties of the start and end indices are ignored. The original list must be sorted. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range"/>s that will be processed. The list must be sorted in order for the ranged to be properly merged. Changes will not be applied to this instance.</param>
        public static List<Range> MergeRanges(this List<Range> ranges)
        {
            ranges = ranges.Clone();

            for (int i = 1; i < ranges.Count; i++)
            {
                // Merge ranges that overlap
                if (ranges[i].Start.Value <= ranges[i - 1].End.Value)
                {
                    if (ranges[i].End.Value > ranges[i - 1].End.Value)
                        ranges[i - 1] = (ranges[i - 1].Start)..(ranges[i].End);
                    ranges.RemoveAt(i);
                    i--;
                }
            }

            return ranges;
        }
        /// <summary>Determines whether a value is contained within a list of ranges, using linear searching.</summary>
        /// <param name="ranges">The <seealso cref="List{T}"/> of <seealso cref="Range"/>s.</param>
        /// <param name="value">The value to try to find.</param>
        public static bool Contains(this List<Range> ranges, int value)
        {
            foreach (var r in ranges)
                if (r.Contains(value))
                    return true;
            return false;
        }
        /// <summary>Determines whether a value is contained within a sorted and merged list of ranges, using binary searching.</summary>
        /// <param name="ranges">The <seealso cref="List{T}"/> of <seealso cref="Range"/>s.</param>
        /// <param name="value">The value to try to find.</param>
        public static bool ContainsBinarySearch(this List<Range> ranges, int value)
        {
            int min = 0;
            int max = ranges.Count - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (!ranges[mid].BeforeEnd(value))
                    min = mid + 1;
                else if (!ranges[mid].AfterStart(value))
                    max = mid - 1;
                else
                    return true;
            }
            return false;
        }

        /// <summary>Gets the absolute range length of the <seealso cref="Range"/>, which is the distance of the start value from the end value.</summary>
        /// <param name="r">The <seealso cref="Range"/> whose absolute range length to get.</param>
        public static int GetAbsoluteRangeLength(this Range r) => r.End.Value - r.Start.Value;

        /// <summary>Determines whether the range is absolute, meaning that netiher the start nor the end indices are related to the end of the collection, that is having their <seealso cref="Index.IsFromEnd"/> property set to <see langword="false"/>.</summary>
        /// <param name="r">The range to determine whether it is absolute.</param>
        public static bool IsAbsoluteRange(this Range r) => !r.Start.IsFromEnd && !r.End.IsFromEnd;

        /// <summary>Converts this <seealso cref="Range"/> instance into a <seealso cref="Range{T}"/> object.</summary>
        /// <param name="r">The <seealso cref="Range"/> to convert.</param>
        public static Range<int> ToGenericRange(this Range r)
        {
            if (!r.IsAbsoluteRange())
                throw new InvalidOperationException("Cannot construct a range of indices that are related to the collection end.");
            return new Range<int>(r.Start.Value, r.End.Value);
        }

        #region Boundary Checks
        /// <summary>Determines whether a value is after the inclusive start. The <seealso cref="Index.IsFromEnd"/> property of the start <seealso cref="Index"/> is ignored.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is after the inclusive start.</param>
        public static bool AfterStart(this Range range, int value)
        {
            return value >= range.Start.Value;
        }
        /// <summary>Determines whether a value is before the exclusive end. The <seealso cref="Index.IsFromEnd"/> property of the end <seealso cref="Index"/> is ignored.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is before the exclusive end.</param>
        public static bool BeforeEnd(this Range range, int value)
        {
            return value < range.End.Value;
        }

        /// <summary>Determines whether a value is contained within the range. The <seealso cref="Index.IsFromEnd"/> properties of the start and end indices are ignored.</summary>
        /// <paramref name="range"/>The range that may contain the specified value.</summary>
        /// <param name="value">The value to check whether it's within the range.</param>
        public static bool Contains(this Range range, int value)
        {
            return range.Start.Value <= value && value < range.End.Value;
        }
        #endregion
        #endregion

        #region Range<T>
        /// <summary>Sorts the <seealso cref="Range{T}"/>s by their starting value, merges them and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range{T}"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range<T>> SortAndMerge<T>(this List<Range<T>> ranges)
            where T : IComparable<T>, IEquatable<T>
        {
            return ranges.SortByStartValue().MergeRanges();
        }
        /// <summary>Sorts the <seealso cref="Range{T}"/>s by their starting value in ascending order and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range{T}"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range<T>> SortByStartValue<T>(this List<Range<T>> ranges)
            where T : IComparable<T>, IEquatable<T>
        {
            return ranges.CloneSort(GenericRangeComparers.RangeStartAscendingComparer);
        }
        /// <summary>Merges the <seealso cref="Range{T}"/>s and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range{T}"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range<T>> MergeRanges<T>(this List<Range<T>> ranges)
            where T : IComparable<T>, IEquatable<T>
        {
            ranges = ranges.Clone();

            for (int i = 1; i < ranges.Count; i++)
            {
                // Merge ranges that overlap
                if (ranges[i].Begin.CompareTo(ranges[i - 1].End) <= 0)
                {
                    if (ranges[i].End.CompareTo(ranges[i - 1].End) > 0)
                        ranges[i - 1] = new Range<T>(ranges[i - 1].Begin, ranges[i].End);
                    ranges.RemoveAt(i);
                    i--;
                }
            }

            return ranges;
        }

        #region Boundary Checks
        /// <summary>Determines whether a value is after the start.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is after the start.</param>
        /// <param name="inclusive">Determines whether the start is inclusive or not.</param>
        public static bool AfterStart<T>(this Range<T> range, T value, bool inclusive)
            where T : IComparable<T>, IEquatable<T>
        {
            return inclusive ? range.AfterInclusiveStart(value) : range.AfterExclusiveStart(value);
        }
        /// <summary>Determines whether a value is after the inclusive start.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is after the inclusive start.</param>
        public static bool AfterInclusiveStart<T>(this Range<T> range, T value)
            where T : IComparable<T>, IEquatable<T>
        {
            return value.CompareTo(range.Begin) >= 0;
        }
        /// <summary>Determines whether a value is after the exclusive start.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is after the exclusive start.</param>
        public static bool AfterExclusiveStart<T>(this Range<T> range, T value)
            where T : IComparable<T>, IEquatable<T>
        {
            return value.CompareTo(range.Begin) > 0;
        }
        /// <summary>Determines whether a value is before the end.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is before the end.</param>
        /// <param name="inclusive">Determines whether the end is inclusive or not.</param>
        public static bool BeforeEnd<T>(this Range<T> range, T value, bool inclusive)
            where T : IComparable<T>, IEquatable<T>
        {
            return inclusive ? range.BeforeInclusiveEnd(value) : range.BeforeExclusiveEnd(value);
        }
        /// <summary>Determines whether a value is before the inclusive end.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is before the inclusive end.</param>
        public static bool BeforeInclusiveEnd<T>(this Range<T> range, T value)
            where T : IComparable<T>, IEquatable<T>
        {
            return value.CompareTo(range.End) <= 0;
        }
        /// <summary>Determines whether a value is before the exclusive end.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is before the exclusive end.</param>
        public static bool BeforeExclusiveEnd<T>(this Range<T> range, T value)
            where T : IComparable<T>, IEquatable<T>
        {
            return value.CompareTo(range.End) < 0;
        }

        /// <summary>Determines whether a value is within the inclusive bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinInclusiveBounds<T>(this Range<T> range, T value)
            where T : IComparable<T>, IEquatable<T>
        {
            return range.IsWithinBounds(value, true, true);
        }
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        /// <param name="inclusiveStart">Determines whether the start is inclusive or not.</param>
        /// <param name="inclusiveEnd">Determines whether the end is inclusive or not.</param>
        public static bool IsWithinBounds<T>(this Range<T> range, T value, bool inclusiveStart, bool inclusiveEnd)
            where T : IComparable<T>, IEquatable<T>
        {
            return range.AfterStart(value, inclusiveStart) && range.BeforeEnd(value, inclusiveEnd);
        }
        #endregion
        #endregion
    }
}
