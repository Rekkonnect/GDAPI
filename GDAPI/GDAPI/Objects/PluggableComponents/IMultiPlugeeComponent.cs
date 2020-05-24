using System.Collections.Generic;

namespace GDAPI.Objects.PluggableComponents
{
    /// <summary>Represents a component that can be composed off of <seealso cref="IPluggableComponent{T}"/>s, while also allowing multiple components of the exact same type to be plugged.</summary>
    /// <typeparam name="T">The type of the components that this plugee component supports.</typeparam>
    public interface IMultiPlugeeComponent<T> : IPlugeeComponent<T>
        where T : IPluggableComponent
    {
        /// <summary>Gets the plugged component of a provided type at index 0.</summary>
        /// <typeparam name="TComponent">The type of the plugged component to return.</typeparam>
        T IPlugeeComponent<T>.GetPluggedComponent<TComponent>() => GetPluggedComponent<TComponent>(0);
        /// <summary>Gets the plugged component at the specified index of a provided type.</summary>
        /// <typeparam name="TComponent">The type of the plugged component to return.</typeparam>
        /// <param name="index">The index of the component of the specified type to get, in order of how they were plugged.</param>
        T GetPluggedComponent<TComponent>(int index)
            where TComponent : T;
        /// <summary>Gets all the plugged components of that type.</summary>
        /// <typeparam name="TComponent">The type of the plugged components to return.</typeparam>
        IEnumerable<T> GetPluggedComponents<TComponent>()
            where TComponent : T;
    }
}
