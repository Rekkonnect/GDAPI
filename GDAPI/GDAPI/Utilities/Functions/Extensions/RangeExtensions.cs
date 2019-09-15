using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Functions.Extensions
{
    /// <summary>Provides extension methods for the <seealso cref="Range{T}"/> struct.s</summary>
    public static class RangeExtensions
    {
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<byte> range, byte value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<short> range, short value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<int> range, int value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<long> range, long value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<sbyte> range, sbyte value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<ushort> range, ushort value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<uint> range, uint value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<ulong> range, ulong value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<float> range, float value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<double> range, double value) => range.Begin <= value && value <= range.End;
        /// <summary>Determines whether a value is within the bounds of the range.</summary>
        /// <param name="range">The range which may contain the evaluated value.</param>
        /// <param name="value">The value to check whether it is within the range.</param>
        public static bool IsWithinBounds(this Range<decimal> range, decimal value) => range.Begin <= value && value <= range.End;
    }
}
