using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Information.GeometryDash;
using GDAPI.Objects.DataStructures;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;
using GDAPI.Objects.GeometryDash.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using static GDAPI.Information.GeometryDash.LevelObjectInformation;
using static System.Convert;

namespace GDAPI.Objects.GeometryDash.LevelObjects
{
    /// <summary>Represents a collection of level objects.</summary>
    public class LevelObjectCollection : IList<GeneralObject>
    {
        private int triggerCount = -1;
        private int colorTriggerCount = -1;

        private PropertyAccessInfoDictionary commonProperties;
        private PropertyAccessInfoDictionary allAvailableProperties;

        private int commonPropertiesUnevaluatedIndex;
        private int allAvailablePropertiesUnevaluatedIndex;
        private NestedLists<GeneralObject> unevaluatedObjects = new NestedLists<GeneralObject>();

        private List<GeneralObject> objects;

        bool ICollection<GeneralObject>.IsReadOnly => false;

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
        /// <param name="objects">The <seealso cref="IEnumerable{T}"/> of objects to use.</param>
        public LevelObjectCollection(IEnumerable<GeneralObject> objects) : this(objects.ToList()) { }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectCollection"/> class.</summary>
        /// <param name="objects">The <seealso cref="List{T}"/> of objects to use.</param>
        public LevelObjectCollection(List<GeneralObject> objects)
        {
            Objects = objects;
        }

