namespace GDAPI.Objects.PluggableComponents
{
    /// <summary>Represents a component that can be composed off of <seealso cref="IPluggableComponent"/>s.</summary>
    public interface IPlugeeComponent
    {
        /// <summary>Unlpugs all components from this plugee component.</summary>
        void UnplugAll();
    }
    /// <summary>Represents a component that can be composed off of <seealso cref="IPluggableComponent{T}"/>s.</summary>
    /// <typeparam name="T">The type of the components that this plugee component supports.</typeparam>
    public interface IPlugeeComponent<T> : IPlugeeComponent
        where T : IPluggableComponent
    {
        /// <summary>Plugs a component into this plugee component.</summary>
        /// <param name="component">The component to plug into this plugee component.</param>
        void Plug(T component);
        /// <summary>Plugs a number of components into this plugee component.</summary>
        /// <param name="components">The components to plug into this plugee component.</param>
        void Plug(params T[] components);
        /// <summary>Unplugs a component from this plugee component. If the component is not plugged to that instance, nothing happens.</summary>
        /// <param name="component">The component to unplug from this plugee component.</param>
        void Unplug(T component);
        /// <summary>Unplugs a number of components from this plugee component. If a component is not plugged to that instance, nothing happens.</summary>
        /// <param name="components">The components to unplug from this plugee component.</param>
        void Unplug(params T[] components);

        /// <summary>Gets a plugged component that matches the type of the provided requested component.</summary>
        /// <typeparam name="TComponent">The type of the plugged component to return.</typeparam>
        T GetPluggedComponent<TComponent>()
            where TComponent : T;
    }
}
