using System;
using GDAPI.Objects.Music;
using static System.Convert;

namespace GDAPI.Objects.TimingPoints
{
    /// <summary>Represents an absolute timing point. The absolute timing point resets the measure of the composition and starts from scratch.</summary>
    public class AbsoluteTimingPoint : TimingPoint<TimeSpan>
    {
        private MeasuredTimePosition relativeTimePosition;

        /// <summary>Represents the relative time position, as calculated from a previous <seealso cref="TimingPoint"/>.</summary>
        public MeasuredTimePosition RelativeTimePosition
        {
            get => relativeTimePosition;
            private set => relativeTimePosition = value;
        }
        /// <summary>Gets or sets the time position in seconds.</summary>
        public double TimePositionSeconds
        {
            get => TimePosition.TotalSeconds;
            set => TimePosition = TimeSpan.FromSeconds(value);
        }

        /// <summary>Initializes a new instance of the <seealso cref="AbsoluteTimingPoint"/> class.</summary>
        /// <param name="timePosition">The time position of the event.</param>
        /// <param name="bpm">The BPM of the timing point.</param>
        /// <param name="timeSignature">The time signature of the timing point.</param>
        public AbsoluteTimingPoint(TimeSpan timePosition, BPM bpm, TimeSignature timeSignature)
            : base(timePosition, bpm, timeSignature) { }
        /// <summary>Initializes a new instance of the <seealso cref="AbsoluteTimingPoint"/> class.</summary>
        /// <param name="timePositionSeconds">The time position of the event in seconds.</param>
        /// <param name="bpm">The BPM of the timing point.</param>
        /// <param name="timeSignature">The time signature of the timing point.</param>
        public AbsoluteTimingPoint(double timePositionSeconds, BPM bpm, TimeSignature timeSignature)
            : base(TimeSpan.FromSeconds(timePositionSeconds), bpm, timeSignature) { }

        /// <summary>Calculates the relative time position of this absolute timing poing based on a previous <seealso cref="TimingPoint"/>.</summary>
        /// <param name="previous">The previous <seealso cref="TimingPoint"/> based on which to calculate the relative time position.</param>
        public void CalculateRelativeTimePosition(TimingPoint previous)
        {
            relativeTimePosition = previous.GetRelativeTimePosition();
            relativeTimePosition.AdvanceValue(previous.BPM.GetMeasuredDuration(TimePosition - previous.GetAbsoluteTimePosition(), previous.TimeSignature), previous.TimeSignature);
            relativeTimePosition.ResetMeasureIfBeyondStart();
        }

        /// <summary>Sets this <seealso cref="AbsoluteTimingPoint"/> as an initial timing point.</summary>
        public void SetAsInitialTimingPoint() => relativeTimePosition = MeasuredTimePosition.Start;

        /// <summary>Adjusts the relative time position of the <seealso cref="AbsoluteTimingPoint"/> by a number of measures. Should only be used when recalculating relative time positions.</summary>
        /// <param name="measures">The measures to adjust the time position by.</param>
        public void AdjustMeasure(int measures) => relativeTimePosition.Measure += measures;

        /// <inheritdoc/>
        protected override int CompareTimePosition(TimingPoint other) => TimePosition.CompareTo(other.GetAbsoluteTimePosition());

        /// <inheritdoc/>
        public override TimeSpan GetAbsoluteTimePosition() => TimePosition;
        /// <inheritdoc/>
        public override MeasuredTimePosition GetRelativeTimePosition() => relativeTimePosition;
        /// <inheritdoc/>
        public override void CalculateTimePosition(TimingPoint previous) => CalculateRelativeTimePosition(previous);

        /// <summary>Parses a string representation of an absolute timing point into a <seealso cref="AbsoluteTimingPoint"/>.</summary>
        /// <param name="s">The string representation of an absolute timing point to parse.</param>
        public static AbsoluteTimingPoint Parse(string s)
        {
            var split = s.Split('|');
            return new AbsoluteTimingPoint(ToDouble(split[0]), ToDouble(split[1]), TimeSignature.Parse(split[2]));
        }

        /// <summary>Attempts to parse a string representation of an absolute timing point into a <seealso cref="AbsoluteTimingPoint"/> and returns a <seealso cref="bool"/> indicating whether the operation was successful or not.</summary>
        /// <param name="s">The string representation of an absolute timing point to parse.</param>
        /// <param name="timingPoint">The <seealso cref="AbsoluteTimingPoint"/> that will be parsed. If the string is unparsable, the value is <see langword="null"/>.</param>
        public static bool TryParse(string s, out AbsoluteTimingPoint timingPoint)
        {
            timingPoint = null;
            var split = s.Split('|');
            if (!double.TryParse(split[0], out double seconds))
                return false;
            if (!double.TryParse(split[1], out double bpm))
                return false;
            if (!TimeSignature.TryParse(split[2], out var timeSignature))
                return false;
            timingPoint = new AbsoluteTimingPoint(seconds, bpm, timeSignature);
            return true;
        }

        /// <summary>Returns the string representation of this absolute timing point.</summary>
        public override string ToString() => $"{TimePositionSeconds}|{BPM}|{TimeSignature}";
    }
}
