using System;
using System.Collections.Generic;

namespace GDAPI.Objects.GeometryDash.Toolbars
{
    public class SingleObjectSelectionToolbarTab : ObjectSelectionToolbarTab, ISingleSelection
    {
        private int selectedPageIndex;

        /// <summary>Gets or sets the index of the currently selected page.</summary>
        public int SelectedPageIndex
        {
            get => selectedPageIndex;
            set
            {
                if (selectedPageIndex < 0 || selectedPageIndex >= Pages.Count)
                    throw new IndexOutOfRangeException();
                selectedPageIndex = value;
            }
        }
        /// <summary>Gets or sets the currently selected page.</summary>
        public ObjectSelectionToolbarTabPage SelectedPage
        {
            get => Pages[selectedPageIndex];
            set
            {
                int index = Pages.IndexOf(value);
                if (index < 0)
                    throw new Exception("The page is not in this toolbar.");
                selectedPageIndex = index;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="SingleObjectSelectionToolbarTab"/> class.</summary>
        /// <param name="sampleImageObjectID">The object ID of the object whose image will be shown as the sample image in the tab.</param>
        /// <param name="pages">The pages this <seealso cref="SingleObjectSelectionToolbarTab"/> will have.</param>
        public SingleObjectSelectionToolbarTab(int sampleImageObjectID, List<ObjectSelectionToolbarTabPage> pages) : base(sampleImageObjectID, pages) { }
    }
}
