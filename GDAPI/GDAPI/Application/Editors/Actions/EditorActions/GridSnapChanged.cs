using GDAPI.Application.Editors.Delegates;

namespace GDAPI.Application.Editors.Actions.EditorActions
{
    /// <summary>Represents an action where the Grid Snap property of the editor has been changed.</summary>
    public class GridSnapChanged : BoolEditorAction
    {
        /// <summary>Initializes a new instance of the <seealso cref="GridSnapChanged"/> class.</summary>
        /// <param name="newValue">The new value that will be set to the Grid Snap property.</param>
        /// <param name="action">The action that will change the Grid Snap property.</param>
        public GridSnapChanged(bool newValue, FieldValueSetter<bool> action)
            : base(newValue, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Set Grid Snap to {NewValue}";
    }
}
