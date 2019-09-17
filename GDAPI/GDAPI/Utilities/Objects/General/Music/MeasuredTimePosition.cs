using GDAPI.Utilities.Enumerations;
using System;

namespace GDAPI.Utilities.Objects.General.Music
{
    /// <summary>Represents a time position that contains a measure, a beat and a fraction of the beat. The struct's size is 8 bytes.</summary>
    public struct MeasuredTimePosition
    {
        /// <summary>Denotes the starting position of a musical composition.</summary>
        public static readonly MeasuredTimePosition Start = new MeasuredTimePosition(1, 1, 0);

        private short m;
        private ushort b;
        private float f;

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
            m = measure;
            b = beat;
            f = fraction;
        }

        /// <summary>Advances the beat fraction by a value based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="fraction">The value to advance the beat fraction by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat fraction.</param>
        public void AdvanceFraction(float fraction, TimeSignature timeSignature)
        {
            AdvanceFraction(fraction);
            FixBeats(timeSignature);
        }

        /// <summary>Advances the beat by one based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceBeat(TimeSignature timeSignature) => AdvanceBeat(1, timeSignature);
        /// <summary>Advances the beat by a value based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="beats">The number of beats to advance by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceBeat(int beats, TimeSignature timeSignature)
        {
            AdvanceBeat(beats);
            FixBeats(timeSignature);
        }

        /// <summary>Advances the measure by one.</summary>
        public void AdvanceMeasure() => m++;
        /// <summary>Advances the measure by a value.</summary>
        /// <param name="measures">The number of measures to advance by.</param>
        public void AdvanceMeasure(int measures) => m += (short)measures;

        /// <summary>Advances this time position by a <seealso cref="MusicalNoteValue"/> based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="value">The value to advance this time position by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceValue(MusicalNoteValue value, TimeSignature timeSignature) => AdvanceFraction((float)value / timeSignature.Denominator, timeSignature);
        /// <summary>Advances this time position by a <seealso cref="MusicalNoteValue"/> based on the provided <seealso cref="TimeSignature"/>.</summary>
        /// <param name="value">The value to advance this time position by.</param>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to advance the beat.</param>
        public void AdvanceValue(RhythmicalValue value, TimeSignature timeSignature) => AdvanceFraction((float)value.TotalValue * timeSignature.Denominator, timeSignature);

        private void AdvanceFraction(float fraction)
        {
            f += fraction;
            FixFraction();
        }
        private void AdvanceBeat() => b++;
        private void AdvanceBeat(int beats) => b += (ushort)beats;

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
    }
}
