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

        [Test]
        public void AdvanceBeat()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceBeat(commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 2, 0), timePosition, "Could not add two fucking numbers");

            timePosition.AdvanceBeat(3, commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 1, 0), timePosition, "Unexplainably failed to wrap to next measure on 4/4");

            timePosition.AdvanceBeat(3, waltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(3, 1, 0), timePosition, "3/4 hurts in the ass");

            timePosition.AdvanceBeat(5, uncommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(4, 1, 0), timePosition, "5/4 is too uncommon for this struct");

            timePosition.AdvanceBeat(-5, commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 4, 0), timePosition, "Going back in time fucks shit up?");
        }
        [Test]
        public void AdvanceFraction()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceFraction(0.5f, commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 1, 0.5f), timePosition, "Fractions seem to be hard to work with");

            timePosition.AdvanceFraction(1.5f, commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 3, 0), timePosition, "Adding full beats and the fraction itself");

            timePosition.AdvanceFraction(2.5f, waltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 2, 0.5f), timePosition, "Advancing to next measure from beat fraction is impossible");

            timePosition.AdvanceFraction(-0.5f, uncommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 2, 0), timePosition, "Going back in time fucks shit up again?");
        }
        [Test]
        public void AdvanceMeasure()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceMeasure(1);
            Assert.AreEqual(new MeasuredTimePosition(2, 1, 0), timePosition, "I cannot believe I wrote a test case for this");

            timePosition.AdvanceMeasure(2);
            Assert.AreEqual(new MeasuredTimePosition(4, 1, 0), timePosition, "I cannot believe I wrote a test case for this");
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
            Assert.AreEqual(new MeasuredTimePosition(1, 2, 0), timePosition, "Looks like musical note values are a bit complicated");

            timePosition.AdvanceValue(MusicalNoteValue.Eighth, commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 2, 0.5f), timePosition, "An eighth is half a quarter");

            timePosition.AdvanceValue(MusicalNoteValue.Whole, commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 2, 0.5f), timePosition, "Whole means entire measure in 4/4");
        }
        [Test]
        public void AdvanceValueRhythmicalValue()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Quarter, 0, 3), waltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 1, 0), timePosition, "Adding 3/4 to a 3/4 measure somehow doesn't add up to a single measure");

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Eighth, 0, 8), commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(3, 1, 0), timePosition, "Adding 8/8 to a 4/4 measure somehow doesn't add up to a single measure");

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Whole, 0, 7), commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(10, 1, 0), timePosition, "Adding 7 wholes to a 4/4 measure somehow does not make 7 measures");
        }
        [Test]
        public void AdvanceValueMeasuredDuration()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceValue(new MeasuredDuration(0, 2, 0.5f), waltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 3, 0.5f), timePosition, "1:1.000 + 0:2.500 in 3/4");

            timePosition.AdvanceValue(new MeasuredDuration(0, 1, 0.5f), waltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 2, 0), timePosition, "1:3.500 + 0:1.500 in 3/4");

            timePosition.AdvanceValue(new MeasuredDuration(0, 3, 0), commonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(3, 1, 0), timePosition, "2:2.000 + 0:3.000 in 4/4");
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
            var timePositions = new MeasuredTimePosition[]
            {
                new MeasuredTimePosition(1, 1, 0.5f),
                new MeasuredTimePosition(1, 2, 0.5f),
                new MeasuredTimePosition(2, 1, 0),
                new MeasuredTimePosition(2, 420, 0.1337f),
                new MeasuredTimePosition(3, 1, 0),
            };

            for (int i = 0; i < timePositions.Length; i++)
            {
                for (int j = 0; j < i; j++)
                    Assert.IsTrue(timePositions[i] > timePositions[j]);

                Assert.IsTrue(timePositions[i] == timePositions[i]);

                for (int j = i + 1; j < timePositions.Length; j++)
                    Assert.IsTrue(timePositions[i] < timePositions[j]);
            }
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
            Assert.AreEqual("1:2.300", new MeasuredTimePosition(1, 2, 0.3f).ToString(), "Bad stringification");
            Assert.AreEqual("1:2.346", new MeasuredTimePosition(1, 2, 0.34567f).ToString(), "Bad rounded stringification");
        }
    }
}