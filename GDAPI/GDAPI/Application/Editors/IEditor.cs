using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;

namespace GDAPI.Application.Editors
{
    /// <summary>Represents a level editor.</summary>
    public interface IEditor
    {
        Bindable<Level> LevelBindable { get; }
        sealed Level Level => LevelBindable.Value;
    }
}
