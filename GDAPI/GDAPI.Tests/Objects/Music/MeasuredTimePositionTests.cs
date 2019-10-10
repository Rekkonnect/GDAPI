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
        public void Parse()
        {
            bool success = MeasuredTimePosition.TryParse("1:1.500", out var timePosition);

            Assert.IsTrue(success, "Unparsable because ???");
            Assert.AreEqual(new MeasuredTimePosition(1, 1, 0.5f), timePosition, "Something wrong with parsing");
        }
    }
}