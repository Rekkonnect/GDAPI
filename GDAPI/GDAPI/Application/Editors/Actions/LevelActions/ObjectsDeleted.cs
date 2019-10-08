using GDAPI.Application.Editors.Delegates;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ObjectsDeleted : LevelActionObjectsOnlyInverse
    {
        /// <summary>Initializes a new instance of the <seealso cref="ObjectsDeleted"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="deleteObjectsAction">The action that deletes the selected objects.</param>
        /// <param name="createObjectsAction">The action that creates the deleted objects.</param>
        public ObjectsDeleted(LevelObjectCollection affectedObjects, ObjectsUndoableActionDelegate deleteObjectsAction, ObjectsUndoableActionDelegate createObjectsAction)
            : base(affectedObjects, createObjectsAction, deleteObjectsAction) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Remove {ObjectCountStringRepresentation}";
    }
}
