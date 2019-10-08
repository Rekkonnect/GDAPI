using System.Drawing;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Objects.GeometryDash.Toolbars
{
    /// <summary>Represents an item in the <seealso cref="ObjectSelectionToolbarTabPage"/>.</summary>
    public class ObjectSelectionToolbarTabItem
    {
        /// <summary>The object that this item refers to, with all the properties it contains.</summary>
        public GeneralObject Object { get; set; }
        /// <summary>The background color of the tab item.</summary>
        public Color BackgroundColor { get; set; } = Color.Silver;

        /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTabItem"/> class.</summary>
        /// <param name="obj">The object of this <seealso cref="ObjectSelectionToolbarTabItem"/>.</param>
        public ObjectSelectionToolbarTabItem(GeneralObject obj)
        {
            Object = obj;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTabItem"/> class.</summary>
        /// <param name="obj">The object of this <seealso cref="ObjectSelectionToolbarTabItem"/>.</param>
        /// <param name="backgroundColor">The background color of this <seealso cref="ObjectSelectionToolbarTabItem"/>.</param>
        public ObjectSelectionToolbarTabItem(GeneralObject obj, Color backgroundColor)
        {
            Object = obj;
            BackgroundColor = backgroundColor;
        }
    }
}
