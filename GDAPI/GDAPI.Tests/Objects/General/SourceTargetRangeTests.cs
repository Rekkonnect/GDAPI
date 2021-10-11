using GDAPI.Objects.General;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.General
{
    public class SourceTargetRangeTests
    {
        [Test]
        public void ValueAdjustment()
        {
            var range = new SourceTargetRange(1, 5, 11);
            Assert.AreEqual(15, range.TargetTo);
            range.SourceFrom = 5;
            Assert.AreEqual(5, range.SourceFrom);
            Assert.AreEqual(11, range.TargetTo);

            range.SourceFrom = 1;
            range.SourceTo = 3;
            Assert.AreEqual(3, range.SourceTo);
            Assert.AreEqual(13, range.TargetTo);

            range.SourceTo = 5;
            range.TargetFrom = 21;
            Assert.AreEqual(21, range.TargetFrom);
            Assert.AreEqual(25, range.TargetTo);

            range.TargetFrom = 11;
            range.TargetTo = 20;
            Assert.AreEqual(20, range.TargetTo);
            Assert.AreEqual(10, range.SourceTo);
        }
    }
}
