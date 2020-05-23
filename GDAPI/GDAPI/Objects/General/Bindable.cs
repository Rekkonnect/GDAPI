using System;
using System.Collections.Generic;

namespace GDAPI.Objects.General
{
    /// <summary>
    /// Represents an object that can have its value changed according to other bindables that this is bound to.<br />
    /// Respectively, it may cause other bindables that are bound to this one to have their value changed to this one's current value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class Bindable<T>
    {
        // No circular dependency checks are being performed.

        private T val;
        private HashSet<Bindable<T>> linked = new HashSet<Bindable<T>>();

        /// <summary>Gets or sets the value of this <seealso cref="Bindable{T}"/>. Setting the bindable's value to a different value causes all the bindables that are bound to this one to have their value changed to this one. No circular dependency checks are being performed, so you're fucked if you try out anything crazy.</summary>
        public T Value
        {
            get => val;
            set
            {
                if (value.Equals(val))
                    return;

                val = value;
                ValueChanged?.Invoke(val);

                // See IsBindable's documentation
                foreach (var l in linked)
                    if (l.IsBindable)
                        l.Value = val;
            }
        }
        /// <summary>Gets or sets the default value for this <seealso cref="Bindable{T}"/>.</summary>
        public T DefaultValue { get; set; }

        /// <summary>
        /// Determines whether this bindable is <seealso cref="Bindable{T}"/> into anything, defaulting to <see langword="true"/>. If <see langword="false"/>, only other bindables may bind to this, but not vice versa.<br />
        /// If the bindables that are bound to this one have this property set to <see langword="false"/>, changing this bindable's <seealso cref="Value"/> will not change their value.
        /// </summary>
        public bool IsBindable { get; set; } = true;

        /// <summary>Raised upon having its value changed.</summary>
        public event Action<T> ValueChanged;

        /// <summary>Initializes a new instance of the <seealso cref="Bindable{T}"/> class.</summary>
        /// <param name="value">The value to initially set to the bindable.</param>
        /// <param name="boundTo">The <seealso cref="Bindable{T}"/>s this will be bound to. It is the equivalent of calling the <seealso cref="BindTo(Bindable{T})"/> for reach element of the array.</param>
        public Bindable(T value = default, T defaultValue = default, params Bindable<T>[] boundTo)
        {
            val = value;
            DefaultValue = defaultValue;
            if (boundTo != null)
                foreach (var b in boundTo)
                    BindTo(b);
        }

        /// <summary>
        /// Binds this <seealso cref="Bindable{T}"/> to another <seealso cref="Bindable{T}"/>, if the <seealso cref="IsBindable"/> is set to <see langword="true"/>, otherwise does nothing.<br />
        /// Once bound, this instance will have its value changed according to the bound bindable's value.
        /// </summary>
        /// <param name="other">The other <seealso cref="Bindable{T}"/> to bind this to.</param>
        /// <returns>A value determining whether this <seealso cref="Bindable{T}"/> was successfully bound to the requested <seealso cref="Bindable{T}"/>.</returns>
        public bool BindTo(Bindable<T> other)
        {
            return IsBindable && other.linked.Add(this);
        }
        /// <summary>Unblinds this <seealso cref="Bindable{T}"/> from another <seealso cref="Bindable{T}"/>, if the <seealso cref="IsBindable"/> is set to <see langword="true"/>, otherwise does nothing.</summary>
        /// <param name="other">The other <seealso cref="Bindable{T}"/> to unbind this from.</param>
        /// <returns>A value determining whether this <seealso cref="Bindable{T}"/> was successfully unbound from the requested <seealso cref="Bindable{T}"/>.</returns>
        public bool UnbindFrom(Bindable<T> other)
        {
            return IsBindable && other.linked.Remove(this);
        }

        /// <summary>Initializes and returns a new <seealso cref="Bindable{T}"/> that is bound to this instance.</summary>
        public Bindable<T> CreateNewBindableBoundToThis()
        {
            var result = new Bindable<T>();
            result.BindTo(this);
            return result;
        }

        /// <summary>Sets the value of this <seealso cref="Bindable{T}"/> to its default.</summary>
        public void SetDefault() => Value = DefaultValue;
    }
}
