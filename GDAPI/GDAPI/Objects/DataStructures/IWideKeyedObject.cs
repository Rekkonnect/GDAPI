namespace GDAPI.Objects.DataStructures
{
    /// <summary>Represents a wide keyed object, containing an array of keys that can address this object.</summary>
    /// <typeparam name="TKey">The type of each individual key in the array this object can be addressed by.</typeparam>
    public interface IWideKeyedObject<TKey> : IKeyedObject<TKey[]> { }
}