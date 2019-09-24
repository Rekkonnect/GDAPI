using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class SelectedObjectsRemoved : LevelActionObjectsOnlyInverse
    {
        /// <summary>Initializes a new instance of the <seealso cref="SelectedObjectsRemoved"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="deselectObjectsAction">The action that deselects the specified objects.</param>
        /// <param name="selectObjectsAction">The action that selects the specified objects.</param>
        public SelectedObjectsRemoved(LevelObjectCollection affectedObjects, ObjectsUndoableActionDelegate deselectObjectsAction, ObjectsUndoableActionDelegate selectObjectsAction)
            : base(affectedObjects, deselectObjectsAction, selectObjectsAction) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Remove {ObjectCountStringRepresentation} from selection";
    }
}
