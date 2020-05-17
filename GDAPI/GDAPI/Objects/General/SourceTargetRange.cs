using GDAPI.Functions.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Convert;

namespace GDAPI.Objects.General
{
    /// <summary>Represents a source-target range of the form A-B > C-D, where A &lt;= B and C &lt;= D.</summary>
    public class SourceTargetRange : IEnumerable<SourceTargetPair>
    {
        /// <summary>The value that determines whether a property is invalid.</summary>
        public const int InvalidValue = -1;

        private int sourceFrom, sourceTo, targetFrom;

        /// <summary>Gets or sets the source's starting value.</summary>
        public int SourceFrom
        {
            get => sourceFrom;
            set => SourceTargetRangeChanged?.Invoke(sourceFrom = value, sourceTo, targetFrom, TargetTo);
        }
        /// <summary>Gets or sets the source's ending value.</summary>
        public int SourceTo
        {
            get => sourceTo;
            set => SourceTargetRangeChanged?.Invoke(sourceFrom, sourceTo = value, targetFrom, TargetTo);
        }
        /// <summary>Gets or sets the target's starting value.</summary>
        public int TargetFrom
        {
            get => targetFrom;
            set => SourceTargetRangeChanged?.Invoke(sourceFrom, sourceTo, targetFrom = value, TargetTo);
        }
        /// <summary>Gets or sets the target's ending value.</summary>
        public int TargetTo
        {
            get => IsAnyValueInvalid() ? InvalidValue : targetFrom + Range;
            set
            {
                if (!IsTargetFromValid)
                    return;

                // Assign to fields to avoid invoking the event multiple times
                if (!IsSourceFromValid && IsSourceToValid)
                    sourceFrom = sourceTo - (value - targetFrom);
                else if (IsSourceFromValid && !IsSourceToValid)
                    sourceTo = sourceFrom + (value - targetFrom);
                else if (!IsAnyValueInvalid())
                    AdjustSourceTo(value - TargetTo, false);

                SourceTargetRangeChanged?.Invoke(sourceFrom, sourceTo, targetFrom, value);
            }
        }

        /// <summary>Determines whether the source from property is valid (> <seealso cref="InvalidValue"/>).</summary>
        public bool IsSourceFromValid => sourceFrom > InvalidValue;
        /// <summary>Determines whether the source to property is valid (> <seealso cref="InvalidValue"/>).</summary>
        public bool IsSourceToValid => sourceTo > InvalidValue;
        /// <summary>Determines whether the target from property is valid (> <seealso cref="InvalidValue"/>).</summary>
        public bool IsTargetFromValid => targetFrom > InvalidValue;

        /// <summary>Gets the range of the source.</summary>
        public int Range => sourceTo - sourceFrom;
        /// <summary>Gets the difference of the starting value of the source and the target (target - source).</summary>
        public int Difference => targetFrom - sourceFrom;

        /// <summary>Raised whenever a property is changed.</summary>
        public event SourceTargetRangeChanged SourceTargetRangeChanged;

        /// <summary>Initializes a new instance of the <seealso cref="SourceTargetRange"/> class. For private usage only.</summary>
        private SourceTargetRange() : this(0, 0, 0) { }
        /// <summary>Initializes a new instance of the <seealso cref="SourceTargetRange"/> class.</summary>
        /// <param name="sourceStart">The starting value of the source.</param>
        /// <param name="sourceEnd">The ending value of the source.</param>
        /// <param name="targetStart">The starting value of the target.</param>
        public SourceTargetRange(int sourceStart, int sourceEnd, int targetStart)
        {
            sourceFrom = sourceStart;
            sourceTo = sourceEnd;
            targetFrom = targetStart;
        }

        /// <summary>Determines whether a value is within the source's range.</summary>
        /// <param name="value">The value to determine whether it's within the source's range.</param>
        public bool IsWithinSourceRange(int value) => sourceFrom <= value && value <= sourceTo;
        /// <summary>Determines whether a value is within the target's range.</summary>
        /// <param name="value">The value to determine whether it's within the target's range.</param>
        public bool IsWithinTargetRange(int value) => targetFrom <= value && value <= TargetTo;

