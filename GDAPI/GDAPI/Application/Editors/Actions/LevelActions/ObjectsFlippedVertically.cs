using GDAPI.Application.Editors.Delegates;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ObjectsFlippedVertically : LevelActionObjectsCommonInverseCentralPoint
    {
        /// <summary>Initializes a new instance of the <seealso cref="ObjectsFlippedVertically"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="centralPoint">The central point that was taken into account while flipping the objects vertically.</param>
        /// <param name="action">The function that rotates the specified objects.</param>
        public ObjectsFlippedVertically(LevelObjectCollection affectedObjects, Point? centralPoint, ObjectPropertyNullableCentralPointSetter action)
            : base(affectedObjects, centralPoint, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Flip {ObjectCountStringRepresentation} vertically";
    }
}
