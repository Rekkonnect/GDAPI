using GDAPI.Utilities.Enumerations;
using System;
using System.Runtime.InteropServices;
using static System.Convert;

namespace GDAPI.Utilities.Objects.General.Music
{
    /// <summary>Represents a time position that contains a measure, a beat and a fraction of the beat. The struct's size is 8 bytes.</summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct MeasuredTimePosition : IComparable<MeasuredTimePosition>
    {
        /// <summary>Denotes the starting position of a musical composition.</summary>
        public static readonly MeasuredTimePosition Start = new MeasuredTimePosition(1, 1, 0);
        /// <summary>A standarized constant that denotes the ending position of a musical composition, without knowing its length.</summary>
        public static readonly MeasuredTimePosition UnknownEnd = new MeasuredTimePosition(short.MinValue, (ushort)0, 0);

        [FieldOffset(0)]
        private short m;
        [FieldOffset(2)]
        private ushort b;
        [FieldOffset(4)]
        private float f;

        [FieldOffset(0)]
        private ulong all;

        /// <summary>The 1-indexed measure of the time position.</summary>
        public int Measure
        {
            get => m;
            set => m = (short)value;
        }
        /// <summary>The 1-indexed beat of the time position.</summary>
        public int Beat
        {
            get => b;
            set => b = (ushort)value;
        }
        /// <summary>The beat fraction of the time position. It has to be within the range [0, 1).</summary>
        public float Fraction
        {
            get => f;
            set
            {
                if (value < 0 || value >= 1)
                    throw new InvalidOperationException("The fraction of the beat cannot be outside of the range [0, 1).");
                f = value;
            }
        }

        /// <summary>Gets or sets the beat along with the beat fraction, without taking the measures into account.</summary>
        public float BeatWithFraction
        {
            get => Beat + Fraction;
            set
            {
                f = value;
                FixFraction();
            }
        }

        /// <summary>Determines whether the time position is on the start of the measure.</summary>
        public bool IsStartOfMeasure
        {
            get => b == 1 && f == 0;
            set => ResetToMeasureStart();
        }
        /// <summary>Determines whether this is equal to the standarized constant <seealso cref="UnknownEnd"/>.</summary>
        public bool IsUnknownEnd => this == UnknownEnd;

        /// <summary>Initializes a new instance of the <seealso cref="MeasuredTimePosition"/> struct.</summary>
        /// <param name="measure">The measure of the time position.</param>
        /// <param name="beat">The beat of the time position.</param>
        public MeasuredTimePosition(int measure, int beat)
            : this(measure, beat, 0) { }
        /// <summary>Initializes a new instance of the <seealso cref="MeasuredTimePosition"/> struct.</summary>
        /// <param name="measure">The measure of the time position.</param>
        /// <param name="beat">The beat of the time position.</param>
        /// <param name="fraction">The beat fraction of the time position. It has to be within the range [0, 1).</param>
        public MeasuredTimePosition(int measure, int beat, float fraction)
            : this()
        {
            Measure = measure;
            Beat = beat;
            Fraction = fraction;
        }
        /// <summary>Initializes a new instance of the <seealso cref="MeasuredTimePosition"/> struct.</summary>
        /// <param name="measure">The measure of the time position.</param>
        /// <param name="beat">The beat of the time position.</param>
        /// <param name="fraction">The beat fraction of the time position. It has to be within the range [0, 1).</param>
        public MeasuredTimePosition(short measure, ushort beat, float fraction)
        {
            all = 0;
            m = measure;
            b = beat;
            f = fraction;
        }

        /// <summary>Advances the beat fraction by a value based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="fraction">The value to advance the beat fraction by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat fraction.</param>
        public void AdvanceFraction(float fraction, TimeSignature timeSignature)
        {
            f += fraction;
            FixFraction(timeSignature);
        }

        /// <summary>Advances the beat by one based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceBeat(TimeSignature timeSignature) => AdvanceBeat(1, timeSignature);
        /// <summary>Advances the beat by a value based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="beats">The number of beats to advance by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceBeat(int beats, TimeSignature timeSignature)
        {
            b += (ushort)beats;
            FixBeats(timeSignature);
        }

        /// <summary>Advances the measure by one.</summary>
        public void AdvanceMeasure() => m++;
        /// <summary>Advances the measure by a value.</summary>
        /// <param name="measures">The number of measures to advance by.</param>
        public void AdvanceMeasure(int measures) => m += (short)measures;
        /// <summary>Advances the measure by one and resets the position to the start of the measure.</summary>
        public void AdvanceToStartOfNextMeasure()
        {
            AdvanceMeasure();
            ResetToMeasureStart();
        }
        /// <summary>Advances to the start of the next measure if the current position is beyond the start of the current measure.</summary>
        public void ResetMeasureIfBeyondStart()
        {
            if (!IsStartOfMeasure)
                AdvanceToStartOfNextMeasure();
        }

        /// <summary>Advances this time position by a <seealso cref="MusicalNoteValue"/> based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="value">The value to advance this time position by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceValue(MusicalNoteValue value, TimeSignature timeSignature) => AdvanceFraction((float)value / timeSignature.Denominator, timeSignature);
        /// <summary>Advances this time position by a <seealso cref="RhythmicalValue"/> based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="value">The value to advance this time position by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceValue(RhythmicalValue value, TimeSignature timeSignature) => AdvanceFraction((float)value.TotalValue * timeSignature.Denominator, timeSignature);
        /// <summary>Advances this time position by a <seealso cref="MeasuredDuration"/> based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="value">The value to advance this time position by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceValue(MeasuredDuration value, TimeSignature timeSignature) => AdvanceFraction(value.BeatsWithFraction, timeSignature);

        /// <summary>Resets the beat fraction to 0, which is the start of the beat.</summary>
        public void ResetToBeatStart() => f = 0;
        /// <summary>Resets the beat to 1 and the beat fraction to 0, which is the start of the measure.</summary>
        public void ResetToMeasureStart()
        {
            ResetToBeatStart();
            b = 1;
        }
        /// <summary>Resets the measure to 1, the beat to 1 and the beat fraction to 0, which is the start of the composition.</summary>
        public void ResetToCompositionStart()
        {
            ResetToMeasureStart();
            m = 1;
        }

        /// <summary>Gets this <seealso cref="MeasuredTimePosition"/>'s absolute beat based on a <seealso cref="TimeSignature"/>, which is calculated based on the measure.</summary>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to calculate the absolute beat.</param>
        public float GetAbsoluteBeat(TimeSignature timeSignature) => timeSignature.Beats * (m - 1) + b + f;

        /// <summary>Gets the distance from another <seealso cref="MeasuredTimePosition"/> based on a <seealso cref="TimeSignature"/> and returns a <seealso cref="MeasuredDuration"/> representing that distance.</summary>
        /// <param name="other">The other <seealso cref="MeasuredTimePosition"/> to get the distance from.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to calculate the distance.</param>
        public MeasuredDuration DistanceFrom(MeasuredTimePosition other, TimeSignature timeSignature)
        {
            float thisBeat = GetAbsoluteBeat(timeSignature);
            float otherBeat = other.GetAbsoluteBeat(timeSignature);

            return new MeasuredDuration(Math.Abs(otherBeat - thisBeat), timeSignature);
        }

        /// <summary>Compares this <seealso cref="MeasuredTimePosition"/> with another based on the absolute position.</summary>
        /// <param name="other">The other <seealso cref="MeasuredTimePosition"/> to compare this to.</param>
        public int CompareTo(MeasuredTimePosition other) => CompareByAbsolutePosition(this, other);

        private void FixFraction()
        {
            b += (ushort)f;
            f %= 1;
        }
        private void FixFraction(TimeSignature timeSignature)
        {
            FixFraction();
            FixBeats(timeSignature);
        }
        private void FixBeats(TimeSignature timeSignature)
        {
            m += (short)((b - 1) / timeSignature.Beats);
            b = (ushort)((b - 1) % timeSignature.Beats + 1);
        }

        public static bool operator ==(MeasuredTimePosition left, MeasuredTimePosition right) => left.all == right.all;
        public static bool operator !=(MeasuredTimePosition left, MeasuredTimePosition right) => left.all == right.all;

        /// <summary>Parses a string of the form {Measure}:{Beat}.{Fraction} into a <seealso cref="MeasuredTimePosition"/>.</summary>
        /// <param name="s">The string to parse into a <seealso cref="MeasuredTimePosition"/>.</param>
        public static MeasuredTimePosition Parse(string s)
        {
            var split = s.Split(':');
            int measure = ToInt32(split[0]);
            float fraction = ToSingle(split[1]);
            int beat = (int)fraction;
            fraction -= beat;
            return new MeasuredTimePosition(measure, beat, fraction);
        }
        /// <summary>Attempts to parse a string into a <seealso cref="MeasuredTimePosition"/>. Returns a <seealso cref="bool"/> determining whether the operation succeeded.</summary>
        /// <param name="s">The string to parse into a <seealso cref="MeasuredTimePosition"/>.</param>
        /// <param name="timePosition">The <seealso cref="MeasuredTimePosition"/> that will be parsed. If the string is unparsable, the value is <see langword="default"/>.</param>
        public static bool TryParse(string s, out MeasuredTimePosition timePosition)
        {
            timePosition = default;
            var split = s.Split(':');
            if (!int.TryParse(split[0], out int measure))
                return false;
            if (!float.TryParse(split[1], out float fraction))
                return false;
            int beat = (int)fraction;
            fraction -= beat;
            timePosition = new MeasuredTimePosition(measure, beat, fraction);
            return true;
        }
        /// <summary>Parses a string of the form {Beat}.{Fraction} into a <seealso cref="MeasuredTimePosition"/>.</summary>
        /// <param name="s">The string to parse into a <seealso cref="MeasuredTimePosition"/>.</param>
        public static MeasuredTimePosition ParseAsBeatWithFraction(string s)
        {
            var result = new MeasuredTimePosition();
            result.BeatWithFraction = float.Parse(s);
            return result;
        }

        /// <summary>Defines a <seealso cref="Comparison{T}"/> to compare between <seealso cref="MeasuredTimePosition"/> based on their measures. Returns the <seealso cref="int"/> value of <seealso cref="IComparable{T}.CompareTo(T)"/>.</summary>
        /// <param name="left">The left <seealso cref="MeasuredTimePosition"/> to compare.</param>
        /// <param name="right">The right <seealso cref="MeasuredTimePosition"/> to compare.</param>
        public static int CompareByMeasure(MeasuredTimePosition left, MeasuredTimePosition right) => left.m.CompareTo(right.m);
        /// <summary>Defines a <seealso cref="Comparison{T}"/> to compare between <seealso cref="MeasuredTimePosition"/> based on their absolute position. That means the one with the larger measure, then beat, then fraction is considered larger. Returns the <seealso cref="int"/> value of <seealso cref="IComparable{T}.CompareTo(T)"/>.</summary>
        /// <param name="left">The left <seealso cref="MeasuredTimePosition"/> to compare.</param>
        /// <param name="right">The right <seealso cref="MeasuredTimePosition"/> to compare.</param>
        public static unsafe int CompareByAbsolutePosition(MeasuredTimePosition left, MeasuredTimePosition right) => left.all.CompareTo(right.all);

        /// <summary>Returns the string representation of this <seealso cref="MeasuredTimePosition"/> of the form {Measure}:{Beat}.{Fraction}.</summary>
        public override string ToString() => $"{m}:{b}.{DecimalPartOf(f.ToString("F3"))}";

        private static string DecimalPartOf(string s) => s.Substring(s.IndexOf('.') + 1);
    }
}
