using GDAPI.Functions.Extensions;
using NUnit.Framework;

namespace GDAPI.Tests.Functions.Extensions
{
    public class StringExtensionsTests
    {
        [Test]
        public void SubstringFrom()
        {
            const string s = "abcdefghijjjjjklmn";

            Assert.AreEqual("jjjklmn", s.SubstringFrom("jj", false));
            Assert.AreEqual("jjjjjklmn", s.SubstringFrom("jj", true));
        }
        [Test]
        public void SubstringUntil()
        {
            const string s = "abcdefghijjjjjklmn";

            Assert.AreEqual("abcdefghi", s.SubstringUntil("jj", false));
            Assert.AreEqual("abcdefghijj", s.SubstringUntil("jj", true));
        }
    }
}
