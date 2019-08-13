using GDAPI.Application.Editor.Delegates;

namespace GDAPI.Application.Editor.Actions.EditorActions
{
    /// <summary>Represents an action where the Free Move property of the editor has been changed.</summary>
    public class ZoomChanged : DoubleEditorAction
    {
        /// <summary>Initializes a new instance of the <seealso cref="ZoomChanged"/> class.</summary>
        /// <param name="newValue">The new value that will be set to the Zoom property.</param>
        /// <param name="oldValue">The old value of the Zoom property before the action was performed.</param>
        /// <param name="action">The action that will change the Free Move property.</param>
        public ZoomChanged(double newValue, double oldValue, FieldValueSetter<double> action)
            : base(newValue, oldValue, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Set Zoom to {NewValue}";
    }
}
