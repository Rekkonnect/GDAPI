using GDAPI.Application.Editor.Delegates;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editor.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ObjectsCopied : LevelActionNewOldObjects<ObjectsUndoableActionDelegate>
    {
        /// <summary>Initializes a new instance of the <seealso cref="ObjectsCopied"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="oldObjects">The objects before the action was performed that will be affected when undoing this action.</param>
        /// <param name="action">The function that rotates the specified objects.</param>
        public ObjectsCopied(LevelObjectCollection affectedObjects, LevelObjectCollection oldObjects, ObjectsUndoableActionDelegate action)
            : base(affectedObjects, oldObjects, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Copy {ObjectCountStringRepresentation}";

        /// <summary>Performs the action.</summary>
        public override void PerformAction() => Action(AffectedObjects, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public override void PerformInverseAction() => Action(OldObjects, false);
    }
}
