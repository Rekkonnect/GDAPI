using GDAPI.Application.Editors.Delegates;

namespace GDAPI.Application.Editors.Actions.EditorActions
{
    /// <summary>Represents an action in the editor which can be undone and redone that also has an inverse function that will be called.</summary>
    public abstract class EditorActionWithInverse<T> : EditorAction<T>
    {
        /// <summary>The inverse action of the action.</summary>
        public FieldValueSetter<T> Inverse;

        /// <summary>Initiailizes a new instance of the <seealso cref="EditorActionWithInverse{T}"/> class.</summary>
        /// <param name="newValue">The new value that this action will set.</param>
        /// <param name="action">The action to be performed.</param>
        /// <param name="inverse">The inverse action of the action.</param>
        public EditorActionWithInverse(T newValue, FieldValueSetter<T> action, FieldValueSetter<T> inverse)
            : base(newValue, action)
        {
            Inverse = inverse;
        }

        /// <summary>Performs the action.</summary>
        public sealed override void PerformAction() => Action(NewValue, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public sealed override void PerformInverseAction() => Inverse(NewValue, false);
    }
}
