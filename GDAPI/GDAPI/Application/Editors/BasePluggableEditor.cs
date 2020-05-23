using GDAPI.Application.Editors.Actions;
using GDAPI.Objects.General;

namespace GDAPI.Application.Editors
{
    /// <summary>The base pluggable editor class for editing a <see cref="GDAPI.Objects.GeometryDash.General.Level"/>.</summary>
    public abstract class BasePluggableEditor : BaseEditor, IPluggableEditor
    {
        public Bindable<Editor> Master { get; }
        /// <summary>The master <seealso cref="Editor"/> component.</summary>
        public Editor MasterEditor => Master.Value;

        /// <summary>The level action <seealso cref="UndoRedoSystem"/> of the master <seealso cref="Editor"/> component.</summary>
        public UndoRedoSystem LevelActions => MasterEditor.LevelActions;
        /// <summary>The editor action <seealso cref="UndoRedoSystem"/> of the master <seealso cref="Editor"/> component.</summary>
        public UndoRedoSystem EditorActions => MasterEditor.EditorActions;

        /// <summary>Initializes the base components contained in the <seealso cref="BasePluggableEditor"/> class for an inheriter class instance. The level bindable is bound to the </summary>
        /// <param name="master">The <seealso cref="Bindable{T}"/> referring to the master <seealso cref="Editor"/> instance.</param>
        protected BasePluggableEditor(Bindable<Editor> master)
            : base(master.Value.LevelBindable.CreateNewBindableBoundToThis())
        {
            Master = master;
        }

        /// <summary>Registers a <seealso cref="GeneralEditorAction"/> into the level actions undo/redo system.</summary>
        /// <param name="action">The <seealso cref="GeneralEditorAction"/> to register.</param>
        public void RegisterLevelAction(GeneralEditorAction action) => MasterEditor.RegisterLevelAction(action);
        /// <summary>Registers a <seealso cref="GeneralEditorAction"/> into the editor actions undo/redo system.</summary>
        /// <param name="action">The <seealso cref="GeneralEditorAction"/> to register.</param>
        public void RegisterEditorAction(GeneralEditorAction action) => MasterEditor.RegisterEditorAction(action);
    }
}
