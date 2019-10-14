using System;
using System.Collections;
using System.Collections.Generic;

namespace GDAPI.Objects.DataStructures
{
    /// <summary>Represents a sorted list of objects of type <seealso cref="T"/>.</summary>
    /// <typeparam name="T">The type of the objects to store in the list.</typeparam>
    public class SortedList<T> : IEnumerable<T>, ICollection<T>, IReadOnlyList<T>
        where T : IComparable<T>
    {
        private Comparison<T> comparison;
        private List<T> list;

        /// <summary>Determines whether this list is read-only. It is not, mind you.</summary>
        public bool IsReadOnly => false;

        /// <summary>The count of the objects in the list.</summary>
        public int Count => list.Count;

        /// <summary>Gets the minimum element in the list.</summary>
        public T Minimum => list[0];
        /// <summary>Gets the maximum element in the list.</summary>
        public T Maximum => list[list.Count - 1];

        /// <summary>Initializes a new instance of the <seealso cref="SortedList{T}"/> class using the default <seealso cref="Comparison{T}"/> for the element type.</summary>
        public SortedList()
            : this(DefaultComparison) { }
        /// <summary>Initializes a new instance of the <seealso cref="SortedList{T}"/> class with the specified capacity using the default <seealso cref="Comparison{T}"/> for the element type.</summary>
        /// <param name="capacity">The default capacity to use in the list.</param>
        public SortedList(int capacity)
            : this(capacity, DefaultComparison) { }
        /// <summary>Initializes a new instance of the <seealso cref="SortedList{T}"/> class from the specified element collcetion using the default <seealso cref="Comparison{T}"/> for the element type.</summary>
        /// <param name="elements">The elements to initialize the list with.</param>
        public SortedList(IEnumerable<T> elements)
            : this(elements, DefaultComparison) { }
        /// <summary>Initializes a new instance of the <seealso cref="SortedList{T}"/> class using a custom <seealso cref="Comparison{T}"/> for the element type.</summary>
        /// <param name="customComparison">The <seealso cref="Comparison{T}"/> to use when comparing the elements.</param>
        public SortedList(Comparison<T> customComparison)
        {
            comparison = customComparison;
            list = new List<T>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="SortedList{T}"/> class with the specified capacity using a custom <seealso cref="Comparison{T}"/> for the element type.</summary>
        /// <param name="capacity">The default capacity to use in the list.</param>
        /// <param name="customComparison">The <seealso cref="Comparison{T}"/> to use when comparing the elements.</param>
        public SortedList(int capacity, Comparison<T> customComparison)
        {
            comparison = customComparison;
            list = new List<T>(capacity);
        }
        /// <summary>Initializes a new instance of the <seealso cref="SortedList{T}"/> class from the specified element collcetion using a custom <seealso cref="Comparison{T}"/> for the element type.</summary>
        /// <param name="elements">The elements to initialize the list with.</param>
        /// <param name="customComparison">The <seealso cref="Comparison{T}"/> to use when comparing the elements.</param>
        public SortedList(IEnumerable<T> elements, Comparison<T> customComparison)
        {
            comparison = customComparison;
            list = new List<T>(elements);
            list.Sort(comparison);
        }

        /// <summary>Adds an element to the list.</summary>
        /// <param name="element">The element to add to the list.</param>
        public void Add(T element)
        {
            list.Insert(IndexToInsert(element), element);
        }
        /// <summary>Adds the specified collection of elements to the list.</summary>
        /// <param name="elements">The collection of elements to add to the list.</param>
        public void Add(IEnumerable<T> elements)
        {
            list.AddRange(elements);
            list.Sort(comparison);
        }
        /// <summary>Adds an element to the list and returns the index at which the element was added. The reason why this is not named "Add" is because this implements the <seealso cref="ICollection{T}"/>, which defines <code><seealso cref="void"/> <seealso cref="ICollection{T}.Add(T)"/></code></summary>
        /// <param name="element">The element to add to the list.</param>
        public int Insert(T element)
        {
            int index = IndexToInsert(element);
            list.Insert(index, element);
            return index;
        }

        /// <summary>Removes the specified element from the sorted list using binary search to identify its index. Returns whether the element existed in the list and was successfully removed.</summary>
        /// <param name="element">The element to remove from the list.</param>
        public bool Remove(T element)
        {
            int index = BinarySearch(element);
            bool result = index > -1;
            if (result)
                list.RemoveAt(index);
            return result;
        }
        /// <summary>Removes the element at the specified index from the sorted list.</summary>
        /// <param name="index">The index of the element to remove from the list.</param>
        public void RemoveAt(int index) => list.RemoveAt(index);
        /// <summary>Removes the specified element from the sorted list if it's not at the specified index using binary search to identify its index, returning a <seealso cref="bool"/> indicating whether the element was removed.</summary>
        /// <param name="element">The element to remove from the list.</param>
        /// <param name="index">The index at which the element should not be located to remove it.</param>
        /// <param name="foundIndex">The index at which the element was found.</param>
        public bool RemoveIfNotAtIndex(T element, int index, out int foundIndex)
        {
            foundIndex = BinarySearch(element);
            if (foundIndex > -1 && foundIndex != index)
            {
                list.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>Copies this list into an array.</summary>
        /// <param name="array">The array to copy this list to.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        /// <summary>Determines whether this sorted contains the specified element, which is determined by the index returned from the binary search.</summary>
        /// <param name="element">The element to search for in the list to determine whether it is contained.</param>
        public bool Contains(T element) => BinarySearch(element) > -1;

        /// <summary>Clears the list.</summary>
        public void Clear() => list.Clear();
        /// <summary>Returns a new <seealso cref="SortedList{T}"/> which contains this <seealso cref="SortedList{T}"/>'s distinct elements.</summary>
        public SortedList<T> RemoveDuplicates()
        {
            var result = new SortedList<T>(Count, comparison);
            result.list.Add(list[0]);
            for (int i = 1; i < Count; i++)
                if (result.Maximum.CompareTo(list[i]) < 0)
                    result.list.Add(list[i]);
            return result;
        }

        /// <summary>Performs binary search in the sorted list using the current sorting <seealso cref="Comparison{T}"/> and returns the index of the element if found, otherwise -1.</summary>
        /// <param name="element">The element to search for in the list.</param>
        public int BinarySearch(T element) => BinarySearch(element, comparison);
        /// <summary>Performs binary search in the sorted list using the current sorting <seealso cref="Comparison{T}"/> and returns the index that the element would have, if added to the sorted list.</summary>
        /// <param name="element">The element whose appropriate index to insert at.</param>
        public int IndexToInsert(T element) => IndexToInsert(element, comparison);
        /// <summary>Performs binary search in the sorted list using a custom <seealso cref="Comparison{T}"/> and returns the index of the element if found, otherwise -1.</summary>
        /// <param name="element">The element to search for in the list.</param>
        /// <param name="customComparison">A custom <seealso cref="Comparison{T}"/> to use for the binary search in the list. This does not affect the way the elements are sorted.</param>
        public int BinarySearch(T element, Comparison<T> customComparison)
        {
            int min = 0;
            int max = Count - 1;
            int mid;
            while (min <= max)
            {
                mid = (min + max) / 2;
                int comparisonResult = customComparison(element, list[mid]);

                if (comparisonResult == 0) // element == list[mid]
                    return mid;
                if (comparisonResult < 0) // element < list[mid]
                    max = mid - 1;
                else // element > list[mid]
                    min = mid + 1;
            }
            return -1;
        }
        /// <summary>Performs binary search in the sorted list using a custom <seealso cref="Comparison{T}"/> and returns the index that the element would have, if added to the sorted list.</summary>
        /// <param name="element">The element whose appropriate index to insert at.</param>
        /// <param name="customComparison">A custom <seealso cref="Comparison{T}"/> to use for the binary search in the list. This does not affect the way the elements are sorted.</param>
        public int IndexToInsert(T element, Comparison<T> customComparison)
        {
            if (Count == 0)
                return 0;

            int min = 0;
            int max = Count - 1;
            int mid = (min + max) / 2;
            while (min <= max)
            {
                int comparisonResult = customComparison(element, list[mid]);

                if (comparisonResult < 0) // element < list[mid]
                {
                    if (min == mid)
                        return min;
                    max = mid - 1;
                }
                else // element >= list[mid]
                {
                    if (max == mid)
                        return max + 1;
                    min = mid + 1;
                }

                mid = (min + max) / 2;
            }
            // Normally, this should never happen, however the compiler complains, therefore at least insert the element at a "reasonable" place
            return Count;
        }
        /// <summary>Performs binary search in the sorted list using the current sorting <seealso cref="Comparison{T}"/> and returns the index of the element that would be before the specified one, if contained in the sorted list.</summary>
        /// <param name="element">The element whose sequential previous to get the index of.</param>
        public int IndexBefore(T element) => IndexToInsert(element) - 1;
        /// <summary>Performs binary search in the sorted list using the current sorting <seealso cref="Comparison{T}"/> and returns the index of the element that would be after the specified one, if contained in the sorted list.</summary>
        /// <param name="element">The element whose sequential next to get the index of.</param>
        public int IndexAfter(T element) => IndexToInsert(element);
        /// <summary>Performs binary search in the sorted list using the current sorting <seealso cref="Comparison{T}"/> and returns the element that would be before the specified one, if contained in the sorted list.</summary>
        /// <param name="element">The element whose sequential previous to get.</param>
        public T ElementBefore(T element) => list[IndexBefore(element)];
        /// <summary>Performs binary search in the sorted list using the current sorting <seealso cref="Comparison{T}"/> and returns the element that would be after the specified one, if contained in the sorted list.</summary>
        /// <param name="element">The element whose sequential next to get.</param>
        public T ElementAfter(T element) => list[IndexAfter(element)];
        /// <summary>Performs binary search in the sorted list using a custom <seealso cref="Comparison{T}"/> and returns the index of the element that would be before the specified one, if contained in the sorted list.</summary>
        /// <param name="element">The element whose sequential previous to get the index of.</param>
        /// <param name="customComparison">A custom <seealso cref="Comparison{T}"/> to use for the binary search in the list. This does not affect the way the elements are sorted.</param>
        public int IndexBefore(T element, Comparison<T> customComparison) => IndexToInsert(element, customComparison) - 1;
        /// <summary>Performs binary search in the sorted list using a custom <seealso cref="Comparison{T}"/> and returns the index of the element that would be after the specified one, if contained in the sorted list.</summary>
        /// <param name="element">The element whose sequential next to get the index of.</param>
        /// <param name="customComparison">A custom <seealso cref="Comparison{T}"/> to use for the binary search in the list. This does not affect the way the elements are sorted.</param>
        public int IndexAfter(T element, Comparison<T> customComparison) => IndexToInsert(element, customComparison) + 1;
        /// <summary>Performs binary search in the sorted list using a custom <seealso cref="Comparison{T}"/> and returns the element that would be before the specified one, if contained in the sorted list.</summary>
        /// <param name="element">The element whose sequential previous to get.</param>
        /// <param name="customComparison">A custom <seealso cref="Comparison{T}"/> to use for the binary search in the list. This does not affect the way the elements are sorted.</param>
        public T ElementBefore(T element, Comparison<T> customComparison) => list[IndexBefore(element, customComparison)];
        /// <summary>Performs binary search in the sorted list using a custom <seealso cref="Comparison{T}"/> and returns the element that would be after the specified one, if contained in the sorted list.</summary>
        /// <param name="element">The element whose sequential next to get.</param>
        /// <param name="customComparison">A custom <seealso cref="Comparison{T}"/> to use for the binary search in the list. This does not affect the way the elements are sorted.</param>
        public T ElementAfter(T element, Comparison<T> customComparison) => list[IndexAfter(element, customComparison)];

        /// <summary>Returns the element at the specified index.</summary>
        /// <param name="index">The index of the element to get.</param>
        public T ElementAt(int index) => list[index];
        /// <summary>Sets the element at the specified index.</summary>
        /// <param name="index">The index of the element to set.</param>
        public void SetElementAt(int index, T value) => list[index] = value;

        /// <summary>Gets the element at the specified index.</summary>
        /// <param name="index">The index of the element to get.</param>
        public T this[int index] => list[index];

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static int DefaultComparison(T left, T right) => left.CompareTo(right);
    }
}
