using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ObjectsCreated : LevelActionObjectsOnlyInverse
    {
        /// <summary>Initializes a new instance of the <seealso cref="ObjectsCreated"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="createObjectsAction">The action that creates the specified objects.</param>
        /// <param name="deleteObjectsAction">The action that deletes the created objects.</param>
        public ObjectsCreated(LevelObjectCollection affectedObjects, ObjectsUndoableActionDelegate deleteObjectsAction, ObjectsUndoableActionDelegate createObjectsAction)
            : base(affectedObjects, createObjectsAction, deleteObjectsAction) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Remove {ObjectCountStringRepresentation}";
    }
}
