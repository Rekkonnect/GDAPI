using GDAPI.Application.Editors.Delegates;

namespace GDAPI.Application.Editors.Actions.EditorActions
{
    /// <summary>Represents an action in the editor which can be undone and redone.</summary>
    /// <typeparam name="T">The type of the value that will be changed.</typeparam>
    public abstract class EditorAction<T> : GeneralEditorAction
    {
        /// <summary>The editor action.</summary>
        public FieldValueSetter<T> Action;

        /// <summary>The new value of the property after the action was performed.</summary>
        public readonly T NewValue;

        /// <summary>Initiailizes a new instance of the <seealso cref="EditorAction{T}"/> class.</summary>
        /// <param name="newValue">The new value that this action will set.</param>
        /// <param name="action">The action to be performed.</param>
        public EditorAction(T newValue, FieldValueSetter<T> action)
            : base()
        {
            NewValue = newValue;
            Action = action;
        }
    }
}
