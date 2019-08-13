using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.Toolbars
{
    public class MultiObjectSelectionToolbarTabPage : ObjectSelectionToolbarTabPage, IMultiSelection
    {
        private List<int> selectedItemIndices;

        /// <summary>Gets or sets the indices of the currently selected items.</summary>
        public List<int> SelectedItemIndices
        {
            get => selectedItemIndices;
            set
            {
                foreach (var i in value)
                    if (i < 0 || i >= Items.Count)
                        throw new IndexOutOfRangeException();
                selectedItemIndices = value;
            }
        }
        /// <summary>Gets or sets the currently selected items.</summary>
        public List<ObjectSelectionToolbarTabItem> SelectedItems
        {
            get
            {
                var result = new List<ObjectSelectionToolbarTabItem>();
                foreach (var i in selectedItemIndices)
                    result.Add(Items[i]);
                return result;
            }
            set
            {
                var result = new List<int>();
                foreach (var s in value)
                    result.Add(Items.IndexOf(s));
                selectedItemIndices = result;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="MultiObjectSelectionToolbarTabPage"/> class.</summary>
        /// <param name="items">The items this <seealso cref="MultiObjectSelectionToolbarTabPage"/> will have.</param>
        public MultiObjectSelectionToolbarTabPage(List<ObjectSelectionToolbarTabItem> items) : base(items) { }

        /// <summary>Adds the selected index to the selected items list.</summary>
        /// <param name="index">The index of the item to add to the list.</param>
        public void AddIndex(int index)
        {
            if (!selectedItemIndices.Contains(index))
                selectedItemIndices.Add(index);
        }
        /// <summary>Adds the selected indices to the selected items list.</summary>
        /// <param name="indices">The indices of the items to add to the list.</param>
        public void AddIndices(List<int> indices)
        {
            foreach (int i in indices)
                if (!selectedItemIndices.Contains(i))
                    selectedItemIndices.Add(i);
        }
        /// <summary>Removes the selected index from the selected items list.</summary>
        /// <param name="index">The index to remove from the selected items list.</param>
        public void RemoveIndex(int index)
        {
            selectedItemIndices.Remove(index);
        }
        /// <summary>Removes the selected indices from the selected items list.</summary>
        /// <param name="indices">The indices to remove from the selected items list.</param>
        public void RemoveIndices(List<int> indices)
        {
            foreach (int i in indices)
                selectedItemIndices.Remove(i);
        }
    }
}
