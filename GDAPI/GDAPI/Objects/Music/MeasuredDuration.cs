using System;
using System.Runtime.InteropServices;
using GDAPI.Enumerations;
using static System.Convert;

namespace GDAPI.Objects.Music
{
    /// <summary>Represents a measured duration represented a measure, a beat and a fraction of the beat. The struct's size is 8 bytes.</summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct MeasuredDuration : IComparable<MeasuredDuration>
    {
        /// <summary>Represents the absolute zero duration.</summary>
        public static readonly MeasuredDuration Zero = new MeasuredDuration(0);

        [FieldOffset(0)]
        private short m;
        [FieldOffset(2)]
        private short b;
        [FieldOffset(4)]
        private float f;

        [FieldOffset(0)]
        private ulong all;

        /// <summary>The measures of the duration.</summary>
        public int Measures
        {
            get => m;
            set => m = (short)value;
        }
        /// <summary>The beats of the duration.</summary>
        public int Beats
        {
            get => b;
            set => b = (short)value;
        }
        /// <summary>The beat fraction of the duration.</summary>
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

        /// <summary>Gets or sets the beats along with the beat fraction, without taking the measures into account.</summary>
        public float BeatsWithFraction
        {
            get => Beats + Fraction;
            set
            {
                f = value;
                FixFraction();
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="MeasuredDuration"/> struct.</summary>
        /// <param name="measures">The measures of the duration.</param>
        /// <param name="beats">The beats of the duration.</param>
        /// <param name="fraction">The beat fraction of the duration. It has to be within the range [0, 1).</param>
        public MeasuredDuration(int measures, int beats = 0, float fraction = 0)
            : this()
        {
            Measures = measures;
            Beats = beats;
            Fraction = fraction;
        }
        /// <summary>Initializes a new instance of the <seealso cref="MeasuredDuration"/> struct.</summary>
        /// <param name="measures">The measures of the duration.</param>
        /// <param name="beats">The beats of the duration.</param>
        /// <param name="fraction">The beat fraction of the duration. It has to be within the range [0, 1).</param>
        public MeasuredDuration(short measures, short beats, float fraction)
        {
            all = 0;
            m = measures;
            b = beats;
            f = fraction;
        }
        /// <summary>Initializes a new instance of the <seealso cref="MeasuredDuration"/> struct.</summary>
        /// <param name="duration">The duration as a <seealso cref="TimeSpan"/>.</param>
        /// <param name="bpm">The <seealso cref="BPM"/> of the duration.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to calculate the beats and measures.</param>
        public MeasuredDuration(TimeSpan duration, BPM bpm, TimeSignature timeSignature)
            : this()
        {
            IncreaseFraction((float)(duration.TotalSeconds / bpm.BeatInterval), timeSignature);
        }
        /// <summary>Initializes a new instance of the <seealso cref="MeasuredDuration"/> struct.</summary>
        /// <param name="absoluteBeats">The absolute beats.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to calculate the beats and measures.</param>
        public MeasuredDuration(float absoluteBeats, TimeSignature timeSignature)
            : this()
        {
            IncreaseFraction(absoluteBeats, timeSignature);
        }
        /// <summary>Initializes a new instance of the <seealso cref="MeasuredDuration"/> struct.</summary>
        /// <param name="bytes">The bytes from which to initialize the struct.</param>
        private MeasuredDuration(ulong bytes)
            : this()
        {
            all = bytes;
        }
        /// <summary>Initializes a new instance of the <seealso cref="MeasuredDuration"/> struct.</summary>
        /// <param name="measuredTimePosition">The <seealso cref="MeasuredTimePosition"/> from which to construct this instance.</param>
        public MeasuredDuration(MeasuredTimePosition measuredTimePosition)
            : this()
        {
            Measures = measuredTimePosition.Measure - 1;
            Beats = measuredTimePosition.Beat - 1;
            Fraction = measuredTimePosition.Fraction;
        }

        /// <summary>Increases the beat fraction by a value based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="fraction">The value to increase the beat fraction by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to increase the beat fraction.</param>
        public void IncreaseFraction(float fraction, TimeSignature timeSignature)
        {
            f += fraction;
            FixFraction(timeSignature);
        }

        /// <summary>Increases the beats by one based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to increase the beats.</param>
        public void IncreaseBeat(TimeSignature timeSignature) => IncreaseBeat(1, timeSignature);
        /// <summary>Increases the beats by a value based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="beats">The number of beats to increase the beats by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to increase the beats.</param>
        public void IncreaseBeat(int beats, TimeSignature timeSignature)
        {
            b += (short)beats;
            FixBeats(timeSignature);
        }

        /// <summary>Increases the measures by one.</summary>
        public void IncreaseMeasure() => m++;
        /// <summary>Increases the measures by a value.</summary>
        /// <param name="measures">The number of measures to increase the measures by.</param>
        public void IncreaseMeasure(int measures) => m += (short)measures;

        /// <summary>Increases this duration by a <seealso cref="MusicalNoteValue"/> based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="value">The value to increase this duration by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to increase the duration.</param>
        public void IncreaseValue(MusicalNoteValue value, TimeSignature timeSignature) => IncreaseFraction((float)value / timeSignature.Denominator, timeSignature);
        /// <summary>Increases this duration by a <seealso cref="MusicalNoteValue"/> based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="value">The value to increase this duration by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to increase the duration.</param>
        public void IncreaseValue(RhythmicalValue value, TimeSignature timeSignature) => IncreaseFraction((float)value.TotalValue * timeSignature.Denominator, timeSignature);
        /// <summary>Advances this duration by a <seealso cref="MeasuredDuration"/> based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="value">The value to advance this duration by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the duration.</param>
        public void IncreaseValue(MeasuredDuration value, TimeSignature timeSignature) => IncreaseFraction(value.TotalBeats(timeSignature), timeSignature);

        /// <summary>Adds a <seealso cref="MeasuredDuration"/> to this duration based on the provided <seealso cref="TimeSignature"/> and returns a new instance containing the result.</summary>
        /// <param name="value">The value to add to this duration.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to add to the duration.</param>
        public MeasuredDuration Add(MeasuredDuration value, TimeSignature timeSignature)
        {
            var result = this;
            result.IncreaseValue(value, timeSignature);
            return result;
        }

        /// <summary>Clones this instance of <seealso cref="MeasuredDuration"/> and returns the new one.</summary>
        public MeasuredDuration Clone() => new MeasuredDuration(all);

        /// <summary>Resets the beat fraction to 0.</summary>
        public void ResetBeatFraction() => f = 0;
        /// <summary>Resets the beats to 0 and the beat fraction to 0.</summary>
        public void ResetBeat()
        {
            ResetBeatFraction();
            b = 0;
        }
        /// <summary>Resets the measures to 0, the beats to 0 and the beat fraction to 0.</summary>
        public void ResetMeasure()
        {
            ResetBeat();
            m = 0;
        }

        /// <summary>Gets the duration as total beats based on a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="timeSignature">The time signature based on which the number of beats is calculated.</param>
        public float TotalBeats(TimeSignature timeSignature) => timeSignature.Beats * m + BeatsWithFraction;
        /// <summary>Gets the duration of this <seealso cref="MeasuredDuration"/> based on a <seealso cref="BPM"/> in seconds based on a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="bpm">The <seealso cref="BPM"/> that determines the duration.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> that determines the measure's beat count.</param>
        public double GetDuration(BPM bpm, TimeSignature timeSignature) => bpm.GetDuration(this, timeSignature);
        /// <summary>Gets the duration of this <seealso cref="MeasuredDuration"/> based on a <seealso cref="BPM"/> as a <seealso cref="TimeSpan"/> based on a <seealso cref="TimeSignature"/>.</summary>
        /// <param name="bpm">The <seealso cref="BPM"/> that determines the duration.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> that determines the measure's beat count.</param>
        public TimeSpan GetDurationTimeSpan(BPM bpm, TimeSignature timeSignature) => bpm.GetDurationTimeSpan(this, timeSignature);

        /// <summary>Compares this <seealso cref="MeasuredDuration"/> with another based on the absolute value.</summary>
        /// <param name="other">The other <seealso cref="MeasuredDuration"/> to compare this to.</param>
        public int CompareTo(MeasuredDuration other) => CompareByAbsoluteValue(this, other);

        private void FixFraction()
        {
            b += (short)f;
            f %= 1;

            if (f < 0)
            {
                b--;
                f++;
            }
        }
        private void FixFraction(TimeSignature timeSignature)
        {
            FixFraction();
            FixBeats(timeSignature);
        }
        private void FixBeats(TimeSignature timeSignature)
        {
            m += (short)(b / timeSignature.Beats);
            b = (short)(b % timeSignature.Beats);

            if (b < 0)
            {
                m--;
                b += (short)timeSignature.Beats;
            }
        }

        public static bool operator ==(MeasuredDuration left, MeasuredDuration right) => left.all == right.all;
        public static bool operator !=(MeasuredDuration left, MeasuredDuration right) => left.all != right.all;

        /// <summary>Parses a string of the form {Measures}:{Beats}.{Fraction} into a <seealso cref="MeasuredDuration"/>.</summary>
        /// <param name="s">The string to parse into a <seealso cref="MeasuredDuration"/>.</param>
        public unsafe static MeasuredDuration Parse(string s)
        {
            var split = s.Split(':');
            int measures = ToInt32(split[0]);
            float fraction = ToSingle(split[1]);
            int beats = (int)fraction;
            fraction -= beats;
            return new MeasuredDuration(measures, beats, fraction);
        }
        /// <summary>Attempts to parse a string into a <seealso cref="MeasuredDuration"/>. Returns a <seealso cref="bool"/> determining whether the operation succeeded.</summary>
        /// <param name="s">The string to parse into a <seealso cref="MeasuredDuration"/>.</param>
        /// <param name="duration">The <seealso cref="MeasuredDuration"/> that will be parsed. If the string is unparsable, the value is <see langword="default"/>.</param>
        public static bool TryParse(string s, out MeasuredDuration duration)
        {
            duration = default;
            var split = s.Split(':');
            if (!int.TryParse(split[0], out int measures))
                return false;
            if (!float.TryParse(split[1], out float fraction))
                return false;
            int beats = (int)fraction;
            fraction -= beats;
            duration = new MeasuredDuration(measures, beats, fraction);
            return true;
        }

        /// <summary>Defines a <seealso cref="Comparison{T}"/> to compare between <seealso cref="MeasuredDuration"/> based on their measures. Returns the <seealso cref="int"/> value of <seealso cref="IComparable{T}.CompareTo(T)"/>.</summary>
        /// <param name="left">The left <seealso cref="MeasuredDuration"/> to compare.</param>
        /// <param name="right">The right <seealso cref="MeasuredDuration"/> to compare.</param>
        public static int CompareByMeasure(MeasuredDuration left, MeasuredDuration right) => left.m.CompareTo(right.m);
        /// <summary>Defines a <seealso cref="Comparison{T}"/> to compare between <seealso cref="MeasuredDuration"/> based on their absolute value. That means the one with the larger measures, then beats, then fraction is considered larger. Returns the <seealso cref="int"/> value of <seealso cref="IComparable{T}.CompareTo(T)"/>.</summary>
        /// <param name="left">The left <seealso cref="MeasuredDuration"/> to compare.</param>
        /// <param name="right">The right <seealso cref="MeasuredDuration"/> to compare.</param>
        public static unsafe int CompareByAbsoluteValue(MeasuredDuration left, MeasuredDuration right) => left.all.CompareTo(right.all);

        /// <summary>Returns the string representation of this <seealso cref="MeasuredDuration"/> of the form {Measures}:{Beats}.{Fraction}.</summary>
        public override string ToString() => $"{m}:{b}.{DecimalPartOf(f.ToString("F3"))}";

        private static string DecimalPartOf(string s) => s.Substring(s.IndexOf('.') + 1);
    }
}