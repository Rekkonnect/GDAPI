using GDAPI.Application.Editors.Delegates;
using System;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action in the editor affecting the level which can be undone and redone containing a collection of affected objects.</summary>
    /// <typeparam name="TActionDelegate">The type of the delegate that represents the action.</typeparam>
    public abstract class LevelActionObjectsValueCommonInverse<TValue, TDelegate> : LevelActionObjectsValueInverse<TValue, TDelegate, TDelegate>
        where TDelegate : Delegate
    {
        /// <summary>Initiailizes a new instance of the <seealso cref="LevelActionObjectsValueCommonInverse{TValue, TDelegate}"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="inverse">The inverse action to be performed.</param>
        public LevelActionObjectsValueCommonInverse(LevelObjectCollection affectedObjects, TValue value, TDelegate action)
            : base(affectedObjects, value, action, action) { }

        /// <summary>Performs the action.</summary>
        public sealed override void PerformAction() => PerformCommonAction();
        /// <summary>Performs the inverse action of the editor action.</summary>
        public sealed override void PerformInverseAction() => PerformCommonAction();

        /// <summary>Performs the common action, which is the action that acts as both the normal and the inverse action.</summary>
        protected abstract void PerformCommonAction();
    }
}
