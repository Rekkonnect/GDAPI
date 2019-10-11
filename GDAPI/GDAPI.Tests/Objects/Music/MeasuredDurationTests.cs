using GDAPI.Objects.Music;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace GDAPI.Tests.Objects.Music
{
    public class MeasuredDurationTests
    {
        private TimeSignature commonTimeSignature = new TimeSignature(4, 4);
        private TimeSignature waltzTimeSignature = new TimeSignature(3, 4);
        private TimeSignature uncommonTimeSignature = new TimeSignature(5, 4);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IncreaseBeat()
        {
            var duration = MeasuredDuration.Zero;

            duration.IncreaseBeat(commonTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(0, 1, 0), "0 + 1 = ?");

            duration.IncreaseBeat(3, commonTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(1, 0, 0), "/shrug Apparently 4 beats in 4/4 are not equal to a single measure in duration");

            duration.IncreaseBeat(3, waltzTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(2, 0, 0), "Being honest, adding 4/4 and 3/4 is a bit illegal (not really)");

            duration.IncreaseBeat(5, uncommonTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(3, 0, 0), "And 5/4 are here to save the day");

            duration.IncreaseBeat(-5, commonTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(1, 3, 0), "Subtraction time failed");
        }
        [Test]
        public void IncreaseFraction()
        {
            var duration = MeasuredDuration.Zero;

            duration.IncreaseFraction(0.5f, commonTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(0, 0, 0.5f), "Fractions seem to be hard to work with");

            duration.IncreaseFraction(1.5f, commonTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(0, 2, 0), "Adding full beats and the fraction itself");

            duration.IncreaseFraction(2.5f, waltzTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(1, 1, 0.5f), "Okay, adding part of a 4/4 measure and part of a 3/4 one is definitely illegal");

            duration.IncreaseFraction(-4, uncommonTimeSignature);
            Assert.AreEqual(duration, new MeasuredDuration(0, 2, 0.5f), "I've honestly lost count");
        }
        [Test]
        public void IncreaseMeasure()
        {
            var duration = MeasuredDuration.Zero;

            duration.IncreaseMeasure(1);
            Assert.AreEqual(duration, new MeasuredDuration(1, 0, 0), "I cannot believe I wrote a test case for this");

            duration.IncreaseMeasure(3);
            Assert.AreEqual(duration, new MeasuredDuration(4, 0, 0), "I cannot believe I wrote a test case for this");

            duration.IncreaseMeasure(-4);
            Assert.AreEqual(duration, MeasuredDuration.Zero, "Ekko R broke or something?");
        }
        [Test]
        public void Comparison()
        {
            var durations = new MeasuredDuration[]
            {
                new MeasuredDuration(0, 0, 0.5f),
                new MeasuredDuration(0, 1, 0.5f),
                new MeasuredDuration(1, 0, 0),
                new MeasuredDuration(1, 420, 0.1337f),
                new MeasuredDuration(2, 1, 0),
            };

            for (int i = 0; i < durations.Length; i++)
            {
                for (int j = 0; j < i; j++)
                    Assert.IsTrue(durations[i] > durations[j]);

                Assert.IsTrue(durations[i] == durations[i]);

                for (int j = i + 1; j < durations.Length; j++)
                    Assert.IsTrue(durations[i] < durations[j]);
            }
        }
        [Test]
        public void Parse()
        {
            bool success = MeasuredDuration.TryParse("1:1.500", out var duration);

            Assert.IsTrue(success, "Unparsable because ???");
            Assert.AreEqual(new MeasuredDuration(1, 1, 0.5f), duration, "Something wrong with parsing");
        }
        [Test]
        public void Stringify()
        {
            Assert.AreEqual(new MeasuredDuration(1, 2, 0.3f).ToString(), "1:2.300", "Bad stringification");
            Assert.AreEqual(new MeasuredDuration(1, 2, 0.34567f).ToString(), "1:2.346", "Bad rounded stringification");
        }
    }
}