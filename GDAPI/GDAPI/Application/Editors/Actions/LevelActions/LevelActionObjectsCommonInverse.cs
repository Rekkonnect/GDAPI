using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;
using System;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public abstract class LevelActionObjectsCommonInverse<TDelegate> : LevelActionWithObjects<TDelegate>
        where TDelegate : Delegate
    {
        /// <summary>Initializes a new instance of the <seealso cref="SelectedObjectsRemoved"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="action">The action to apply to the specified objects.</param>
        public LevelActionObjectsCommonInverse(LevelObjectCollection affectedObjects, TDelegate action)
            : base(affectedObjects, action) { }

        /// <summary>Performs the action.</summary>
        public sealed override void PerformAction() => PerformCommonAction();
        /// <summary>Performs the inverse action of the editor action.</summary>
        public sealed override void PerformInverseAction() => PerformCommonAction();

        /// <summary>Performs the common action, which is the action that acts as both the normal and the inverse action.</summary>
        protected abstract void PerformCommonAction();
    }
}
