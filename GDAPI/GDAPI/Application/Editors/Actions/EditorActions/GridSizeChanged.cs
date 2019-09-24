using GDAPI.Application.Editors.Delegates;

namespace GDAPI.Application.Editors.Actions.EditorActions
{
    /// <summary>Represents an action where the Free Move property of the editor has been changed.</summary>
    public class GridSizeChanged : DoubleEditorAction
    {
        /// <summary>Initializes a new instance of the <seealso cref="GridSizeChanged"/> class.</summary>
        /// <param name="newValue">The new value that will be set to the Grid Size property.</param>
        /// <param name="oldValue">The old value of the Grid Size property before the action was performed.</param>
        /// <param name="action">The action that will change the Free Move property.</param>
        public GridSizeChanged(double newValue, double oldValue, FieldValueSetter<double> action)
            : base(newValue, oldValue, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Set Grid Size to {NewValue}";
    }
}
