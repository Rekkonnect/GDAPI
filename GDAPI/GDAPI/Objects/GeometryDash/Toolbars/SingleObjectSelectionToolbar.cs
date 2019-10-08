using System;
using System.Collections.Generic;

namespace GDAPI.Objects.GeometryDash.Toolbars
{
    public class SingleObjectSelectionToolbar : ObjectSelectionToolbar, ISingleSelection
    {
        private int selectedTabIndex;

        /// <summary>Gets or sets the index of the currently selected tab.</summary>
        public int SelectedTabIndex
        {
            get => selectedTabIndex;
            set
            {
                if (selectedTabIndex < 0 || selectedTabIndex >= Tabs.Count)
                    throw new IndexOutOfRangeException();
                selectedTabIndex = value;
            }
        }
        /// <summary>Gets or sets the currently selected tab.</summary>
        public ObjectSelectionToolbarTab SelectedTab
        {
            get => Tabs[selectedTabIndex];
            set
            {
                int index = Tabs.IndexOf(value);
                if (index < 0)
                    throw new Exception("The tab is not in this toolbar.");
                selectedTabIndex = index;
            }
        }
        
        /// <summary>Initializes a new instance of the <seealso cref="SingleObjectSelectionToolbar"/> class.</summary>
        /// <param name="tabs">The tabs this <seealso cref="SingleObjectSelectionToolbar"/> will have.</param>
        public SingleObjectSelectionToolbar(List<ObjectSelectionToolbarTab> tabs) : base(tabs) { }
    }
}
