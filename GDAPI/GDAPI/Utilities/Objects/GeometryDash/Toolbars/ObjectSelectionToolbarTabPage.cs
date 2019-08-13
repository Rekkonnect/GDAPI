using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.Toolbars
{
    /// <summary>Represents a page in the <seealso cref="ObjectSelectionToolbarTab"/>.</summary>
    public class ObjectSelectionToolbarTabPage
    {
        /// <summary>The items of the page.</summary>
        public List<ObjectSelectionToolbarTabItem> Items { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTabPage"/> class.</summary>
        public ObjectSelectionToolbarTabPage()
        {
            Items = new List<ObjectSelectionToolbarTabItem>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTabPage"/> class.</summary>
        /// <param name="items">The items this <seealso cref="ObjectSelectionToolbarTabPage"/> will have.</param>
        public ObjectSelectionToolbarTabPage(List<ObjectSelectionToolbarTabItem> items)
        {
            Items = items;
        }
    }
}
