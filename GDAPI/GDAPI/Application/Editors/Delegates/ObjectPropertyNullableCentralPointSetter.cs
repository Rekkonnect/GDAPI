using System;
using System.Collections.Generic;
using System.Text;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Delegates
{
    /// <summary>Represents a function that applies a transformation to the specified objects based on a central point.</summary>
    /// <param name="affectedObjects">The objects that will be affected by this function.</param>
    /// <param name="centralPoint">The central point that was taken into account while performing the action.</param>
    /// <param name="registerUndoable">Determines whether this action should be registered in the undo stack after performing it.</param>
    public delegate void ObjectPropertyNullableCentralPointSetter(LevelObjectCollection affectedObjects, Point? centralPoint, bool registerUndoable);
}
