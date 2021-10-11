using GDAPI.Objects.GeometryDash.IDTypes;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Contains the IDs that are being used in a <seealso cref="GeneralObject"/>.</summary>
    public class LevelObjectIDUsage : IEnumerable<int>
    {
        /// <summary>The primary ID of the object.</summary>
        public int PrimaryID { get; set; }
        /// <summary>The secondary ID of the object.</summary>
        public int SecondaryID { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectIDUsage"/> class.</summary>
        public LevelObjectIDUsage() { }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectIDUsage"/> class.</summary>
        /// <param name="primaryID">The primary ID of the object.</param>
        /// <param name="secondaryID">The secondary ID of the object.</param>
        public LevelObjectIDUsage(int primaryID, int secondaryID)
        {
            PrimaryID = primaryID;
            SecondaryID = secondaryID;
        }

        /// <summary>Overwrites this instance's <seealso cref="PrimaryID"/> to the provided object's primary ID for the specified type.</summary>
        /// <typeparam name="T">The type of primary ID to use.</typeparam>
        /// <param name="obj">The object whose primary ID to get.</param>
        public void AddPrimaryID<T>(GeneralObject obj)
            where T : IID
        {
            if (obj is IHasPrimaryID<T> p)
                AddPrimaryID(p);
        }
        /// <summary>Overwrites this instance's <seealso cref="SecondaryID"/> to the provided object's secondary ID for the specified type.</summary>
        /// <typeparam name="T">The type of secondary ID to use.</typeparam>
        /// <param name="obj">The object whose secondary ID to get.</param>
        public void AddSecondaryID<T>(GeneralObject obj)
            where T : IID
        {
            if (obj is IHasSecondaryID<T> s)
                AddSecondaryID(s);
        }

        /// <summary>Overwrites this instance's <seealso cref="PrimaryID"/> to the provided object's primary ID for the specified type.</summary>
        /// <typeparam name="T">The type of primary ID to use.</typeparam>
        /// <param name="p">The object whose primary ID to get.</param>
        public void AddPrimaryID<T>(IHasPrimaryID<T> p)
            where T : IID
        {
            PrimaryID = p.PrimaryID.ID;
        }
        /// <summary>Overwrites this instance's <seealso cref="SecondaryID"/> to the provided object's secondary ID for the specified type.</summary>
        /// <typeparam name="T">The type of secondary ID to use.</typeparam>
        /// <param name="s">The object whose secondary ID to get.</param>
        public void AddSecondaryID<T>(IHasSecondaryID<T> s)
            where T : IID
        {
            SecondaryID = s.SecondaryID.ID;
        }

        /// <summary>Gets an enumerator that enumerates through all the used IDs that are registered for this object, skipping zeroes.</summary>
        public virtual IEnumerator<int> GetEnumerator() => new LevelObjectIDUsageEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Gets a <seealso cref="LevelObjectGroupIDUsage"/> from a <seealso cref="GeneralObject"/>'s used Group IDs.</summary>
        /// <param name="obj">The <seealso cref="GeneralObject"/> whose Group IDs to analyze.</param>
        public static LevelObjectGroupIDUsage GroupIDsFromObject(GeneralObject obj)
        {
            var result = IDsFromObject<GroupID, LevelObjectGroupIDUsage>(obj);
            result.AssignedIDs = obj.GroupIDs;
            return result;
        }
        /// <summary>Gets a <seealso cref="LevelObjectIDUsage"/> from a <seealso cref="GeneralObject"/>'s used Color IDs.</summary>
        /// <param name="obj">The <seealso cref="GeneralObject"/> whose Color IDs to analyze.</param>
        public static LevelObjectColorIDUsage ColorIDsFromObject(GeneralObject obj)
        {
            var result = IDsFromObject<ColorID, LevelObjectColorIDUsage>(obj);
            result.MainColorID = obj.Color1ID;
            result.DetailColorID = obj.Color2ID;
            return result;
        }
        /// <summary>Gets a <seealso cref="LevelObjectIDUsage"/> from a <seealso cref="GeneralObject"/>'s used Item IDs.</summary>
        /// <param name="obj">The <seealso cref="GeneralObject"/> whose Item IDs to analyze.</param>
        public static LevelObjectIDUsage ItemIDsFromObject(GeneralObject obj)
        {
            return IDsFromObject<ItemID, LevelObjectIDUsage>(obj);
        }
        /// <summary>Gets a <seealso cref="LevelObjectIDUsage"/> from a <seealso cref="GeneralObject"/>'s used Block IDs.</summary>
        /// <param name="obj">The <seealso cref="GeneralObject"/> whose Block IDs to analyze.</param>
        public static LevelObjectIDUsage BlockIDsFromObject(GeneralObject obj)
        {
            return IDsFromObject<BlockID, LevelObjectIDUsage>(obj);
        }

        protected static TUsage IDsFromObject<TID, TUsage>(GeneralObject obj)
            where TID : IID
            where TUsage : LevelObjectIDUsage, new()
        {
            var result = new TUsage();
            result.AddPrimaryID<TID>(obj);
            result.AddSecondaryID<TID>(obj);
            return result;
        }

        /// <summary>Represents an enumerator for all the used IDs in a <seealso cref="LevelObjectIDUsage"/> object.</summary>
        protected class LevelObjectIDUsageEnumerator : IEnumerator<int>
        {
            private int index = -1;

            /// <summary>The <seealso cref="LevelObjectIDUsage"/> object.</summary>
            protected readonly LevelObjectIDUsage Usage;

            /// <summary>The length of the sequence that will be enumerated.</summary>
            protected virtual int Length => 2;

            public int Current { get; private set; }

            object IEnumerator.Current => Current;

            public LevelObjectIDUsageEnumerator(LevelObjectIDUsage u)
            {
                Usage = u;
            }

            public void Dispose() { }
            public bool MoveNext()
            {
                while (++index < Length)
                {
                    if ((Current = GetCurrentElement()) > 0)
                        break;
                }
                return index < Length;
            }
            public void Reset()
            {
                index = -1;
                Current = default;
            }

            /// <summary>Gets the element in the sequence based on the given index.</summary>
            /// <param name="index">The index representing the element to return, with 0 being the <seealso cref="PrimaryID"/>, and 1 being the <seealso cref="SecondaryID"/>.</param>
            protected virtual int GetElementAtIndex(int index)
            {
                return index switch
                {
                    0 => Usage.PrimaryID,
                    1 => Usage.SecondaryID,
                    _ => default,
                };
            }

            private int GetCurrentElement() => GetElementAtIndex(index);
        }
    }
}
