using GDAPI.Application.Editors.Delegates;

namespace GDAPI.Application.Editors.Actions.EditorActions
{
    /// <summary>Represents an action where the Dual Layer Mode property of the editor has been changed.</summary>
    public class DualLayerModeChanged : BoolEditorAction
    {
        /// <summary>Initializes a new instance of the <seealso cref="DualLayerModeChanged"/> class.</summary>
        /// <param name="newValue">The new value that will be set to the Dual Layer Mode property.</param>
        /// <param name="action">The action that will change the Dual Layer Mode property.</param>
        public DualLayerModeChanged(bool newValue, FieldValueSetter<bool> action)
            : base(newValue, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Set Dual Layer Mode to {NewValue}";
    }
}
