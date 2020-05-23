using GDAPI.Application.Editors.Actions;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.LevelObjects;

namespace GDAPI.Application.Editors
{
    /// <summary>The base pluggable editor class for editing a <see cref="GDAPI.Objects.GeometryDash.General.Level"/> which handles the level's objects.</summary>
    public abstract class BaseObjectSelectionPluggableEditor : BasePluggableEditor
    {
        #region Level
        /// <summary>The currently selected objects.</summary>
        public LevelObjectCollection SelectedObjects
        {
            get => MasterEditor.SelectedObjects;
            set => MasterEditor.SelectedObjects = value;
        }
        /// <summary>The objects that are copied into the clipboard and can be pasted.</summary>
        public LevelObjectCollection ObjectClipboard
        {
            get => MasterEditor.ObjectClipboard;
            set => MasterEditor.ObjectClipboard = value;
        }
        #endregion

        /// <summary>Initializes the base components contained in the <seealso cref="BasePluggableEditor"/> class for an inheriter class instance. The level bindable is bound to the </summary>
        /// <param name="master">The <seealso cref="Bindable{T}"/> referring to the master <seealso cref="Editor"/> instance.</param>
        protected BaseObjectSelectionPluggableEditor(Bindable<Editor> master)  : base(master) { }
    }
}
