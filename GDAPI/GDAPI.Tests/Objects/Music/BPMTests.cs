using GDAPI.Objects.Music;
using NUnit.Framework;
using System;

namespace GDAPI.Tests.Objects.Music
{
    public class BPMTests
    {
        private TimeSignature commonTimeSignature = new TimeSignature(4, 4);

        [Test]
        public void BPMStuff()
        {
            BPM bpm = 120;

            Assert.AreEqual(0.5, bpm.BeatInterval, "What's the duration of a beat at 120 BPM?");

            Assert.AreEqual(2, bpm.MeasureInterval(commonTimeSignature), "What's the duration of a 4/4 measure at 120 BPM?");
            Assert.AreEqual(2.75, bpm.GetDuration(new MeasuredDuration(1, 1, 0.5f), commonTimeSignature), "What's the duration of 1:1.500 at 4/4 at 120 BPM?");
            Assert.AreEqual(new MeasuredDuration(1, 0, 0), bpm.GetMeasuredDuration(new TimeSpan(0, 0, 2), commonTimeSignature), "What's the measured duration of 2 seconds at 4/4 at 120 BPM?");
        }
        [Test]
        public void Parse()
        {
            bool success = BPM.TryParse("120", out var bpm);

            Assert.IsTrue(success, "Everybody gangsta till double.TryParse breaks entirely");
            Assert.AreEqual(new BPM(120), bpm, "Something wrong with parsing");
        }
    }
}