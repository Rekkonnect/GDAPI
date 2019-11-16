using GDAPI.Objects.Music;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace GDAPI.Tests.Objects.Music
{
    public class MeasuredDurationTests : MusicTestsBase
    {
        [Test]
        public void IncreaseBeat()
        {
            var duration = MeasuredDuration.Zero;

            duration.IncreaseBeat(CommonTimeSignature);
            Assert.AreEqual(new MeasuredDuration(0, 1, 0), duration, "0 + 1 = ?");

            duration.IncreaseBeat(3, CommonTimeSignature);
            Assert.AreEqual(new MeasuredDuration(1, 0, 0), duration, "/shrug Apparently 4 beats in 4/4 are not equal to a single measure in duration");

            duration.IncreaseBeat(3, WaltzTimeSignature);
            Assert.AreEqual(new MeasuredDuration(2, 0, 0), duration, "Being honest, adding 4/4 and 3/4 is a bit illegal (not really)");

            duration.IncreaseBeat(5, UncommonTimeSignature);
            Assert.AreEqual(new MeasuredDuration(3, 0, 0), duration, "And 5/4 are here to save the day");

            duration.IncreaseBeat(-5, CommonTimeSignature);
            Assert.AreEqual(new MeasuredDuration(1, 3, 0), duration, "Subtraction time failed");
        }
        [Test]
        public void IncreaseFraction()
        {
            var duration = MeasuredDuration.Zero;

            duration.IncreaseFraction(0.5f, CommonTimeSignature);
            Assert.AreEqual(new MeasuredDuration(0, 0, 0.5f), duration, "Fractions seem to be hard to work with");

            duration.IncreaseFraction(1.5f, CommonTimeSignature);
            Assert.AreEqual(new MeasuredDuration(0, 2, 0), duration, "Adding full beats and the fraction itself");

            duration.IncreaseFraction(2.5f, WaltzTimeSignature);
            Assert.AreEqual(new MeasuredDuration(1, 1, 0.5f), duration, "Okay, adding part of a 4/4 measure and part of a 3/4 one is definitely illegal");

            duration.IncreaseFraction(-4, UncommonTimeSignature);
            Assert.AreEqual(new MeasuredDuration(0, 2, 0.5f), duration, "I've honestly lost count");
        }
        [Test]
        public void IncreaseMeasure()
        {
            var duration = MeasuredDuration.Zero;

            duration.IncreaseMeasure(1);
            Assert.AreEqual(new MeasuredDuration(1, 0, 0), duration, "I cannot believe I wrote a test case for this");

            duration.IncreaseMeasure(3);
            Assert.AreEqual(new MeasuredDuration(4, 0, 0), duration, "I cannot believe I wrote a test case for this");

            duration.IncreaseMeasure(-4);
            Assert.AreEqual(MeasuredDuration.Zero, duration, "Ekko R broke or something?");
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
            Assert.AreEqual("1:2.300", new MeasuredDuration(1, 2, 0.3f).ToString(), "Bad stringification");
            Assert.AreEqual("1:2.346", new MeasuredDuration(1, 2, 0.34567f).ToString(), "Bad rounded stringification");
        }
    }
}