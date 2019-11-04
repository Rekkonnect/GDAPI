namespace GDAPI.Objects.KeyedObjects
{
    /// <summary>Represents an object that can be addressed by two distinct keys where the first key may either be considered an array of <typeparamref name="T1"/>, or a single instance of the type.</summary>
    /// <typeparam name="T1">The type of the first key this object can be addressed by.</typeparam>
    /// <typeparam name="T2">The type of the second key this object can be addressed by.</typeparam>
    public interface IFirstWideDoubleKeyedObject<T1, T2> : IDoubleKeyedObject<T1[], T2>, IDoubleKeyedObject<T1, T2>
    {
        /// <summary>The converted non-array version of the first key that this object can be addressed by.</summary>
        T1 ConvertedKey { get; }
        /// <summary>The converted non-array version of the first key that this object can be addressed by.</summary>
        T1 IDoubleKeyedObject<T1, T2>.Key1 => ConvertedKey;
    }
}