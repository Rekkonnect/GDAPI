using GDAPI.Functions.Extensions;
using System;
using System.Runtime.InteropServices;
using static System.Convert;

namespace GDAPI.Objects.Music
{
    /// <summary>Represents a time signature. The struct's size is 4 bytes.</summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct TimeSignature : IEquatable<TimeSignature>
    {
        [FieldOffset(0)]
        private ushort b;
        [FieldOffset(2)]
        private ushort d;

        [FieldOffset(0)]
        private readonly uint all;

        /// <summary>The beats of the time signature.</summary>
        public int Beats
        {
            get => b;
            set
            {
                if (value < 1)
                    throw new InvalidOperationException("Cannot set the beats of the time signature to a non-positive integer.");
                b = (ushort)value;
            }
        }
        /// <summary>The denominator of the time signature.</summary>
        public int Denominator
        {
            get => d;
            set
            {
                if (value < 1)
                    throw new InvalidOperationException("Cannot set the denominator of the time signature to a non-positive integer.");
                if (!value.IsPowerOfTwo())
                    throw new InvalidOperationException("Cannot set the denominator of the time signature to anything but a power of two.");
                d = (ushort)value;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="TimeSignature"/> struct.</summary>
        /// <param name="beats">The beats of the time signature.</param>
        /// <param name="denominator">The denominator of the time signature.</param>
        public TimeSignature(int beats, int denominator)
            : this()
        {
            Beats = beats;
            Denominator = denominator;
        }
        /// <summary>Initializes a new instance of the <seealso cref="TimeSignature"/> struct.</summary>
        /// <param name="timeSignature">The tuple containing the time signature. The first element represents the beats and the second represents the denominator.</param>
        public TimeSignature((int b, int d) timeSignature) : this(timeSignature.b, timeSignature.d) { }

        /// <summary>Determines whether this <seealso cref="TimeSignature"/> is equal to another.</summary>
        /// <param name="other">The other <seealso cref="TimeSignature"/> to determine equality with.</param>
        public bool Equals(TimeSignature other) => all == other.all;

        public static bool operator ==(TimeSignature left, TimeSignature right) => left.all == right.all;
        public static bool operator !=(TimeSignature left, TimeSignature right) => left.all != right.all;

        public static implicit operator TimeSignature((int, int) tuple) => new TimeSignature(tuple);
        public static implicit operator (int, int)(TimeSignature timeSignature) => (timeSignature.Beats, timeSignature.Denominator);

        /// <summary>Parses a string of the form {Beats}/{Denominator} into a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="s">The string of the form {Beats}/{Denominator} to parse.</param>
        public static TimeSignature Parse(string s)
        {
            var split = s.Split('/');
            return new TimeSignature(ToInt32(split[0]), ToInt32(split[1]));
        }
        /// <summary>Attempts to parse a string into a <seealso cref="TimeSignature"/>. Returns a <seealso cref="bool"/> determining whether the operation succeeded.</summary>
        /// <param name="s">The string to attempt parsing.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> that will be parsed. If the string is unparsable, the value is <see langword="default"/>.</param>
        public static bool TryParse(string s, out TimeSignature timeSignature)
        {
            timeSignature = default;
            var split = s.Split('/');
            if (!int.TryParse(split[0], out int beats))
                return false;
            if (!int.TryParse(split[1], out int denominator))
                return false;
            timeSignature = new TimeSignature(beats, denominator);
            return true;
        }

        /// <summary>Determines whether this <seealso cref="TimeSignature"/> equals another object.</summary>
        /// <param name="obj">The other object to determine equality with.</param>
        public override bool Equals(object obj) => all == ((TimeSignature)obj).all;
        /// <summary>Gets the hash code of this <seealso cref="TimeSignature"/>.</summary>
        public override int GetHashCode() => all.GetHashCode();
        /// <summary>Returns the string representation of this <seealso cref="TimeSignature"/> in the form {Beats}/{Denominator}.</summary>
        public override string ToString() => $"{Beats}/{Denominator}";
    }
}
