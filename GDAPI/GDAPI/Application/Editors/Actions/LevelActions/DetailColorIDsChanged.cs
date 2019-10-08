using GDAPI.Application.Editors.Delegates;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class DetailColorIDsChanged : LevelActionObjectsOffsetResult<int>
    {
        /// <summary>Initializes a new instance of the <seealso cref="DetailColorIDsChanged"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="offset">The offset to apply to the specified parameter of the affected objects.</param>
        /// <param name="resultingColorID">The resulting value of the detail color ID.</param>
        /// <param name="action">The action to be performed.</param>
        public DetailColorIDsChanged(LevelObjectCollection affectedObjects, int offset, int resultingColorID, ObjectPropertyOffsetSetter<int> action)
            : base(affectedObjects, offset, resultingColorID, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Set detail color ID to {ResultingValue}";

        /// <summary>Gets the inverse offset based on the given offset.</summary>
        protected override int GetInverseOffset() => -Offset;
    }
}
