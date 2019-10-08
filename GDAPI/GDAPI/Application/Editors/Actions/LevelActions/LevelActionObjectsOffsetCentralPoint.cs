using GDAPI.Application.Editors.Delegates;
using System;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action in the editor affecting the level which can be undone and redone containing a collection of affected objects whose properties are adjusted with an offset.</summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public abstract class LevelActionObjectsOffsetCentralPoint<TValue> : LevelActionObjectsOffset<TValue, ObjectPropertyOffsetCentralPointSetter<TValue>>
    {
        /// <summary>The central point that was taken into account while performing the action.</summary>
        public readonly Point? CentralPoint;

        /// <summary>Initiailizes a new instance of the <seealso cref="LevelActionObjectsOffsetCentralPoint{TValue}"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="offset">The offset to apply to the specified parameter of the affected objects.</param>
        /// <param name="centralPoint">The central point that was taken into account while performing the action.</param>
        /// <param name="action">The action to be performed.</param>
        public LevelActionObjectsOffsetCentralPoint(LevelObjectCollection affectedObjects, TValue offset, Point? centralPoint, ObjectPropertyOffsetCentralPointSetter<TValue> action)
            : base(affectedObjects, offset, action) { }

        /// <summary>Performs the action.</summary>
        public override void PerformAction() => Action(AffectedObjects, Offset, CentralPoint, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public override void PerformInverseAction() => Action(AffectedObjects, InverseOffset, CentralPoint, false);
    }
}
