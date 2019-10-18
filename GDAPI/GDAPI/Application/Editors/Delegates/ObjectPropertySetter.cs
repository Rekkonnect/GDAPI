using System;
using System.Collections.Generic;
using System.Text;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Delegates
{
    /// <summary>Represents a function that changes the value of an object property of type <typeparamref name="T"/> in the specified objects.</summary>
    /// <param name="affectedObjects">The objects that will be affected by this function.</param>
    /// <param name="newValue">The new value to set to the field.</param>
    /// <param name="registerUndoable">Determines whether this action should be registered in the undo stack after performing it.</param>
    public delegate void ObjectPropertySetter<T>(LevelObjectCollection affectedObjects, T newValue, bool registerUndoable);
}
