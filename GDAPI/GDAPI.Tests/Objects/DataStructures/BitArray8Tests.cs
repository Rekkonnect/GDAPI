using GDAPI.Objects.DataStructures;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.DataStructures
{
    public class BitArray8Tests
    {
        [Test]
        public void BitArray()
        {
            var falseArray = new BitArray8(false);
            for (int i = 0; i < 8; i++)
            {
                Assert.IsFalse(falseArray.GetBoolBit(i));
                Assert.AreEqual(0, falseArray.GetBit(i));
            }

            var trueArray = new BitArray8(true);
            for (int i = 0; i < 8; i++)
            {
                Assert.IsTrue(trueArray.GetBoolBit(i));
                Assert.AreEqual(1, trueArray.GetBit(i));
            }

            // Do not you fucking dare set a bit to anything other than 0 or 1, the entire thing will break
            // Just like your heart when you see your data being fucked up, so watch out kid

            var array = new BitArray8(false);
            array.SetBit(2, 1);
            Assert.AreEqual(1, array.GetBit(2));
            array.SetBoolBit(2, false);
            Assert.AreEqual(0, array.GetBit(2));
            array[2] = true;
            Assert.IsTrue(array[2]);
            array[2] = false;
            Assert.IsFalse(array[2]);
        }
    }
}