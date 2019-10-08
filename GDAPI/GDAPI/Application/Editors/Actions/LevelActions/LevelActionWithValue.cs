using GDAPI.Application.Editors.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;
using System;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action in the editor affecting the level which can be undone and redone containing a value that was set to the property.</summary>
    /// <typeparam name="TValue">The type of the value that will be changed.</typeparam>
    /// <typeparam name="TDelegate">The type of the delegate that represents the action.</typeparam>
    public abstract class LevelActionWithValue<TValue, TDelegate> : LevelAction<TDelegate>
        where TDelegate : Delegate
    {
        /// <summary>The new value of the property after the action was performed.</summary>
        public readonly TValue NewValue;

        /// <summary>Initiailizes a new instance of the <seealso cref="LevelActionWithValue{TValue, TDelegate}"/> class.</summary>
        /// <param name="newValue">The new value that this action will set.</param>
        /// <param name="action">The action to be performed.</param>
        public LevelActionWithValue(TValue newValue, TDelegate action)
            : base(action)
        {
            NewValue = newValue;
        }
    }
}
