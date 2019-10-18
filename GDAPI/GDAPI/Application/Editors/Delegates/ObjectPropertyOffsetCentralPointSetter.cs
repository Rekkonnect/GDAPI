using System;
using System.Collections.Generic;
using System.Text;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Delegates
{
    /// <summary>Represents a function that changes the value of an object property of type <typeparamref name="T"/> by an offset in the specified objects.</summary>
    /// <param name="affectedObjects">The objects that will be affected by this function.</param>
    /// <param name="offset">The offset that will be applied to the field's value.</param>
    /// <param name="centralPoint">The central point that was taken into account while performing the action.</param>
    /// <param name="registerUndoable">Determines whether this action should be registered in the undo stack after performing it.</param>
    public delegate void ObjectPropertyOffsetCentralPointSetter<T>(LevelObjectCollection affectedObjects, T offset, Point? centralPoint, bool registerUndoable);
}
