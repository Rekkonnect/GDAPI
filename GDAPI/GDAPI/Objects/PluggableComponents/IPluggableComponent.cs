using GDAPI.Objects.General;

namespace GDAPI.Objects.PluggableComponents
{
    /// <summary>Represents a component that can be plugged into a <seealso cref="IPlugeeComponent{T}"/>.</summary>
    public interface IPluggableComponent
    {
    }
    /// <summary>Represents a component that can be plugged into a <seealso cref="IPlugeeComponent{T}"/>.</summary>
    /// <typeparam name="T">The type of the <seealso cref="IPlugeeComponent{T}"/>.</typeparam>
    public interface IPluggableComponent<T> : IPluggableComponent
        where T : IPlugeeComponent
    {
        /// <summary>A <seealso cref="Bindable{T}"/> containing the master component.</summary>
        Bindable<T> Master { get; }
    }
}
