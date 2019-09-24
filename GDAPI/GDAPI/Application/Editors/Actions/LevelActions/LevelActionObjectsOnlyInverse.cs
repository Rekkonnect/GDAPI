using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public abstract class LevelActionObjectsOnlyInverse : LevelActionObjectsInverse<ObjectsUndoableActionDelegate, ObjectsUndoableActionDelegate>
    {
        /// <summary>Initializes a new instance of the <seealso cref="SelectedObjectsRemoved"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="action">The action that deselects the specified objects.</param>
        /// <param name="inverse">The action that selects the specified objects.</param>
        public LevelActionObjectsOnlyInverse(LevelObjectCollection affectedObjects, ObjectsUndoableActionDelegate action, ObjectsUndoableActionDelegate inverse)
            : base(affectedObjects, action, inverse) { }

        /// <summary>Performs the action.</summary>
        public sealed override void PerformAction() => Action(AffectedObjects, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public sealed override void PerformInverseAction() => InverseAction(AffectedObjects, false);
    }
}
