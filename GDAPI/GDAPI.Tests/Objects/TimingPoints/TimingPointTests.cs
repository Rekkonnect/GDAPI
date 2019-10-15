using System;
using GDAPI.Objects.Music;
using GDAPI.Objects.TimingPoints;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.TimingPoints
{
    public class TimingPointTests
    {
        private TimeSignature commonTimeSignature = new TimeSignature(4, 4);
        private TimeSignature waltzTimeSignature = new TimeSignature(3, 4);
        private TimeSignature uncommonTimeSignature = new TimeSignature(5, 4);

        private TimingPointList timingPoints;

        [SetUp]
        public void Setup()
        {
            timingPoints = new TimingPointList
            {
                new AbsoluteTimingPoint(1, 120, commonTimeSignature),
                new RelativeTimingPoint(new MeasuredTimePosition(2, 1, 0), 120, commonTimeSignature),   // should be at 3.000 seconds
                new RelativeTimingPoint(new MeasuredTimePosition(3, 1, 0), 180, commonTimeSignature),   // should be at 5.000 seconds
                new RelativeTimingPoint(new MeasuredTimePosition(4, 1, 0), 180, waltzTimeSignature),    // should be at 6.333 seconds
                new RelativeTimingPoint(new MeasuredTimePosition(6, 3, 0), 180, uncommonTimeSignature), // should be at 9.000 seconds
            };
        }

        [Test]
        public void TimingPointAtTimeSpan()
        {
            TimingPoint t;

            t = timingPoints.TimingPointAtTime(TimeSpan.FromSeconds(1.5));
            Assert.AreEqual(timingPoints[0], t);

            t = timingPoints.TimingPointAtTime(TimeSpan.FromSeconds(3));
            Assert.AreEqual(timingPoints[1], t);

            t = timingPoints.TimingPointAtTime(TimeSpan.FromSeconds(4.5));
            Assert.AreEqual(timingPoints[1], t);

            t = timingPoints.TimingPointAtTime(TimeSpan.FromSeconds(6));
            Assert.AreEqual(timingPoints[2], t);

            t = timingPoints.TimingPointAtTime(TimeSpan.FromSeconds(7));
            Assert.AreEqual(timingPoints[3], t);

            t = timingPoints.TimingPointAtTime(TimeSpan.FromSeconds(10));
            Assert.AreEqual(timingPoints[4], t);
        }
        [Test]
        public void TimingPointAtMeasuredTimePosition()
        {
            TimingPoint t;

            // Measured time position should always comply with the current timing point's time signature
            // Behavior when violating that rule is not evaluated, so fuck you and get your code to work right

            t = timingPoints.TimingPointAtTime(new MeasuredTimePosition(1, 1, 0));
            Assert.AreEqual(timingPoints[0], t);

            t = timingPoints.TimingPointAtTime(new MeasuredTimePosition(2, 1, 0));
            Assert.AreEqual(timingPoints[1], t);

            t = timingPoints.TimingPointAtTime(new MeasuredTimePosition(2, 4, 0));
            Assert.AreEqual(timingPoints[1], t);

            t = timingPoints.TimingPointAtTime(new MeasuredTimePosition(3, 3, 0));
            Assert.AreEqual(timingPoints[2], t);

            t = timingPoints.TimingPointAtTime(new MeasuredTimePosition(4, 3, 0));
            Assert.AreEqual(timingPoints[3], t);

            t = timingPoints.TimingPointAtTime(new MeasuredTimePosition(7, 1, 0));
            Assert.AreEqual(timingPoints[4], t);
        }
    }
}
