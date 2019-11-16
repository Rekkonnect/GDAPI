using System;
using GDAPI.Objects.Music;
using GDAPI.Objects.TimingPoints;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.TimingPoints
{
    public class TimingPointTests : MusicTestsBase
    {
        private TimingPointList timingPoints;

        [SetUp]
        public void Setup()
        {
            timingPoints = new TimingPointList
            {
                new AbsoluteTimingPoint(1, 120, CommonTimeSignature),
                new RelativeTimingPoint(new MeasuredTimePosition(2, 1, 0), 120, CommonTimeSignature),   // should be at 3.000 seconds
                new RelativeTimingPoint(new MeasuredTimePosition(3, 1, 0), 180, CommonTimeSignature),   // should be at 5.000 seconds
                new RelativeTimingPoint(new MeasuredTimePosition(4, 1, 0), 180, WaltzTimeSignature),    // should be at 6.333 seconds
                new RelativeTimingPoint(new MeasuredTimePosition(6, 3, 0), 180, UncommonTimeSignature), // should be at 9.000 seconds
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
