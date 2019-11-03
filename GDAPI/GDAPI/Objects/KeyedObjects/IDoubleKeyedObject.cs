namespace GDAPI.Objects.KeyedObjects
{
    /// <summary>Represents an object that can be addressed by two distinct keys.</summary>
    /// <typeparam name="T1">The type of the first key this object can be addressed by.</typeparam>
    /// <typeparam name="T2">The type of the second key this object can be addressed by.</typeparam>
    public interface IDoubleKeyedObject<T1, T2> : IKeyedObject<(T1, T2)>
    {
        /// <summary>The first key that this object can be addressed by.</summary>
        T1 Key1 { get; }
        /// <summary>The second key that this object can be addressed by.</summary>
        T2 Key2 { get; }

        /// <summary>The keys that this object can be addressed by.</summary>
        (T1, T2) IKeyedObject<(T1, T2)>.Key => (Key1, Key2);
        // Shouldn't this be able to be marked as sealed so that no other type can implement it?
    }
}