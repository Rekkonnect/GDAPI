using GDAPI.Objects.DataStructures;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.DataStructures
{
    public class SortedListTests
    {
        [Test]
        public void SortedListDefaultComparison()
        {
            var array = new int[] { 2, 4, 122, 6, 234, 51, 7 };
            var sortedArray = new int[] { 2, 4, 6, 7, 51, 122, 234 };

            var sortedList = new SortedList<int>(array);

            Assert.AreEqual(2, sortedList.Minimum);
            Assert.AreEqual(234, sortedList.Maximum);

            for (int i = 0; i < sortedList.Count; i++)
                Assert.AreEqual(sortedList[i], sortedArray[i]);

            Assert.AreEqual(0, sortedList.IndexToInsert(1));
            Assert.AreEqual(4, sortedList.IndexToInsert(8));
            Assert.AreEqual(3, sortedList.IndexBefore(8));
            Assert.AreEqual(4, sortedList.IndexAfter(8));
            Assert.AreEqual(4, sortedList.IndexAfter(7));
            Assert.AreEqual(7, sortedList.IndexToInsert(235));
        }
        [Test]
        public void SortedListCustomComparison()
        {
            var array = new int[] { 2, 4, 122, 6, 234, 51, 7 };
            var sortedArray = new int[] { 234, 122, 51, 7, 6, 4, 2 };

            var sortedList = new SortedList<int>(array, CustomComparison);

            Assert.AreEqual(234, sortedList.Minimum);
            Assert.AreEqual(2, sortedList.Maximum);

            for (int i = 0; i < sortedList.Count; i++)
                Assert.AreEqual(sortedList[i], sortedArray[i]);

            Assert.AreEqual(7, sortedList.IndexToInsert(1));
            Assert.AreEqual(3, sortedList.IndexToInsert(8));
            Assert.AreEqual(2, sortedList.IndexBefore(8));
            Assert.AreEqual(3, sortedList.IndexAfter(8));
            Assert.AreEqual(4, sortedList.IndexAfter(7));
            Assert.AreEqual(0, sortedList.IndexToInsert(235));
        }

        private int CustomComparison(int left, int right) => right.CompareTo(left);
    }
}