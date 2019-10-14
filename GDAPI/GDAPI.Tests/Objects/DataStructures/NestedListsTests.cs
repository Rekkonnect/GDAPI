using GDAPI.Objects.DataStructures;
using NUnit.Framework;
using System.Collections.Generic;

namespace GDAPI.Tests.Objects.DataStructures
{
    public class NestedListsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NestedListsStuff()
        {
            var lists = new List<int>[]
            {
                new List<int> { 1, 2, 4, 67 },
                new List<int> { 32, 25 },
                new List<int> { 54, 321 },
                new List<int> { 990, 99, 9, 12 },
            };
            var nestedLists = new NestedLists<int>();

            foreach (var l in lists)
                nestedLists.Add(l);

            Assert.AreEqual(12, nestedLists.Count);
            Assert.AreEqual(4, nestedLists.ListCount);

            for (int i = 0; i < lists.Length; i++)
                Assert.AreEqual(lists[i], nestedLists[i]);

            int currentElementIndex = 0;
            for (int i = 0; i < lists.Length; i++)
                for (int j = 0; j < lists[i].Count; j++, currentElementIndex++)
                {
                    Assert.AreEqual(lists[i][j], nestedLists[i, j]);
                    Assert.AreEqual(lists[i][j], nestedLists[currentElementIndex, false]);
                }

            nestedLists.RemoveFirst(2);
            Assert.AreEqual(6, nestedLists.Count);
            Assert.AreEqual(2, nestedLists.ListCount);

            nestedLists.Clear();
            Assert.AreEqual(0, nestedLists.Count);
            Assert.AreEqual(0, nestedLists.ListCount);
        }
    }
}