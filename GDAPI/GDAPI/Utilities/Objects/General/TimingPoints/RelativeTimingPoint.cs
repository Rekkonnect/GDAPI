using GDAPI.Utilities.Objects.General.Music;
using System;
using static System.Convert;

namespace GDAPI.Utilities.Objects.General.TimingPoints
{
    /// <summary>Represents a relative timing point in a <seealso cref="GuidelineEditorPreset"/>. The relative timing point only affects measure calculation. It will reset the measure if the beat is not acceptable from this timing point's time signature.</summary>
    public class RelativeTimingPoint : TimingPoint<MeasuredTimePosition>
    {
        /// <summary>Represents the absolute time position, as calculated from a previous <seealso cref="TimingPoint"/>.</summary>
        public TimeSpan AbsoluteTimePosition { get; private set; }

        /// <summary>Initializes a new instance of the <seealso cref="RelativeTimingPoint"/> class.</summary>
        /// <param name="timePosition">The time position of the event.</param>
        /// <param name="bpm">The BPM of the timing point.</param>
        /// <param name="timeSignature">The time signature of the timing point.</param>
        public RelativeTimingPoint(MeasuredTimePosition timePosition, BPM bpm, TimeSignature timeSignature)
            : base(timePosition, bpm, timeSignature) { }

        /// <summary>Calculates the absolute time position of this relative timing poing based on a previous <seealso cref="TimingPoint"/>.</summary>
        /// <param name="previous">The previous <seealso cref="TimingPoint"/> based on which to calculate the absolute time position.</param>
        public void CalculateAbsoluteTimePosition(TimingPoint previous)
        {
            // Fuck this is so long
            AbsoluteTimePosition = previous.GetAbsoluteTimePosition() + previous.BPM.GetDurationTimeSpan(TimePosition.DistanceFrom(previous.GetRelativeTimePosition(), previous.TimeSignature), previous.TimeSignature);
        }

        /// <inheritdoc/>
        public override TimeSpan GetAbsoluteTimePosition() => AbsoluteTimePosition;
        /// <inheritdoc/>
        public override MeasuredTimePosition GetRelativeTimePosition() => TimePosition;
        /// <inheritdoc/>
        public override void CalculateTimePosition(TimingPoint previous) => CalculateAbsoluteTimePosition(previous);

        /// <summary>Parses a string representation of a relative timing point into a <seealso cref="RelativeTimingPoint"/>.</summary>
        /// <param name="s">The string representation of a relative timing point to parse.</param>
        public static RelativeTimingPoint Parse(string s)
        {
            var split = s.Split('|');
            return new RelativeTimingPoint(MeasuredTimePosition.Parse(split[0]), ToDouble(split[1]), TimeSignature.Parse(split[2]));
        }

        /// <summary>Attempts to parse a string representation of a relative timing point into a <seealso cref="RelativeTimingPoint"/> and returns a <seealso cref="bool"/> indicating whether the operation was successful or not.</summary>
        /// <param name="s">The string representation of a relative timing point to parse.</param>
        /// <param name="timingPoint">The <seealso cref="RelativeTimingPoint"/> that will be parsed. If the string is unparsable, the value is <see langword="null"/>.</param>
        public static bool TryParse(string s, out RelativeTimingPoint timingPoint)
        {
            timingPoint = null;
            var split = s.Split('|');
            if (!MeasuredTimePosition.TryParse(split[0], out var timePosition))
                return false;
            if (!double.TryParse(split[1], out double bpm))
                return false;
            if (!TimeSignature.TryParse(split[2], out var timeSignature))
                return false;
            timingPoint = new RelativeTimingPoint(timePosition, bpm, timeSignature);
            return true;
        }
    }
}