        /// <summary>Gets the resulting target value for a provided source value. That is, if the source value is contained within the source range, this function returns its respective target value, otherwise returns the provided value itself.</summary>
        /// <param name="source">The source value whose target value to get.</param>
        public int GetTargetFor(int source)
        {
            if (!IsWithinSourceRange(source))
                return source;
            return source - sourceFrom + targetFrom;
        }
        /// <summary>Gets the resulting source value for a provided target value. That is, if the target value is contained within the target range, this function returns its respective source value, otherwise returns the provided value itself.</summary>
        /// <param name="target">The target value whose source value to get.</param>
        public int GetSourceFor(int target)
        {
            if (!IsWithinTargetRange(target))
                return target;
            return target - targetFrom + sourceFrom;
        }

        /// <summary>Determines whether an offset is valid, that is, being non-negative and less or equal to the range.</summary>
        /// <param name="offset">The offset from the starting point of each range.</param>
        public bool IsValidOffset(int offset) => offset >= 0 && offset <= Range;
        /// <summary>Gets a <seealso cref="SourceTargetPair"/> that matches the source and the target.</summary>
        /// <param name="offset">The zero-based offset from the starting point of each range.</param>
        public SourceTargetPair GetSourceTargetPairAt(int offset) => new SourceTargetPair(sourceFrom + offset, targetFrom + offset);

        /// <summary>Determines whether any of the source from, source to and target from values is equal to <seealso cref="InvalidValue"/>.</summary>
        public bool IsAnyValueInvalid() => sourceFrom == InvalidValue || sourceTo == InvalidValue || targetFrom == InvalidValue;

        /// <summary>Adjusts the source to property while also being able to maintain the source difference.</summary>
        /// <param name="adjustment">The adjustment to apply to the source to property.</param>
        /// <param name="maintainDifference">Determines whether source difference will be maintained. Defaults to <see langword="true"/>.</param>
        public void AdjustSourceTo(int adjustment, bool maintainDifference = true)
        {
            sourceTo += adjustment;
            if (maintainDifference)
                sourceFrom += adjustment;
        }

        /// <summary>Clones this <seealso cref="SourceTargetRange"/> and returns the cloned object.</summary>
        public SourceTargetRange Clone() => new SourceTargetRange(SourceFrom, SourceTo, TargetFrom);

        /// <summary>Inverts this <seealso cref="SourceTargetRange"/> by inverting the target and the source (the individual ranges remain the same).</summary>
        public SourceTargetRange Invert() => new SourceTargetRange(TargetFrom, TargetTo, SourceFrom);

        /// <summary>Determines whether the object has the specified values.</summary>
        /// <param name="sourceFrom">The desired value of the source from property.</param>
        /// <param name="sourceTo">The desired value of the source to property.</param>
        /// <param name="targetFrom">The desired value of the target from property.</param>
        /// <param name="targetTo">The desired value of the target to property.</param>
        public bool HasValues(int sourceFrom, int sourceTo, int targetFrom, int targetTo)
        {
            return sourceFrom == SourceFrom
                && sourceTo == SourceTo
                && targetFrom == TargetFrom
                && targetTo == TargetTo;
        }

        public IEnumerator<SourceTargetPair> GetEnumerator() => new SourceTargetRangeEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Parses a string of the form "A-B > C-D" into a <seealso cref="SourceTargetRange"/>.</summary>
        /// <param name="str">The string to parse into a <seealso cref="SourceTargetRange"/>. The string must be of the form "A-B > C-D", where A-B can simply be A if A = B and C-D can respectively be C if C = D.</param>
        public static SourceTargetRange Parse(string str)
        {
            string[,] split = str.Split('>').Split('-');
            int length0 = split.GetLength(0);
            int length1 = split.GetLength(1);

            for (int i = 0; i < length0; i++)
                for (int j = 0; j < length1; j++)
                {
                    while (split[i, j].First() == ' ')
                        split[i, j] = split[i, j].Remove(0, 1);
                    while (split[i, j].Last() == ' ')
                        split[i, j] = split[i, j].Remove(split[i, j].Length - 1, 1);
                }
            return new SourceTargetRange(ToInt32(split[0, 0]), ToInt32(split[0, length1 - 1]), ToInt32(split[1, 0]));
        }
        /// <summary>Loads a number of <seealso cref="SourceTargetRange"/>s from a string array.</summary>
        /// <param name="lines">The lines to load the <seealso cref="SourceTargetRange"/>s from.</param>
        /// <param name="ignoreEmptyLines">Determines whether empty lines will be ignored during parsing. There is almost no reason to set that to <see langword="false"/> unless you're a weirdo.</param>
        public static List<SourceTargetRange> LoadRangesFromStringArray(string[] lines, bool ignoreEmptyLines = true)
        {
            var list = new List<SourceTargetRange>();
            foreach (string s in lines)
                if (!ignoreEmptyLines || s != "")
                    list.Add(Parse(s));
            return list;
        }
        /// <summary>Loads a number of <seealso cref="SourceTargetRange"/>s from a string array.</summary>
        /// <param name="lines">The lines to load the <seealso cref="SourceTargetRange"/>s from.</param>
        /// <param name="ignoreEmptyLines">Determines whether empty lines will be ignored during parsing. There is almost no reason to set that to <see langword="false"/> unless you're a weirdo.</param>
        public static string[] ConvertRangesToStringArray(List<SourceTargetRange> ranges)
        {
            string[] result = new string[ranges.Count];
            for (int i = 0; i < ranges.Count; i++)
                result[i] = ranges[i].ToString();
            return result;
        }

