using GDAPI.Application.Editors.Delegates;
using System;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action in the editor affecting the level which can be undone and redone.</summary>
    /// <typeparam name="TDelegate">The type of the delegate that represents the action.</typeparam>
    public abstract class LevelAction<TDelegate> : GeneralEditorAction
        where TDelegate : Delegate
    {
        /// <summary>The level action.</summary>
        public TDelegate Action;

        /// <summary>Initiailizes a new instance of the <seealso cref="LevelAction{TDelegate}"/> class.</summary>
        /// <param name="action">The action to be performed.</param>
        public LevelAction(TDelegate action)
            : base()
        {
            Action = action;
        }
    }
}
