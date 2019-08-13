using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.Toolbars
{
    public class MultiObjectSelectionToolbarTab : ObjectSelectionToolbarTab, IMultiSelection
    {
        private List<int> selectedPageIndices;

        /// <summary>Gets or sets the indices of the currently selected pages.</summary>
        public List<int> SelectedPageIndices
        {
            get => selectedPageIndices;
            set
            {
                foreach (var i in value)
                    if (i < 0 || i >= Pages.Count)
                        throw new IndexOutOfRangeException();
                selectedPageIndices = value;
            }
        }
        /// <summary>Gets or sets the currently selected pages.</summary>
        public List<ObjectSelectionToolbarTabPage> SelectedPages
        {
            get
            {
                var result = new List<ObjectSelectionToolbarTabPage>();
                foreach (var i in selectedPageIndices)
                    result.Add(Pages[i]);
                return result;
            }
            set
            {
                var result = new List<int>();
                foreach (var s in value)
                    result.Add(Pages.IndexOf(s));
                selectedPageIndices = result;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="MultiObjectSelectionToolbarTab"/> class.</summary>
        /// <param name="sampleImageObjectID">The object ID of the object whose image will be shown as the sample image in the tab.</param>
        /// <param name="pages">The pages this <seealso cref="MultiObjectSelectionToolbarTab"/> will have.</param>
        public MultiObjectSelectionToolbarTab(int sampleImageObjectID, List<ObjectSelectionToolbarTabPage> pages) : base(sampleImageObjectID, pages) { }

        /// <summary>Adds the selected index to the selected pages list.</summary>
        /// <param name="index">The index of the page to add to the list.</param>
        public void AddIndex(int index)
        {
            if (!selectedPageIndices.Contains(index))
                selectedPageIndices.Add(index);
        }
        /// <summary>Adds the selected indices to the selected pages list.</summary>
        /// <param name="indices">The indices of the pages to add to the list.</param>
        public void AddIndices(List<int> indices)
        {
            foreach (int i in indices)
                if (!selectedPageIndices.Contains(i))
                    selectedPageIndices.Add(i);
        }
        /// <summary>Removes the selected index from the selected pages list.</summary>
        /// <param name="index">The index to remove from the selected pages list.</param>
        public void RemoveIndex(int index)
        {
            selectedPageIndices.Remove(index);
        }
        /// <summary>Removes the selected indices from the selected pages list.</summary>
        /// <param name="indices">The indices to remove from the selected pages list.</param>
        public void RemoveIndices(List<int> indices)
        {
            foreach (int i in indices)
                selectedPageIndices.Remove(i);
        }
    }
}
