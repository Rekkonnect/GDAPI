using GDAPI.Objects.DataStructures;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.DataStructures
{
    public class DirectionalTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DirectionalStuff()
        {
            int[,] values = new int[,]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },
            };
            var d = new Directional<int>(values);
            Assert.AreEqual(0, d.TopLeft);
            Assert.AreEqual(1, d.TopCenter);
            Assert.AreEqual(2, d.TopRight);
            Assert.AreEqual(3, d.MiddleLeft);
            Assert.AreEqual(4, d.Center);
            Assert.AreEqual(5, d.MiddleRight);
            Assert.AreEqual(6, d.BottomLeft);
            Assert.AreEqual(7, d.BottomCenter);
            Assert.AreEqual(8, d.BottomRight);
        }
    }
}