using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.DataStructures;
using GDAPI.Objects.Music;

namespace GDAPI.Objects.TimingPoints
{
    /// <summary>Contains a list of <seealso cref="TimingPoint"/>s.</summary>
    public class TimingPointList : IEnumerable<TimingPoint>
    {
        private SortedList<TimingPoint> timingPoints;

        /// <summary>Gets the count of timing points in this <seealso cref="TimingPointList"/>.</summary>
        public int Count => timingPoints.Count;

        /// <summary>Creates a new empty instance of the <seealso cref="TimingPointList"/> class.</summary>
        public TimingPointList() => timingPoints = new SortedList<TimingPoint>();
        /// <summary>Creates a new instance of the <seealso cref="TimingPointList"/> class.</summary>
        /// <param name="presetTimingPoints">The list of timing points that are contained.</param>
        public TimingPointList(List<TimingPoint> presetTimingPoints)
        {
            timingPoints = ConvertToSortedList(presetTimingPoints);
        }
        /// <summary>Creates a new instance of the <seealso cref="TimingPointList"/> class.</summary>
        /// <param name="presetTimingPoints">The list of timing points that are contained.</param>
        public TimingPointList(SortedList<TimingPoint> presetTimingPoints)
        {
            timingPoints = new SortedList<TimingPoint>(presetTimingPoints);
        }

        /// <summary>Adds a <seealso cref="TimingPoint"/> to the list.</summary>
        /// <param name="timingPoint">The <seealso cref="TimingPoint"/> to add to the list.</param>
        public void Add(TimingPoint timingPoint)
        {
            if (Count == 0)
            {
                if (!(timingPoint is AbsoluteTimingPoint a))
                    throw new InvalidOperationException("The first timing point in the list has to be an absolute timing point.");
                a.SetAsInitialTimingPoint();
            }
            int index = timingPoints.Insert(timingPoint);
            if (Count > 1)
                RecalculateTimePositions(index);
        }
        /// <summary>Adds a collection of <seealso cref="TimingPoint"/>s to the list.</summary>
        /// <param name="points">The <seealso cref="TimingPoint"/>s to add to the list.</param>
        public void AddRange(IEnumerable<TimingPoint> points)
        {
            if (!points.Any())
                return;

            int minIndex = int.MaxValue;
            foreach (var p in points)
            {
                int index = timingPoints.Insert(p) + 1;
                if (index < minIndex)
                    minIndex = index;
            }
            RecalculateTimePositions(minIndex);
        }
        /// <summary>Removes a <seealso cref="TimingPoint"/> from the list.</summary>
        /// <param name="timingPoint">The <seealso cref="TimingPoint"/> to remove from the list.</param>
        public void Remove(TimingPoint timingPoint)
        {
            if (timingPoints.RemoveIfNotAtIndex(timingPoint, 0, out int index))
                RecalculateTimePositions(index);
        }

        /// <summary>Clones this instance and returns the new instance.</summary>
        public TimingPointList Clone() => new TimingPointList(timingPoints);

        /// <summary>Gets the timing point that is applied at the specified absolute time.</summary>
        /// <param name="absoluteTime">The absolute time at which the current timing point applies.</param>
        public TimingPoint TimingPointAtTime(TimeSpan absoluteTime) => timingPoints.ElementBefore(new AbsoluteTimingPoint(absoluteTime, 120, new TimeSignature()), TimingPoint.AbsoluteComparison);
        /// <summary>Gets the timing point that is applied at the specified relative time.</summary>
        /// <param name="relativeTime">The relative time at which the current timing point applies.</param>
        public TimingPoint TimingPointAtTime(MeasuredTimePosition relativeTime) => timingPoints.ElementBefore(new RelativeTimingPoint(relativeTime, 120, new TimeSignature()), TimingPoint.RelativeComparison);

