using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.Toolbars
{
    public class MultiObjectSelectionToolbar : ObjectSelectionToolbar, IMultiSelection
    {
        private List<int> selectedTabIndices;

        /// <summary>Gets or sets the indices of the currently selected tabs.</summary>
        public List<int> SelectedTabIndices
        {
            get => selectedTabIndices;
            set
            {
                foreach (var i in value)
                    if (i < 0 || i >= Tabs.Count)
                        throw new IndexOutOfRangeException();
                selectedTabIndices = value;
            }
        }
        /// <summary>Gets or sets the currently selected tabs.</summary>
        public List<ObjectSelectionToolbarTab> SelectedTabs
        {
            get
            {
                var result = new List<ObjectSelectionToolbarTab>();
                foreach (var i in selectedTabIndices)
                    result.Add(Tabs[i]);
                return result;
            }
            set
            {
                var result = new List<int>();
                foreach (var s in value)
                    result.Add(Tabs.IndexOf(s));
                selectedTabIndices = result;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="MultiObjectSelectionToolbar"/> class.</summary>
        /// <param name="tabs">The tabs this <seealso cref="MultiObjectSelectionToolbar"/> will have.</param>
        public MultiObjectSelectionToolbar(List<ObjectSelectionToolbarTab> tabs) : base(tabs) { }

        /// <summary>Adds the selected index to the selected tabs list.</summary>
        /// <param name="index">The index of the tab to add to the list.</param>
        public void AddIndex(int index)
        {
            if (!selectedTabIndices.Contains(index))
                selectedTabIndices.Add(index);
        }
        /// <summary>Adds the selected indices to the selected tabs list.</summary>
        /// <param name="indices">The indices of the tabs to add to the list.</param>
        public void AddIndices(List<int> indices)
        {
            foreach (int i in indices)
                if (!selectedTabIndices.Contains(i))
                    selectedTabIndices.Add(i);
        }
        /// <summary>Removes the selected index from the selected tabs list.</summary>
        /// <param name="index">The index to remove from the selected tabs list.</param>
        public void RemoveIndex(int index)
        {
            selectedTabIndices.Remove(index);
        }
        /// <summary>Removes the selected indices from the selected tabs list.</summary>
        /// <param name="indices">The indices to remove from the selected tabs list.</param>
        public void RemoveIndices(List<int> indices)
        {
            foreach (int i in indices)
                selectedTabIndices.Remove(i);
        }
    }
}
