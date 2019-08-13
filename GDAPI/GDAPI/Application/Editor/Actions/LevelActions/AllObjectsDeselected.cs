using GDAPI.Application.Editor.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editor.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class AllObjectsDeselected : LevelActionObjectsInverse<ObjectsUndoableActionDelegate, UndoableActionDelegate>
    {
        /// <summary>Initializes a new instance of the <seealso cref="SelectedObjectsRemoved"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="deselectObjectsAction">The action that deselects all the objects.</param>
        /// <param name="selectObjectsAction">The action that selects the previously selected objects.</param>
        public AllObjectsDeselected(LevelObjectCollection affectedObjects, UndoableActionDelegate deselectObjectsAction, ObjectsUndoableActionDelegate selectObjectsAction)
            : base(affectedObjects, selectObjectsAction, deselectObjectsAction) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Clear selection";

        /// <summary>Performs the action.</summary>
        public sealed override void PerformAction() => Action(AffectedObjects, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public sealed override void PerformInverseAction() => InverseAction(false);
    }
}
