using GDAPI.Application.Editor.Delegates;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editor.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ObjectsFlippedHorizontally : LevelActionObjectsCommonInverseCentralPoint
    {
        /// <summary>Initializes a new instance of the <seealso cref="ObjectsFlippedHorizontally"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="centralPoint">The central point that was taken into account while flipping the objects horizontally.</param>
        /// <param name="action">The function that rotates the specified objects.</param>
        public ObjectsFlippedHorizontally(LevelObjectCollection affectedObjects, Point? centralPoint, ObjectPropertyNullableCentralPointSetter action)
            : base(affectedObjects, centralPoint, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Flip {ObjectCountStringRepresentation} horizontally";
    }
}
