using GDAPI.Application.Editors.Delegates;

namespace GDAPI.Application.Editors.Actions.EditorActions
{
    /// <summary>Represents an action where the Free Move property of the editor has been changed.</summary>
    public class FreeMoveChanged : BoolEditorAction
    {
        /// <summary>Initializes a new instance of the <seealso cref="FreeMoveChanged"/> class.</summary>
        /// <param name="newValue">The new value that will be set to the Free Move property.</param>
        /// <param name="action">The action that will change the Free Move property.</param>
        public FreeMoveChanged(bool newValue, FieldValueSetter<bool> action)
            : base(newValue, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Set Free Move to {NewValue}";
    }
}
