using GDAPI.Functions.Comparers;
using System;
using System.CodeDom;
using System.Collections.Generic;

namespace GDAPI.Functions.Extensions
{
    /// <summary>Provides extension methods for the <seealso cref="Range{T}"/> struct.</summary>
    public static class RangeExtensions
    {
        #region Range
        /// <summary>Sorts the <seealso cref="Range"/>s by their starting value, merges them and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range> SortAndMerge(this List<Range> ranges) => ranges.SortByStartValue().MergeRanges();
        /// <summary>Sorts the <seealso cref="Range"/>s by their starting value using a comparer and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range"/>s that will be processed. Changes will not be applied to this instance.</param>
        /// <param name="comparer">The comparer to use to compare the ranges when sorting them.</param>
        public static List<Range> SortByStartValue(this List<Range> ranges, Comparison<Range> comparer)
        {
            ranges = ranges.Clone();
            ranges.Sort(comparer);
            return ranges;
        }
        /// <summary>Sorts the <seealso cref="Range"/>s by their starting value in ascending order and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range> SortByStartValue(this List<Range> ranges)
        {
            return ranges.SortByStartValue(RangeComparers.RangeStartAscendingComparer);
        }
        /// <summary>Merges the <seealso cref="Range"/>s and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range> MergeRanges(this List<Range> ranges)
        {
            ranges = ranges.Clone();

            for (int i = 1; i < ranges.Count; i++)
            {
                // Merge ranges that overlap
                if (ranges[i].Start.Value <= ranges[i - 1].End.Value)
                {
                    ranges[i - 1] = (ranges[i - 1].Start)..(ranges[i].End);
                    ranges.RemoveAt(i);
                    i--;
                }
            }

            return ranges;
        }

        #region Boundary Checks
        /// <summary>Determines whether a value is after the inclusive start.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is after the inclusive start.</param>
        public static bool AfterStart(this Range range, int value)
        {
            return value >= range.Start.Value;
        }
        /// <summary>Determines whether a value is before the exclusive end.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is before the exclusive end.</param>
        public static bool BeforeEnd(this Range range, int value)
        {
            return value < range.End.Value;
        }

        /// <summary>Determines whether a value is contained within the range. The <seealso cref="Index.IsFromEnd"/> properties of the start and end indices are ignored./summary>
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
        /// <summary>Sorts the <seealso cref="Range{T}"/>s by their starting value using a comparer and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range{T}"/>s that will be processed. Changes will not be applied to this instance.</param>
        /// <param name="comparer">The comparer to use to compare the ranges when sorting them.</param>
        public static List<Range<T>> SortByStartValue<T>(this List<Range<T>> ranges, Comparison<Range<T>> comparer)
            where T : IComparable<T>, IEquatable<T>
        {
            ranges = ranges.Clone();
            ranges.Sort(comparer);
            return ranges;
        }
        /// <summary>Sorts the <seealso cref="Range{T}"/>s by their starting value in ascending order and returns the resulting list. The provided list remains intact.</summary>
        /// <param name="ranges">The list of <seealso cref="Range{T}"/>s that will be processed. Changes will not be applied to this instance.</param>
        public static List<Range<T>> SortByStartValue<T>(this List<Range<T>> ranges)
            where T : IComparable<T>, IEquatable<T>
        {
            return ranges.SortByStartValue();
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
