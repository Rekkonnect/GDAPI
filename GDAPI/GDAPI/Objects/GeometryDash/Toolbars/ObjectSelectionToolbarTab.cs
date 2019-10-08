using System.Collections.Generic;

namespace GDAPI.Objects.GeometryDash.Toolbars
{
    /// <summary>Represents a tab in the <seealso cref="ObjectSelectionToolbar"/>.</summary>
    public class ObjectSelectionToolbarTab
    {
        /// <summary>The object ID of the object whose image will be shown as the sample image in the tab.</summary>
        public int SampleImageObjectID { get; set; }
        /// <summary>The pages of the tab.</summary>
        public List<ObjectSelectionToolbarTabPage> Pages { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTab"/> class.</summary>
        /// <param name="sampleImageObjectID">The object ID of the object whose image will be shown as the sample image in the tab.</param>
        public ObjectSelectionToolbarTab(int sampleImageObjectID)
        {
            SampleImageObjectID = sampleImageObjectID;
            Pages = new List<ObjectSelectionToolbarTabPage>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTab"/> class.</summary>
        /// <param name="sampleImageObjectID">The object ID of the object whose image will be shown as the sample image in the tab.</param>
        /// <param name="pages">The pages this <seealso cref="ObjectSelectionToolbarTab"/> will have.</param>
        public ObjectSelectionToolbarTab(int sampleImageObjectID, List<ObjectSelectionToolbarTabPage> pages)
        {
            SampleImageObjectID = sampleImageObjectID;
            Pages = pages;
        }
    }
}
