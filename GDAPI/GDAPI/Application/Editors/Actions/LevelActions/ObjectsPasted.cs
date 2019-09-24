using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ObjectsPasted : LevelActionObjectsInverse<ObjectPropertyCentralPointSetter, ObjectsUndoableActionDelegate>
    {
        /// <summary>The central point that was taken into account while performing the action.</summary>
        public readonly Point CentralPoint;

        /// <summary>Initializes a new instance of the <seealso cref="ObjectsPasted"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="centralPoint">The central point that was taken into account while performing the action.</param>
        /// <param name="action">The function that rotates the specified objects.</param>
        /// <param name="inverse">The inverse action to be performed.</param>
        public ObjectsPasted(LevelObjectCollection affectedObjects, Point centralPoint, ObjectPropertyCentralPointSetter action, ObjectsUndoableActionDelegate inverse)
            : base(affectedObjects, action, inverse)
        {
            CentralPoint = centralPoint;
        }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Paste {ObjectCountStringRepresentation}";

        /// <summary>Performs the action.</summary>
        public override void PerformAction() => Action(AffectedObjects, CentralPoint, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public override void PerformInverseAction() => InverseAction(AffectedObjects, false);
    }
}
