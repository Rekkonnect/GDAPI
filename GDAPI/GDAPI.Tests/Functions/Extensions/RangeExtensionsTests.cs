using GDAPI.Functions.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GDAPI.Tests.Functions.Extensions
{
    public class RangeExtensionsTests
    {
        private readonly List<Range> unsortedRanges = new List<Range> { 0..4, 7..12, 12..15, 1..3, 2..3, 0..1, 16..23, 17..18, 8..10 };
        private readonly List<int> sortedRangeStarts = new List<int> { 0, 0, 1, 2, 7, 8, 12, 16, 17 };
        private readonly List<Range> mergedRanges = new List<Range> { 0..4, 7..15, 16..23 };

        private List<Range<int>> unsortedGenericRanges, mergedGenericRanges;

        [SetUp]
        public void Setup()
        {
            unsortedGenericRanges = unsortedRanges.Select(r => r.ToGenericRange()).ToList();
            mergedGenericRanges = mergedRanges.Select(r => r.ToGenericRange()).ToList();
        }

        #region Range
        [Test]
        public void Contains()
        {
            var range = 1..3;

            Assert.IsFalse(range.Contains(0));
            Assert.IsTrue(range.Contains(1));
            Assert.IsTrue(range.Contains(2));
            Assert.IsFalse(range.Contains(3));
            Assert.IsFalse(range.Contains(4));
        }

        [Test]
        public void AfterStart()
        {
            var range = 1..3;

            Assert.IsFalse(range.AfterStart(0));
            Assert.IsTrue(range.AfterStart(1));
            Assert.IsTrue(range.AfterStart(2));
            Assert.IsTrue(range.AfterStart(3));
            Assert.IsTrue(range.AfterStart(4));
        }

        [Test]
        public void BeforeEnd()
        {
            var range = 1..3;

            Assert.IsTrue(range.BeforeEnd(0));
            Assert.IsTrue(range.BeforeEnd(1));
            Assert.IsTrue(range.BeforeEnd(2));
            Assert.IsFalse(range.BeforeEnd(3));
            Assert.IsFalse(range.BeforeEnd(4));
        }

        [Test]
        public void SortByStartValue()
        {
            var sorted = unsortedRanges.SortByStartValue();
            for (int i = 0; i < sorted.Count; i++)
                Assert.AreEqual(sortedRangeStarts[i], sorted[i].Start.Value);
        }

        [Test]
        public void SortAndMerge()
        {
            var merged = unsortedRanges.SortAndMerge();
            for (int i = 0; i < merged.Count; i++)
                Assert.AreEqual(mergedRanges[i], merged[i]);
        }

        [Test]
        public void ListContainsBinarySearch()
        {
            var sorted = unsortedRanges.SortAndMerge();

            var shouldBeContained = new bool[mergedRanges.Last().End.Value];
            foreach (var r in mergedRanges)
            {
                for (int i = 0; i < r.GetAbsoluteRangeLength(); i++)
                    shouldBeContained[r.Start.Value + i] = true;
            }

            for (int i = 0; i < shouldBeContained.Length; i++)
                Assert.AreEqual(shouldBeContained[i], sorted.ContainsBinarySearch(i));
        }
        #endregion

        #region Range<T>
        [Test]
        public void GenericIsWithinBounds()
        {
            var range = (1..3).ToGenericRange();

            Assert.IsFalse(range.IsWithinBounds(0, true, true));
            Assert.IsTrue(range.IsWithinBounds(1, true, true));
            Assert.IsTrue(range.IsWithinBounds(2, true, true));
            Assert.IsTrue(range.IsWithinBounds(3, true, true));
            Assert.IsFalse(range.IsWithinBounds(4, true, true));

            Assert.IsFalse(range.IsWithinBounds(0, true, false));
            Assert.IsTrue(range.IsWithinBounds(1, true, false));
            Assert.IsTrue(range.IsWithinBounds(2, true, false));
            Assert.IsFalse(range.IsWithinBounds(3, true, false));
            Assert.IsFalse(range.IsWithinBounds(4, true, false));

            Assert.IsFalse(range.IsWithinBounds(0, false, true));
            Assert.IsFalse(range.IsWithinBounds(1, false, true));
            Assert.IsTrue(range.IsWithinBounds(2, false, true));
            Assert.IsTrue(range.IsWithinBounds(3, false, true));
            Assert.IsFalse(range.IsWithinBounds(4, false, true));

            Assert.IsFalse(range.IsWithinBounds(0, false, false));
            Assert.IsFalse(range.IsWithinBounds(1, false, false));
            Assert.IsTrue(range.IsWithinBounds(2, false, false));
            Assert.IsFalse(range.IsWithinBounds(3, false, false));
            Assert.IsFalse(range.IsWithinBounds(4, false, false));
        }
        [Test]
        public void GenericIsWithinInclusiveBounds()
        {
            var range = (1..3).ToGenericRange();

            Assert.IsFalse(range.IsWithinInclusiveBounds(0));
            Assert.IsTrue(range.IsWithinInclusiveBounds(1));
            Assert.IsTrue(range.IsWithinInclusiveBounds(2));
            Assert.IsTrue(range.IsWithinInclusiveBounds(3));
            Assert.IsFalse(range.IsWithinInclusiveBounds(4));
        }

        [Test]
        public void GenericAfterStart()
        {
            var range = (1..3).ToGenericRange();

            Assert.IsFalse(range.AfterStart(0, true));
            Assert.IsTrue(range.AfterStart(1, true));
            Assert.IsTrue(range.AfterStart(2, true));
            Assert.IsTrue(range.AfterStart(3, true));
            Assert.IsTrue(range.AfterStart(4, true));

            Assert.IsFalse(range.AfterStart(0, false));
            Assert.IsFalse(range.AfterStart(1, false));
            Assert.IsTrue(range.AfterStart(2, false));
            Assert.IsTrue(range.AfterStart(3, false));
            Assert.IsTrue(range.AfterStart(4, false));

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(range.AfterStart(i, false), range.AfterExclusiveStart(i));
                Assert.AreEqual(range.AfterStart(i, true), range.AfterInclusiveStart(i));
            }
        }

        [Test]
        public void GenericBeforeEnd()
        {
            var range = (1..3).ToGenericRange();

            Assert.IsTrue(range.BeforeEnd(0, true));
            Assert.IsTrue(range.BeforeEnd(1, true));
            Assert.IsTrue(range.BeforeEnd(2, true));
            Assert.IsTrue(range.BeforeEnd(3, true));
            Assert.IsFalse(range.BeforeEnd(4, true));

            Assert.IsTrue(range.BeforeEnd(0, false));
            Assert.IsTrue(range.BeforeEnd(1, false));
            Assert.IsTrue(range.BeforeEnd(2, false));
            Assert.IsFalse(range.BeforeEnd(3, false));
            Assert.IsFalse(range.BeforeEnd(4, false));

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(range.BeforeEnd(i, false), range.BeforeExclusiveEnd(i));
                Assert.AreEqual(range.BeforeEnd(i, true), range.BeforeInclusiveEnd(i));
            }
        }

        [Test]
        public void GenericSortByStartValue()
        {
            var sorted = unsortedGenericRanges.SortByStartValue();
            for (int i = 0; i < sorted.Count; i++)
                Assert.AreEqual(sortedRangeStarts[i], sorted[i].Begin);
        }

        [Test]
        public void GenericSortAndMerge()
        {
            var merged = unsortedGenericRanges.SortAndMerge();
            for (int i = 0; i < merged.Count; i++)
                Assert.AreEqual(mergedGenericRanges[i], merged[i]);
        }
        #endregion
    }
}
