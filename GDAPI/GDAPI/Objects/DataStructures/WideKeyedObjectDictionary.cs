using System.Collections;
using System.Collections.Generic;

namespace GDAPI.Objects.DataStructures
{
    /// <summary>Represents a dictionary of <seealso cref="IWideKeyedObject{TKey}"/> objects.</summary>
    /// <typeparam name="TKey">The type of the key that the <seealso cref="IWideKeyedObject{TKey}"/> returns.</typeparam>
    /// <typeparam name="TObject">The type of the <seealso cref="IWideKeyedObject{TKey}"/> that will be stored.</typeparam>
    public class WideKeyedObjectDictionary<TKey, TObject> : KeyedObjectDictionary<TKey, TObject>
        where TObject : IKeyedObject<TKey>
    {
        /// <summary>Initializes a new instance of the <seealso cref="WideKeyedObjectDictionary{TKey, TObject}"/> class.</summary>
        public WideKeyedObjectDictionary() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="WideKeyedObjectDictionary{TKey, TObject}"/> class.</summary>
        /// <param name="d">The dictionary to initialize this dictionary out of.</param>
        public WideKeyedObjectDictionary(WideKeyedObjectDictionary<TKey, TObject> d) : base(d) { }
        // TODO: Add more constructors if necessary

        // TODO: Investigate why this overload is an absolute must
        /// <summary>Adds an <seealso cref="IKeyedObject{TKey}"/> to the dictionary.</summary>
        /// <param name="o">The <seealso cref="IKeyedObject{TKey}"/> to add to the dictionary.</param>
        public void Add(IKeyedObject<TKey> o) => base.Add((TObject)o);
        /// <summary>Adds an <seealso cref="IWideKeyedObject{TKey}"/> to the dictionary.</summary>
        /// <param name="o">The <seealso cref="IWideKeyedObject{TKey}"/> to add to the dictionary.</param>
        public void Add(IWideKeyedObject<TKey> o)
        {
            foreach (var k in o.Key)
                Dictionary.Add(k, (TObject)o);
        }
        /// <summary>Removes an <seealso cref="IKeyedObject{TKey}"/> from the dictionary.</summary>
        /// <param name="o">The <seealso cref="IKeyedObject{TKey}"/> to remove from the dictionary.</param>
        public void Remove(IKeyedObject<TKey> o) => base.Remove((TObject)o);
        /// <summary>Removes an <seealso cref="IWideKeyedObject{TKey}"/> from the dictionary.</summary>
        /// <param name="o">The <seealso cref="IWideKeyedObject{TKey}"/> to remove from the dictionary.</param>
        public void Remove(IWideKeyedObject<TKey> o)
        {
            foreach (var k in o.Key)
                Dictionary.Remove(k);
        }
    }
}
