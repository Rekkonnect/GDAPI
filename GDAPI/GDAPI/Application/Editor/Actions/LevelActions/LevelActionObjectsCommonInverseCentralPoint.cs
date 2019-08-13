using GDAPI.Application.Editor.Delegates;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;
using System;

namespace GDAPI.Application.Editor.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public abstract class LevelActionObjectsCommonInverseCentralPoint : LevelActionObjectsCommonInverse<ObjectPropertyNullableCentralPointSetter>
    {
        /// <summary>The central point that was taken into account while performing the action.</summary>
        public readonly Point? CentralPoint;

        /// <summary>Initializes a new instance of the <seealso cref="LevelActionObjectsCommonInverseCentralPoint"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="centralPoint">The central point that was taken into account while performing the action.</param>
        /// <param name="action">The action to apply to the specified objects.</param>
        public LevelActionObjectsCommonInverseCentralPoint(LevelObjectCollection affectedObjects, Point? centralPoint, ObjectPropertyNullableCentralPointSetter action)
            : base(affectedObjects, action)
        {
            CentralPoint = centralPoint;
        }

        /// <summary>Performs the common action, which is the action that acts as both the normal and the inverse action.</summary>
        protected override void PerformCommonAction() => Action(AffectedObjects, CentralPoint, false);
    }
}
