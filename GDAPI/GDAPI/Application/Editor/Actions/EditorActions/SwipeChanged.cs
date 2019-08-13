using GDAPI.Application.Editor.Delegates;

namespace GDAPI.Application.Editor.Actions.EditorActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class SwipeChanged : BoolEditorAction
    {
        /// <summary>Initializes a new instance of the <seealso cref="SwipeChanged"/> class.</summary>
        /// <param name="newValue">The new value that will be set to the Swipe property.</param>
        /// <param name="action">The action that will change the Swipe property.</param>
        public SwipeChanged(bool newValue, FieldValueSetter<bool> action)
            : base(newValue, action) { }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Set Swipe to {NewValue}";
    }
}
