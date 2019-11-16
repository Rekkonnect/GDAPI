using GDAPI.Objects.DataStructures;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.DataStructures
{
    public class BitArray16Tests
    {
        [Test]
        public void BitArray()
        {
            var falseArray = new BitArray16(false);
            for (int i = 0; i < 16; i++)
            {
                Assert.IsFalse(falseArray.GetBoolBit(i));
                Assert.AreEqual(0, falseArray.GetBit(i));
            }

            var trueArray = new BitArray16(true);
            for (int i = 0; i < 16; i++)
            {
                Assert.IsTrue(trueArray.GetBoolBit(i));
                Assert.AreEqual(1, trueArray.GetBit(i));
            }

            // Do not you fucking dare set a bit to anything other than 0 or 1, the entire thing will break
            // Just like your heart when you see your data being fucked up, so watch out kid

            var array = new BitArray16(false);
            array.SetBit(12, 1);
            Assert.AreEqual(1, array.GetBit(12));
            array.SetBoolBit(12, false);
            Assert.AreEqual(0, array.GetBit(12));
            array[12] = true;
            Assert.IsTrue(array[12]);
            array[12] = false;
            Assert.IsFalse(array[12]);
        }
    }
}