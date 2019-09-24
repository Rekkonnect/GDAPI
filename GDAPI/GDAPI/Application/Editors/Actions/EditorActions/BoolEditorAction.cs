using GDAPI.Application.Editors.Delegates;

namespace GDAPI.Application.Editors.Actions.EditorActions
{
    /// <summary>Represents an editor action that changes the value of a <see langword="bool"/> property.</summary>
    public abstract class BoolEditorAction : EditorAction<bool>
    {
        /// <summary>Initializes a new instance of the <seealso cref="BoolEditorAction"/> class.</summary>
        /// <param name="newValue">The new value that will be set to the property.</param>
        /// <param name="action">The action to perform.</param>
        public BoolEditorAction(bool newValue, FieldValueSetter<bool> action)
            : base(newValue, action) { }

        /// <summary>Performs the action.</summary>
        public override void PerformAction() => Action(NewValue, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public override void PerformInverseAction() => Action(!NewValue, false);
    }
}