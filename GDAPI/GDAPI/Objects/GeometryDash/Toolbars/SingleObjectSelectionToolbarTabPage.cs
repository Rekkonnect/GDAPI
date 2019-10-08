using System;
using System.Collections.Generic;

namespace GDAPI.Objects.GeometryDash.Toolbars
{
    public class SingleObjectSelectionToolbarTabPage : ObjectSelectionToolbarTabPage, ISingleSelection
    {
        private int selectedItemIndex;

        /// <summary>Gets or sets the index of the currently selected item.</summary>
        public int SelectedItemIndex
        {
            get => selectedItemIndex;
            set
            {
                if (selectedItemIndex < 0 || selectedItemIndex >= Items.Count)
                    throw new IndexOutOfRangeException();
                selectedItemIndex = value;
            }
        }
        /// <summary>Gets or sets the currently selected item.</summary>
        public ObjectSelectionToolbarTabItem SelectedItem
        {
            get => Items[selectedItemIndex];
            set
            {
                int index = Items.IndexOf(value);
                if (index < 0)
                    throw new Exception("The item is not in this toolbar.");
                selectedItemIndex = index;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="SingleObjectSelectionToolbarTabPage"/> class.</summary>
        /// <param name="sampleImageObjectID">The object ID of the object whose image will be shown as the sample image in the tab.</param>
        /// <param name="items">The items this <seealso cref="SingleObjectSelectionToolbarTabPage"/> will have.</param>
        public SingleObjectSelectionToolbarTabPage(List<ObjectSelectionToolbarTabItem> items) : base(items) { }
    }
}
