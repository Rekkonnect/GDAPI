using GDAPI.Application.Editor.Delegates;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;
using System;

namespace GDAPI.Application.Editor.Actions.LevelActions
{
    /// <summary>Represents an action in the editor affecting the level which can be undone and redone containing a collection of affected objects.</summary>
    /// <typeparam name="TDelegate">The type of the delegate that represents the action.</typeparam>
    public abstract class LevelActionWithObjects<TDelegate> : LevelAction<TDelegate>
        where TDelegate : Delegate
    {
        /// <summary>The objects that this action will affect.</summary>
        public readonly LevelObjectCollection AffectedObjects;

        /// <summary>The string representation of the object count in the form $"{n} objects" for n != 1, or "1 object".</summary>
        protected readonly string ObjectCountStringRepresentation;

        /// <summary>Initiailizes a new instance of the <seealso cref="LevelActionWithObjects{TDelegate}"/> class.</summary>
        /// <param name="affectedObjects">The objects that this action will affect.</param>
        /// <param name="action">The action to be performed.</param>
        public LevelActionWithObjects(LevelObjectCollection affectedObjects, TDelegate action)
            : base(action)
        {
            AffectedObjects = affectedObjects;
            ObjectCountStringRepresentation = GetAppropriateForm(AffectedObjects.Count, "object");
        }

        private static string GetAppropriateForm(int count, string thing) => $"{count} {thing}{(count != 1 ? "" : "s")}";
    }
}
