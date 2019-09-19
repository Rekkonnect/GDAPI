using GDAPI.Utilities.Enumerations;
using System;

namespace GDAPI.Utilities.Objects.General.Music
{
    /// <summary>Represents a measured duration represented a measure, a beat and a fraction of the beat. The struct's size is 8 bytes.</summary>
    public struct MeasuredDuration
    {
        /// <summary>Denotes .</summary>
        public static readonly MeasuredDuration Zero = new MeasuredDuration(0);

        private short m;
        private ushort b;
        private float f;

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
            set => b = (ushort)value;
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
        public MeasuredDuration(short measures, ushort beats, float fraction)
        {
            m = measures;
            b = beats;
            f = fraction;
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
            b += (ushort)beats;
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
