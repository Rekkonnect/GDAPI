using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ObjectsScaled : LevelActionObjectsOffsetCentralPoint<double>
    {
        /// <summary>Initializes a new instance of the <seealso cref="ObjectsScaled"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="offset">The offset to apply to the specified parameter of the affected objects.</param>
        /// <param name="centralPoint">The central point that was taken into account while scaling the objects.</param>
        /// <param name="action">The function that scales the specified objects.</param>
        public ObjectsScaled(LevelObjectCollection affectedObjects, double offset, Point? centralPoint, ObjectPropertyOffsetCentralPointSetter<double> action)
            : base(affectedObjects, offset, centralPoint, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Scale {ObjectCountStringRepresentation} by {Offset}";

        /// <summary>Gets the inverse offset based on the given offset.</summary>
        protected override double GetInverseOffset() => 1 / Offset;
    }
}
