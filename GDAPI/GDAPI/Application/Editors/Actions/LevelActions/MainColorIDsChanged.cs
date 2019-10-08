using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class MainColorIDsChanged : LevelActionObjectsOffsetResult<int>
    {
        /// <summary>Initializes a new instance of the <seealso cref="MainColorIDsChanged"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="offset">The offset to apply to the specified parameter of the affected objects.</param>
        /// <param name="resultingColorID">The resulting value of the main color ID.</param>
        /// <param name="action">The action to be performed.</param>
        public MainColorIDsChanged(LevelObjectCollection affectedObjects, int offset, int resultingColorID, ObjectPropertyOffsetSetter<int> action)
            : base(affectedObjects, offset, resultingColorID, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Set main color ID to {ResultingValue}";

        /// <summary>Gets the inverse offset based on the given offset.</summary>
        protected override int GetInverseOffset() => -Offset;
    }
}
