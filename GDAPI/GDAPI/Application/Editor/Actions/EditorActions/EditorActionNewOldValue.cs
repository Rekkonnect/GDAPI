using GDAPI.Application.Editor.Delegates;

namespace GDAPI.Application.Editor.Actions.EditorActions
{
    /// <summary>Represents an action in the editor which can be undone and redone.</summary>
    /// <typeparam name="T">The type of the value that will be changed.</typeparam>
    public abstract class EditorActionNewOldValue<T> : EditorAction<T>
    {
        /// <summary>The old value of the property before the action was performed.</summary>
        public T OldValue { get; set; }

        /// <summary>Initiailizes a new instance of the <seealso cref="EditorActionNewOldValue{T}"/> class.</summary>
        /// <param name="newValue">The new value that this action will set.</param>
        /// <param name="oldValue">The old value of the property before the action was performed.</param>
        /// <param name="action">The action to be performed.</param>
        public EditorActionNewOldValue(T newValue, T oldValue, FieldValueSetter<T> action)
            : base(newValue, action)
        {
            OldValue = oldValue;
        }

        /// <summary>Performs the action.</summary>
        public sealed override void PerformAction() => Action(NewValue, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public sealed override void PerformInverseAction() => Action(OldValue, false);
    }
}
