using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.General
{
    /// <summary>Represents a symmetrical range.</summary>
    /// <typeparam name="T">The type of the value range.</typeparam>
    public struct SymmetricalRange<T>
    {
        /// <summary>The middle value of the range.</summary>
        public T MiddleValue;
        /// <summary>The maximum distance from the middle value.</summary>
        public T MaximumDistance;

        /// <summary>Initializes a new instance of the <seealso cref="SymmetricalRange{T}"/> struct.</summary>
        /// <param name="middleValue">The middle value of the range.</param>
        /// <param name="maximumDistance">The maximum distance from the middle value.</param>
        public SymmetricalRange(T middleValue, T maximumDistance)
        {
            MiddleValue = middleValue;
            MaximumDistance = maximumDistance;
        }
    }

    // This is so sad, can we get some language improvements please?
    public static class SymmetricalRangeMethods
    {
        public static bool AreEqual(SymmetricalRange<byte> left, SymmetricalRange<byte> right) => left.MiddleValue == right.MiddleValue && left.MaximumDistance == right.MaximumDistance;
        public static bool AreEqual(SymmetricalRange<short> left, SymmetricalRange<short> right) => left.MiddleValue == right.MiddleValue && left.MaximumDistance == right.MaximumDistance;
        public static bool AreEqual(SymmetricalRange<int> left, SymmetricalRange<int> right) => left.MiddleValue == right.MiddleValue && left.MaximumDistance == right.MaximumDistance;
        public static bool AreEqual(SymmetricalRange<long> left, SymmetricalRange<long> right) => left.MiddleValue == right.MiddleValue && left.MaximumDistance == right.MaximumDistance;
        public static bool AreEqual(SymmetricalRange<float> left, SymmetricalRange<float> right) => left.MiddleValue == right.MiddleValue && left.MaximumDistance == right.MaximumDistance;
        public static bool AreEqual(SymmetricalRange<double> left, SymmetricalRange<double> right) => left.MiddleValue == right.MiddleValue && left.MaximumDistance == right.MaximumDistance;
        public static bool AreEqual(SymmetricalRange<Color> left, SymmetricalRange<Color> right) => left.MiddleValue == right.MiddleValue && left.MaximumDistance == right.MaximumDistance;
    }
}
