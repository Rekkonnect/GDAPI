using System.Globalization;

namespace GDAPI.Functions.General
{
    /// <summary>Provides parsing functions for GD-related content.</summary>
    public static class Parsing
    {
        /// <summary>Parses an <seealso cref="int"/> out of its string representation as found in GD-related content.</summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>The represented value.</returns>
        public static int ParseInt32(string value) => int.Parse(value, CultureInfo.InvariantCulture);
        /// <summary>Parses a <seealso cref="double"/> out of its string representation as found in GD-related content.</summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>The represented value.</returns>
        public static double ParseDouble(string value) => double.Parse(value, CultureInfo.InvariantCulture);
        /// <summary>Parses a <seealso cref="bool"/> out of its string representation as found in GD-related content.</summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>The represented value.</returns>
        public static bool ParseBoolean(string value) => value is not "0";
    }
}
