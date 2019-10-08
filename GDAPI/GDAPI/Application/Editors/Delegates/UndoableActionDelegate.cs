using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Application.Editors.Delegates
{
    /// <summary>Represents an undoable function that performs changes.</summary>
    /// <param name="registerUndoable">Determines whether this action should be registered in the undo stack after performing it.</param>
    public delegate void UndoableActionDelegate(bool registerUndoable);
}
