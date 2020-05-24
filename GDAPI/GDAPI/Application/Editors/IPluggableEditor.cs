using GDAPI.Objects.PluggableComponents;

namespace GDAPI.Application.Editors
{
    /// <summary>Represents a pluggable editor, which will be bound to a master editor.</summary>
    public interface IPluggableEditor : IPluggableComponent<Editor>, IEditor
    {
    }
}
