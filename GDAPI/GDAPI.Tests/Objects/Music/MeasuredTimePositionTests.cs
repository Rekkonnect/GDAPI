using GDAPI.Enumerations;
using GDAPI.Objects.Music;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.Music
{
    public class MeasuredTimePositionTests
    {
        private TimeSignature commonTimeSignature = new TimeSignature(4, 4);
        private TimeSignature waltzTimeSignature = new TimeSignature(3, 4);
        private TimeSignature uncommonTimeSignature = new TimeSignature(5, 4);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AdvanceBeat()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceBeat(commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(1, 2, 0), "Could not add two fucking numbers");

            timePosition.AdvanceBeat(3, commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(2, 1, 0), "Unexplainably failed to wrap to next measure on 4/4");

            timePosition.AdvanceBeat(3, waltzTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(3, 1, 0), "3/4 hurts in the ass");

            timePosition.AdvanceBeat(5, uncommonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(4, 1, 0), "5/4 is too uncommon for this struct");

            timePosition.AdvanceBeat(-5, commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(2, 4, 0), "Going back in time fucks shit up?");
        }
        [Test]
        public void AdvanceFraction()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceFraction(0.5f, commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(1, 1, 0.5f), "Fractions seem to be hard to work with");

            timePosition.AdvanceFraction(1.5f, commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(1, 3, 0), "Adding full beats and the fraction itself");

            timePosition.AdvanceFraction(2.5f, waltzTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(2, 2, 0.5f), "Advancing to next measure from beat fraction is impossible");

            timePosition.AdvanceFraction(-0.5f, uncommonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(2, 2, 0), "Going back in time fucks shit up again?");
        }
        [Test]
        public void AdvanceMeasure()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceMeasure(1);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(2, 1, 0), "I cannot believe I wrote a test case for this");

            timePosition.AdvanceMeasure(2);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(4, 1, 0), "I cannot believe I wrote a test case for this");
        }
        [Test]
        public void AdvanceToStartOfNextMeasure()
        {
            var timePosition = new MeasuredTimePosition(1, 2, 0);

            timePosition.AdvanceToStartOfNextMeasure();
            Assert.AreEqual(new MeasuredTimePosition(2, 1, 0), timePosition, "I cannot believe I wrote a test case for this");
        }
        [Test]
        public void AdvanceValueMusicalNoteValue()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceValue(MusicalNoteValue.Quarter, commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(1, 2, 0), "Looks like musical note values are a bit complicated");

            timePosition.AdvanceValue(MusicalNoteValue.Eighth, commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(1, 2, 0.5f), "An eighth is half a quarter");

            timePosition.AdvanceValue(MusicalNoteValue.Whole, commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(2, 2, 0.5f), "Whole means entire measure in 4/4");
        }
        [Test]
        public void AdvanceValueRhythmicalValue()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Quarter, 0, 3), waltzTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(2, 1, 0), "Adding 3/4 to a 3/4 measure somehow doesn't add up to a single measure");

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Eighth, 0, 8), commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(3, 1, 0), "Adding 8/8 to a 4/4 measure somehow doesn't add up to a single measure");

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Whole, 0, 7), commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(10, 1, 0), "Adding 7 wholes to a 4/4 measure somehow does not make 7 measures");
        }
        [Test]
        public void AdvanceValueMeasuredDuration()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceValue(new MeasuredDuration(0, 2, 0.5f), waltzTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(1, 3, 0.5f), "1:1.000 + 0:2.500 in 3/4");

            timePosition.AdvanceValue(new MeasuredDuration(0, 1, 0.5f), waltzTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(2, 2, 0), "1:3.500 + 0:1.500 in 3/4");

            timePosition.AdvanceValue(new MeasuredDuration(0, 3, 0), commonTimeSignature);
            Assert.AreEqual(timePosition, new MeasuredTimePosition(3, 1, 0), "2:2.000 + 0:3.000 in 4/4");
        }
        [Test]
        public void DistanceFrom()
        {
            var timePosition = new MeasuredTimePosition(1, 2, 0);
            var otherTimePosition = new MeasuredTimePosition(2, 1, 0);

            Assert.AreEqual(new MeasuredDuration(0, 3, 0), timePosition.DistanceFrom(otherTimePosition, commonTimeSignature), "4/4: 1:2.000 - 2:1.000");
            Assert.AreEqual(new MeasuredDuration(0, 3, 0), otherTimePosition.DistanceFrom(timePosition, commonTimeSignature), "4/4: 2:1.000 - 1:2.000");
            Assert.AreEqual(new MeasuredDuration(0, 2, 0), timePosition.DistanceFrom(otherTimePosition, waltzTimeSignature), "3/4: 1:2.000 - 2:1.000");
            Assert.AreEqual(new MeasuredDuration(0, 2, 0), otherTimePosition.DistanceFrom(timePosition, waltzTimeSignature), "3/4: 2:1.000 - 1:2.000");
            Assert.AreEqual(new MeasuredDuration(0, 4, 0), timePosition.DistanceFrom(otherTimePosition, uncommonTimeSignature), "5/4: 1:2.000 - 2:1.000");
            Assert.AreEqual(new MeasuredDuration(0, 4, 0), otherTimePosition.DistanceFrom(timePosition, uncommonTimeSignature), "5/4: 2:1.000 - 1:2.000");
        }
        [Test]
        public void Comparison()
        {
            var a = new MeasuredTimePosition(1, 1, 0.5f);
            var b = new MeasuredTimePosition(1, 2, 0.5f);
            var c = new MeasuredTimePosition(2, 1, 0);
            var d = new MeasuredTimePosition(2, 420, 0.1337f);
            var e = new MeasuredTimePosition(3, 1, 0);

            Assert.IsTrue(a == a);
            Assert.IsTrue(a < b);
            Assert.IsTrue(a < c);
            Assert.IsTrue(a < d);
            Assert.IsTrue(a < e);

            Assert.IsTrue(b > a);
            Assert.IsTrue(b == b);
            Assert.IsTrue(b < c);
            Assert.IsTrue(b < d);
            Assert.IsTrue(b < e);

            Assert.IsTrue(c > a);
            Assert.IsTrue(c > b);
            Assert.IsTrue(c == c);
            Assert.IsTrue(c < d);
            Assert.IsTrue(c < e);

            Assert.IsTrue(d > a);
            Assert.IsTrue(d > b);
            Assert.IsTrue(d > c);
            Assert.IsTrue(d == d);
            Assert.IsTrue(d < e);

            Assert.IsTrue(e > a);
            Assert.IsTrue(e > b);
            Assert.IsTrue(e > c);
            Assert.IsTrue(e > d);
            Assert.IsTrue(e == e);
        }
        [Test]
        public void Parse()
        {
            bool success = MeasuredTimePosition.TryParse("1:1.500", out var timePosition);

            Assert.IsTrue(success, "Unparsable because ???");
            Assert.AreEqual(new MeasuredTimePosition(1, 1, 0.5f), timePosition, "Something wrong with parsing");
        }
        [Test]
        public void Stringify()
        {
            Assert.AreEqual(new MeasuredTimePosition(1, 2, 0.3f).ToString(), "1:2.300", "Bad stringification");
            Assert.AreEqual(new MeasuredTimePosition(1, 2, 0.34567f).ToString(), "1:2.346", "Bad rounded stringification");
        }
    }
}