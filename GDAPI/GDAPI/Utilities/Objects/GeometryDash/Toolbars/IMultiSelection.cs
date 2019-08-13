using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.Toolbars
{
    /// <summary>Represents a multi-selection toolbar element.</summary>
    public interface IMultiSelection
    {
        /// <summary>Adds the selected index to the selected elements list.</summary>
        /// <param name="index">The index of the element to add to the list.</param>
        void AddIndex(int index);
        /// <summary>Adds the selected indices to the selected elements list.</summary>
        /// <param name="indices">The indices of the elements to add to the list.</param>
        void AddIndices(List<int> indices);
        /// <summary>Removes the selected index from the selected elements list.</summary>
        /// <param name="index">The index to remove from the selected elements list.</param>
        void RemoveIndex(int index);
        /// <summary>Removes the selected indices from the selected elements list.</summary>
        /// <param name="indices">The indices to remove from the selected elements list.</param>
        void RemoveIndices(List<int> indices);
    }
}
