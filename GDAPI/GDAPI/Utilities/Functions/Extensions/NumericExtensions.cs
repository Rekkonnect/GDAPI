using System;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace GDAPI.Utilities.Functions.Extensions
{
    /// <summary>Contains extension methods for numeric structures.</summary>
    public static class NumericExtensions
    {
        #region IsPowerOfTwo
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this byte value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount(value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1. Negative values are ignored.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this short value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount((uint)value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1. Negative values are ignored.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this int value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount((uint)value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1. Negative values are ignored.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this long value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount((ulong)value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1. Negative values are ignored.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this sbyte value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount((uint)value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this ushort value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount(value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this uint value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount(value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this ulong value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount(value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        #endregion

        #region OneOrGreater
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static byte OneOrGreater(this byte a) => a < 1 ? (byte)1 : a;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static short OneOrGreater(this short a) => a < 1 ? (short)1 : a;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static int OneOrGreater(this int a) => a < 1 ? 1 : a;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static long OneOrGreater(this long a) => a < 1 ? 1 : a;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static sbyte OneOrGreater(this sbyte a) => a < 1 ? (sbyte)1 : a;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static ushort OneOrGreater(this ushort a) => a < 1 ? (ushort)1 : a;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static uint OneOrGreater(this uint a) => a < 1 ? 1 : a;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static ulong OneOrGreater(this ulong a) => a < 1 ? 1 : a;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static float OneOrGreater(this float d) => d < 1 ? 1 : d;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static double OneOrGreater(this double d) => d < 1 ? 1 : d;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="d">The value to determine whether it is ≥1.</param>
        public static decimal OneOrGreater(this decimal d) => d < 1 ? 1 : d;
        #endregion

        #region ZeroOrGreater
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static byte ZeroOrGreater(this byte a) => a < 0 ? (byte)0 : a;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static short ZeroOrGreater(this short a) => a < 0 ? (short)0 : a;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static int ZeroOrGreater(this int a) => a < 0 ? 0 : a;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static long ZeroOrGreater(this long a) => a < 0 ? 0 : a;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static sbyte ZeroOrGreater(this sbyte a) => a < 0 ? (sbyte)0 : a;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static ushort ZeroOrGreater(this ushort a) => a < 0 ? (ushort)0 : a;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static uint ZeroOrGreater(this uint a) => a < 0 ? 0 : a;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static ulong ZeroOrGreater(this ulong a) => a < 0 ? 0 : a;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static float ZeroOrGreater(this float d) => d < 0 ? 0 : d;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static double ZeroOrGreater(this double d) => d < 0 ? 0 : d;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="d">The value to determine whether it is ≥0.</param>
        public static decimal ZeroOrGreater(this decimal d) => d < 0 ? 0 : d;
        #endregion
    }
}
