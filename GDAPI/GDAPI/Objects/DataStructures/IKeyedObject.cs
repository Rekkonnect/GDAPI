namespace GDAPI.Objects.DataStructures
{
    /// <summary>Represents an object that can be addressed via a key.</summary>
    /// <typeparam name="TKey">The type of the key this object can be addressed by.</typeparam>
    public interface IKeyedObject<TKey>
    {
        /// <summary>The key that this object can be addressed by.</summary>
        TKey Key { get; }
    }
}