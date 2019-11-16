using GDAPI.Objects.Music;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;

namespace GDAPI.Tests.Objects.Music
{
    public class TimeSignatureTests
    {
        [Test]
        public void TimeSignatureStuff()
        {
            var commonTimeSignature = new TimeSignature(4, 4);

            Assert.Catch(() => commonTimeSignature.Denominator = 5, "The moment non-binary denominators are acceptable, call me at +306969696969");
            Assert.Catch(() => commonTimeSignature.Beats = -2, "How would negative beats even be considered in a time signature?");
            Assert.Catch(() => commonTimeSignature.Denominator = -5, "Somebody has to teach computers that negative denominators make even less sense in this context");

            int den = 1;
            for (int i = 0; i < sizeof(int) * 8 - 1; i++, den <<= 1)
                Assert.DoesNotThrow(() => commonTimeSignature.Denominator = den, $"Unacceptable binary denominator {den}");
        }
        [Test]
        public void Parse()
        {
            bool success = TimeSignature.TryParse("4/4", out var commonTimeSignature);

            Assert.IsTrue(success, "Cannot parse the string representation of the time signature which literally is splitting two values by this character: /");
            Assert.AreEqual(new TimeSignature(4, 4), commonTimeSignature, "Apparently this is not 4/4, even if correctly parsed");
        }
    }
}