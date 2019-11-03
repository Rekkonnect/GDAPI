using GDAPI.Objects.KeyedObjects;
using System.Collections;
using System.Collections.Generic;

namespace GDAPI.Objects.DataStructures
{
    /// <summary>Represents a dictionary of <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> objects.</summary>
    /// <typeparam name="TKey1">The type of the first key that the <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> returns.</typeparam>
    /// <typeparam name="TKey2">The type of the second key that the <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> returns.</typeparam>
    /// <typeparam name="TObject">The type of the <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> that will be stored.</typeparam>
    public class DoubleKeyedObjectDictionary<TKey1, TKey2, TObject> : IEnumerable<KeyValuePair<(TKey1, TKey2), TObject>>
        where TObject : IDoubleKeyedObject<TKey1, TKey2>
    {
        /// <summary>The first dictionary containing the objects.</summary>
        protected Dictionary<TKey1, TObject> Dictionary1 { get; private set; }
        /// <summary>The second dictionary containing the objects.</summary>
        protected Dictionary<TKey2, TObject> Dictionary2 { get; private set; }

        /// <summary>Initializes a new instance of the <seealso cref="DoubleKeyedObjectDictionary{TKey1, TKey2, TObject}"/> class.</summary>
        public DoubleKeyedObjectDictionary()
        {
            Dictionary1 = new Dictionary<TKey1, TObject>();
            Dictionary2 = new Dictionary<TKey2, TObject>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="DoubleKeyedObjectDictionary{TKey1, TKey2, TObject}"/> class.</summary>
        /// <param name="d">The dictionary to initialize this dictionary out of.</param>
        public DoubleKeyedObjectDictionary(DoubleKeyedObjectDictionary<TKey1, TKey2, TObject> d)
        {
            Dictionary1 = new Dictionary<TKey1, TObject>(d.Dictionary1);
            Dictionary2 = new Dictionary<TKey2, TObject>(d.Dictionary2);
        }
        // TODO: Add more constructors if necessary

        /// <summary>Adds an object to the dictionary.</summary>
        /// <param name="o">The object to add to the dictionary.</param>
        public void Add(TObject o)
        {
            Dictionary1.Add(o.Key1, o);
            Dictionary2.Add(o.Key2, o);
        }
        /// <summary>Adds an <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> to the dictionary.</summary>
        /// <param name="o">The <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> to add to the dictionary.</param>
        public void Add(IDoubleKeyedObject<TKey1[], TKey2> o)
        {
            foreach (var k in o.Key1)
                Dictionary1.Add(k, (TObject)o);
            Dictionary2.Add(o.Key2, (TObject)o);
        }
        /// <summary>Adds an <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> to the dictionary.</summary>
        /// <param name="o">The <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> to add to the dictionary.</param>
        public void Add(IDoubleKeyedObject<TKey1, TKey2[]> o)
        {
            Dictionary1.Add(o.Key1, (TObject)o);
            foreach (var k in o.Key2)
                Dictionary2.Add(k, (TObject)o);
        }
        /// <summary>Adds an <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> to the dictionary.</summary>
        /// <param name="o">The <seealso cref="IDoubleKeyedObject{TKey1, TKey2}"/> to add to the dictionary.</param>
        public void Add(IDoubleKeyedObject<TKey1[], TKey2[]> o)
        {
            foreach (var k in o.Key1)
                Dictionary1.Add(k, (TObject)o);
            foreach (var k in o.Key2)
                Dictionary2.Add(k, (TObject)o);
        }
        /// <summary>Removes an object from the dictionary based on a given first key.</summary>
        /// <param name="key">The first key of the object to remove from the dictionary.</param>
        public bool Remove(TKey1 key)
        {
            var o = this[key];
            if (o == null)
                return false;
            Dictionary1.Remove(key);
            Dictionary2.Remove(o.Key2);
            return true;
        }
        /// <summary>Removes an object from the dictionary based on a given second key.</summary>
        /// <param name="key">The second key of the object to remove from the dictionary.</param>
        public bool Remove(TKey2 key)
        {
            var o = this[key];
            if (o == null)
                return false;
            Dictionary1.Remove(o.Key1);
            Dictionary2.Remove(key);
            return true;
        }

        /// <summary>Determines whether the specified value is contained.</summary>
        /// <param name="value">The value to check whether it is contained.</param>
        public bool ContainsValue(TObject value) => Dictionary1.ContainsKey(value.Key1);
        /// <summary>Determines whether the specified first key is contained.</summary>
        /// <param name="key">The first key to check whether it is contained.</param>
        public bool ContainsKey(TKey1 key) => Dictionary1.ContainsKey(key);
        /// <summary>Determines whether the specified second key is contained.</summary>
        /// <param name="key">The second key to check whether it is contained.</param>
        public bool ContainsKey(TKey2 key) => Dictionary2.ContainsKey(key);
        /// <summary>Attempts to get the value of a specified first key.</summary>
        /// <param name="key">The first key to attempt to get the value of.</param>
        /// <param name="value">The value that was retrieved from the first key.</param>
        public bool TryGetValue(TKey1 key, out TObject value) => Dictionary1.TryGetValue(key, out value);
        /// <summary>Attempts to get the value of a specified second key.</summary>
        /// <param name="key">The second key to attempt to get the value of.</param>
        /// <param name="value">The value that was retrieved from the second key.</param>
        public bool TryGetValue(TKey2 key, out TObject value) => Dictionary2.TryGetValue(key, out value);

        /// <summary>Gets or sets the object given a specified first key.</summary>
        /// <param name="key">The first key of the object to get or set.</param>
        public TObject this[TKey1 key]
        {
            get => Dictionary1[key];
            set => Dictionary1[key] = value;
        }
        /// <summary>Gets or sets the object given a specified second key.</summary>
        /// <param name="key">The second key of the object to get or set.</param>
        public TObject this[TKey2 key]
        {
            get => Dictionary2[key];
            set => Dictionary2[key] = value;
        }

        public IEnumerator<KeyValuePair<(TKey1, TKey2), TObject>> GetEnumerator()
        {
            foreach (var o in Dictionary1)
                yield return new KeyValuePair<(TKey1, TKey2), TObject>((o.Value.Key1, o.Value.Key2), o.Value);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
