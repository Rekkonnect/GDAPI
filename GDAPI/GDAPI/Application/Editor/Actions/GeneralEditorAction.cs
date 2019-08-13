using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Application.Editor.Actions
{
    public abstract class GeneralEditorAction
    {
        /// <summary>The description of the action.</summary>
        public readonly string Description;

        /// <summary>Initiailizes a new instance of the <seealso cref="GeneralEditorAction"/> class.</summary>
        public GeneralEditorAction() => Description = GenerateDescription();

        /// <summary>Performs the action.</summary>
        public abstract void PerformAction();
        /// <summary>Performs the inverse action of the editor action.</summary>
        public abstract void PerformInverseAction();

        /// <summary>Undoes this action.</summary>
        public void Undo() => PerformInverseAction();
        /// <summary>Redoes this action.</summary>
        public void Redo() => PerformAction();

        /// <summary>Generates the description of the action.</summary>
        protected abstract string GenerateDescription();
    }
}
