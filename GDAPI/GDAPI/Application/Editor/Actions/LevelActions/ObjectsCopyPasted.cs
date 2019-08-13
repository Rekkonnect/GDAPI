using GDAPI.Application.Editor.Delegates;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editor.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ObjectsCopyPasted : LevelActionNewOldObjectsInverse<ObjectsUndoableActionDelegate, OldNewObjectsHandler>
    {
        /// <summary>Initializes a new instance of the <seealso cref="ObjectsCopyPasted"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="oldObjects">The objects before the action was performed that will be affected when undoing this action.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="inverse">The inverse action to be performed.</param>
        public ObjectsCopyPasted(LevelObjectCollection affectedObjects, LevelObjectCollection oldObjects, ObjectsUndoableActionDelegate action, OldNewObjectsHandler inverse)
            : base(affectedObjects, oldObjects, action, inverse) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Copy-paste {ObjectCountStringRepresentation}";

        /// <summary>Performs the action.</summary>
        public override void PerformAction() => Action(AffectedObjects, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public override void PerformInverseAction() => InverseAction(AffectedObjects, OldObjects, false);
    }
}
