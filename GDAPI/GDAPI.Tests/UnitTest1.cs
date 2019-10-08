using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private string WhosGay;
        
        [SetUp]
        public void Setup()
        {
            WhosGay = "Alex";
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(WhosGay, "Alten", "Uh oh! seems like Alex is gay here...");
        }
    }
}