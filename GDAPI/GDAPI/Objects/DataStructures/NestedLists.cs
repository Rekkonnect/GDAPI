using System;
using System.Collections;
using System.Collections.Generic;

namespace GDAPI.Objects.DataStructures
{
    /// <summary>Represents a list of lists of objects of type <seealso cref="T"/>.</summary>
    /// <typeparam name="T">The type of the objects to store in the lists.</typeparam>
    public class NestedLists<T> : IEnumerable<T>
    {
        private List<List<T>> lists = new List<List<T>>();

        /// <summary>The count of the objects in the list.</summary>
        public int ListCount => lists.Count;
        /// <summary>The count of the objects in the list.</summary>
        public int Count { get; private set; }

        /// <summary>Adds a new list containing only the specified object to the lists.</summary>
        /// <param name="item">The item to add to the lists.</param>
        public void Add(T item)
        {
            lists.Add(new List<T> { item });
            Count++;
        }
        /// <summary>Adds the specified list to the lists.</summary>
        /// <param name="items">The list of items to add to the lists.</param>
        public void Add(List<T> items)
        {
            lists.Add(new List<T>(items));
            Count += items.Count;
        }
        // Consider adding removal functions some other time

        /// <summary>Removes the specified number of lists from the start of the list.</summary>
        /// <param name="count">The number of lists to remove.</param>
        public void RemoveFirst(int count)
        {
            Count -= ElementCount(0, count);
            lists.RemoveRange(0, count);
        }

        /// <summary>Clears the lists.</summary>
        public void Clear()
        {
            lists.Clear();
            Count = 0;
        }

        /// <summary>Returns the count of elements in the list at the specified index.</summary>
        /// <param name="index">The index of the list whose count to retrieve.</param>
        public int ElementCount(int index) => lists[index].Count;
        /// <summary>Returns the count of elements in the specified range of lists.</summary>
        /// <param name="index">The index of the first list.</param>
        /// <param name="count">The total number of lists whose count to retrieve.</param>
        public int ElementCount(int index, int count)
        {
            int sum = 0;
            for (int i = 0; i < count; i++)
                sum += lists[index + i].Count;
            return sum;
        }

        /// <summary>Returns the element at the specified index.</summary>
        /// <param name="index">The index of the element to get. The index reflects the respective index in the unified list of this list of lists.</param>
        public T ElementAt(int index)
        {
            var l = GetListContainingIndex(index, out int startingIndex);
            return l[index - startingIndex];
        }
        /// <summary>Sets the element at the specified index.</summary>
        /// <param name="index">The index of the element to set. The index reflects the respective index in the unified list of this list of lists.</param>
        public void SetElementAt(int index, T value)
        {
            var l = GetListContainingIndex(index, out int startingIndex);
            l[index - startingIndex] = value;
        }

        private List<T> GetListContainingIndex(int index, out int startingIndex)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException("The provided index is not within the index of the array.");

            startingIndex = 0;
            foreach (var l in lists)
            {
                int nextStartingIndex = startingIndex + l.Count;
                if (startingIndex <= index && index < nextStartingIndex)
                    return l;
                startingIndex += l.Count;
            }

            throw new Exception(); // Sadly necessary line because compiler cannot understand that my magic code works:tm:
        }

        /// <summary>Gets or sets the list at the specified index.</summary>
        /// <param name="index">The index of the list to get or set.</param>
        public List<T> this[int index]
        {
            get => lists[index];
            set => lists[index] = value;
        }
        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <param name="index">The index of the element to get or set.</param>
        /// <param name="dummy">A dummy parameter to separate the indexer from the other one.</param>
        public T this[int index, bool dummy]
        {
            get => ElementAt(index);
            set => SetElementAt(index, value);
        }
        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <param name="listIndex">The index of the list whose element to get or set.</param>
        /// <param name="elementIndex">The index of the element in the specified list to get or set.</param>
        public T this[int listIndex, int elementIndex]
        {
            get => this[listIndex][elementIndex];
            set => this[listIndex][elementIndex] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var l in lists)
                foreach (var i in l)
                    yield return i;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