        /// <summary>Adds an object to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="o">The object to add.</param>
        public void Add(GeneralObject o)
        {
            AddToCounters(o);
            objects.Add(o);
            RegisterUnevaluatedObject(o);
        }
        /// <summary>Adds a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="addedObjects">The objects to add.</param>
        public void AddRange(IEnumerable<GeneralObject> addedObjects)
        {
            AddToCounters(addedObjects);
            objects.AddRange(addedObjects);
            RegisterUnevaluatedObjects(addedObjects);
        }
        /// <summary>Adds a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to add.</param>
        public void AddRange(LevelObjectCollection objects) => AddRange(objects.Objects);
        /// <summary>Adds a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to add.</param>
        public void AddRange(params GeneralObject[] objects) => AddRange((IEnumerable<GeneralObject>)objects);
        /// <summary>Inserts an object to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index to insert the object at.</param>
        /// <param name="o">The object to insert.</param>
        public void Insert(int index, GeneralObject o)
        {
            AddToCounters(o);
            objects.Insert(index, o);
            RegisterUnevaluatedObject(o);
        }
        /// <summary>Inserts a collection of objects to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index of the first object to insert at.</param>
        /// <param name="insertedObjects">The objects to insert.</param>
        public void InsertRange(int index, List<GeneralObject> insertedObjects)
        {
            AddToCounters(insertedObjects);
            objects.InsertRange(index, insertedObjects);
            RegisterUnevaluatedObjects(insertedObjects);
        }
        /// <summary>Inserts a collection of objects to the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index of the first object to insert at.</param>
        /// <param name="objects">The objects to insert.</param>
        public void InsertRange(int index, LevelObjectCollection objects) => InsertRange(index, objects.Objects);
        /// <summary>Removes an object from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="o">The object to remove.</param>
        public bool Remove(GeneralObject o)
        {
            RemoveFromCounters(o);
            bool result = objects.Remove(o);
            ClearPropertyCache();
            return result;
        }
        /// <summary>Removes an object from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="index">The index of the object to remove.</param>
        public void RemoveAt(int index)
        {
            RemoveFromCounters(objects[index]);
            objects.RemoveAt(index);
            ClearPropertyCache();
        }
        /// <summary>Removes a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="removedObjects">The objects to remove.</param>
        public void RemoveRange(List<GeneralObject> removedObjects)
        {
            foreach (var o in removedObjects)
            {
                RemoveFromCounters(o);
                objects.Remove(o);
            }
            ClearPropertyCache();
        }
        /// <summary>Removes a collection of objects from the <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objects">The objects to remove.</param>
        public void RemoveRange(LevelObjectCollection objects) => RemoveRange(objects.Objects);
        /// <summary>Clears the <seealso cref="LevelObjectCollection"/>.</summary>
        public void Clear()
        {
            ObjectCounts.Clear();
            GroupCounts.Clear();
            objects.Clear();
            SetPropertyCacheToDefault();
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

        /// <summary>Determines whether an object is contained in this <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="obj">The object to be searched.</param>
        public bool Contains(GeneralObject obj) => objects.Contains(obj);
        /// <summary>Returns the index of the first instance of an object if it is contained in this <seealso cref="LevelObjectCollection"/>, otherwise -1.</summary>
        /// <param name="obj">The object to be searched.</param>
        public int IndexOf(GeneralObject obj) => objects.IndexOf(obj);

        /// <summary>Copies this <seealso cref="LevelObjectCollection"/> into an array starting at the specified index.</summary>
        /// <param name="array">The array to copy this <seealso cref="LevelObjectCollection"/>'s elements into.</param>
        /// <param name="arrayIndex">The starting index in the array of the first element from this <seealso cref="LevelObjectCollection"/>.</param>
        public void CopyTo(GeneralObject[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
                array[arrayIndex + i] = this[i];
        }

        #region Location
        /// <summary>Gets the median point of this <seealso cref="LevelObjectCollection"/>'s objects.</summary>
        public Point GetMedianPoint()
        {
            Point result = new Point();
            foreach (var o in this)
                result += o.Location;
            return result / Count;
        }
        /// <summary>Applies an offset to each of this <seealso cref="LevelObjectCollection"/>'s objects' location.</summary>
        public void ApplyLocationOffset(Point offset)
        {
            foreach (var o in this)
                o.Location += offset;
        }
        #endregion

        /// <summary>Attempts to get the common value of an object property from this collection of objects given its ID.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <param name="common">The common value of the property.</param>
        public bool TryGetCommonPropertyWithID<T>(int ID, out T common)
        {
            common = default;
            if (Count == 0)
                return false;

            // Get the property that will be retrieved
            var property = GetPropertyAccessInfo(ID, out int objectIndex);

            if (property == null)
                return false;

            this[objectIndex].TryGetPropertyValue(property, out common);

            for (int i = objectIndex + 1; i < Count; i++)
            {
                var p = this[i].GetPropertyAccessInfo(ID);
                if (p == null)
                    continue;
                if (!this[i].TryGetPropertyValue(p, out T compared))
                    continue; // Ignore objects that do not contain that property
                if (!common.Equals(compared))
                    return false;
            }
            return true;
        }
        /// <summary>Attempts to set the common value of an object property from this collection of objects given its ID.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <param name="newValue">The new value of the property to set to all the objects.</param>
        public bool TrySetCommonPropertyWithID<T>(int ID, T newValue)
        {
            if (Count == 0)
                return false;

            // Store the property that will be changed
            var property = GetPropertyAccessInfo(ID, out _);

            if (property == null)
                return false;

            foreach (var o in this)
                o.TrySetPropertyValue(o.GetPropertyAccessInfo(ID), newValue);
            return true;
        }
        /// <summary>Attempts to get the common value of an object property from this collection of objects given its ID.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <param name="common">The common value of the property.</param>
        public bool TryGetCommonPropertyWithID<T>(ObjectProperty ID, out T common) => TryGetCommonPropertyWithID((int)ID, out common);
        /// <summary>Attempts to set the common value of an object property from this collection of objects given its ID.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <param name="newValue">The new value of the property to set to all the objects.</param>
        public bool TrySetCommonPropertyWithID<T>(ObjectProperty ID, T newValue) => TrySetCommonPropertyWithID((int)ID, newValue);
        /// <summary>Gets the common value of an object property from this collection of objects given its ID. Throws an <seealso cref="InvalidOperationException"/> if the retrieval failed.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <exception cref="InvalidOperationException"/>
        public T GetCommonPropertyWithID<T>(int ID)
        {
            if (TryGetCommonPropertyWithID(ID, out T result))
                return result;
            throw new InvalidOperationException($"Cannot get the common property with ID {ID}.");
        }
        /// <summary>Sets the common value of an object property from this collection of objects given its ID. Throws an <seealso cref="InvalidOperationException"/> if the retrieval failed.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <param name="newValue">The new value of the property to set to all the objects.</param>
        /// <exception cref="InvalidOperationException"/>
        public void SetCommonPropertyWithID<T>(int ID, T newValue)
        {
            if (!TrySetCommonPropertyWithID(ID, newValue))
                throw new InvalidOperationException($"Cannot set the common property with ID {ID}.");
        }
        /// <summary>Gets the common value of an object property from this collection of objects given its ID. Throws an <seealso cref="InvalidOperationException"/> if the retrieval failed.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <exception cref="InvalidOperationException"/>
        public T GetCommonPropertyWithID<T>(ObjectProperty ID) => GetCommonPropertyWithID<T>((int)ID);
        /// <summary>Sets the common value of an object property from this collection of objects given its ID. Throws an <seealso cref="InvalidOperationException"/> if the retrieval failed.</summary>
        /// <typeparam name="T">The value type of the property.</typeparam>
        /// <param name="ID">The ID of the property.</param>
        /// <param name="newValue">The new value of the property to set to all the objects.</param>
        /// <exception cref="InvalidOperationException"/>
        public void SetCommonPropertyWithID<T>(ObjectProperty ID, T newValue) => SetCommonPropertyWithID((int)ID, newValue);

        /// <summary>Gets a <seealso cref="GeneralObject"/> instance containing all the common properties of this <seealso cref="LevelObjectCollection"/>'s objects, for a specific object ID.</summary>
        /// <param name="objectID">The object ID whose common object properties to get.</param>
        /// <returns>The common <seealso cref="GeneralObject"/> containing the common properties.</returns>
        public GeneralObject GetCommonObject(short objectID)
        {
            var common = LevelObjectFactory.GetNewObjectInstance(objectID);

            for (int i = Count; i >= 0; i--)
                if (this[i].ObjectID != objectID)
                    RemoveAt(i);
            if (Count > 1)
            {
                var properties = common.GetType().GetProperties();
                foreach (var p in properties)
                    if (p.GetCustomAttributes<ObjectStringMappableAttribute>(false).Any())
                    {
                        var v = p.GetValue(this[0]);
                        bool isCommon = true;
                        foreach (var o in this)
                            if (isCommon = (p.GetValue(o) != v))
                                break;
                        if (isCommon)
                            p.SetValue(common, v);
                    }
                return common;
            }
            else if (Count == 1)
                return this[0];
            else
                return null;
        }

        private PropertyAccessInfo GetPropertyAccessInfo(int ID, out int objectIndex)
        {
            var failedTypes = new HashSet<Type>();
            PropertyAccessInfo property = null;
            for (objectIndex = 0; objectIndex < Count; objectIndex++)
            {
                var obj = this[objectIndex];
                var type = obj.GetType();
                if (failedTypes.Contains(type))
                    continue;

                property = obj.GetPropertyAccessInfo(ID);

                if (property == null)
                    failedTypes.Add(type);
                else
                    break;
            }
            return property;
        }

        #region Object Properties
        // The code below was proudly automatically generated
        /// <summary>Gets or sets the common Object ID property of the objects in this collection.</summary>
        public int CommonObjectID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.ObjectID);
            set => SetCommonPropertyWithID(ObjectProperty.ObjectID, value);
        }
        /// <summary>Gets or sets the common X property of the objects in this collection.</summary>
        public double CommonX
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.X);
            set => SetCommonPropertyWithID(ObjectProperty.X, value);
        }
        /// <summary>Gets or sets the common Y property of the objects in this collection.</summary>
        public double CommonY
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Y);
            set => SetCommonPropertyWithID(ObjectProperty.Y, value);
        }
        /// <summary>Gets or sets the common Flipped Horizontally property of the objects in this collection.</summary>
        public bool CommonFlippedHorizontally
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.FlippedHorizontally);
            set => SetCommonPropertyWithID(ObjectProperty.FlippedHorizontally, value);
        }
        /// <summary>Gets or sets the common Flipped Vertically property of the objects in this collection.</summary>
        public bool CommonFlippedVertically
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.FlippedVertically);
            set => SetCommonPropertyWithID(ObjectProperty.FlippedVertically, value);
        }
        /// <summary>Gets or sets the common Rotation property of the objects in this collection.</summary>
        public double CommonRotation
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Rotation);
            set => SetCommonPropertyWithID(ObjectProperty.Rotation, value);
        }
        /// <summary>Gets or sets the common Red property of the objects in this collection.</summary>
        public int CommonRed
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Red);
            set => SetCommonPropertyWithID(ObjectProperty.Red, value);
        }
        /// <summary>Gets or sets the common Green property of the objects in this collection.</summary>
        public int CommonGreen
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Green);
            set => SetCommonPropertyWithID(ObjectProperty.Green, value);
        }
        /// <summary>Gets or sets the common Blue property of the objects in this collection.</summary>
        public int CommonBlue
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Blue);
            set => SetCommonPropertyWithID(ObjectProperty.Blue, value);
        }
        /// <summary>Gets or sets the common Duration property of the objects in this collection.</summary>
        public double CommonDuration
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Duration);
            set => SetCommonPropertyWithID(ObjectProperty.Duration, value);
        }
        /// <summary>Gets or sets the common Touch Triggered property of the objects in this collection.</summary>
        public bool CommonTouchTriggered
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.TouchTriggered);
            set => SetCommonPropertyWithID(ObjectProperty.TouchTriggered, value);
        }
        /// <summary>Gets or sets the common Secret Coin ID property of the objects in this collection.</summary>
        public int CommonSecretCoinID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.SecretCoinID);
            set => SetCommonPropertyWithID(ObjectProperty.SecretCoinID, value);
        }
        /// <summary>Gets or sets the common Special Object Checked property of the objects in this collection.</summary>
        public bool CommonSpecialObjectChecked
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.SpecialObjectChecked);
            set => SetCommonPropertyWithID(ObjectProperty.SpecialObjectChecked, value);
        }
        /// <summary>Gets or sets the common Tint Ground property of the objects in this collection.</summary>
        public bool CommonTintGround
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.TintGround);
            set => SetCommonPropertyWithID(ObjectProperty.TintGround, value);
        }
        /// <summary>Gets or sets the common Player Color 1 property of the objects in this collection.</summary>
        public bool CommonPlayerColor1
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.PlayerColor1);
            set => SetCommonPropertyWithID(ObjectProperty.PlayerColor1, value);
        }
        /// <summary>Gets or sets the common Player Color 2 property of the objects in this collection.</summary>
        public bool CommonPlayerColor2
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.PlayerColor2);
            set => SetCommonPropertyWithID(ObjectProperty.PlayerColor2, value);
        }
        /// <summary>Gets or sets the common Blending property of the objects in this collection.</summary>
        public bool CommonBlending
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.Blending);
            set => SetCommonPropertyWithID(ObjectProperty.Blending, value);
        }
        /// <summary>Gets or sets the common EL 1 property of the objects in this collection.</summary>
        public int CommonEL1
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.EL1);
            set => SetCommonPropertyWithID(ObjectProperty.EL1, value);
        }
        /// <summary>Gets or sets the common Color 1 ID property of the objects in this collection.</summary>
        public int CommonColor1ID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Color1ID);
            set => SetCommonPropertyWithID(ObjectProperty.Color1ID, value);
        }
        /// <summary>Gets or sets the common Color 2 ID property of the objects in this collection.</summary>
        public int CommonColor2ID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Color2ID);
            set => SetCommonPropertyWithID(ObjectProperty.Color2ID, value);
        }
        /// <summary>Gets or sets the common Target Color ID property of the objects in this collection.</summary>
        public int CommonTargetColorID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.TargetColorID);
            set => SetCommonPropertyWithID(ObjectProperty.TargetColorID, value);
        }
        /// <summary>Gets or sets the common Z Layer property of the objects in this collection.</summary>
        public int CommonZLayer
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.ZLayer);
            set => SetCommonPropertyWithID(ObjectProperty.ZLayer, value);
        }
        /// <summary>Gets or sets the common Z Order property of the objects in this collection.</summary>
        public int CommonZOrder
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.ZOrder);
            set => SetCommonPropertyWithID(ObjectProperty.ZOrder, value);
        }
        /// <summary>Gets or sets the common Offset X property of the objects in this collection.</summary>
        public double CommonOffsetX
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.OffsetX);
            set => SetCommonPropertyWithID(ObjectProperty.OffsetX, value);
        }
        /// <summary>Gets or sets the common Offset Y property of the objects in this collection.</summary>
        public double CommonOffsetY
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.OffsetY);
            set => SetCommonPropertyWithID(ObjectProperty.OffsetY, value);
        }
        /// <summary>Gets or sets the common Easing property of the objects in this collection.</summary>
        public Easing CommonEasing
        {
            get => GetCommonPropertyWithID<Easing>(ObjectProperty.Easing);
            set => SetCommonPropertyWithID(ObjectProperty.Easing, value);
        }
        /// <summary>Gets or sets the common Text Object Text property of the objects in this collection.</summary>
        public string CommonTextObjectText
        {
            get => GetCommonPropertyWithID<string>(ObjectProperty.TextObjectText);
            set => SetCommonPropertyWithID(ObjectProperty.TextObjectText, value);
        }
        /// <summary>Gets or sets the common Scaling property of the objects in this collection.</summary>
        public double CommonScaling
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Scaling);
            set => SetCommonPropertyWithID(ObjectProperty.Scaling, value);
        }
        /// <summary>Gets or sets the common Group Parent property of the objects in this collection.</summary>
        public bool CommonGroupParent
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.GroupParent);
            set => SetCommonPropertyWithID(ObjectProperty.GroupParent, value);
        }
        /// <summary>Gets or sets the common Opacity property of the objects in this collection.</summary>
        public double CommonOpacity
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Opacity);
            set => SetCommonPropertyWithID(ObjectProperty.Opacity, value);
        }
        /// <summary>Gets or sets the common Unknown Feature 36 property of the objects in this collection.</summary>
        public bool CommonUnknownFeature36
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.UnknownFeature36);
            set => SetCommonPropertyWithID(ObjectProperty.UnknownFeature36, value);
        }
        /// <summary>Gets or sets the common Color 1 HSV Enabled property of the objects in this collection.</summary>
        public bool CommonColor1HSVEnabled
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.Color1HSVEnabled);
            set => SetCommonPropertyWithID(ObjectProperty.Color1HSVEnabled, value);
        }
        /// <summary>Gets or sets the common Color 2 HSV Enabled property of the objects in this collection.</summary>
        public bool CommonColor2HSVEnabled
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.Color2HSVEnabled);
            set => SetCommonPropertyWithID(ObjectProperty.Color2HSVEnabled, value);
        }
        /// <summary>Gets or sets the common Color 1 HSV property of the objects in this collection.</summary>
        public HSVAdjustment CommonColor1HSV
        {
            get => GetCommonPropertyWithID<HSVAdjustment>(ObjectProperty.Color1HSV);
            set => SetCommonPropertyWithID(ObjectProperty.Color1HSV, value);
        }
        /// <summary>Gets or sets the common Color 2 HSV property of the objects in this collection.</summary>
        public HSVAdjustment CommonColor2HSV
        {
            get => GetCommonPropertyWithID<HSVAdjustment>(ObjectProperty.Color2HSV);
            set => SetCommonPropertyWithID(ObjectProperty.Color2HSV, value);
        }
        /// <summary>Gets or sets the common Fade In property of the objects in this collection.</summary>
        public double CommonFadeIn
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.FadeIn);
            set => SetCommonPropertyWithID(ObjectProperty.FadeIn, value);
        }
        /// <summary>Gets or sets the common Hold property of the objects in this collection.</summary>
        public double CommonHold
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Hold);
            set => SetCommonPropertyWithID(ObjectProperty.Hold, value);
        }
        /// <summary>Gets or sets the common Fade Out property of the objects in this collection.</summary>
        public double CommonFadeOut
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.FadeOut);
            set => SetCommonPropertyWithID(ObjectProperty.FadeOut, value);
        }
        /// <summary>Gets or sets the common Pulse Mode property of the objects in this collection.</summary>
        public PulseMode CommonPulseMode
        {
            get => GetCommonPropertyWithID<PulseMode>(ObjectProperty.PulseMode);
            set => SetCommonPropertyWithID(ObjectProperty.PulseMode, value);
        }
        /// <summary>Gets or sets the common Copied Color HSV Values property of the objects in this collection.</summary>
        public HSVAdjustment CommonCopiedColorHSVValues
        {
            get => GetCommonPropertyWithID<HSVAdjustment>(ObjectProperty.CopiedColorHSVValues);
            set => SetCommonPropertyWithID(ObjectProperty.CopiedColorHSVValues, value);
        }
        /// <summary>Gets or sets the common Copied Color ID property of the objects in this collection.</summary>
        public int CommonCopiedColorID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.CopiedColorID);
            set => SetCommonPropertyWithID(ObjectProperty.CopiedColorID, value);
        }
        /// <summary>Gets or sets the common Target Group ID property of the objects in this collection.</summary>
        public int CommonTargetGroupID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.TargetGroupID);
            set => SetCommonPropertyWithID(ObjectProperty.TargetGroupID, value);
        }
        /// <summary>Gets or sets the common Target Type property of the objects in this collection.</summary>
        public PulseTargetType CommonTargetType
        {
            get => GetCommonPropertyWithID<PulseTargetType>(ObjectProperty.TargetType);
            set => SetCommonPropertyWithID(ObjectProperty.TargetType, value);
        }
        /// <summary>Gets or sets the common Yellow Teleportation Portal Distance property of the objects in this collection.</summary>
        public double CommonYellowTeleportationPortalDistance
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.YellowTeleportationPortalDistance);
            set => SetCommonPropertyWithID(ObjectProperty.YellowTeleportationPortalDistance, value);
        }
        /// <summary>Gets or sets the common Activate Group property of the objects in this collection.</summary>
        public bool CommonActivateGroup
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.ActivateGroup);
            set => SetCommonPropertyWithID(ObjectProperty.ActivateGroup, value);
        }
        /// <summary>Gets or sets the common Group IDs property of the objects in this collection.</summary>
        public int[] CommonGroupIDs
        {
            get => GetCommonPropertyWithID<int[]>(ObjectProperty.GroupIDs);
            set => SetCommonPropertyWithID(ObjectProperty.GroupIDs, value);
        }
        /// <summary>Gets or sets the common Lock To Player X property of the objects in this collection.</summary>
        public bool CommonLockToPlayerX
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.LockToPlayerX);
            set => SetCommonPropertyWithID(ObjectProperty.LockToPlayerX, value);
        }
        /// <summary>Gets or sets the common Lock To Player Y property of the objects in this collection.</summary>
        public bool CommonLockToPlayerY
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.LockToPlayerY);
            set => SetCommonPropertyWithID(ObjectProperty.LockToPlayerY, value);
        }
        /// <summary>Gets or sets the common Copy Opacity property of the objects in this collection.</summary>
        public bool CommonCopyOpacity
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.CopyOpacity);
            set => SetCommonPropertyWithID(ObjectProperty.CopyOpacity, value);
        }
        /// <summary>Gets or sets the common EL 2 property of the objects in this collection.</summary>
        public int CommonEL2
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.EL2);
            set => SetCommonPropertyWithID(ObjectProperty.EL2, value);
        }
        /// <summary>Gets or sets the common Spawn Triggered property of the objects in this collection.</summary>
        public bool CommonSpawnTriggered
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.SpawnTriggered);
            set => SetCommonPropertyWithID(ObjectProperty.SpawnTriggered, value);
        }
        /// <summary>Gets or sets the common Spawn Delay property of the objects in this collection.</summary>
        public double CommonSpawnDelay
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.SpawnDelay);
            set => SetCommonPropertyWithID(ObjectProperty.SpawnDelay, value);
        }
        /// <summary>Gets or sets the common Dont Fade property of the objects in this collection.</summary>
        public bool CommonDontFade
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.DontFade);
            set => SetCommonPropertyWithID(ObjectProperty.DontFade, value);
        }
        /// <summary>Gets or sets the common Main Only property of the objects in this collection.</summary>
        public bool CommonMainOnly
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.MainOnly);
            set => SetCommonPropertyWithID(ObjectProperty.MainOnly, value);
        }
        /// <summary>Gets or sets the common Detail Only property of the objects in this collection.</summary>
        public bool CommonDetailOnly
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.DetailOnly);
            set => SetCommonPropertyWithID(ObjectProperty.DetailOnly, value);
        }
        /// <summary>Gets or sets the common Dont Enter property of the objects in this collection.</summary>
        public bool CommonDontEnter
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.DontEnter);
            set => SetCommonPropertyWithID(ObjectProperty.DontEnter, value);
        }
        /// <summary>Gets or sets the common Degrees property of the objects in this collection.</summary>
        public int CommonDegrees
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Degrees);
            set => SetCommonPropertyWithID(ObjectProperty.Degrees, value);
        }
        /// <summary>Gets or sets the common Times 360 property of the objects in this collection.</summary>
        public int CommonTimes360
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Times360);
            set => SetCommonPropertyWithID(ObjectProperty.Times360, value);
        }
        /// <summary>Gets or sets the common Lock Object Rotation property of the objects in this collection.</summary>
        public bool CommonLockObjectRotation
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.LockObjectRotation);
            set => SetCommonPropertyWithID(ObjectProperty.LockObjectRotation, value);
        }
        /// <summary>Gets or sets the common Follow Group ID property of the objects in this collection.</summary>
        public int CommonFollowGroupID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.FollowGroupID);
            set => SetCommonPropertyWithID(ObjectProperty.FollowGroupID, value);
        }
        /// <summary>Gets or sets the common Target Pos Group ID property of the objects in this collection.</summary>
        public int CommonTargetPosGroupID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.TargetPosGroupID);
            set => SetCommonPropertyWithID(ObjectProperty.TargetPosGroupID, value);
        }
        /// <summary>Gets or sets the common Center Group ID property of the objects in this collection.</summary>
        public int CommonCenterGroupID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.CenterGroupID);
            set => SetCommonPropertyWithID(ObjectProperty.CenterGroupID, value);
        }
        /// <summary>Gets or sets the common Secondary Group ID property of the objects in this collection.</summary>
        public int CommonSecondaryGroupID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.SecondaryGroupID);
            set => SetCommonPropertyWithID(ObjectProperty.SecondaryGroupID, value);
        }
        /// <summary>Gets or sets the common X Mod property of the objects in this collection.</summary>
        public double CommonXMod
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.XMod);
            set => SetCommonPropertyWithID(ObjectProperty.XMod, value);
        }
        /// <summary>Gets or sets the common Y Mod property of the objects in this collection.</summary>
        public double CommonYMod
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.YMod);
            set => SetCommonPropertyWithID(ObjectProperty.YMod, value);
        }
        /// <summary>Gets or sets the common Strength property of the objects in this collection.</summary>
        public double CommonStrength
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Strength);
            set => SetCommonPropertyWithID(ObjectProperty.Strength, value);
        }
        /// <summary>Gets or sets the common Animation ID property of the objects in this collection.</summary>
        public int CommonAnimationID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.AnimationID);
            set => SetCommonPropertyWithID(ObjectProperty.AnimationID, value);
        }
        /// <summary>Gets or sets the common Count property of the objects in this collection.</summary>
        public int CommonCount
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Count);
            set => SetCommonPropertyWithID(ObjectProperty.Count, value);
        }
        /// <summary>Gets or sets the common Subtract Count property of the objects in this collection.</summary>
        public bool CommonSubtractCount
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.SubtractCount);
            set => SetCommonPropertyWithID(ObjectProperty.SubtractCount, value);
        }
        /// <summary>Gets or sets the common Pickup Mode property of the objects in this collection.</summary>
        public PickupItemPickupMode CommonPickupMode
        {
            get => GetCommonPropertyWithID<PickupItemPickupMode>(ObjectProperty.PickupMode);
            set => SetCommonPropertyWithID(ObjectProperty.PickupMode, value);
        }
        /// <summary>Gets or sets the common Item ID property of the objects in this collection.</summary>
        public int CommonItemID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.ItemID);
            set => SetCommonPropertyWithID(ObjectProperty.ItemID, value);
        }
        /// <summary>Gets or sets the common Block ID property of the objects in this collection.</summary>
        public int CommonBlockID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.BlockID);
            set => SetCommonPropertyWithID(ObjectProperty.BlockID, value);
        }
        /// <summary>Gets or sets the common Block A ID property of the objects in this collection.</summary>
        public int CommonBlockAID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.BlockAID);
            set => SetCommonPropertyWithID(ObjectProperty.BlockAID, value);
        }
        /// <summary>Gets or sets the common Hold Mode property of the objects in this collection.</summary>
        public bool CommonHoldMode
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.HoldMode);
            set => SetCommonPropertyWithID(ObjectProperty.HoldMode, value);
        }
        /// <summary>Gets or sets the common Toggle Mode property of the objects in this collection.</summary>
        public TouchToggleMode CommonToggleMode
        {
            get => GetCommonPropertyWithID<TouchToggleMode>(ObjectProperty.ToggleMode);
            set => SetCommonPropertyWithID(ObjectProperty.ToggleMode, value);
        }
        /// <summary>Gets or sets the common Interval property of the objects in this collection.</summary>
        public double CommonInterval
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Interval);
            set => SetCommonPropertyWithID(ObjectProperty.Interval, value);
        }
        /// <summary>Gets or sets the common Easing Rate property of the objects in this collection.</summary>
        public double CommonEasingRate
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EasingRate);
            set => SetCommonPropertyWithID(ObjectProperty.EasingRate, value);
        }
        /// <summary>Gets or sets the common Exclusive property of the objects in this collection.</summary>
        public bool CommonExclusive
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.Exclusive);
            set => SetCommonPropertyWithID(ObjectProperty.Exclusive, value);
        }
        /// <summary>Gets or sets the common Multi Trigger property of the objects in this collection.</summary>
        public bool CommonMultiTrigger
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.MultiTrigger);
            set => SetCommonPropertyWithID(ObjectProperty.MultiTrigger, value);
        }
        /// <summary>Gets or sets the common Comparison property of the objects in this collection.</summary>
        public InstantCountComparison CommonComparison
        {
            get => GetCommonPropertyWithID<InstantCountComparison>(ObjectProperty.Comparison);
            set => SetCommonPropertyWithID(ObjectProperty.Comparison, value);
        }
        /// <summary>Gets or sets the common Dual Mode property of the objects in this collection.</summary>
        public bool CommonDualMode
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.DualMode);
            set => SetCommonPropertyWithID(ObjectProperty.DualMode, value);
        }
        /// <summary>Gets or sets the common Speed property of the objects in this collection.</summary>
        public double CommonSpeed
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Speed);
            set => SetCommonPropertyWithID(ObjectProperty.Speed, value);
        }
        /// <summary>Gets or sets the common Follow Delay property of the objects in this collection.</summary>
        public double CommonFollowDelay
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.FollowDelay);
            set => SetCommonPropertyWithID(ObjectProperty.FollowDelay, value);
        }
        /// <summary>Gets or sets the common Y Offset property of the objects in this collection.</summary>
        public double CommonYOffset
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.YOffset);
            set => SetCommonPropertyWithID(ObjectProperty.YOffset, value);
        }
        /// <summary>Gets or sets the common Trigger On Exit property of the objects in this collection.</summary>
        public bool CommonTriggerOnExit
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.TriggerOnExit);
            set => SetCommonPropertyWithID(ObjectProperty.TriggerOnExit, value);
        }
        /// <summary>Gets or sets the common Dynamic Block property of the objects in this collection.</summary>
        public bool CommonDynamicBlock
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.DynamicBlock);
            set => SetCommonPropertyWithID(ObjectProperty.DynamicBlock, value);
        }
        /// <summary>Gets or sets the common Block B ID property of the objects in this collection.</summary>
        public int CommonBlockBID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.BlockBID);
            set => SetCommonPropertyWithID(ObjectProperty.BlockBID, value);
        }
        /// <summary>Gets or sets the common Disable Glow property of the objects in this collection.</summary>
        public bool CommonDisableGlow
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.DisableGlow);
            set => SetCommonPropertyWithID(ObjectProperty.DisableGlow, value);
        }
        /// <summary>Gets or sets the common Custom Rotation Speed property of the objects in this collection.</summary>
        public int CommonCustomRotationSpeed
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.CustomRotationSpeed);
            set => SetCommonPropertyWithID(ObjectProperty.CustomRotationSpeed, value);
        }
        /// <summary>Gets or sets the common Disable Rotation property of the objects in this collection.</summary>
        public bool CommonDisableRotation
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.DisableRotation);
            set => SetCommonPropertyWithID(ObjectProperty.DisableRotation, value);
        }
        /// <summary>Gets or sets the common Multi Activate property of the objects in this collection.</summary>
        public bool CommonMultiActivate
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.MultiActivate);
            set => SetCommonPropertyWithID(ObjectProperty.MultiActivate, value);
        }
        /// <summary>Gets or sets the common Enable Use Target property of the objects in this collection.</summary>
        public bool CommonEnableUseTarget
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.EnableUseTarget);
            set => SetCommonPropertyWithID(ObjectProperty.EnableUseTarget, value);
        }
        /// <summary>Gets or sets the common Target Pos Coordinates property of the objects in this collection.</summary>
        public TargetPosCoordinates CommonTargetPosCoordinates
        {
            get => GetCommonPropertyWithID<TargetPosCoordinates>(ObjectProperty.TargetPosCoordinates);
            set => SetCommonPropertyWithID(ObjectProperty.TargetPosCoordinates, value);
        }
        /// <summary>Gets or sets the common Editor Disable property of the objects in this collection.</summary>
        public bool CommonEditorDisable
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.EditorDisable);
            set => SetCommonPropertyWithID(ObjectProperty.EditorDisable, value);
        }
        /// <summary>Gets or sets the common High Detail property of the objects in this collection.</summary>
        public bool CommonHighDetail
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.HighDetail);
            set => SetCommonPropertyWithID(ObjectProperty.HighDetail, value);
        }
        /// <summary>Gets or sets the common Max Speed property of the objects in this collection.</summary>
        public double CommonMaxSpeed
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.MaxSpeed);
            set => SetCommonPropertyWithID(ObjectProperty.MaxSpeed, value);
        }
        /// <summary>Gets or sets the common Randomize Start property of the objects in this collection.</summary>
        public bool CommonRandomizeStart
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.RandomizeStart);
            set => SetCommonPropertyWithID(ObjectProperty.RandomizeStart, value);
        }
        /// <summary>Gets or sets the common Animation Speed property of the objects in this collection.</summary>
        public double CommonAnimationSpeed
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.AnimationSpeed);
            set => SetCommonPropertyWithID(ObjectProperty.AnimationSpeed, value);
        }
        /// <summary>Gets or sets the common Linked Group ID property of the objects in this collection.</summary>
        public int CommonLinkedGroupID
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.LinkedGroupID);
            set => SetCommonPropertyWithID(ObjectProperty.LinkedGroupID, value);
        }
        /// <summary>Gets or sets the common Unrevealed Text Box Feature 115 property of the objects in this collection.</summary>
        public int CommonUnrevealedTextBoxFeature115
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.UnrevealedTextBoxFeature115);
            set => SetCommonPropertyWithID(ObjectProperty.UnrevealedTextBoxFeature115, value);
        }
        /// <summary>Gets or sets the common Switch Player Direction property of the objects in this collection.</summary>
        public bool CommonSwitchPlayerDirection
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.SwitchPlayerDirection);
            set => SetCommonPropertyWithID(ObjectProperty.SwitchPlayerDirection, value);
        }
        /// <summary>Gets or sets the common No Effects property of the objects in this collection.</summary>
        public bool CommonNoEffects
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.NoEffects);
            set => SetCommonPropertyWithID(ObjectProperty.NoEffects, value);
        }
        /// <summary>Gets or sets the common Ice Block property of the objects in this collection.</summary>
        public bool CommonIceBlock
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.IceBlock);
            set => SetCommonPropertyWithID(ObjectProperty.IceBlock, value);
        }
        /// <summary>Gets or sets the common Non Stick property of the objects in this collection.</summary>
        public bool CommonNonStick
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.NonStick);
            set => SetCommonPropertyWithID(ObjectProperty.NonStick, value);
        }
        /// <summary>Gets or sets the common Unstuckable property of the objects in this collection.</summary>
        public bool CommonUnstuckable
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.Unstuckable);
            set => SetCommonPropertyWithID(ObjectProperty.Unstuckable, value);
        }
        /// <summary>Gets or sets the common Unreadable Property 1 property of the objects in this collection.</summary>
        public bool CommonUnreadableProperty1
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.UnreadableProperty1);
            set => SetCommonPropertyWithID(ObjectProperty.UnreadableProperty1, value);
        }
        /// <summary>Gets or sets the common Unreadable Property 2 property of the objects in this collection.</summary>
        public bool CommonUnreadableProperty2
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.UnreadableProperty2);
            set => SetCommonPropertyWithID(ObjectProperty.UnreadableProperty2, value);
        }
        /// <summary>Gets or sets the common Transformation Scaling X property of the objects in this collection.</summary>
        public double CommonTransformationScalingX
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.TransformationScalingX);
            set => SetCommonPropertyWithID(ObjectProperty.TransformationScalingX, value);
        }
        /// <summary>Gets or sets the common Transformation Scaling Y property of the objects in this collection.</summary>
        public double CommonTransformationScalingY
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.TransformationScalingY);
            set => SetCommonPropertyWithID(ObjectProperty.TransformationScalingY, value);
        }
        /// <summary>Gets or sets the common Transformation Scaling Center X property of the objects in this collection.</summary>
        public double CommonTransformationScalingCenterX
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.TransformationScalingCenterX);
            set => SetCommonPropertyWithID(ObjectProperty.TransformationScalingCenterX, value);
        }
        /// <summary>Gets or sets the common Transformation Scaling Center Y property of the objects in this collection.</summary>
        public double CommonTransformationScalingCenterY
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.TransformationScalingCenterY);
            set => SetCommonPropertyWithID(ObjectProperty.TransformationScalingCenterY, value);
        }
        /// <summary>Gets or sets the common Exit Static property of the objects in this collection.</summary>
        public bool CommonExitStatic
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.ExitStatic);
            set => SetCommonPropertyWithID(ObjectProperty.ExitStatic, value);
        }
        /// <summary>Gets or sets the common Reversed property of the objects in this collection.</summary>
        public bool CommonReversed
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.Reversed);
            set => SetCommonPropertyWithID(ObjectProperty.Reversed, value);
        }
        /// <summary>Gets or sets the common Lock Y property of the objects in this collection.</summary>
        public bool CommonLockY
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.LockY);
            set => SetCommonPropertyWithID(ObjectProperty.LockY, value);
        }
        /// <summary>Gets or sets the common Chance property of the objects in this collection.</summary>
        public double CommonChance
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Chance);
            set => SetCommonPropertyWithID(ObjectProperty.Chance, value);
        }
        /// <summary>Gets or sets the common Chance Lots property of the objects in this collection.</summary>
        public object CommonChanceLots
        {
            get => GetCommonPropertyWithID<object>(ObjectProperty.ChanceLots);
            set => SetCommonPropertyWithID(ObjectProperty.ChanceLots, value);
        }
        /// <summary>Gets or sets the common Chance Lot Groups property of the objects in this collection.</summary>
        public int[] CommonChanceLotGroups
        {
            get => GetCommonPropertyWithID<int[]>(ObjectProperty.ChanceLotGroups);
            set => SetCommonPropertyWithID(ObjectProperty.ChanceLotGroups, value);
        }
        /// <summary>Gets or sets the common Zoom property of the objects in this collection.</summary>
        public int CommonZoom
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Zoom);
            set => SetCommonPropertyWithID(ObjectProperty.Zoom, value);
        }
        /// <summary>Gets or sets the common Grouping property of the objects in this collection.</summary>
        public CustomParticleGrouping CommonGrouping
        {
            get => GetCommonPropertyWithID<CustomParticleGrouping>(ObjectProperty.Grouping);
            set => SetCommonPropertyWithID(ObjectProperty.Grouping, value);
        }
        /// <summary>Gets or sets the common Property 1 property of the objects in this collection.</summary>
        public CustomParticleProperty1 CommonProperty1
        {
            get => GetCommonPropertyWithID<CustomParticleProperty1>(ObjectProperty.Property1);
            set => SetCommonPropertyWithID(ObjectProperty.Property1, value);
        }
        /// <summary>Gets or sets the common Max Particles property of the objects in this collection.</summary>
        public int CommonMaxParticles
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.MaxParticles);
            set => SetCommonPropertyWithID(ObjectProperty.MaxParticles, value);
        }
        /// <summary>Gets or sets the common Custom Particle Duration property of the objects in this collection.</summary>
        public double CommonCustomParticleDuration
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.CustomParticleDuration);
            set => SetCommonPropertyWithID(ObjectProperty.CustomParticleDuration, value);
        }
        /// <summary>Gets or sets the common Lifetime property of the objects in this collection.</summary>
        public double CommonLifetime
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Lifetime);
            set => SetCommonPropertyWithID(ObjectProperty.Lifetime, value);
        }
        /// <summary>Gets or sets the common Lifetime Adjustment property of the objects in this collection.</summary>
        public double CommonLifetimeAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.LifetimeAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.LifetimeAdjustment, value);
        }
        /// <summary>Gets or sets the common Emission property of the objects in this collection.</summary>
        public int CommonEmission
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Emission);
            set => SetCommonPropertyWithID(ObjectProperty.Emission, value);
        }
        /// <summary>Gets or sets the common Angle property of the objects in this collection.</summary>
        public double CommonAngle
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.Angle);
            set => SetCommonPropertyWithID(ObjectProperty.Angle, value);
        }
        /// <summary>Gets or sets the common Angle Adjustment property of the objects in this collection.</summary>
        public double CommonAngleAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.AngleAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.AngleAdjustment, value);
        }
        /// <summary>Gets or sets the common Custom Particle Speed property of the objects in this collection.</summary>
        public double CommonCustomParticleSpeed
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.CustomParticleSpeed);
            set => SetCommonPropertyWithID(ObjectProperty.CustomParticleSpeed, value);
        }
        /// <summary>Gets or sets the common Speed Adjustment property of the objects in this collection.</summary>
        public double CommonSpeedAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.SpeedAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.SpeedAdjustment, value);
        }
        /// <summary>Gets or sets the common Pos Var X property of the objects in this collection.</summary>
        public double CommonPosVarX
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.PosVarX);
            set => SetCommonPropertyWithID(ObjectProperty.PosVarX, value);
        }
        /// <summary>Gets or sets the common Pos Var Y property of the objects in this collection.</summary>
        public double CommonPosVarY
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.PosVarY);
            set => SetCommonPropertyWithID(ObjectProperty.PosVarY, value);
        }
        /// <summary>Gets or sets the common Gravity X property of the objects in this collection.</summary>
        public double CommonGravityX
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.GravityX);
            set => SetCommonPropertyWithID(ObjectProperty.GravityX, value);
        }
        /// <summary>Gets or sets the common Gravity Y property of the objects in this collection.</summary>
        public double CommonGravityY
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.GravityY);
            set => SetCommonPropertyWithID(ObjectProperty.GravityY, value);
        }
        /// <summary>Gets or sets the common Accel Rad property of the objects in this collection.</summary>
        public double CommonAccelRad
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.AccelRad);
            set => SetCommonPropertyWithID(ObjectProperty.AccelRad, value);
        }
        /// <summary>Gets or sets the common Accel Rad Adjustment property of the objects in this collection.</summary>
        public double CommonAccelRadAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.AccelRadAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.AccelRadAdjustment, value);
        }
        /// <summary>Gets or sets the common Accel Tan property of the objects in this collection.</summary>
        public double CommonAccelTan
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.AccelTan);
            set => SetCommonPropertyWithID(ObjectProperty.AccelTan, value);
        }
        /// <summary>Gets or sets the common Accel Tan Adjustment property of the objects in this collection.</summary>
        public double CommonAccelTanAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.AccelTanAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.AccelTanAdjustment, value);
        }
        /// <summary>Gets or sets the common Start Size property of the objects in this collection.</summary>
        public int CommonStartSize
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.StartSize);
            set => SetCommonPropertyWithID(ObjectProperty.StartSize, value);
        }
        /// <summary>Gets or sets the common Start Size Adjustment property of the objects in this collection.</summary>
        public int CommonStartSizeAdjustment
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.StartSizeAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.StartSizeAdjustment, value);
        }
        /// <summary>Gets or sets the common End Size property of the objects in this collection.</summary>
        public int CommonEndSize
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.EndSize);
            set => SetCommonPropertyWithID(ObjectProperty.EndSize, value);
        }
        /// <summary>Gets or sets the common End Size Adjustment property of the objects in this collection.</summary>
        public int CommonEndSizeAdjustment
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.EndSizeAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.EndSizeAdjustment, value);
        }
        /// <summary>Gets or sets the common Start Spin property of the objects in this collection.</summary>
        public int CommonStartSpin
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.StartSpin);
            set => SetCommonPropertyWithID(ObjectProperty.StartSpin, value);
        }
        /// <summary>Gets or sets the common Start Spin Adjustment property of the objects in this collection.</summary>
        public int CommonStartSpinAdjustment
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.StartSpinAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.StartSpinAdjustment, value);
        }
        /// <summary>Gets or sets the common End Spin property of the objects in this collection.</summary>
        public int CommonEndSpin
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.EndSpin);
            set => SetCommonPropertyWithID(ObjectProperty.EndSpin, value);
        }
        /// <summary>Gets or sets the common End Spin Adjustment property of the objects in this collection.</summary>
        public int CommonEndSpinAdjustment
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.EndSpinAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.EndSpinAdjustment, value);
        }
        /// <summary>Gets or sets the common Start A property of the objects in this collection.</summary>
        public double CommonStartA
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.StartA);
            set => SetCommonPropertyWithID(ObjectProperty.StartA, value);
        }
        /// <summary>Gets or sets the common Start A Adjustment property of the objects in this collection.</summary>
        public double CommonStartAAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.StartAAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.StartAAdjustment, value);
        }
        /// <summary>Gets or sets the common Start R property of the objects in this collection.</summary>
        public double CommonStartR
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.StartR);
            set => SetCommonPropertyWithID(ObjectProperty.StartR, value);
        }
        /// <summary>Gets or sets the common Start R Adjustment property of the objects in this collection.</summary>
        public double CommonStartRAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.StartRAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.StartRAdjustment, value);
        }
        /// <summary>Gets or sets the common Start G property of the objects in this collection.</summary>
        public double CommonStartG
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.StartG);
            set => SetCommonPropertyWithID(ObjectProperty.StartG, value);
        }
        /// <summary>Gets or sets the common Start G Adjustment property of the objects in this collection.</summary>
        public double CommonStartGAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.StartGAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.StartGAdjustment, value);
        }
        /// <summary>Gets or sets the common Start B property of the objects in this collection.</summary>
        public double CommonStartB
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.StartB);
            set => SetCommonPropertyWithID(ObjectProperty.StartB, value);
        }
        /// <summary>Gets or sets the common Start B Adjustment property of the objects in this collection.</summary>
        public double CommonStartBAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.StartBAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.StartBAdjustment, value);
        }
        /// <summary>Gets or sets the common End A property of the objects in this collection.</summary>
        public double CommonEndA
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EndA);
            set => SetCommonPropertyWithID(ObjectProperty.EndA, value);
        }
        /// <summary>Gets or sets the common End A Adjustment property of the objects in this collection.</summary>
        public double CommonEndAAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EndAAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.EndAAdjustment, value);
        }
        /// <summary>Gets or sets the common End R property of the objects in this collection.</summary>
        public double CommonEndR
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EndR);
            set => SetCommonPropertyWithID(ObjectProperty.EndR, value);
        }
        /// <summary>Gets or sets the common End R Adjustment property of the objects in this collection.</summary>
        public double CommonEndRAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EndRAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.EndRAdjustment, value);
        }
        /// <summary>Gets or sets the common End G property of the objects in this collection.</summary>
        public double CommonEndG
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EndG);
            set => SetCommonPropertyWithID(ObjectProperty.EndG, value);
        }
        /// <summary>Gets or sets the common End G Adjustment property of the objects in this collection.</summary>
        public double CommonEndGAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EndGAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.EndGAdjustment, value);
        }
        /// <summary>Gets or sets the common End B property of the objects in this collection.</summary>
        public double CommonEndB
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EndB);
            set => SetCommonPropertyWithID(ObjectProperty.EndB, value);
        }
        /// <summary>Gets or sets the common End B Adjustment property of the objects in this collection.</summary>
        public double CommonEndBAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.EndBAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.EndBAdjustment, value);
        }
        /// <summary>Gets or sets the common Custom Particle Fade In property of the objects in this collection.</summary>
        public double CommonCustomParticleFadeIn
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.CustomParticleFadeIn);
            set => SetCommonPropertyWithID(ObjectProperty.CustomParticleFadeIn, value);
        }
        /// <summary>Gets or sets the common Fade In Adjustment property of the objects in this collection.</summary>
        public double CommonFadeInAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.FadeInAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.FadeInAdjustment, value);
        }
        /// <summary>Gets or sets the common Custom Particle Fade Out property of the objects in this collection.</summary>
        public double CommonCustomParticleFadeOut
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.CustomParticleFadeOut);
            set => SetCommonPropertyWithID(ObjectProperty.CustomParticleFadeOut, value);
        }
        /// <summary>Gets or sets the common Fade Out Adjustment property of the objects in this collection.</summary>
        public double CommonFadeOutAdjustment
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.FadeOutAdjustment);
            set => SetCommonPropertyWithID(ObjectProperty.FadeOutAdjustment, value);
        }
        /// <summary>Gets or sets the common Additive property of the objects in this collection.</summary>
        public bool CommonAdditive
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.Additive);
            set => SetCommonPropertyWithID(ObjectProperty.Additive, value);
        }
        /// <summary>Gets or sets the common Start Size Equals End property of the objects in this collection.</summary>
        public bool CommonStartSizeEqualsEnd
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.StartSizeEqualsEnd);
            set => SetCommonPropertyWithID(ObjectProperty.StartSizeEqualsEnd, value);
        }
        /// <summary>Gets or sets the common Start Spin Equals End property of the objects in this collection.</summary>
        public bool CommonStartSpinEqualsEnd
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.StartSpinEqualsEnd);
            set => SetCommonPropertyWithID(ObjectProperty.StartSpinEqualsEnd, value);
        }
        /// <summary>Gets or sets the common Start Radius Equals End property of the objects in this collection.</summary>
        public bool CommonStartRadiusEqualsEnd
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.StartRadiusEqualsEnd);
            set => SetCommonPropertyWithID(ObjectProperty.StartRadiusEqualsEnd, value);
        }
        /// <summary>Gets or sets the common Start Rotation Is Dir property of the objects in this collection.</summary>
        public bool CommonStartRotationIsDir
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.StartRotationIsDir);
            set => SetCommonPropertyWithID(ObjectProperty.StartRotationIsDir, value);
        }
        /// <summary>Gets or sets the common Dynamic Rotation property of the objects in this collection.</summary>
        public bool CommonDynamicRotation
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.DynamicRotation);
            set => SetCommonPropertyWithID(ObjectProperty.DynamicRotation, value);
        }
        /// <summary>Gets or sets the common Use Object Color property of the objects in this collection.</summary>
        public bool CommonUseObjectColor
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.UseObjectColor);
            set => SetCommonPropertyWithID(ObjectProperty.UseObjectColor, value);
        }
        /// <summary>Gets or sets the common Uniform Object Color property of the objects in this collection.</summary>
        public bool CommonUniformObjectColor
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.UniformObjectColor);
            set => SetCommonPropertyWithID(ObjectProperty.UniformObjectColor, value);
        }
        /// <summary>Gets or sets the common Texture property of the objects in this collection.</summary>
        public int CommonTexture
        {
            get => GetCommonPropertyWithID<int>(ObjectProperty.Texture);
            set => SetCommonPropertyWithID(ObjectProperty.Texture, value);
        }
        /// <summary>Gets or sets the common Scale X property of the objects in this collection.</summary>
        public double CommonScaleX
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.ScaleX);
            set => SetCommonPropertyWithID(ObjectProperty.ScaleX, value);
        }
        /// <summary>Gets or sets the common Scale Y property of the objects in this collection.</summary>
        public double CommonScaleY
        {
            get => GetCommonPropertyWithID<double>(ObjectProperty.ScaleY);
            set => SetCommonPropertyWithID(ObjectProperty.ScaleY, value);
        }
        /// <summary>Gets or sets the common Lock Object Scale property of the objects in this collection.</summary>
        public bool CommonLockObjectScale
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.LockObjectScale);
            set => SetCommonPropertyWithID(ObjectProperty.LockObjectScale, value);
        }
        /// <summary>Gets or sets the common Only Move Scale property of the objects in this collection.</summary>
        public bool CommonOnlyMoveScale
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.OnlyMoveScale);
            set => SetCommonPropertyWithID(ObjectProperty.OnlyMoveScale, value);
        }
        /// <summary>Gets or sets the common Lock To Camera X property of the objects in this collection.</summary>
        public bool CommonLockToCameraX
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.LockToCameraX);
            set => SetCommonPropertyWithID(ObjectProperty.LockToCameraX, value);
        }
        /// <summary>Gets or sets the common Lock To Camera Y property of the objects in this collection.</summary>
        public bool CommonLockToCameraY
        {
            get => GetCommonPropertyWithID<bool>(ObjectProperty.LockToCameraY);
            set => SetCommonPropertyWithID(ObjectProperty.LockToCameraY, value);
        }
        #endregion

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
                commonProperties = LevelObjectFactory.GetCommonProperties(this);
            else
            {
                for (; commonPropertiesUnevaluatedIndex < unevaluatedObjects.ListCount; commonPropertiesUnevaluatedIndex++)
                    commonProperties = LevelObjectFactory.GetCommonProperties(unevaluatedObjects[commonPropertiesUnevaluatedIndex], commonProperties);
                RemoveEvaluatedObjects();
            }
            return commonProperties;
        }
        /// <summary>Returns all the available object properties found in this <seealso cref="LevelObjectCollection"/>.</summary>
        public PropertyAccessInfoDictionary GetAllAvailableProperties()
        {
            if (allAvailableProperties == null)
                allAvailableProperties = LevelObjectFactory.GetAllAvailableProperties(this);
            else
            {
                for (; allAvailablePropertiesUnevaluatedIndex < unevaluatedObjects.ListCount; allAvailablePropertiesUnevaluatedIndex++)
                    allAvailableProperties = LevelObjectFactory.GetAllAvailableProperties(unevaluatedObjects[allAvailablePropertiesUnevaluatedIndex], allAvailableProperties);
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

        /// <summary>Creates and returns a <seealso cref="SortedDictionary{TKey, TValue}"/> categorized by the objects' used Group IDs (one object may belong in more than one categories).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SortedDictionary<int, LevelObjectCollection> GetObjectsByUsedGroupIDs() => GetObjectsByUsedIDs(LevelObjectIDType.Group);
        /// <summary>Creates and returns a <seealso cref="SortedDictionary{TKey, TValue}"/> categorized by the objects' used Color IDs (one object may belong in more than one categories).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SortedDictionary<int, LevelObjectCollection> GetObjectsByUsedColorIDs() => GetObjectsByUsedIDs(LevelObjectIDType.Color);
        /// <summary>Creates and returns a <seealso cref="SortedDictionary{TKey, TValue}"/> categorized by the objects' used Item IDs (one object may belong in more than one categories).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SortedDictionary<int, LevelObjectCollection> GetObjectsByUsedItemIDs() => GetObjectsByUsedIDs(LevelObjectIDType.Item);
        /// <summary>Creates and returns a <seealso cref="SortedDictionary{TKey, TValue}"/> categorized by the objects' used Block IDs (one object may belong in more than one categories).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SortedDictionary<int, LevelObjectCollection> GetObjectsByUsedBlockIDs() => GetObjectsByUsedIDs(LevelObjectIDType.Block);
        /// <summary>Creates and returns a <seealso cref="SortedDictionary{TKey, TValue}"/> categorized by the objects' used IDs of a specified type (one object may belong in more than one categories).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SortedDictionary<int, LevelObjectCollection> GetObjectsByUsedIDs(LevelObjectIDType type)
        {
            var result = new SortedDictionary<int, LevelObjectCollection>();

            foreach (var o in this)
                foreach (var id in o.GetUsedIDsFromType(type))
                    result.AddElementOrAddCollection(id, o);

            return result;
        }
        #endregion

        /// <summary>Parses an object string into a <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="objectString">The object strign to parse.</param>
        /// <returns>The parsed <seealso cref="LevelObjectCollection"/>.</returns>
        public static LevelObjectCollection ParseObjects(string objectString)
        {
            List<GeneralObject> objects = new List<GeneralObject>();
            while (objectString.Length > 0 && objectString[objectString.Length - 1] == ';')
                objectString = objectString.Remove(objectString.Length - 1);
            if (objectString.Length > 0)
            {
                string[][] objectProperties = objectString.Split(';').SplitAsJagged(',');
                for (int i = 0; i < objectProperties.Length; i++)
                {
                    try
                    {
                        var objectInfo = objectProperties[i];
                        var instance = LevelObjectFactory.GetNewObjectInstance(ToInt16(objectInfo[1]));
                        objects.Add(instance); // Get IDs of the selected objects
                        for (int j = 3; j < objectInfo.Length; j += 2)
                        {
                            try
                            {
                                int propertyID = ToInt32(objectInfo[j - 1]);
                                switch (GetPropertyIDAttribute(propertyID))
                                {
                                    case IGenericAttribute<int> _:
                                        instance.SetPropertyWithID(propertyID, ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<bool> _:
                                        instance.SetPropertyWithID(propertyID, ToBoolean(ToInt32(objectInfo[j])));
                                        break;
                                    case IGenericAttribute<double> _:
                                        instance.SetPropertyWithID(propertyID, ToDouble(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<string> _:
                                        instance.SetPropertyWithID(propertyID, objectInfo[j]);
                                        break;
                                    case IGenericAttribute<HSVAdjustment> _:
                                        instance.SetPropertyWithID(propertyID, objectInfo[j].ToString());
                                        break;
                                    case IGenericAttribute<int[]> _:
                                        instance.SetPropertyWithID(propertyID, objectInfo[j].ToString().Split('.').ToInt32Array());
                                        break;
                                    case IGenericAttribute<Easing> _:
                                        instance.SetPropertyWithID(propertyID, (Easing)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<InstantCountComparison> _:
                                        instance.SetPropertyWithID(propertyID, (InstantCountComparison)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<PickupItemPickupMode> _:
                                        instance.SetPropertyWithID(propertyID, (PickupItemPickupMode)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<PulseMode> _:
                                        instance.SetPropertyWithID(propertyID, (PulseMode)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<PulseTargetType> _:
                                        instance.SetPropertyWithID(propertyID, (PulseTargetType)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<TargetPosCoordinates> _:
                                        instance.SetPropertyWithID(propertyID, (TargetPosCoordinates)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<TouchToggleMode> _:
                                        instance.SetPropertyWithID(propertyID, (TouchToggleMode)ToInt32(objectInfo[j]));
                                        break;
                                }
                            }
                            catch (FormatException) // If the property is not just a number; most likely a Start Pos object
                            {
                                // After logging the exceptions in the console, the exception is ignorable
                            }
                            catch (KeyNotFoundException e)
                            {
                                int propertyID = ToInt32(objectInfo[j - 1]);
                                if (propertyID == 36)
                                    continue;
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // So far this only happens when attempting to abstractly create a yellow teleportation portal
                    }
                }
                objectProperties = null;
            }
            return new LevelObjectCollection(objects);
        }

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
