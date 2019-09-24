using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Application.Editors.Delegates
{
    /// <summary>Represents a function that contains information about changing a <see langword="double"/> field.</summary>
    /// <param name="newValue">The new value of the field.</param>
    public delegate void ValueChangedHandler<T>(T newValue, bool registerUndoable);
}
