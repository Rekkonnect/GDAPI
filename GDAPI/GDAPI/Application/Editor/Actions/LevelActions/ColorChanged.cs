using GDAPI.Application.Editor.Delegates;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editor.Actions.LevelActions
{
    /// <summary>Represents an action where the Swipe property of the editor has been changed.</summary>
    public class ColorChanged : LevelActionNewOldValue<Color, FieldValueSetter<int, Color>>
    {
        /// <summary>The color ID that was changed.</summary>
        public int ColorID { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ColorChanged"/> class.</summary>
        /// <param name="colorID">The color ID that was changed.</param>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="createObjectsAction">The action that creates the specified objects.</param>
        /// <param name="changeColor">The action that deletes the created objects.</param>
        public ColorChanged(int colorID, Color newColor, Color oldColor, FieldValueSetter<int, Color> changeColor)
            : base(newColor, oldColor, changeColor)
        {
            ColorID = ColorID;
        }

        /// <summary>Generates the description of the action.</summary>
        protected override string GenerateDescription() => $"Change color ID {ColorID}";

        /// <summary>Performs the action.</summary>
        public override void PerformAction() => Action(ColorID, NewValue, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public override void PerformInverseAction() => Action(ColorID, NewValue, false);
    }
}
