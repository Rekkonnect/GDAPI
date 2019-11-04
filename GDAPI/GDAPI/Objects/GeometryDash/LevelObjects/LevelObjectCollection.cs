using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Information.GeometryDash;
using GDAPI.Objects.DataStructures;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;
using GDAPI.Objects.GeometryDash.Reflection;

namespace GDAPI.Objects.GeometryDash.LevelObjects
{
    /// <summary>Represents a collection of level objects.</summary>
    public class LevelObjectCollection : IEnumerable<GeneralObject>
    {
        private int triggerCount = -1;
        private int colorTriggerCount = -1;

        private PropertyAccessInfoDictionary commonProperties;
        private PropertyAccessInfoDictionary allAvailableProperties;

        private int commonPropertiesUnevaluatedIndex;
        private int allAvailablePropertiesUnevaluatedIndex;
        private NestedLists<GeneralObject> unevaluatedObjects = new NestedLists<GeneralObject>();

        private List<GeneralObject> objects;

        /// <summary>The count of the level objects in the collection.</summary>
        public int Count => objects.Count;

        /// <summary>The list of objects in the collection.</summary>
        public List<GeneralObject> Objects
        {
            get => objects;
            set
            {
                ResetCounters();
                objects = value;
                ObjectCounts.Clear();
                GroupCounts.Clear();
                ClearPropertyCache();
            }
        }

