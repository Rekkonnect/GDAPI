using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GDAPI.Functions.Extensions;

namespace GDAPI.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Represents a dictionary that contains <seealso cref="ObjectHitboxDefinition"/>s and encapsulates useful manipulation functions. All definitions are unique per value, which means that object IDs and hitboxes have to be unique across the entire dictionary.</summary>
    public class ObjectHitboxDefinitionDictionary : IDictionary<List<int>, Hitbox>, ICollection<ObjectHitboxDefinition>
    {
        private readonly List<ObjectHitboxDefinition> list;
        private List<int> allIDs;

        /// <summary>Determines whether the dictionary is read only (it's not).</summary>
        public bool IsReadOnly => false;
        /// <summary>Retrieves the count of definition entries.</summary>
        public int Count => list.Count;

        /// <summary>Gets all the object IDs that are defined in this dictionary.</summary>
        public ICollection<int> AllObjectIDs
        {
            get
            {
                if (allIDs == null)
                {
                    allIDs = new List<int>();
                    foreach (var d in list)
                        allIDs.AddRange(d.ObjectIDs);
                }
                return allIDs;
            }
        }

        /// <summary>Gets the keys of the dictionary.</summary>
        public ICollection<List<int>> Keys
        {
            get
            {
                var result = new List<List<int>>();
                foreach (var d in list)
                    result.Add(d.ObjectIDs);
                return result;
            }
        }
        /// <summary>Gets the values of the dictionary.</summary>
        public ICollection<Hitbox> Values
        {
            get
            {
                var result = new List<Hitbox>();
                foreach (var d in list)
                    result.Add(d.Hitbox);
                return result;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectHitboxDefinitionDictionary"/> class.</summary>
        /// <param name="l">The list of <seealso cref="ObjectHitboxDefinition"/> to initialize the <seealso cref="ObjectHitboxDefinitionDictionary"/> out of.</param>
        public ObjectHitboxDefinitionDictionary(List<ObjectHitboxDefinition> l)
        {
            list = l;
            Validate();
        }

        /// <summary>Returns a <seealso cref="ObjectHitboxDefinition"/> that matches a given list of object IDs or <see langword="null"/> if not found.</summary>
        /// <param name="objectIDs">The list of object IDs that the requested <seealso cref="ObjectHitboxDefinition"/> contains.</param>
        public ObjectHitboxDefinition Find(List<int> objectIDs) => list.Find(d => d.ObjectIDs.ContainsAll(objectIDs));
        /// <summary>Returns a <seealso cref="ObjectHitboxDefinition"/> that matches a given list of object IDs or <see langword="null"/> if not found.</summary>
        /// <param name="hitbox">The hitbox that the requested <seealso cref="ObjectHitboxDefinition"/> contains.</param>
        public ObjectHitboxDefinition Find(Hitbox hitbox) => list.Find(d => d.Hitbox == hitbox);
        /// <summary>Returns the <seealso cref="ObjectHitboxDefinition"/> that is common among the specified object IDs.</summary>
        /// <param name="objectIDs">The object IDs to get the common <seealso cref="ObjectHitboxDefinition"/>.</param>
        public ObjectHitboxDefinition GetCommonObjectHitboxDefinition(List<int> objectIDs)
        {
            if (objectIDs.Count == 1)
                return GetObjectHitboxDefinition(objectIDs[0]);
            if (objectIDs.Count == 0)
                throw new ArgumentException("The provided list is empty.");
            foreach (var d in list)
            {
                bool found = false;
                bool contains;
                for (int i = 0; i < objectIDs.Count; i++)
                    if (contains = d.ObjectIDs.Contains(objectIDs[i]))
                        found = true;
                    else if (found && !contains)
                        return null;
                    else
                        break;
                if (found)
                    return d;
            }
            throw new KeyNotFoundException("Some of the selected object IDs could not be addressed in the current dictionary.");
        }

        /// <summary>Adds a new <seealso cref="ObjectHitboxDefinition"/> to the dictionary.</summary>
        /// <param name="objectID">The object ID to use.</param>
        /// <param name="hitbox">The <seealso cref="Hitbox"/> that will be added.</param>
        public void Add(int objectID, Hitbox hitbox) => Add(new ObjectHitboxDefinition(objectID, hitbox));
        /// <summary>Adds a new <seealso cref="ObjectHitboxDefinition"/> to the dictionary.</summary>
        /// <param name="objectIDs">The object IDs to use.</param>
        /// <param name="hitbox">The <seealso cref="Hitbox"/> that will be added.</param>
        public void Add(List<int> objectIDs, Hitbox hitbox) => Add(new ObjectHitboxDefinition(objectIDs, hitbox));
        /// <summary>Adds a new <seealso cref="ObjectHitboxDefinition"/> to the dictionary.</summary>
        /// <param name="entry">The key/value pair to add to the dictionary.</param>
        public void Add(KeyValuePair<List<int>, Hitbox> entry) => Add(entry.Key, entry.Value);
        /// <summary>Adds a new <seealso cref="ObjectHitboxDefinition"/> to the dictionary.</summary>
        /// <param name="definition">The <seealso cref="ObjectHitboxDefinition"/> to add to the dictionary.</param>
        public void Add(ObjectHitboxDefinition definition)
        {
            var f = Find(definition.Hitbox);
            if (f == null)
                list.Add(definition);
            else
                f.ObjectIDs.AddRange(definition.ObjectIDs);
            allIDs?.AddRange(definition.ObjectIDs);
            Validate();
        }

        /// <summary>Clears the dictionary.</summary>
        public void Clear()
        {
            allIDs?.Clear();
            list.Clear();
        }

        /// <summary>Determines whether the dictionary contains an entry with the specified object IDs mapped to the specified hitbox.</summary>
        /// <param name="objectIDs">The object IDs to check whether they are mapped to the specified hitbox.</param>
        /// <param name="hitbox">The hitbox to check whether it's mapped to the specified object IDs.</param>
        public bool Contains(List<int> objectIDs, Hitbox hitbox) => list.Any(m => m.ObjectIDs.ContainsAll(objectIDs) && m.Hitbox == hitbox);
        /// <summary>Determines whether the dictionary contains a definition with the specified object IDs and the respective hitbox.</summary>
        /// <param name="entry">The key/value pair containing information for a respective <seealso cref="ObjectHitboxDefinition"/> object.</param>
        public bool Contains(KeyValuePair<List<int>, Hitbox> entry) => Contains(entry.Key, entry.Value);
        /// <summary>Determines whether the dictionary contains a definition with the specified object IDs and the respective hitbox.</summary>
        /// <param name="definition">The <seealso cref="ObjectHitboxDefinition"/> to check if it is contained.</param>
        public bool Contains(ObjectHitboxDefinition definition) => list.Contains(definition);
        /// <summary>Determines whether the dictionary contains a key with the specified object IDs.</summary>
        /// <param name="objectIDs">The object IDs of the key.</param>
        public bool ContainsKey(List<int> objectIDs) => list.Any(d => d.ObjectIDs.ContainsAll(objectIDs));

        /// <summary>Removes the dictionary entry with the specified object IDs.</summary>
        /// <param name="objectIDs">The object IDs of the key of the entry to remove.</param>
        public bool Remove(List<int> objectIDs) => Remove(Find(objectIDs));
        /// <summary>Finds the dictionary entry that has a specified hitbox for value and removes the specified object IDs from its definition.</summary>
        /// <param name="objectIDs">The object IDs to remove from the definition.</param>
        /// <param name="hitbox">The hitbox of the entry whose object IDs to remove.</param>
        public bool Remove(List<int> objectIDs, Hitbox hitbox)
        {
            var d = Find(objectIDs);
            if (d.Hitbox == hitbox)
            {
                RemoveFromAllIDs(objectIDs);
                return list.Remove(d);
            }
            return false;
        }
        /// <summary>Finds the dictionary entry that has a specified hitbox for value and removes the specified object IDs from its definition based on a key/value pair.</summary>
        /// <param name="entry">The key/value pair that contains the information to determine the entry.</param>
        public bool Remove(KeyValuePair<List<int>, Hitbox> entry) => Remove(entry.Key, entry.Value);
        /// <summary>Removes the specified <seealso cref="ObjectHitboxDefinition"/> from the dictionary.</summary>
        /// <param name="definition">The <seealso cref="ObjectHitboxDefinition"/> to remove from the dictionary.</param>
        public bool Remove(ObjectHitboxDefinition definition)
        {
            bool result = list.Remove(definition);
            if (result)
                RemoveFromAllIDs(definition.ObjectIDs);
            return result;
        }
        /// <summary>Removes the definition of a specified object ID. If the associated entry contains no other elements, it is also removed from the dictionary altogether.</summary>
        /// <param name="objectID">The object ID whose definition to remove.</param>
        public bool RemoveDefinition(int objectID)
        {
            foreach (var d in list)
                if (d.ObjectIDs.Remove(objectID))
                {
                    allIDs?.Remove(objectID);
                    if (d.ObjectIDs.Count == 0)
                        list.Remove(d);
                    return true;
                }
            return false;
        }
        /// <summary>Removes the definitions of the specified object IDs. If the associated entries contain no other elements, they are also removed from the dictionary altogether.</summary>
        /// <param name="objectIDs">The object IDs whose definitions to remove.</param>
        public void RemoveMultipleDefinitions(List<int> objectIDs)
        {
            var c = objectIDs.Clone();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                for (int j = c.Count - 1; j >= 0; j--)
                    if (list[i].ObjectIDs.Remove(c[j]))
                        c.RemoveAt(j);
                if (list[i].ObjectIDs.Count == 0)
                    list.RemoveAt(i);
            }
            RemoveFromAllIDs(objectIDs);
        }

        // Absolutely disgusting code that should not be necessary
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);
        IEnumerator<ObjectHitboxDefinition> IEnumerable<ObjectHitboxDefinition>.GetEnumerator() => new Enumerator(this);
        IEnumerator<KeyValuePair<List<int>, Hitbox>> IEnumerable<KeyValuePair<List<int>, Hitbox>>.GetEnumerator() => new KVPEnumerator(this);

        /// <summary>A useless function that nobody will and should ever touch.</summary>
        /// <param name="target">Seriously I'm wasting more time writing this documentation than actually implementing the function.</param>
        /// <param name="startIndex">Why would I give up? It takes less brain power to generate this "documentation".</param>
        public void CopyTo(KeyValuePair<List<int>, Hitbox>[] target, int startIndex) { }
        /// <summary>A useless function that nobody will and should ever touch. Version 2.0b</summary>
        /// <param name="target">Seriously I'm wasting more time writing this documentation than actually implementing the function.</param>
        /// <param name="startIndex">Why would I give up? It takes less brain power to generate this "documentation".</param>
        public void CopyTo(ObjectHitboxDefinition[] target, int startIndex) { }

        /// <summary>Attempts to get the value of a specified key. Returns <see langword="true"/> if the operation succeeded, otherwise <see langword="false"/>.</summary>
        /// <param name="key">The key to try to get the value of in this <seealso cref="ObjectHitboxDefinitionDictionary"/>.</param>
        /// <param name="value">The value of the key. If the operation failed, <see langword="null"/> is returned.</param>
        public bool TryGetValue(List<int> key, out Hitbox value)
        {
            try
            {
                value = GetCommonHitbox(key);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        /// <summary>Gets the <seealso cref="ObjectHitboxDefinition"/> that contains a specified object ID.</summary>
        /// <param name="objectID">The object ID that is contained in the returned <seealso cref="ObjectHitboxDefinition"/>.</param>
        public ObjectHitboxDefinition GetObjectHitboxDefinition(int objectID)
        {
            foreach (var d in list)
                if (d.ObjectIDs.Contains(objectID))
                    return d;
            throw new KeyNotFoundException("The selected object ID could not be addressed in the current dictionary.");
        }
        /// <summary>Gets the <seealso cref="Hitbox"/> of a specified object ID.</summary>
        /// <param name="objectID">The object ID whose <seealso cref="Hitbox"/> to get.</param>
        public Hitbox GetHitbox(int objectID) => GetObjectHitboxDefinition(objectID).Hitbox;
        /// <summary>Sets the <seealso cref="Hitbox"/> of a specified object ID.</summary>
        /// <param name="objectID">The object ID whose <seealso cref="Hitbox"/> to set.</param>
        /// <param name="hitbox">The <seealso cref="Hitbox"/> to set.</param>
        public void SetHitbox(int objectID, Hitbox hitbox)
        {
            RemoveDefinition(objectID);
            Add(objectID, hitbox);
        }
        /// <summary>Gets the <seealso cref="Hitbox"/> of the specified object IDs. Returns <see langword="null"/> if the object IDs' <seealso cref="Hitbox"/> is not common. Throws respective exceptions on other errors.</summary>
        /// <param name="objectIDs">The object IDs whose common <seealso cref="Hitbox"/> to get.</param>
        public Hitbox GetCommonHitbox(List<int> objectIDs) => GetCommonObjectHitboxDefinition(objectIDs).Hitbox;
        /// <summary>Sets the <seealso cref="Hitbox"/> of a specified object ID. Returns <see langword="null"/> if the object IDs' <seealso cref="Hitbox"/> is not common. Throws respective exceptions on other errors.</summary>
        /// <param name="objectIDs">The object ID whose <seealso cref="Hitbox"/> to set.</param>
        /// <param name="hitbox">The <seealso cref="Hitbox"/> to set.</param>
        public void SetCommonHitbox(List<int> objectIDs, Hitbox hitbox)
        {
            RemoveMultipleDefinitions(objectIDs);
            Add(objectIDs, hitbox);
        }

        /// <summary>Gets or sets the <seealso cref="Hitbox"/> of a specified object ID.</summary>
        /// <param name="objectID">The object ID whose <seealso cref="Hitbox"/> to get or set.</param>
        public Hitbox this[int objectID]
        {
            get => GetHitbox(objectID);
            set => SetHitbox(objectID, value);
        }
        /// <summary>Gets or sets the <seealso cref="Hitbox"/> of the specified object IDs.</summary>
        /// <param name="objectIDs">The object IDs whose <seealso cref="Hitbox"/> to get or set.</param>
        public Hitbox this[List<int> objectIDs]
        {
            get => GetCommonHitbox(objectIDs);
            set => SetCommonHitbox(objectIDs, value);
        }

        private void RemoveFromAllIDs(List<int> objectIDs)
        {
            if (allIDs != null)
                foreach (var o in objectIDs)
                    allIDs.Remove(o);
        }
        private void Validate()
        {
            if (AllObjectIDs.Count != AllObjectIDs.Distinct().Count())
                throw new InvalidOperationException("The dictionary cannot contain duplicate object ID entries.");
        }

        #region Please collapse this
        // Even more absolutely disgusting code that should not be necessary
        private class Enumerator : IEnumerator<ObjectHitboxDefinition>
        {
            private int index;
            private readonly ObjectHitboxDefinitionDictionary dictionary;

            object IEnumerator.Current => dictionary.list[index];
            public ObjectHitboxDefinition Current => dictionary.list[index];

            public Enumerator(ObjectHitboxDefinitionDictionary d) => dictionary = d;

            public bool MoveNext() => ++index < dictionary.Count;
            public void Reset() => index = 0;
            public void Dispose() { }
        }
        private class KVPEnumerator : IEnumerator<KeyValuePair<List<int>, Hitbox>>
        {
            private int index;
            private readonly ObjectHitboxDefinitionDictionary dictionary;
            private ObjectHitboxDefinition current => dictionary.list[index];

            object IEnumerator.Current => dictionary.list[index];
            public KeyValuePair<List<int>, Hitbox> Current => new(current.ObjectIDs, current.Hitbox);

            public KVPEnumerator(ObjectHitboxDefinitionDictionary d) => dictionary = d;

            public bool MoveNext() => ++index < dictionary.Count;
            public void Reset() => index = 0;
            public void Dispose() { }
        }
        #endregion
    }
}
