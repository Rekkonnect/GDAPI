using GDAPI.Application.Editors.Delegates;
using System;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors.Actions.LevelActions
{
    /// <summary>Represents an action in the editor affecting the level which can be undone and redone containing a collection of affected objects whose properties are adjusted with an offset.</summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TActionDelegate">The type of the delegate that represents the action.</typeparam>
    public abstract class LevelActionObjectsOffset<TValue, TActionDelegate> : LevelActionWithObjects<TActionDelegate>
        where TActionDelegate : Delegate
    {
        /// <summary>The offset to apply to the specified property of the affected objects.</summary>
        public TValue Offset { get; }
        /// <summary>The inverse of the offset to apply to the specified property of the affected objects.</summary>
        public TValue InverseOffset { get; protected set; }

        /// <summary>Initiailizes a new instance of the <seealso cref="LevelActionObjectsOffset{TActionDelegate, TInverseDelegate}"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="offset">The offset to apply to the specified property of the affected objects.</param>
        /// <param name="action">The action to be performed.</param>
        public LevelActionObjectsOffset(LevelObjectCollection affectedObjects, TValue offset, TActionDelegate action)
            : base(affectedObjects, action)
        {
            Offset = offset;
            InverseOffset = GetInverseOffset();
        }

        /// <summary>Gets the inverse offset based on the given offset.</summary>
        protected abstract TValue GetInverseOffset();
    }

    /// <summary>Represents an action in the editor affecting the level which can be undone and redone containing a collection of affected objects whose properties are adjusted with an offset.</summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public abstract class LevelActionObjectsOffset<TValue> : LevelActionObjectsOffset<TValue, ObjectPropertyOffsetSetter<TValue>>
    {
        /// <summary>Initiailizes a new instance of the <seealso cref="LevelActionObjectsOffset{TActionDelegate, TInverseDelegate}"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="offset">The offset to apply to the specified property of the affected objects.</param>
        /// <param name="action">The action to be performed.</param>
        public LevelActionObjectsOffset(LevelObjectCollection affectedObjects, TValue offset, ObjectPropertyOffsetSetter<TValue> action)
            : base(affectedObjects, offset, action) { }

        /// <summary>Performs the action.</summary>
        public sealed override void PerformAction() => Action(AffectedObjects, Offset, false);
        /// <summary>Performs the inverse action of the editor action.</summary>
        public sealed override void PerformInverseAction() => Action(AffectedObjects, InverseOffset, false);
    }
}