        /// <summary>Returns the index of the specified <seealso cref="TimingPoint"/> in the list.</summary>
        /// <param name="timingPoint">The <seealso cref="TimingPoint"/> whose index in the list to get.</param>
        public int IndexOf(TimingPoint timingPoint) => timingPoints.BinarySearch(timingPoint);

        /// <summary>Gets the <seealso cref="TimingPoint"/> at the specified index.</summary>
        /// <param name="index">The index of the <seealso cref="TimingPoint"/> to get.</param>
        public TimingPoint this[int index] => timingPoints[index];

        public IEnumerator<TimingPoint> GetEnumerator() => ((IEnumerable<TimingPoint>)timingPoints).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TimingPoint>)timingPoints).GetEnumerator();

        private void RecalculateTimePositions(int firstIndex)
        {
            for (int i = firstIndex; i < timingPoints.Count; i++)
                timingPoints[i].CalculateTimePosition(timingPoints[i - 1]);
        }

        /// <summary>Gets the string representation of this <seealso cref="TimingPointList"/>.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var p in timingPoints)
                result.AppendLine(p.ToString()).AppendLine();

            return result.RemoveLastIfEndsWith('\n').ToString();
        }

        private static SortedList<TimingPoint> ConvertToSortedList(List<TimingPoint> presetTimingPoints)
        {
            var timingPoints = new SortedList<TimingPoint>(presetTimingPoints.Count, CompareTimingPoints);

            var absoluteTimingPoints = Convert<TimingPoint, AbsoluteTimingPoint>(presetTimingPoints);

            if (absoluteTimingPoints.Count == 0)
                throw new InvalidOperationException("No absolute timing points were used");
            absoluteTimingPoints[0].SetAsInitialTimingPoint();

            var relativeTimingPoints = Convert<TimingPoint, RelativeTimingPoint>(presetTimingPoints);
            relativeTimingPoints.Sort((a, b) => MeasuredTimePosition.CompareByAbsolutePosition(a.TimePosition, b.TimePosition));

            // Add absolute timing points and calculate their relative time positions
            AbsoluteTimingPoint previousAbsolute = null;
            foreach (var a in absoluteTimingPoints)
            {
                if (previousAbsolute != null)
                    a.CalculateRelativeTimePosition(previousAbsolute);
                timingPoints.Add(previousAbsolute = a);
            }

            // Add relative timing points, calculate their absolute time positions and adjust the absolute timing points' relative time positions
            foreach (var r in relativeTimingPoints)
            {
                int index = timingPoints.IndexBefore(r);
                r.CalculateAbsoluteTimePosition(timingPoints[index]);
                if (index + 1 < timingPoints.Count)
                {
                    // The next timing point is certainly an absolute timing point since no relative timing points have been added beyond that one yet
                    var nextAbsolute = timingPoints[index + 1] as AbsoluteTimingPoint;

                    // Calculate the measure adjustment for the first absolute timing point to apply it to all the rest
                    int currentMeasure = nextAbsolute.RelativeTimePosition.Measure;
                    nextAbsolute.CalculateRelativeTimePosition(r);
                    int newMeasure = nextAbsolute.RelativeTimePosition.Measure;
                    int measureAdjustment = newMeasure - currentMeasure;

                    if (measureAdjustment != 0)
                        for (int i = index + 2; i < timingPoints.Count; i++)
                            (timingPoints[i] as AbsoluteTimingPoint).AdjustMeasure(measureAdjustment);
                }
                timingPoints.Add(r);
            }

            return timingPoints;
        }

        private static List<TConverted> Convert<TBase, TConverted>(List<TBase> list)
            where TConverted : class
        {
            var result = list.ConvertAll(p => p as TConverted);
            result.RemoveAll(p => p is null);
            return result;
        }

        private static int CompareTimingPoints(TimingPoint left, TimingPoint right) => left.GetRelativeTimePosition().CompareTo(right.GetRelativeTimePosition());
    }
}
