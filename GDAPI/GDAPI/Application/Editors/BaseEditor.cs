using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;

namespace GDAPI.Application.Editors
{
    /// <summary>The base editor class for editing a <see cref="GDAPI.Objects.GeometryDash.General.Level"/>.</summary>
    public abstract class BaseEditor : IEditor
    {
        /// <summary>The level bindable.</summary>
        public Bindable<Level> LevelBindable { get; }
        public Level Level => LevelBindable.Value;

        protected BaseEditor(Bindable<Level> levelBindable) => LevelBindable = levelBindable;
    }
}
