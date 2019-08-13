using GDAPI.Application.Editor.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editor.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class SelectedObjectsAdded : LevelActionObjectsOnlyInverse
    {
        /// <summary>Initializes a new instance of the <seealso cref="SelectedObjectsAdded"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="selectObjectsAction">The action that selects the specified objects.</param>
        /// <param name="deselectObjectsAction">The action that deselects the specified objects.</param>
        public SelectedObjectsAdded(LevelObjectCollection affectedObjects, ObjectsUndoableActionDelegate selectObjectsAction, ObjectsUndoableActionDelegate deselectObjectsAction)
            : base(affectedObjects, selectObjectsAction, deselectObjectsAction) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Add {ObjectCountStringRepresentation} to selection";
    }
}
