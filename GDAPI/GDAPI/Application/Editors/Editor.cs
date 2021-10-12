using GDAPI.Application.Editors.Actions;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.PluggableComponents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GDAPI.Application.Editors
{
    /// <summary>A master editor where other <seealso cref="IPluggableEditor"/>s can be plugged into to add functionality.</summary>
    public class Editor : BaseEditor, IPlugeeComponent<IPluggableEditor>
    {
        public UndoRedoSystem EditorActions { get; } = new UndoRedoSystem();
        public UndoRedoSystem LevelActions { get; } = new UndoRedoSystem();

        #region IPlugeeComponent
        private readonly Dictionary<Type, IPluggableEditor> pluggedEditors = new();

        public void Plug(IPluggableEditor component)
        {
            pluggedEditors.Add(component.GetType(), component);
            component.LevelBindable.BindTo(LevelBindable);
            component.Master.Value = this;
        }
        public void Plug(params IPluggableEditor[] components)
        {
            foreach (var c in components)
                Plug(c);
        }
        public void Unplug(IPluggableEditor component)
        {
            if (component.Master.Value != this)
                return;
            pluggedEditors.Remove(component.GetType());
            component.LevelBindable.UnbindFrom(LevelBindable);
        }
        public void Unplug(params IPluggableEditor[] components)
        {
            foreach (var c in components)
                Unplug(c);
        }

        public void UnplugAll()
        {
            var components = new IPluggableEditor[pluggedEditors.Count];
            pluggedEditors.Values.CopyTo(components, 0);
            Unplug(components);
        }

        public IPluggableEditor? GetPluggedComponent<TComponent>()
            where TComponent : IPluggableEditor
        {
            if (pluggedEditors.TryGetValue(typeof(TComponent), out var value))
                return (TComponent)value;
            return null;
        }
        #endregion

        #region Level
        /// <summary>The currently selected objects.</summary>
        public Bindable<LevelObjectCollection> SelectedObjectsBindable { get; } = new Bindable<LevelObjectCollection>(new LevelObjectCollection());
        /// <summary>The objects that are copied into the clipboard and can be pasted.</summary>
        public Bindable<LevelObjectCollection> ObjectClipboardBindable { get; } = new Bindable<LevelObjectCollection>(new LevelObjectCollection());

        /// <summary>The currently selected objects.</summary>
        public LevelObjectCollection SelectedObjects
        {
            get => SelectedObjectsBindable.Value;
            set => SelectedObjectsBindable.Value = value;
        }
        /// <summary>The objects that are copied into the clipboard and can be pasted.</summary>
        public LevelObjectCollection ObjectClipboard
        {
            get => ObjectClipboardBindable.Value;
            set => ObjectClipboardBindable.Value = value;
        }
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <seealso cref="Editor"/> class.</summary>
        /// <param name="level">The level to edit. Upon initializing this instance, the level's cached level string data is cleared.</param>
        public Editor(Level level) : this(new Bindable<Level>(level)) { }
        /// <summary>Initializes a new instance of the <seealso cref="Editor"/> class.</summary>
        /// <param name="levelBindable">The <seealso cref="Bindable{T}"/> containing the level to edit. Upon initializing this instance, the level's cached level string data is cleared.</param>
        public Editor(Bindable<Level> levelBindable)
            : base(levelBindable)
        {
            Level?.ClearCachedLevelStringData();
        }
        #endregion

        #region Object Filtering
        /// <summary>Returns the object within a rectangular range.</summary>
        /// <param name="startingX">The X of the starting point.</param>
        /// <param name="startingY">The Y of the starting point.</param>
        /// <param name="endingX">The X of the ending point.</param>
        /// <param name="endingY">The Y of the ending point.</param>
        public LevelObjectCollection GetObjectsWithinRange(double startingX, double startingY, double endingX, double endingY)
        {
            // Fix starting/ending points to avoid having to fix in the editor itself
            if (startingX > endingX)
                Swap(ref startingX, ref endingX);
            if (startingY > endingY)
                Swap(ref startingY, ref endingY);
            var result = new LevelObjectCollection();
            foreach (var o in Level.LevelObjects)
                if (o.IsWithinRange(startingX, startingY, endingX, endingY))
                    result.Add(o);
            return result;
        }
        /// <summary>Returns all the objects that are in a specific layer.</summary>
        /// <param name="EL">The editor layer which contains the objects to retrieve.</param>
        public LevelObjectCollection GetObjectsByLayer(int EL)
        {
            if (EL == -1) // Indicates the All layer
                return Level.LevelObjects;
            var result = new LevelObjectCollection();
            foreach (var o in Level.LevelObjects)
                if (o.EL1 == EL || o.EL2 == EL)
                    result.Add(o);
            return result;
        }
        /// <summary>Returns a collection of objects based on a predicate.</summary>
        /// <param name="predicate">The predicate to determine the resulting object collection.</param>
        public LevelObjectCollection GetObjects(Predicate<GeneralObject> predicate)
        {
            return new(Level.LevelObjects.Where(predicate.ToFuncDelegate()));
        }
        #endregion

        #region Database
        /// <summary>Saves this level to the database.</summary>
        /// <param name="database">The database this level should be saved to.</param>
        /// <param name="index">The index at which the level will be saved in the database.</param>
        public void Save(Database database, int index)
        {
            database.UserLevels[index] = Level;
            database.WriteLevelData();
        }
        #endregion

        #region Undo/Redo
        // TODO: Obsolete action registrations
        // TODO: Consider revamping the undoable action categorization
        /// <summary>Registers a <seealso cref="GeneralEditorAction"/> into the level actions undo/redo system.</summary>
        /// <param name="action">The <seealso cref="GeneralEditorAction"/> to register.</param>
        public void RegisterLevelAction(GeneralEditorAction action) => LevelActions.AddTemporaryAction(action);
        /// <summary>Registers a <seealso cref="GeneralEditorAction"/> into the editor actions undo/redo system.</summary>
        /// <param name="action">The <seealso cref="GeneralEditorAction"/> to register.</param>
        public void RegisterEditorAction(GeneralEditorAction action) => EditorActions.AddTemporaryAction(action);

        /// <summary>Undoes a number of actions from the level actions undo-redo system.</summary>
        /// <param name="count">The number of actions to undo.</param>
        public void UndoLevelActions(int count = 1) => LevelActions.Undo(count);
        /// <summary>Redoes a number of actions from the level actions undo-redo system.</summary>
        /// <param name="count">The number of actions to redo.</param>
        public void RedoLevelActions(int count = 1) => LevelActions.Redo(count);
        /// <summary>Undoes a number of actions from the editor actions undo-redo system.</summary>
        /// <param name="count">The number of actions to undo.</param>
        public void UndoEditorActions(int count = 1) => EditorActions.Undo(count);
        /// <summary>Redoes a number of actions from the editor actions undo-redo system.</summary>
        /// <param name="count">The number of actions to redo.</param>
        public void RedoEditorActions(int count = 1) => EditorActions.Redo(count);
        #endregion

        #region Private Methods
        private static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
        #endregion
    }
}
