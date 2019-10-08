using GDAPI.Application.Editors.Delegates;
using System.Collections.Generic;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class GroupIDsAdded : LevelActionObjectsValueInverse<IEnumerable<int>, ObjectPropertySetter<IEnumerable<int>>, ObjectPropertySetter<IEnumerable<int>>>
    {
        /// <summary>Initializes a new instance of the <seealso cref="GroupIDsAdded"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="action">The function that rotates the specified objects.</param>
        public GroupIDsAdded(LevelObjectCollection affectedObjects, IEnumerable<int> groupIDs, ObjectPropertySetter<IEnumerable<int>> action, ObjectPropertySetter<IEnumerable<int>> inverse)
            : base(affectedObjects, groupIDs, action, inverse) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Add group IDs to {ObjectCountStringRepresentation}";

        /// <summary>Performs the action.</summary>
        public override void PerformAction() => Action(AffectedObjects, Value, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public override void PerformInverseAction() => InverseAction(AffectedObjects, Value, false);
    }
}
