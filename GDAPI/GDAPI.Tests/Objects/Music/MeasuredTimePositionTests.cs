using GDAPI.Enumerations;
using GDAPI.Objects.Music;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.Music
{
    public class MeasuredTimePositionTests : MusicTestsBase
    {
        [Test]
        public void AdvanceBeat()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceBeat(CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 2, 0), timePosition, "Could not add two fucking numbers");

            timePosition.AdvanceBeat(3, CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 1, 0), timePosition, "Unexplainably failed to wrap to next measure on 4/4");

            timePosition.AdvanceBeat(3, WaltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(3, 1, 0), timePosition, "3/4 hurts in the ass");

            timePosition.AdvanceBeat(5, UncommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(4, 1, 0), timePosition, "5/4 is too uncommon for this struct");

            timePosition.AdvanceBeat(-5, CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 4, 0), timePosition, "Going back in time fucks shit up?");
        }
        [Test]
        public void AdvanceFraction()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceFraction(0.5f, CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 1, 0.5f), timePosition, "Fractions seem to be hard to work with");

            timePosition.AdvanceFraction(1.5f, CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 3, 0), timePosition, "Adding full beats and the fraction itself");

            timePosition.AdvanceFraction(2.5f, WaltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 2, 0.5f), timePosition, "Advancing to next measure from beat fraction is impossible");

            timePosition.AdvanceFraction(-0.5f, UncommonTimeSignature);
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

            timePosition.AdvanceValue(MusicalNoteValue.Quarter, CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 2, 0), timePosition, "Looks like musical note values are a bit complicated");

            timePosition.AdvanceValue(MusicalNoteValue.Eighth, CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 2, 0.5f), timePosition, "An eighth is half a quarter");

            timePosition.AdvanceValue(MusicalNoteValue.Whole, CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 2, 0.5f), timePosition, "Whole means entire measure in 4/4");
        }
        [Test]
        public void AdvanceValueRhythmicalValue()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Quarter, 0, 3), WaltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 1, 0), timePosition, "Adding 3/4 to a 3/4 measure somehow doesn't add up to a single measure");

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Eighth, 0, 8), CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(3, 1, 0), timePosition, "Adding 8/8 to a 4/4 measure somehow doesn't add up to a single measure");

            timePosition.AdvanceValue(new RhythmicalValue(MusicalNoteValue.Whole, 0, 7), CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(10, 1, 0), timePosition, "Adding 7 wholes to a 4/4 measure somehow does not make 7 measures");
        }
        [Test]
        public void AdvanceValueMeasuredDuration()
        {
            var timePosition = MeasuredTimePosition.Start;

            timePosition.AdvanceValue(new MeasuredDuration(0, 2, 0.5f), WaltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(1, 3, 0.5f), timePosition, "1:1.000 + 0:2.500 in 3/4");

            timePosition.AdvanceValue(new MeasuredDuration(0, 1, 0.5f), WaltzTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(2, 2, 0), timePosition, "1:3.500 + 0:1.500 in 3/4");

            timePosition.AdvanceValue(new MeasuredDuration(0, 3, 0), CommonTimeSignature);
            Assert.AreEqual(new MeasuredTimePosition(3, 1, 0), timePosition, "2:2.000 + 0:3.000 in 4/4");
        }
        [Test]
        public void DistanceFrom()
        {
            var timePosition = new MeasuredTimePosition(1, 2, 0);
            var otherTimePosition = new MeasuredTimePosition(2, 1, 0);

            Assert.AreEqual(new MeasuredDuration(0, 3, 0), timePosition.DistanceFrom(otherTimePosition, CommonTimeSignature), "4/4: 1:2.000 - 2:1.000");
            Assert.AreEqual(new MeasuredDuration(0, 3, 0), otherTimePosition.DistanceFrom(timePosition, CommonTimeSignature), "4/4: 2:1.000 - 1:2.000");
            Assert.AreEqual(new MeasuredDuration(0, 2, 0), timePosition.DistanceFrom(otherTimePosition, WaltzTimeSignature), "3/4: 1:2.000 - 2:1.000");
            Assert.AreEqual(new MeasuredDuration(0, 2, 0), otherTimePosition.DistanceFrom(timePosition, WaltzTimeSignature), "3/4: 2:1.000 - 1:2.000");
            Assert.AreEqual(new MeasuredDuration(0, 4, 0), timePosition.DistanceFrom(otherTimePosition, UncommonTimeSignature), "5/4: 1:2.000 - 2:1.000");
            Assert.AreEqual(new MeasuredDuration(0, 4, 0), otherTimePosition.DistanceFrom(timePosition, UncommonTimeSignature), "5/4: 2:1.000 - 1:2.000");
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