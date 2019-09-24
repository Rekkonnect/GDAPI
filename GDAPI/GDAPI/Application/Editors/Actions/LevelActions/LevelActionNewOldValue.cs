using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;
using System;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action in the editor which can be undone and redone containing the object collection after the action and the collection prior to it.</summary>
    /// <typeparam name="TDelegate">The type of the delegate that represents the action.</typeparam>
    public abstract class LevelActionNewOldValue<TValue, TDelegate> : LevelActionWithValue<TValue, TDelegate>
        where TDelegate : Delegate
    {
        /// <summary>The objects before the action was performed that will be affected when undoing this action.</summary>
        public TValue OldValue { get; set; }

        /// <summary>Initiailizes a new instance of the <seealso cref="LevelActionNewOldValue{TValue, TDelegate}"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="oldValue">The objects before the action was performed that will be affected when undoing this action.</param>
        /// <param name="action">The action to be performed.</param>
        public LevelActionNewOldValue(TValue newValue, TValue oldValue, TDelegate action)
            : base(newValue, action)
        {
            OldValue = oldValue;
        }
    }
}