        /// <summary>The count of all the triggers in the collection (excludes Start Pos).</summary>
        public int TriggerCount
        {
            get
            {
                if (triggerCount == -1)
                {
                    triggerCount = 0;
                    foreach (var kvp in ObjectCounts)
                        if (ObjectLists.TriggerList.Contains(kvp.Key))
                            triggerCount += kvp.Value;
                }
                return triggerCount;
            }
        }
        /// <summary>The count of all the color triggers in the collection.</summary>
        public int ColorTriggerCount
        {
            get
            {
                // TODO: Simplify this like the TriggerCount property
                if (colorTriggerCount == -1)
                {
                    colorTriggerCount = ObjectCounts.ValueOrDefault((int)TriggerType.Color);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.BG);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.GRND);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.GRND2);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Line);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Obj);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.ThreeDL);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Color1);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Color2);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Color3);
                    colorTriggerCount += ObjectCounts.ValueOrDefault((int)TriggerType.Color4);
                }
                return colorTriggerCount;
            }
        }
        /// <summary>Contains the count of objects per object ID in the collection.</summary>
        public Dictionary<int, int> ObjectCounts { get; private set; } = new Dictionary<int, int>();
        /// <summary>Contains the count of groups per object ID in the collection.</summary>
        public Dictionary<int, int> GroupCounts { get; private set; } = new Dictionary<int, int>();
        /// <summary>The different object IDs in the collection.</summary>
        public int DifferentObjectIDCount => ObjectCounts.Keys.Count;
        /// <summary>The different object IDs in the collection.</summary>
        public int[] DifferentObjectIDs => ObjectCounts.Keys.ToArray();
        /// <summary>The group IDs in the collection.</summary>
        public int[] UsedGroupIDs => GroupCounts.Keys.ToArray();
        #region Trigger info
        public int MoveTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Move);
        public int StopTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Stop);
        public int PulseTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Pulse);
        public int AlphaTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Alpha);
        public int ToggleTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Toggle);
        public int SpawnTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Spawn);
        public int CountTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Count);
        public int InstantCountTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.InstantCount);
        public int PickupTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Pickup);
        public int FollowTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Follow);
        public int FollowPlayerYTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.FollowPlayerY);
        public int TouchTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Touch);
        public int AnimateTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Animate);
        public int RotateTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Rotate);
        public int ShakeTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Shake);
        public int CollisionTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.Collision);
        public int OnDeathTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.OnDeath);
        public int HidePlayerTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.HidePlayer);
        public int ShowPlayerTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.ShowPlayer);
        public int DisableTrailTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.DisableTrail);
        public int EnableTrailTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.EnableTrail);
        public int BGEffectOnTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.BGEffectOn);
        public int BGEffectOffTriggersCount => ObjectCounts.ValueOrDefault((int)TriggerType.BGEffectOff);
        #endregion

        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectCollection"/> class.</summary>
        public LevelObjectCollection()
        {
            Objects = new List<GeneralObject>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectCollection"/> class.</summary>
        /// <param name="obj">The object to use.</param>
        public LevelObjectCollection(GeneralObject obj)
        {
            Objects = new List<GeneralObject> { obj };
        }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectCollection"/> class.</summary>
        /// <param name="objects">The list of objects to use.</param>
        public LevelObjectCollection(List<GeneralObject> objects)
        {
            Objects = objects;
        }

        /// <summary>Adds an object to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="o">The object to add.</param>
        public LevelObjectCollection Add(GeneralObject o)
        {
            AddToCounters(o);
            objects.Add(o);
            RegisterUnevaluatedObject(o);
            return this;
        }
        /// <summary>Adds a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="addedObjects">The objects to add.</param>
        public LevelObjectCollection AddRange(IEnumerable<GeneralObject> addedObjects)
        {
            AddToCounters(addedObjects);
            objects.AddRange(addedObjects);
            RegisterUnevaluatedObjects(addedObjects);
            return this;
        }
        /// <summary>Adds a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to add.</param>
        public LevelObjectCollection AddRange(LevelObjectCollection objects) => AddRange(objects.Objects);
        /// <summary>Adds a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to add.</param>
        public LevelObjectCollection AddRange(params GeneralObject[] objects) => AddRange((IEnumerable<GeneralObject>)objects);
        /// <summary>Inserts an object to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index to insert the object at.</param>
        /// <param name="o">The object to insert.</param>
        public LevelObjectCollection Insert(int index, GeneralObject o)
        {
            AddToCounters(o);
            objects.Insert(index, o);
            RegisterUnevaluatedObject(o);
            return this;
        }
        /// <summary>Inserts a collection of objects to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index of the first object to insert at.</param>
        /// <param name="insertedObjects">The objects to insert.</param>
        public LevelObjectCollection InsertRange(int index, List<GeneralObject> insertedObjects)
        {
            AddToCounters(insertedObjects);
            objects.InsertRange(index, insertedObjects);
            RegisterUnevaluatedObjects(insertedObjects);
            return this;
        }
        /// <summary>Inserts a collection of objects to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index of the first object to insert at.</param>
        /// <param name="objects">The objects to insert.</param>
        public LevelObjectCollection InsertRange(int index, LevelObjectCollection objects) => InsertRange(index, objects.Objects);
        /// <summary>Removes an object from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="o">The object to remove.</param>
        public LevelObjectCollection Remove(GeneralObject o)
        {
            RemoveFromCounters(o);
            objects.Remove(o);
            ClearPropertyCache();
            return this;
        }
        /// <summary>Removes an object from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index of the object to remove.</param>
        public LevelObjectCollection RemoveAt(int index)
        {
            RemoveFromCounters(objects[index]);
            objects.RemoveAt(index);
            ClearPropertyCache();
            return this;
        }
        /// <summary>Removes a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="removedObjects">The objects to remove.</param>
        public LevelObjectCollection RemoveRange(List<GeneralObject> removedObjects)
        {
            foreach (var o in removedObjects)
            {
                RemoveFromCounters(o);
                objects.Remove(o);
            }
            ClearPropertyCache();
            return this;
        }
        /// <summary>Removes a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to remove.</param>
        public LevelObjectCollection RemoveRange(LevelObjectCollection objects) => RemoveRange(objects.Objects);
        /// <summary>Clears the <seealso cref="LevelObjectCollection"/>.</summary>
        public LevelObjectCollection Clear()
        {
            ObjectCounts.Clear();
            GroupCounts.Clear();
            objects.Clear();
            SetPropertyCacheToDefault();
            return this;
        }
        /// <summary>Clones the <seealso cref="LevelObjectCollection"/> and returns the cloned instance.</summary>
        public LevelObjectCollection Clone()
        {
            var result = new LevelObjectCollection();
            result.ObjectCounts = ObjectCounts.Clone();
            result.GroupCounts = GroupCounts.Clone();
            result.objects = objects.Clone();
            result.allAvailableProperties = new PropertyAccessInfoDictionary(allAvailableProperties);
            result.commonProperties = new PropertyAccessInfoDictionary(commonProperties);
            return result;
        }

        /// <summary>Attempts to get the common value of an object property from this collection of objects given its ID.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <param name="common">The common value of the property.</param>
        public bool TryGetCommonPropertyWithID<T>(int ID, out T common) => GeneralObject.TryGetCommonPropertyWithID(this, ID, out common);
        /// <summary>Attempts to set the common value of an object property from this collection of objects given its ID.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <param name="newValue">The new value of the property to set to all the objects.</param>
        public bool TrySetCommonPropertyWithID<T>(int ID, T newValue) => GeneralObject.TrySetCommonPropertyWithID(this, ID, newValue);

        /// <summary>Returns a <seealso cref="LevelObjectCollection"/> that contains the objects that have a group ID equal to the provided value.</summary>
        /// <param name="groupID">The group ID of the objects to look for.</param>
        public LevelObjectCollection GetObjectsByGroupID(int groupID)
        {
            var result = new LevelObjectCollection();
            foreach (var o in objects)
                if (o.GroupIDs.Contains(groupID))
                    result.Add(o);
            return result;
        }
        /// <summary>Returns a <seealso cref="LevelObjectCollection"/> that contains the objects that have a main or detail color ID equal to the provided value.</summary>
        /// <param name="colorID">The color ID of the objects to look for.</param>
        public LevelObjectCollection GetObjectsByColorID(int colorID)
        {
            var result = new LevelObjectCollection();
            foreach (var o in objects)
                if (o.Color1ID == colorID || o.Color2ID == colorID)
                    result.Add(o);
            return result;
        }

        #region Object Property Metadata
        /// <summary>Returns the common object properties found in this <seealso cref="LevelObjectCollection"/>.</summary>
        public PropertyAccessInfoDictionary GetCommonProperties()
        {
            if (commonProperties == null)
                commonProperties = GeneralObject.GetCommonProperties(this);
            else
            {
                for (; commonPropertiesUnevaluatedIndex < unevaluatedObjects.ListCount; commonPropertiesUnevaluatedIndex++)
                    commonProperties = GeneralObject.GetCommonProperties(unevaluatedObjects[commonPropertiesUnevaluatedIndex], commonProperties);
                RemoveEvaluatedObjects();
            }
            return commonProperties;
        }
        /// <summary>Returns all the available object properties found in this <seealso cref="LevelObjectCollection"/>.</summary>
        public PropertyAccessInfoDictionary GetAllAvailableProperties()
        {
            if (allAvailableProperties == null)
                allAvailableProperties = GeneralObject.GetAllAvailableProperties(this);
            else
            {
                for (; allAvailablePropertiesUnevaluatedIndex < unevaluatedObjects.ListCount; allAvailablePropertiesUnevaluatedIndex++)
                    allAvailableProperties = GeneralObject.GetAllAvailableProperties(unevaluatedObjects[allAvailablePropertiesUnevaluatedIndex], allAvailableProperties);
                RemoveEvaluatedObjects();
            }
            return allAvailableProperties;
        }

        private void RemoveEvaluatedObjects()
        {
            int count = Math.Min(commonPropertiesUnevaluatedIndex, allAvailablePropertiesUnevaluatedIndex);
            unevaluatedObjects.RemoveFirst(count);
            commonPropertiesUnevaluatedIndex -= count;
            allAvailablePropertiesUnevaluatedIndex -= count;
        }
        #endregion

        #region Dictionaries
        // Keep in mind, those functions' performance is really low
        /// <summary>Returns a <seealso cref="Dictionary{TKey, TValue}"/> that categorizes the level objects in this <seealso cref="LevelObjectCollection"/> based on their main color ID.</summary>
        public Dictionary<int, LevelObjectCollection> GetMainColorIDObjectDictionary() => GetObjectDictionary(o => o.Color1ID);
        /// <summary>Returns a <seealso cref="Dictionary{TKey, TValue}"/> that categorizes the level objects in this <seealso cref="LevelObjectCollection"/> based on their detail color ID.</summary>
        public Dictionary<int, LevelObjectCollection> GetDetailColorIDObjectDictionary() => GetObjectDictionary(o => o.Color2ID);
        /// <summary>Returns a <seealso cref="Dictionary{TKey, TValue}"/> that categorizes the level objects in this <seealso cref="LevelObjectCollection"/> based on their main and detail color IDs.</summary>
        public Dictionary<int, LevelObjectCollection> GetColorIDObjectDictionary() => GetObjectDictionary(o => (IEnumerable<int>)new List<int> { o.Color1ID, o.Color2ID });
        /// <summary>Returns a <seealso cref="Dictionary{TKey, TValue}"/> that categorizes the level objects in this <seealso cref="LevelObjectCollection"/> based on their group IDs.</summary>
        public Dictionary<int, LevelObjectCollection> GetGroupIDObjectDictionary() => GetObjectDictionary(o => (IEnumerable<int>)o.GroupIDs);

        /// <summary>Returns a <seealso cref="Dictionary{TKey, TValue}"/> that categorizes the level objects in this <seealso cref="LevelObjectCollection"/> based on a selector.</summary>
        /// <param name="selector">The selector function to categorize this <seealso cref="LevelObjectCollection"/>'s objects in the dictionary.</param>
        public Dictionary<TKey, LevelObjectCollection> GetObjectDictionary<TKey>(Func<GeneralObject, TKey> selector)
        {
            var result = new Dictionary<TKey, LevelObjectCollection>();
            foreach (var o in objects)
                HandleEntryInsertion(result, selector(o), o);
            return result;
        }
        /// <summary>Returns a <seealso cref="Dictionary{TKey, TValue}"/> that categorizes the level objects in this <seealso cref="LevelObjectCollection"/> based on a multiple key selector.</summary>
        /// <param name="selector">The selector function to categorize this <seealso cref="LevelObjectCollection"/>'s objects in the dictionary. Each of the returned keys will contain this object.</param>
        public Dictionary<TKey, LevelObjectCollection> GetObjectDictionary<TKey>(Func<GeneralObject, IEnumerable<TKey>> selector)
        {
            var result = new Dictionary<TKey, LevelObjectCollection>();
            foreach (var o in objects)
                foreach (var key in selector(o))
                    HandleEntryInsertion(result, key, o);
            return result;
        }
        #endregion

        /// <summary>Gets or sets the level object at the specified index.</summary>
        /// <param name="index">The index of the level object.</param>
        public GeneralObject this[int index]
        {
            get => objects[index];
            set => objects[index] = value;
        }

        public IEnumerator<GeneralObject> GetEnumerator() => objects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void AddToCounters(IEnumerable<GeneralObject> objects)
        {
            foreach (var o in objects)
                AddToCounters(o);
        }
        private void AddToCounters(GeneralObject o)
        {
            AdjustCounters(o, 1);
            ObjectCounts.IncrementOrAddKeyValue(o.ObjectID);
            foreach (var g in o.GroupIDs)
                GroupCounts.IncrementOrAddKeyValue(g);
        }
        private void RemoveFromCounters(GeneralObject o)
        {
            AdjustCounters(o, -1);
            ObjectCounts[o.ObjectID]--;
            foreach (var g in o.GroupIDs)
                GroupCounts[g]--;
        }
        private void AdjustCounters(GeneralObject o, int adjustment)
        {
            switch (o)
            {
                case ColorTrigger _:
                    if (colorTriggerCount > -1)
                        colorTriggerCount += adjustment;
                    break;
                case Trigger _:
                    if (triggerCount > -1)
                        triggerCount += adjustment;
                    break;
            }
        }
        private void ResetCounters()
        {
            colorTriggerCount = -1;
            triggerCount = -1;
        }

        private void RegisterUnevaluatedObject(GeneralObject o)
        {
            if (ShouldRegisterUnevaluatedObjects())
                unevaluatedObjects.Add(o);
        }
        private void RegisterUnevaluatedObjects(IEnumerable<GeneralObject> objects)
        {
            if (ShouldRegisterUnevaluatedObjects())
                unevaluatedObjects.Add(objects);
        }
        private bool ShouldRegisterUnevaluatedObjects() => commonProperties != null || allAvailableProperties != null;
        private void SetPropertyCacheToDefault()
        {
            commonProperties = new PropertyAccessInfoDictionary();
            allAvailableProperties = new PropertyAccessInfoDictionary();
            ResetUnevaluatedObjects();
        }
        private void ClearPropertyCache()
        {
            commonProperties = null;
            allAvailableProperties = null;
            ResetUnevaluatedObjects();
        }
        private void ResetUnevaluatedObjects()
        {
            unevaluatedObjects.Clear();
            commonPropertiesUnevaluatedIndex = 0;
            allAvailablePropertiesUnevaluatedIndex = 0;
        }

        private static void HandleEntryInsertion<TKey>(Dictionary<TKey, LevelObjectCollection> dictionary, TKey key, GeneralObject o)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(o);
            else
                dictionary.Add(key, new LevelObjectCollection(o));
        }

        /// <summary>Returns a <see langword="string"/> that represents the current object.</summary>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (var o in objects)
                s.Append($"{o};");
            return s.ToString();
        }
    }
}
