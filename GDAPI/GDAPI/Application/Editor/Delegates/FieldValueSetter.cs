using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Application.Editor.Delegates
{
    /// <summary>Represents a function that changes the value of a field of type <typeparamref name="R2"/>.</summary>
    /// <param name="newValue">The new value to set to the field.</param>
    /// <param name="registerUndoable">Determines whether this action should be registered in the undo stack after performing it.</param>
    public delegate void FieldValueSetter<T>(T newValue, bool registerUndoable);
    /// <summary>Represents a function that changes the value of a field of type <typeparamref name="R2"/>.</summary>
    /// <param name="newValue2">The new value to set to the field.</param>
    /// <param name="registerUndoable">Determines whether this action should be registered in the undo stack after performing it.</param>
    public delegate void FieldValueSetter<T1, T2>(T1 newValue1, T2 newValue2, bool registerUndoable);
}
