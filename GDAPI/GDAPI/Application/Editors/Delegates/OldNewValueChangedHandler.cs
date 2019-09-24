using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Application.Editors.Delegates
{
    /// <summary>Represents a function that contains information about changing a field of type <typeparamref name="T"/>, including its current and its previous values.</summary>
    /// <param name="oldValue">The old value of the field.</param>
    /// <param name="newValue">The new value of the field.</param>
    public delegate void OldNewValueChangedHandler<T>(T oldValue, T newValue, bool registerUndoable);
}
