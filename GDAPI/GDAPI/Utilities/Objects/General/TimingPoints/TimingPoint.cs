using GDAPI.Utilities.Objects.General.Music;
using System;

namespace GDAPI.Utilities.Objects.General.TimingPoints
{
    /// <summary>Represents a timing point.</summary>
    public abstract class TimingPoint : IComparable<TimingPoint>
    {
        /// <summary>The BPM of the timing point.</summary>
        public BPM BPM;
        /// <summary>The time signature of the timing point.</summary>
        public TimeSignature TimeSignature;

        /// <summary>Initializes a new instance of the <seealso cref="TimingPoint"/> class.</summary>
        /// <param name="bpm">The BPM of the timing point.</param>
        /// <param name="timeSignature">The time signature of the timing point.</param>
        public TimingPoint(BPM bpm, TimeSignature timeSignature)
        {
            BPM = bpm;
            TimeSignature = timeSignature;
        }

        /// <summary>Attempts to parse a string representation of a timing point into a <seealso cref="TimingPoint"/> and returns the resulting parsed object, or <see langword="null"/>.</summary>
        /// <param name="s">The string representation of a timing point to parse.</param>
        public static TimingPoint Parse(string s)
        {
            if (AbsoluteTimingPoint.TryParse(s, out var absolute))
                return absolute;
            if (RelativeTimingPoint.TryParse(s, out var relative))
                return relative;
            return null;
        }

        /// <summary>Compares this <seealso cref="TimingPoint"/>'s time position with another.</summary>
        /// <param name="other">The other <seealso cref="TimingPoint"/> whose time position to compare this one's to.</param>
        public int CompareTo(TimingPoint other) => CompareTimePosition(other);

        /// <summary>Returns the absolute time position of this timing point as a <seealso cref="TimeSpan"/>.</summary>
        public abstract TimeSpan GetAbsoluteTimePosition();
        /// <summary>Returns the relative time position of this timing point as a <seealso cref="MeasuredTimePosition"/>.</summary>
        public abstract MeasuredTimePosition GetRelativeTimePosition();
        /// <summary>Calculates the relative or absolute time position of this timing poing based on a previous <seealso cref="TimingPoint"/>, depending on which is not known.</summary>
        /// <param name="previous">The previous <seealso cref="TimingPoint"/> based on which to calculate the relative or absolute time position.</param>
        public abstract void CalculateTimePosition(TimingPoint previous);

        /// <summary>Compares the time position of this and another timing point's. It is defined so that inherited classes handle comparison.</summary>
        /// <param name="other">The other timing point to compare to.</param>
        protected abstract int CompareTimePosition(TimingPoint other);
    }
    /// <summary>Represents a timing point with a time position of a specified type.</summary>
    /// <typeparam name="T">The type of the time position.</typeparam>
    public abstract class TimingPoint<T> : TimingPoint, IComparable<TimingPoint<T>>
        where T : struct, IComparable<T>
    {
        /// <summary>The time position of the event.</summary>
        public T TimePosition;

        /// <summary>Initializes a new instance of the <seealso cref="TimingPoint{T}"/> class.</summary>
        /// <param name="timePosition">The time position of the event.</param>
        /// <param name="bpm">The BPM of the timing point.</param>
        /// <param name="timeSignature">The time signature of the timing point.</param>
        public TimingPoint(T timePosition, BPM bpm, TimeSignature timeSignature)
            : base(bpm, timeSignature)
        {
            TimePosition = timePosition;
        }

        /// <summary>Compares this <seealso cref="TimingPoint"/>'s time position with another.</summary>
        /// <param name="other">The other <seealso cref="TimingPoint"/> whose time position to compare this one's to.</param>
        public int CompareTo(TimingPoint<T> other) => TimePosition.CompareTo(other.TimePosition);

        /// <inheritdoc/>
        protected sealed override int CompareTimePosition(TimingPoint other) => CompareTo(other as TimingPoint<T>);

        /// <summary>Returns the string representation of this timing point.</summary>
        public override string ToString() => $"{TimePosition}|{BPM}|{TimeSignature}";
    }
}
