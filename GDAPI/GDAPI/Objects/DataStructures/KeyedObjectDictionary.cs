using GDAPI.Objects.KeyedObjects;
using System.Collections;
using System.Collections.Generic;

namespace GDAPI.Objects.DataStructures
{
    /// <summary>Represents a dictionary of <seealso cref="IKeyedObject{TKey}"/> objects.</summary>
    /// <typeparam name="TKey">The type of the key that the <seealso cref="IKeyedObject{TKey}"/> returns.</typeparam>
    /// <typeparam name="TObject">The type of the <seealso cref="IKeyedObject{TKey}"/> that will be stored.</typeparam>
    public class KeyedObjectDictionary<TKey, TObject> : IEnumerable<KeyValuePair<TKey, TObject>>
        where TObject : IKeyedObject<TKey>
    {
        /// <summary>The dictionary containing the objects.</summary>
        protected Dictionary<TKey, TObject> Dictionary { get; private set; }

        /// <summary>Gets the number of <seealso cref="TObject"/> objects that are contained in the dictionary.</summary>
        public int Count => Dictionary.Count;

        public ICollection<TKey> Keys => ((IDictionary<TKey, TObject>)Dictionary).Keys;
        public ICollection<TObject> Values => ((IDictionary<TKey, TObject>)Dictionary).Values;

        public bool IsReadOnly => false;

        /// <summary>Initializes a new instance of the <seealso cref="KeyedObjectDictionary{TKey, TObject}"/> class.</summary>
        public KeyedObjectDictionary()
        {
            Dictionary = new Dictionary<TKey, TObject>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="KeyedObjectDictionary{TKey, TObject}"/> class.</summary>
        /// <param name="d">The dictionary to initialize this dictionary out of.</param>
        public KeyedObjectDictionary(KeyedObjectDictionary<TKey, TObject> d)
        {
            Dictionary = new Dictionary<TKey, TObject>(d.Dictionary);
        }
        // TODO: Add more constructors if necessary

        /// <summary>Adds an object to the dictionary.</summary>
        /// <param name="o">The object to add to the dictionary.</param>
        public void Add(TObject o) => Dictionary.Add(o.Key, o);
        /// <summary>Adds an <seealso cref="IKeyedObject{TKey}"/> to the dictionary.</summary>
        /// <param name="o">The <seealso cref="IKeyedObject{TKey}"/> to add to the dictionary.</param>
        public void Add(IKeyedObject<TKey[]> o)
        {
            foreach (var k in o.Key)
                Dictionary.Add(k, (TObject)o);
        }
        /// <summary>Removes an object from the dictionary based on a given key.</summary>
        /// <param name="key">The key of the object to remove from the dictionary.</param>
        public bool Remove(TKey key) => Dictionary.Remove(key);
        /// <summary>Removes an object from the dictionary based on a given value.</summary>
        /// <param name="o">The object to remove from the dictionary.</param>
        public void Remove(TObject o) => Dictionary.Remove(o.Key);
        /// <summary>Removes an <seealso cref="IKeyedObject{TKey}"/> from the dictionary.</summary>
        /// <param name="o">The <seealso cref="IKeyedObject{TKey}"/> to remove from the dictionary.</param>
        public void Remove(IKeyedObject<TKey[]> o)
        {
            foreach (var k in o.Key)
                Remove(k);
        }
        /// <summary>Clears the dictionary.</summary>
        public void Clear() => Dictionary.Clear();

        /// <summary>Determines whether the specified value is contained.</summary>
        /// <param name="value">The value to check whether it is contained.</param>
        public bool ContainsValue(TObject value) => Dictionary.ContainsKey(value.Key);
        /// <summary>Determines whether the specified key is contained.</summary>
        /// <param name="key">The key to check whether it is contained.</param>
        public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);
        /// <summary>Attempts to get the value of a specified key.</summary>
        /// <param name="key">The key to attempt to get the value of.</param>
        /// <param name="value">The value that was retrieved from the key.</param>
        public bool TryGetValue(TKey key, out TObject value) => Dictionary.TryGetValue(key, out value);

        /// <summary>Gets or sets the object given a specified key.</summary>
        /// <param name="key">The key of the object to get or set.</param>
        public TObject this[TKey key]
        {
            get => Dictionary[key];
            set => Dictionary[key] = value;
        }

        public IEnumerator<KeyValuePair<TKey, TObject>> GetEnumerator() => ((IEnumerable<KeyValuePair<TKey, TObject>>)Dictionary).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<KeyValuePair<TKey, TObject>>)Dictionary).GetEnumerator();
    }
}