        /// <summary>Inverts the ordering of a provided list of ranges and inverts the individual ranges and returns the resulting list.</summary>
        /// <param name="ranges">The list of ranges to invert.</param>
        public static List<SourceTargetRange> Invert(List<SourceTargetRange> ranges)
        {
            var list = new List<SourceTargetRange>();
            for (int i = ranges.Count - 1; i >= 0; i--)
                list.Add(ranges[i].Invert());
            return list;
        }

        /// <summary>Gets the common properties of the provided <seealso cref="SourceTargetRange"/>s.</summary>
        /// <param name="ranges">The list of ranges to get the common range of.</param>
        public static SourceTargetRange GetCommon(List<SourceTargetRange> ranges)
        {
            if (ranges.Count == 0)
                return null;
            var result = ranges[0].Clone();
            for (int i = 1; i < ranges.Count; i++)
            {
                // This kind of copy-pasting looks like it can be better, but that's the best you can get
                GetCommonPropertyComparer(ref result.sourceFrom, ref ranges[i].sourceFrom);
                GetCommonPropertyComparer(ref result.sourceTo, ref ranges[i].sourceTo);
                GetCommonPropertyComparer(ref result.targetFrom, ref ranges[i].targetFrom);
            }
            return result;
        }
        public override string ToString() => $"{SourceToString()} > {TargetToString()}";
        /// <summary>Returns the string representation of this <seealso cref="SourceTargetRange"/> with the option to include a space between the ranges and the right arrow.</summary>
        /// <param name="addSpace">Determines whether the spaces will be added or not.</param>
        public string ToString(bool addSpace) => $"{SourceToString()}{(addSpace ? " " : "")}>{(addSpace ? " " : "")}{TargetToString()}";
        /// <summary>Returns the string representation of the source.</summary>
        public string SourceToString() => ToString(SourceFrom, SourceTo);
        /// <summary>Returns the string representation of the target.</summary>
        public string TargetToString() => ToString(TargetFrom, TargetTo);

        private static void GetCommonPropertyComparer(ref int result, ref int value)
        {
            if (result > InvalidValue && result != value)
                result = InvalidValue;
        }

        private static string ToString(int from, int to) => $"{from}{(to - from > 0 ? $"-{to}" : "")}";

        private class SourceTargetRangeEnumerator : IEnumerator<SourceTargetPair>
        {
            private SourceTargetRange range;
            private int offset = -1;

            public SourceTargetPair Current => range.GetSourceTargetPairAt(offset);
            object IEnumerator.Current => Current;

            public SourceTargetRangeEnumerator(SourceTargetRange sourceTargetRange) => range = sourceTargetRange;

            public void Dispose() { }
            public bool MoveNext()
            {
                offset++;
                return range.IsValidOffset(offset);
            }
            public void Reset()
            {
                offset = -1;
            }
        }
    }

    /// <summary>Represents a function that contains the new state of the <seealso cref="SourceTargetRange"/> instance that was changed.</summary>
    /// <param name="sourceFrom">The new value of the source from property.</param>
    /// <param name="sourceTo">The new value of the source to property.</param>
    /// <param name="targetFrom">The new value of the target from property.</param>
    /// <param name="targetTo">The new value of the target to property.</param>
    public delegate void SourceTargetRangeChanged(int sourceFrom, int sourceTo, int targetFrom, int targetTo);
}
