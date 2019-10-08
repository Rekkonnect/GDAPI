using GDAPI.Application.Editors.Delegates;
using System;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action in the editor affecting the level which can be undone and redone containing a collection of affected objects.</summary>
    /// <typeparam name="TActionDelegate">The type of the delegate that represents the action.</typeparam>
    /// <typeparam name="TInverseDelegate">The type of the delegate that represents the inverse action.</typeparam>
    public abstract class LevelActionNewOldObjectsInverse<TActionDelegate, TInverseDelegate> : LevelActionNewOldObjects<TActionDelegate>
        where TActionDelegate : Delegate
    {
        /// <summary>The inverse action of the level action.</summary>
        public readonly TInverseDelegate InverseAction;

        /// <summary>Initiailizes a new instance of the <seealso cref="LevelActionNewOldObjectsInverse{TActionDelegate, TInverseDelegate}"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="oldObjects">The objects before the action was performed that will be affected when undoing this action.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="inverse">The inverse action to be performed.</param>
        public LevelActionNewOldObjectsInverse(LevelObjectCollection affectedObjects, LevelObjectCollection oldObjects, TActionDelegate action, TInverseDelegate inverse)
            : base(affectedObjects, oldObjects, action)
        {
            InverseAction = inverse;
        }
    }
}
