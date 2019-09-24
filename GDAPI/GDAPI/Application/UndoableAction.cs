using GDAPI.Application.Editors.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Application
{
    /// <summary>Contains information about an action that can be undone.</summary>
    public class UndoableAction
    {
        private readonly List<GeneralEditorAction> editorActions = new List<GeneralEditorAction>();

        /// <summary>The description of the undoable action.</summary>
        public string Description { get; set; }

        /// <summary>Gets the count of actions that are registered in this undoable action.</summary>
        public int Count => editorActions.Count;

        /// <summary>Initializes a new instance of the <seealso cref="UndoableAction"/> class.</summary>
        /// <param name="description">The description to set for this action. Defaults to <see langword="null"/>.</param>
        public UndoableAction(string description = null) => Description = description;

        /// <summary>Adds an undoable editor action to the action list.</summary>
        /// <param name="action">The action to add to the list.</param>
        public void Add(GeneralEditorAction action) => editorActions.Add(action);

        /// <summary>Undoes all the actions in the list.</summary>
        public void Undo()
        {
            for (int i = editorActions.Count - 1; i >= 0; i--)
                editorActions[i].Undo();
        }
        /// <summary>Redoes all the actions in the list.</summary>
        public void Redo()
        {
            for (int i = 0; i < editorActions.Count - 1; i++)
                editorActions[i].Redo();
        }
        /// <summary>Clears the action list.</summary>
        public void Clear() => editorActions.Clear();
    }
}
