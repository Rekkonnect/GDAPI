using GDAPI.Application.Editor.Delegates;

namespace GDAPI.Application.Editor.Actions.EditorActions
{
    /// <summary>Represents an editor action that changes the value of a <see langword="double"/> property.</summary>
    public abstract class DoubleEditorAction : EditorActionNewOldValue<double>
    {
        /// <summary>Initiailizes a new instance of the <seealso cref="EditorAction{T}"/> class.</summary>
        /// <param name="newValue">The new value that this action will set.</param>
        /// <param name="oldValue">The old value of the property before the action was performed.</param>
        /// <param name="action">The action to be performed</param>
        public DoubleEditorAction(double newValue, double oldValue, FieldValueSetter<double> action)
            : base(newValue, oldValue, action) { }
    }
}