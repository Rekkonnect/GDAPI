using GDAPI.Enumerations;
using GDAPI.Objects.Music;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace GDAPI.Tests.Objects.Music
{
    public class RhythmicalValueTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RhythmicalValueStuff()
        {
            var fourQuarters = new RhythmicalValue(MusicalNoteValue.Quarter, 0, 4);
            Assert.AreEqual(1, fourQuarters.TotalValue);

            var singleDottedQuarter = new RhythmicalValue(MusicalNoteValue.Quarter, 1);
            Assert.AreEqual(1 / 4d + 1 / 8d, singleDottedQuarter.TotalValue);

            var doubleDottedQuarter = new RhythmicalValue(MusicalNoteValue.Quarter, 2);
            Assert.AreEqual(1 / 4d + 1 / 8d + 1 / 16d, doubleDottedQuarter.TotalValue);
        }
    }
}