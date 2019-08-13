using GDAPI.Application.Editor.Actions;
using System;
using System.Collections.Generic;
using static System.Math;

namespace GDAPI.Application
{
    /// <summary>A system that allows handling undoing and redoing undoable actions.</summary>
    public class UndoRedoSystem
    {
        private bool multipleActionToggle;

        // I know, the naming is terrible on this one (hence the documentation), we need to find a better name
        /// <summary>Determines whether multiple actions will be logged in the undo stack. Setting this to <see langword="false"/> will register all actions.</summary>
        public bool MultipleActionToggle
        {
            get => multipleActionToggle;
            set
            {
                if (!(multipleActionToggle = value) && TemporaryUndoableAction.Count > 0)
                    RegisterActions(DefaultDescription);
            }
        }

        /// <summary>The default description for the undoable actions.</summary>
        public string DefaultDescription;

        /// <summary>The temporarily stored undoable action that is to be registered upon completing an operation.</summary>
        public UndoableAction TemporaryUndoableAction = new UndoableAction();
        /// <summary>The stack that contains all actions that are to be undone.</summary>
        public readonly Stack<UndoableAction> UndoStack = new Stack<UndoableAction>();
        /// <summary>The stack that contains all actions that are to be redone.</summary>
        public readonly Stack<UndoableAction> RedoStack = new Stack<UndoableAction>();

        /// <summary>Initializes a new instance of the <seealso cref="UndoRedoSystem"/> class.</summary>
        /// <param name="defaultDescription">The default description for the undoable actions.</param>
        public UndoRedoSystem(string defaultDescription = "") => DefaultDescription = defaultDescription;

        /// <summary>Registers all the actions.</summary>
        /// <param name="description">The description to use for the registered actions. If the description is <see langword="null"/>, the default description is used instead.</param>
        public void RegisterActions(string description = null)
        {
            TemporaryUndoableAction.Description = description ?? DefaultDescription;
            UndoStack.Push(TemporaryUndoableAction);
            TemporaryUndoableAction = new UndoableAction();
            RedoStack.Clear();
        }
        /// <summary>Adds a temporary action to the temporary action object and registers the undoable action if the multiple action toggle is <see langword="false"/>.</summary>
        /// <param name="action">The editor action.</param>
        public void AddTemporaryAction(GeneralEditorAction action)
        {
            TemporaryUndoableAction.Add(action);
            if (!MultipleActionToggle)
                RegisterActions(action.Description);
        }

        /// <summary>Undoes a number of actions. If the specified count is greater than the available actions to undo, all actions are undone.</summary>
        /// <param name="count">The number of actions to undo.</param>
        public void Undo(int count = 1)
        {
            int actions = Min(count, UndoStack.Count);
            for (int i = 0; i < actions; i++)
            {
                var action = UndoStack.Pop();
                action.Undo();
                RedoStack.Push(action);
            }
        }
        /// <summary>Redoes a number of actions. If the specified count is greater than the available actions to redo, all actions are redone.</summary>
        /// <param name="count">The number of actions to redo.</param>
        public void Redo(int count = 1)
        {
            int actions = Min(count, RedoStack.Count);
            for (int i = 0; i < actions; i++)
            {
                var action = RedoStack.Pop();
                action.Redo();
                UndoStack.Push(action);
            }
        }
    }
}
