using System;
using System.Globalization;
using static GDAPI.Functions.General.Parsing;

namespace GDAPI.Objects.Music
{
    /// <summary>Represents a BPM value. This struct encapsulates a <seealso cref="double"/> that holds the BPM value and provides several tools to convert/utilize that value.</summary>
    public struct BPM : IComparable<BPM>, IEquatable<BPM>
    {
        /// <summary>The BPM value.</summary>
        public double Value;

        /// <summary>Gets the current value's beat interval in seconds.</summary>
        public double BeatInterval => 60 / Value;
        /// <summary>Gets the current value's beat interval as a <seealso cref="TimeSpan"/>.</summary>
        public TimeSpan BeatIntervalTimeSpan => TimeSpan.FromSeconds(BeatInterval);

        /// <summary>Initializes a new value of the <seealso cref="BPM"/> struct.</summary>
        /// <param name="value">The BPM value.</param>
        public BPM(double value) => Value = value;

        /// <summary>Gets the current value's measure interval in seconds based on a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> that determines the measure's beat count.</param>
        public double MeasureInterval(TimeSignature timeSignature) => BeatInterval * timeSignature.Beats;
        /// <summary>Gets the current value's measure interval as a <seealso cref="TimeSpan"/> based on a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> that determines the measure's beat count.</param>
        public TimeSpan MeasureIntervalTimeSpan(TimeSignature timeSignature) => TimeSpan.FromSeconds(BeatIntervalTimeSpan.TotalSeconds * timeSignature.Beats);

        /// <summary>Gets the duration of a <seealso cref="MeasuredDuration"/> based on the current BPM value in seconds based on a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="duration">The <seealso cref="MeasuredDuration"/> that will be calculated w.r.t. the current BPM value.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> that determines the measure's beat count.</param>
        public double GetDuration(MeasuredDuration duration, TimeSignature timeSignature) => BeatInterval * duration.TotalBeats(timeSignature);
        /// <summary>Gets the duration of a <seealso cref="MeasuredDuration"/> based on the current BPM value as a <seealso cref="TimeSpan"/> based on a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="duration">The <seealso cref="MeasuredDuration"/> that will be calculated w.r.t. the current BPM value.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> that determines the measure's beat count.</param>
        public TimeSpan GetDurationTimeSpan(MeasuredDuration duration, TimeSignature timeSignature) => TimeSpan.FromSeconds(GetDuration(duration, timeSignature));
        /// <summary>Gets the <seealso cref="MeasuredDuration"/> of a <seealso cref="TimeSpan"/> based on the current BPM value and a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="duration">The <seealso cref="TimeSpan"/> that represents the duration that will be converted.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> that determines the measure's beat count.</param>
        public MeasuredDuration GetMeasuredDuration(TimeSpan duration, TimeSignature timeSignature) => new((float)(duration.TotalMinutes * Value), timeSignature);

        /// <summary>Compares this <seealso cref="BPM"/> to another, based on their raw value.</summary>
        /// <param name="other">The other <seealso cref="BPM"/> to compare this to.</param>
        public int CompareTo(BPM other) => Value.CompareTo(other.Value);
        /// <summary>Determines whether this <seealso cref="BPM"/> is equal to another, based on their raw value.</summary>
        /// <param name="other">The other <seealso cref="BPM"/> to determine equality with.</param>
        public bool Equals(BPM other) => Value == other.Value;

        public static bool operator ==(BPM left, BPM right) => left.Value == right.Value;
        public static bool operator !=(BPM left, BPM right) => left.Value != right.Value;
        public static bool operator <(BPM left, BPM right) => left.Value < right.Value;
        public static bool operator >(BPM left, BPM right) => left.Value > right.Value;
        public static bool operator <=(BPM left, BPM right) => left.Value <= right.Value;
        public static bool operator >=(BPM left, BPM right) => left.Value >= right.Value;

        public static implicit operator BPM(double value) => new(value);
        public static explicit operator double(BPM value) => value.Value;

        /// <summary>Parses a string into a <seealso cref="BPM"/>.</summary>
        /// <param name="s">The string to parse into a <seealso cref="BPM"/>.</param>
        public static BPM Parse(string s) => new(ParseDouble(s));
        /// <summary>Attempts to parse a string into a <seealso cref="BPM"/>. Returns a <seealso cref="bool"/> determining whether the operation succeeded.</summary>
        /// <param name="s">The string to parse into a <seealso cref="BPM"/>.</param>
        /// <param name="bpm">The <seealso cref="BPM"/> that will be parsed. If the string is unparsable, the value is <see langword="default"/>.</param>
        public static bool TryParse(string s, out BPM bpm)
        {
            bpm = default;
            if (!double.TryParse(s, out double value))
                return false;
            bpm = new BPM(value);
            return true;
        }

        /// <summary>Returns the string representation of this <seealso cref="BPM"/>.</summary>
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
        /// <summary>Determines whether this <seealso cref="BPM"/> equals another object.</summary>
        /// <param name="obj">The other object to determine equality with.</param>
        public override bool Equals(object obj) => Value == ((BPM)obj).Value || Value == (double)obj; // This is probably never going to check whether the object is a double
        /// <summary>Gets the hash code of this <seealso cref="BPM"/> based on the value.</summary>
        public override int GetHashCode() => Value.GetHashCode();
    }
}
